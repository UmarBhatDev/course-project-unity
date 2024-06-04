using Bootstrap.CanvasBootstrap.Data;
using Features.Actor.Rules;
using Features.Journey.Controllers;
using Features.Persistence.Services;
using Features.Roadmap.Data;
using Features.StoryNodes.Services;
using UnityEngine.Scripting;
using Zenject;

namespace Features.Journey.Factories
{
    [Preserve]
    public class JourneyControllerFactory : IFactory<Stage, JourneyController>
    {
        private readonly ActorRule _actorRule;
        private readonly CanvasData _canvasData;
        private readonly NodeService _nodeService;
        private readonly JourneyProgress _journeyProgress;
        
        private JourneyController _instance;
        
        public JourneyControllerFactory(ActorRule actorRule, CanvasData canvasData, NodeService nodeService,
            JourneyProgress journeyProgress)
        {
            _actorRule = actorRule;
            _canvasData = canvasData;
            _nodeService = nodeService;
            _journeyProgress = journeyProgress;
        }

        public JourneyController GetInstance()
        {
            return _instance;
        }

        public JourneyController Create(Stage stage)
        {
            _instance = new JourneyController(stage, _nodeService, _actorRule, _canvasData, _journeyProgress);
            return _instance;
        }
    }
}