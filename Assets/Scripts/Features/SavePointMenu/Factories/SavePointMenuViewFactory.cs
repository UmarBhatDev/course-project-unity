using Bootstrap.CanvasBootstrap.Data;
using Features.SavePointMenu.Views;
using UnityEngine.Scripting;
using Zenject;

namespace Features.SavePointMenu.Factories
{
    [Preserve]
    public class SavePointMenuViewFactory : IFactory<SavePointMenuView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public SavePointMenuViewFactory(ViewRegistry viewRegistry, DiContainer container, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = container;
            _viewRegistry = viewRegistry;
        }

        public SavePointMenuView Create()
        {
            return _diContainer.InstantiatePrefabForComponent<SavePointMenuView>(_viewRegistry.SavePointMenuPanel, _canvasData.Canvas.transform);
        }
    }
}