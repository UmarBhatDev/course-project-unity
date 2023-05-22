using Bootstrap.CanvasBootstrap.Data;
using Features.GameOverMenu.Views;
using Zenject;

namespace Features.GameOverMenu.Factories
{
    public class GameOverViewFactory : IFactory<GameOverMenuView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public GameOverViewFactory(ViewRegistry viewRegistry, DiContainer container, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = container;
            _viewRegistry = viewRegistry;
        }
        
        public GameOverMenuView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<GameOverMenuView>(_viewRegistry.GameOverMenuView, _canvasData.Canvas.transform);
        }
    }
}