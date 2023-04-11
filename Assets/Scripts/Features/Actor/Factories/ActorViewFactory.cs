using Bootstrap.CanvasBootstrap.Data;
using Features.Actor.Models;
using Features.Actor.Views;
using Zenject;

namespace Features.Actor.Factories
{
    public class ActorViewFactory : IFactory<ActorModel, ActorView>
    {
        private readonly DiContainer _diContainer;
        private readonly ViewRegistry _viewRegistry;

        public ActorViewFactory(DiContainer diContainer, ViewRegistry viewRegistry)
        {
            _diContainer = diContainer;
            _viewRegistry = viewRegistry;
        }

        public ActorView Create(ActorModel actorModel)
        {
            return _diContainer.InstantiatePrefabForComponent<ActorView>(_viewRegistry.ActorView,
                new object[] { actorModel });
        }
    }
}