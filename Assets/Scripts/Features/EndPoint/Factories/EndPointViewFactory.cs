using Bootstrap.CanvasBootstrap.Data;
using Features.EndPoint.Views;
using UnityEngine;
using Zenject;

namespace Features.EndPoint.Factories
{
    public class EndPointViewFactory : IFactory<Transform, EndPointView>
    {
        private readonly ViewRegistry _viewRegistry;
        private readonly DiContainer _diContainer;

        public EndPointViewFactory(DiContainer diContainer, ViewRegistry viewRegistry)
        {
            _viewRegistry = viewRegistry;
            _diContainer = diContainer;
        }

        public EndPointView Create(Transform spawnAt)
        {
            var endPointView = _diContainer.InstantiatePrefabForComponent<EndPointView>(_viewRegistry.EndPointView);
            endPointView.transform.position = spawnAt.position;
            return endPointView;
        }
    }
}