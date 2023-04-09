using Bootstrap.CanvasBootstrap;
using Features.SceneTransitions.Data;
using Features.SceneTransitions.Views;
using FSM.Data;
using Zenject;

namespace Features.SceneTransitions.Factories
{
    public class CurtainViewFactory : IFactory<CurtainType, CurtainViewBase>
    {
        private readonly DiContainer _container;
        private readonly CanvasData _canvasData;
        private readonly CurtainRegistry _curtainRegistry;

        public CurtainViewFactory(DiContainer container, CurtainRegistry curtainRegistry, CanvasData canvasData)
        {
            _container = container;
            _canvasData = canvasData;
            _curtainRegistry = curtainRegistry;
        }

        public CurtainViewBase Create(CurtainType curtainType)     
        {
            return _container.InstantiatePrefabForComponent<CurtainViewBase>(_curtainRegistry.GetCurtainByType(curtainType), _canvasData.Canvas.transform);
        }
    }
}