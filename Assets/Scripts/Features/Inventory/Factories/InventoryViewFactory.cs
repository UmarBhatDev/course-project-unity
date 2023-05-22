using Bootstrap.CanvasBootstrap.Data;
using Features.Inventory.Views;
using Zenject;

namespace Features.Inventory.Factories
{
    public class InventoryViewFactory : IFactory<InventoryView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;
        
        public InventoryViewFactory(DiContainer diContainer, ViewRegistry viewRegistry, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = diContainer;
            _viewRegistry = viewRegistry;           
        }

        public InventoryView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<InventoryView>(_viewRegistry.InventoryView, _canvasData.Canvas.transform);
        }
    }
}