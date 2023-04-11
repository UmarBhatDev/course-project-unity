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
        private readonly NodeService _nodeService;
        private readonly WinViewFactory _winViewFactory;
        
        private JourneyController _instance;
        public JourneyControllerFactory(ActorRule actorRule, NodeService nodeService, WinViewFactory winViewFactory)
        {
            _actorRule = actorRule;
            _nodeService = nodeService;
            _winViewFactory = winViewFactory;
        }

        public JourneyController GetInstance()
        {
            return _instance;
        }

        public JourneyController Create(Stage stage)
        {
            _instance = new JourneyController(stage, _nodeService, _winViewFactory, _actorRule);
            return _instance;
        }
    }
}