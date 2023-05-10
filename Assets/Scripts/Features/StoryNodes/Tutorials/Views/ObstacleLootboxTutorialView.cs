using System;
using Cysharp.Threading.Tasks;
using Features.Actor.Services;
using Features.Interactables.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.StoryNodes.Tutorials.Views
{
    public class ObstacleLootboxTutorialView : TutorialViewBase
    {
        [SerializeField] private TextMesh _claimLootboxText;
        [SerializeField] private TextMesh _jumpOverObstacleText;
        [SerializeField] private TextMesh _hideBehindObstacleText;
        
        [Inject] private PlayerStateService _playerStateService;
        [Inject] private InteractableRule _interactableRule;
        
        private IDisposable _claimLootboxTutorialDisposable;
        private IDisposable _jumpOverObstacleTutorialDisposable;
        private IDisposable _hideBehindObstacleTutorialDisposable;
        
        private UniTaskCompletionSource _claimLootboxTutorialCompletionSource;
        private UniTaskCompletionSource _jumpOverObstacleTutorialCompletionSource;
        private UniTaskCompletionSource _hideBehindObstacleTutorialCompletionSource;

        protected override void Start()
        {
            _claimLootboxTutorialCompletionSource = new UniTaskCompletionSource();
            _jumpOverObstacleTutorialCompletionSource = new UniTaskCompletionSource();
            _hideBehindObstacleTutorialCompletionSource = new UniTaskCompletionSource();
            
            _hideBehindObstacleTutorialDisposable = 
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (!_playerStateService.IsInCover())
                            return;
                    
                        _hideBehindObstacleText.GetComponent<MeshRenderer>().material.color = Color.green;
                        _hideBehindObstacleTutorialCompletionSource.TrySetResult();
                        _hideBehindObstacleTutorialDisposable?.Dispose();
                    });
            
            _jumpOverObstacleTutorialDisposable = 
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (!_playerStateService.IsVaulting())
                            return;
                    
                        _jumpOverObstacleText.GetComponent<MeshRenderer>().material.color = Color.green;
                        _jumpOverObstacleTutorialCompletionSource.TrySetResult();
                        _jumpOverObstacleTutorialDisposable?.Dispose();
                    });

            _claimLootboxTutorialDisposable = 
                _interactableRule
                    .GetInteractedAsObservable()
                    .Subscribe(_ =>
                    {
                        _claimLootboxText.GetComponent<MeshRenderer>().material.color = Color.green;
                        _claimLootboxTutorialCompletionSource.TrySetResult();
                        _claimLootboxTutorialDisposable?.Dispose();
                    });
            
            base.Start();
        }

        protected override async UniTask WaitForTutorialToComplete()
        {
            await UniTask.WhenAll(_claimLootboxTutorialCompletionSource.Task,
                _jumpOverObstacleTutorialCompletionSource.Task,
                _hideBehindObstacleTutorialCompletionSource.Task);
        }

        public override void Dispose()
        {
            _claimLootboxTutorialDisposable?.Dispose();
            _jumpOverObstacleTutorialDisposable?.Dispose();
            _hideBehindObstacleTutorialDisposable?.Dispose();
            
            base.Dispose();
        }
    }
}