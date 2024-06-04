using Bootstrap.CanvasBootstrap.Data;
using Features.PaperHint.Views;
using UnityEngine.Scripting;
using Zenject;

namespace Features.PaperHint.Factories
{
    [Preserve]
    public class PaperHintViewFactory : IFactory<PaperHintView>
    {
        private readonly CanvasData _canvasData;
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public PaperHintViewFactory(ViewRegistry viewRegistry, DiContainer container, CanvasData canvasData)
        {
            _canvasData = canvasData;
            _diContainer = container;
            _viewRegistry = viewRegistry;
        }

        public PaperHintView Create() => _diContainer.InstantiatePrefabForComponent<PaperHintView>(_viewRegistry.PaperHintView, _canvasData.Canvas.transform);
    }
}