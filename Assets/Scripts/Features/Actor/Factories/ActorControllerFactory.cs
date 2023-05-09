using Features.Actor.Controllers;
using Features.Actor.Models;
using Features.Actor.Views;
using Zenject;

namespace Features.Actor.Factories
{
    public class ActorControllerFactory : IFactory<ActorModel, ActorView, ActorController>
    {
        public ActorController Create(ActorModel model, ActorView actorView)
            => new ActorController(model, actorView);
    }
}