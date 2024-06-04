using System;
using System.Collections.Generic;
using Bootstrap.CanvasBootstrap.Data;
using Features.Persistence.Services;
using Features.Roadmap.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Features.SavePointMenu.Views
{
    public class MoveToLocationPanelView : MonoBehaviour
    {
        public event Action<string> OnLocationChosen;
        public event Action OnCloseButtonClicked;

        [SerializeField] private Transform _scrollContentTransfrom;
        [SerializeField] private Button _closeButton;

        [Inject] private RoadmapRegistry _roadmapRegistry;
        [Inject] private JourneyProgress _journeyProgress;
        [Inject] private DiContainer _diContainer;
        [Inject] private ViewRegistry _viewRegistry;

        private List<MoveToLocationElementView> _spawnedElements = new();
        
        public void OnEnable()
        {
            _closeButton.onClick.AddListener(() => OnCloseButtonClicked?.Invoke());
            
            var elementsCount = _roadmapRegistry.Roadmap.Stages.Count;
            const int debugElementsCount = 5;
            
            for (var index = 0; index < elementsCount + debugElementsCount; index++)
            {
                var element = _diContainer
                    .InstantiatePrefabForComponent<MoveToLocationElementView>(_viewRegistry.MoveToLocationElementView, _scrollContentTransfrom);

                Stage stage = null;
                var stageStatus = StageStatus.Unvisited;
                
                if (index < elementsCount)
                {
                    stage = _roadmapRegistry.Roadmap.Stages[index];
                    stageStatus = _journeyProgress.GetStageStatus(stage.Id);
                }
                
                switch (stageStatus)
                {
                    case StageStatus.Unvisited:
                        element.Setup("Coming soon", _roadmapRegistry.UnknownLocationSprite, false);
                        break;
                    case StageStatus.Visited or StageStatus.Active:
                        element.Setup(stage?.Id, stage?.LocationPreviewSprite, true);
                        break;
                }

                element.OnGoToLocationButtonPressed += () => OnLocationChosen?.Invoke(stage?.Id);

                _spawnedElements.Add(element);
            }
        }

        public void OnDisable()
        {
            foreach (var moveToLocationElementView in _spawnedElements)
            {
                DestroyImmediate(moveToLocationElementView.gameObject);
            }

            _spawnedElements = new List<MoveToLocationElementView>();
        }
    }
}