using Features.Actor.Controllers;
using Features.Actor.Models;
using Zenject;

namespace Features.Actor.Factories
{
    public class ActorControllerFactory : IFactory<ActorModel, ActorController>
    {
        public ActorController Create(ActorModel model)
            => new ActorController(model);
    }
}