using System;
using Cysharp.Threading.Tasks;
using Features.Lootboxes.Views;
using UniRx;
using UnityEngine;

namespace Features.StoryNodes.Tutorials.Views
{
    public class PromoteTutorialView : TutorialViewBase
    {
        [SerializeField] private UndestroyableLootbox _undestroyableLootbox;

        private IDisposable _disposable;
        private UniTaskCompletionSource _completed;
        
        protected override void Start()
        {
            _completed = new UniTaskCompletionSource();
            _disposable = _undestroyableLootbox
                .Interacted
                .Subscribe(_ =>
                {
                    _completed.TrySetResult();
                    _disposable?.Dispose();
                });
            
            base.Start();
        }

        protected override async UniTask WaitForTutorialToComplete()
        {
            await _completed.Task;
        }
    }
}