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
        public event Action<ActorModel, PlayerView> ActorCreated;

        private readonly ActorViewFactory _actorViewFactory;
        private readonly ActorModelFactory _actorModelFactory;
        private readonly ActorControllerFactory _actorControllerFactory;

        private ActorModel _actorModel;
        private PlayerView _actorView;
        private ActorController _actorController;

        public ActorRule(ActorViewFactory actorViewFactory, ActorModelFactory actorModelFactory,
            ActorControllerFactory actorControllerFactory)
        {
            _actorViewFactory = actorViewFactory;
            _actorModelFactory = actorModelFactory;
            _actorControllerFactory = actorControllerFactory;
        }

        public void Spawn(Vector3 position, Quaternion rotation)
        {
            var actorModel = _actorModelFactory.Create(position, rotation);
            var actorView = _actorViewFactory.Create(actorModel, position);
            // actorModel.SetRigidbody(actorView.GetComponent<Rigidbody>());
            // var actorController = _actorControllerFactory.Create(actorModel, actorView);

            _actorModel = actorModel;
            _actorView = actorView;
            // _actorController = actorController;

            ActorCreated?.Invoke(actorModel, actorView);
        }

        public ActorModel GetActorModel()
            => _actorModel;
        
        public PlayerView GetActorView()
            => _actorView;
        
        public void DisposeActor()
        {
            _actorModel?.Dispose();
            _actorController?.Dispose();
        }
    }
}