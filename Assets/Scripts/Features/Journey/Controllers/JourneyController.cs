﻿using System;
using System.Threading;
using Bootstrap.CanvasBootstrap.Data;
using Cysharp.Threading.Tasks;
using Features.Actor.Rules;
using Features.Persistence.Services;
using Features.Roadmap.Data;
using Features.StoryNodes.Services;
using UnityEngine;

namespace Features.Journey.Controllers
{
    public class JourneyController : IDisposable
    {
        private readonly Stage _stage;
        private readonly ActorRule _actorRule;
        private readonly CanvasData _canvasData;
        private readonly NodeService _nodeService;
        private readonly JourneyProgress _journeyProgress;

        private bool _levelCompletionRequested;
        private CanvasAutoReference _autoReference;
        
        private const string KillsKey = "Kills";

        public JourneyController(Stage stage, NodeService nodeService, ActorRule actorRule,
            CanvasData canvasData, JourneyProgress journeyProgress)
        {
            _stage = stage;
            _actorRule = actorRule;
            _canvasData = canvasData;
            _nodeService = nodeService;
            _journeyProgress = journeyProgress;
        }

        public async UniTask Play(CancellationToken cancellationToken)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            PlayerPrefs.SetInt(KillsKey, 0);

            var nodeScriptPresenter = _journeyProgress.GetStageStatus(_stage.Id) == StageStatus.Visited
                ? _stage.LevelPassedScript
                : _stage.Script;
            
            var script = _nodeService.CreateScript(_stage.Id, nodeScriptPresenter);
            script?.Play();

            _autoReference = _canvasData.Canvas.GetComponent<CanvasAutoReference>();
            _autoReference.GunAmmo.gameObject.SetActive(true);
            _autoReference.MiniMap.gameObject.SetActive(true);
            // _autoReference.Compass.SetActive(true);
            _autoReference.CompassNavigatorPro.enabled = true;

            await UniTask.WaitUntil(ShouldExit, cancellationToken: cancellationToken);
        }

        private bool ShouldExit()
        {
            return _levelCompletionRequested;
        }

        public void Dispose()
        {
            _autoReference ??= _canvasData.Canvas.GetComponent<CanvasAutoReference>();
            _autoReference.GunAmmo.gameObject.SetActive(false);
            _autoReference.MiniMap.gameObject.SetActive(false);
            // _autoReference.Compass.SetActive(false);
            _autoReference.CompassNavigatorPro.enabled = false;

            _actorRule.DisposeActor();
        }
    }
}