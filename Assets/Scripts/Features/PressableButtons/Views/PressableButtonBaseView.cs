using System.Threading;
using Cysharp.Threading.Tasks;
using Features.Hints.Data;
using Features.Hints.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.PressableButtons.Views
{
    public class PressableButtonBaseView : MonoBehaviour
    {
        public ReactiveCommand Interacted = new();

        protected KeyCodeService KeyCodeService;
        protected KeyGraphicsStorage KeyGraphicsStorage;
        
        [SerializeField] private KeyType _keyType;
        [SerializeField] private KeyHintCanvas _keyHintCanvas;
        [SerializeField] private float _timeToTrigger;
        
        private CancellationTokenSource _cancellationTokenSource;
        private CompositeDisposable _compositeDisposable;

        private Sprite _keyUntappedGraphics;
        private Sprite _keyTappedGraphics;

        [Inject]
        public void Construct(KeyCodeService keyCodeService, KeyGraphicsStorage keyGraphicsStorage)
        {
            KeyCodeService = keyCodeService;
            KeyGraphicsStorage = keyGraphicsStorage;
        }

        private void Start()
        {
            AwaitForPlayerInput().Forget();
        }

        private async UniTask AwaitForPlayerInput()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            var actionKey = KeyCodeService.GetKeyBind(_keyType);

            _keyUntappedGraphics = KeyGraphicsStorage.GetUntappedKeySprite(actionKey);
            _keyTappedGraphics = KeyGraphicsStorage.GetTappedKeySprite(actionKey);
            
            _keyHintCanvas.SetHintImage(_keyUntappedGraphics);
            
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                float elapsedTime = 0;
                float completionPercent;
                
                var actionKeyPressedCompletionSource = new UniTaskCompletionSource();
                _compositeDisposable = new CompositeDisposable();
                
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (_cancellationTokenSource.Token.IsCancellationRequested)
                            return;
                    
                        if (Input.GetKeyDown(actionKey))
                        {
                            _keyHintCanvas.SetHintImage(_keyTappedGraphics);
                            _keyHintCanvas.SetProgressActive(true);
                        }
                        else if (Input.GetKey(actionKey))
                        {
                            elapsedTime += Time.deltaTime;
                            completionPercent = elapsedTime / _timeToTrigger;
                        
                            _keyHintCanvas.SetProgress(completionPercent);
                        
                            if (elapsedTime > _timeToTrigger) 
                                actionKeyPressedCompletionSource.TrySetResult();
                        }
                        else
                        {
                            _keyHintCanvas.SetHintImage(_keyUntappedGraphics);
                            _keyHintCanvas.SetProgressActive(false);
                            elapsedTime = 0;
                        }
                    })
                    .AddTo(_compositeDisposable);

                await actionKeyPressedCompletionSource.Task; 
                OnTaskCompleted();
            }
        }
        
        protected virtual void OnTaskCompleted()
        {
            Interacted?.Execute();
            _keyHintCanvas.SetHintImage(_keyUntappedGraphics);
            _keyHintCanvas.SetProgressActive(false);
        }
    }
}