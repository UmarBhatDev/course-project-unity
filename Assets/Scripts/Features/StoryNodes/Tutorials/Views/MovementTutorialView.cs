using System;
using Cysharp.Threading.Tasks;
using Features.Actor.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.StoryNodes.Tutorials.Views
{
    public class MovementTutorialView : TutorialViewBase
    {
        [SerializeField] private TextMesh _moveText;
        [SerializeField] private TextMesh _jumpText;
        [SerializeField] private TextMesh _sprintText;
        [SerializeField] private TextMesh _crouchText;

        [Inject] private PlayerStateService _playerStateService;

        private IDisposable _jumpTutorialDisposable;
        private IDisposable _moveTutorialDisposable;
        private IDisposable _sprintTutorialDisposable;
        private IDisposable _crouchTutorialDisposable;

        private UniTaskCompletionSource _jumpTutorialCompletionSource;
        private UniTaskCompletionSource _moveTutorialCompletionSource;
        private UniTaskCompletionSource _sprintTutorialCompletionSource;
        private UniTaskCompletionSource _crouchTutorialCompletionSource;

        protected override void Start()
        {
            _jumpTutorialCompletionSource = new UniTaskCompletionSource();
            _moveTutorialCompletionSource = new UniTaskCompletionSource();
            _sprintTutorialCompletionSource = new UniTaskCompletionSource();
            _crouchTutorialCompletionSource = new UniTaskCompletionSource();
        
            _jumpTutorialDisposable = 
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (!_playerStateService.IsJumping())
                            return;
                    
                        _jumpText.GetComponent<MeshRenderer>().material.color = Color.green;
                        _jumpTutorialCompletionSource.TrySetResult();
                        _jumpTutorialDisposable?.Dispose();
                    });
        
            _moveTutorialDisposable = 
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (!_playerStateService.IsWalking())
                            return;
                    
                        _moveText.GetComponent<MeshRenderer>().material.color = Color.green;
                        _moveTutorialCompletionSource.TrySetResult();
                        _moveTutorialDisposable?.Dispose();
                    });
        
            _sprintTutorialDisposable = 
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (!_playerStateService.IsSprinting())
                            return;
                    
                        _sprintText.GetComponent<MeshRenderer>().material.color = Color.green;
                        _sprintTutorialCompletionSource.TrySetResult();
                        _sprintTutorialDisposable?.Dispose();
                    });
        
            _crouchTutorialDisposable = 
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (!_playerStateService.IsCrouching())
                            return;
                    
                        _crouchText.GetComponent<MeshRenderer>().material.color = Color.green;
                        _crouchTutorialCompletionSource.TrySetResult();
                        _crouchTutorialDisposable?.Dispose();
                    });
        
            base.Start();
        }

        protected override async UniTask WaitForTutorialToComplete()
        {
            await UniTask.WhenAll(_jumpTutorialCompletionSource.Task,
                _moveTutorialCompletionSource.Task,
                _sprintTutorialCompletionSource.Task,
                _crouchTutorialCompletionSource.Task);
        }

        public override void Dispose()
        {
            _jumpTutorialDisposable?.Dispose();
            _moveTutorialDisposable?.Dispose();
            _sprintTutorialDisposable?.Dispose();
            _crouchTutorialDisposable?.Dispose();
            
            base.Dispose();
        }
    }
}