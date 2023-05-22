using Bootstrap.CanvasBootstrap.Data;
using Features.Actor.Rules;
using Features.Journey.Controllers;
using Features.Roadmap.Data;
using Features.StoryNodes.Services;
using Features.Win;
using Zenject;

namespace Features.Journey.Factories
{
    public class JourneyControllerFactory : IFactory<Stage, JourneyController>
    {
        private readonly ActorRule _actorRule;
        private readonly CanvasData _canvasData;
        private readonly NodeService _nodeService;
        
        private JourneyController _instance;
        
        public JourneyControllerFactory(ActorRule actorRule, CanvasData canvasData, NodeService nodeService)
        {
            _actorRule = actorRule;
            _canvasData = canvasData;
            _nodeService = nodeService;
        }

        public JourneyController GetInstance()
        {
            return _instance;
        }

        public JourneyController Create(Stage stage)
        {
            _instance = new JourneyController(stage, _nodeService, _actorRule, _canvasData);
            return _instance;
        }
    }
}