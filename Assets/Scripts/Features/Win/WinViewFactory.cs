using Bootstrap.CanvasBootstrap;
using Bootstrap.CanvasBootstrap.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Features.Win
{
    public class WinViewFactory : IFactory<WinView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;
        
        public WinViewFactory(DiContainer diContainer, ViewRegistry viewRegistry, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = diContainer;
            _viewRegistry = viewRegistry;           
        }

        public WinView Create()
        {
            if (Object.FindObjectOfType<EventSystem>() == null)
            {
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
            }
            
            return _diContainer.InstantiatePrefabForComponent<WinView>(_viewRegistry.WinView, _canvasData.Canvas.transform);
        }
    }
}