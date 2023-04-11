using System;
using Features.Actor.Controllers;
using Features.Actor.Factories;
using Features.Actor.Models;
using Features.Actor.Views;
using UnityEngine;

namespace Features.Actor.Rules
{
    public class ActorRule : IGameRule
    {
        public event Action<ActorModel> ActorCreated;
        
        private readonly ActorViewFactory _actorViewFactory;
        private readonly ActorModelFactory _actorModelFactory;
        private readonly ActorControllerFactory _actorControllerFactory;

        private ActorModel _actorModel;
        private ActorController _actorController;

        public ActorRule(ActorViewFactory actorViewFactory, ActorModelFactory actorModelFactory, ActorControllerFactory actorControllerFactory)
        {
            _actorViewFactory = actorViewFactory;
            _actorModelFactory = actorModelFactory;
            _actorControllerFactory = actorControllerFactory;
        }

        public ActorModel GetModel()
            => _actorModel;

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            var actorModel = _actorModelFactory.Create(position, rotation);
            var actorController = _actorControllerFactory.Create(actorModel);
            var actorView = _actorViewFactory.Create(actorModel);

            _actorModel = actorModel;
            _actorController = actorController;
            
            ActorCreated?.Invoke(actorModel);
        }

        public void DisposeActor()
        {
            _actorModel?.Dispose();
            _actorController?.Dispose();
        }
    }
}