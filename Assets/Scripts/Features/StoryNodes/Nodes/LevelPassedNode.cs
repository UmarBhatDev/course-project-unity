using System;
using System.Collections;
using Features.Journey.Factories;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Unit = Unity.VisualScripting.Unit;

namespace Features.StoryNodes.Nodes
{
    public class LevelPassedNode : Unit
    {
        [DoNotSerialize] public ControlInput Start;
        [DoNotSerialize] public ControlOutput Complete;

        [Inject] private JourneyControllerFactory _journeyControllerFactory;

        protected override void Definition()
        {
            Start = ControlInputCoroutine("Start", RunCoroutine);
            Complete = ControlOutput("Complete");
        }

        private IEnumerator RunCoroutine(Flow flow)
        {
            yield return Start;

            yield return new WaitForSeconds(3);
            var journeyController = _journeyControllerFactory.GetInstance();
            journeyController.RequestLevelCompletion();
            
            yield return Complete;
        }

    }
}