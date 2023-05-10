using Bootstrap;
using Bootstrap.CanvasBootstrap.Data;
using Bootstrap.GlobalDisposable.Services;
using Features.EndPoint.Views;
using UnityEngine;
using Zenject;

namespace Features.EndPoint.Factories
{
    public class EndPointViewFactory : IFactory<Transform, EndPointView>
    {
        private readonly ViewRegistry _viewRegistry;
        private readonly DiContainer _diContainer;
        private readonly GlobalCompositeDisposable _globalCompositeDisposable;

        public EndPointViewFactory(DiContainer diContainer, ViewRegistry viewRegistry, GlobalCompositeDisposable globalCompositeDisposable)
        {
            _globalCompositeDisposable = globalCompositeDisposable;
            _viewRegistry = viewRegistry;
            _diContainer = diContainer;
        }

        public EndPointView Create(Transform spawnAt)
        {
            var endPointView = _diContainer.InstantiatePrefabForComponent<EndPointView>(_viewRegistry.EndPointView);
            endPointView.transform.position = spawnAt.position;
            _globalCompositeDisposable.AddDisposable(endPointView);
            return endPointView;
        }
    }
}