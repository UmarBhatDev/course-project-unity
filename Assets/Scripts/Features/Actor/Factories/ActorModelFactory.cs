using Features.Actor.Models;
using UnityEngine;
using Zenject;

namespace Features.Actor.Factories
{
    public class ActorModelFactory : IFactory<Vector3, Quaternion, ActorModel>
    {
        public ActorModel Create(Vector3 position, Quaternion rotation)
            => new ActorModel(position, rotation);
    }
}