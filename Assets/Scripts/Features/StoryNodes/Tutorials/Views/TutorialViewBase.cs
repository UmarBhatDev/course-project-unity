using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Features.StoryNodes.Tutorials.Views
{
    public class TutorialViewBase : MonoBehaviour, IDisposable
    {
        public ReactiveCommand TutorialPassed = new();
    
        protected virtual async void Start()
        {
            await WaitForTutorialToComplete();
        
            TutorialPassed?.Execute();
        }

        protected virtual async UniTask WaitForTutorialToComplete()
        {
        
        }

        public virtual void Dispose()
        {
            TutorialPassed?.Dispose();
            Destroy(gameObject);
        }
    }
}