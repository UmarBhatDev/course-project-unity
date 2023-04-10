using Features.Journey.Controllers;
using Features.Roadmap.Data;
using Features.StoryNodes.Services;
using Features.Win;
using Zenject;

namespace Features.Journey.Factories
{
    public class JourneyControllerFactory : IFactory<Stage, JourneyController>
    {
        private readonly NodeService _nodeService;
        private readonly WinViewFactory _winViewFactory;
        
        private JourneyController _instance;
        public JourneyControllerFactory(NodeService nodeService, WinViewFactory winViewFactory)
        {
            _nodeService = nodeService;
            _winViewFactory = winViewFactory;
        }

        public JourneyController GetInstance()
        {
            return _instance;
        }

        public JourneyController Create(Stage stage)
        {
            _instance = new JourneyController(stage, _nodeService, _winViewFactory);
            return _instance;
        }
    }
}