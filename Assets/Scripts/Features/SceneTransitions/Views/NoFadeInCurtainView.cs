using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.SceneTransitions.Data;
using FSM.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Features.SceneTransitions.Views
{
    [AttributeCurtainType(CurtainType.NoFadeIn)]
    public class NoFadeInCurtainView : CurtainViewBase
    {
        [SerializeField] private Image _graphic;
        [SerializeField] private float _fadeOutDuration;

        private Tween _fadeTween;

        public override UniTask FadeInCompletionSource => UniTask.CompletedTask;
        
        public override void FadeIn()
        {
            var currentColor = _graphic.color;
            currentColor.a = 1;
            _graphic.color = currentColor;
        }

        public override void FadeOut()
        {
            FadeOutAsync().Forget();

            async UniTask FadeOutAsync()
            {
                await _graphic.DOFade(0, _fadeOutDuration).AwaitForComplete();
                Destroy(gameObject);
            }
        }
    }
}