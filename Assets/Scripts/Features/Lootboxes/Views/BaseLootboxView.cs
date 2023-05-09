using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Features.Hints.Data;
using Features.Hints.Services;
using Features.Interactables.Base;
using Features.Interactables.Data;
using Features.Lootboxes.Data;
using Features.Lootboxes.Services;
using UniRx;
using UnityEngine;
using Zenject;

namespace Features.Lootboxes.Views
{
    public class BaseLootboxView : MonoBehaviour, IInteractable
    {
        protected KeyCodeService KeyCodeService;
        protected LootboxService LootboxService;
        protected KeyGraphicsStorage KeyGraphicsStorage;
        protected InteractableStorage InteractableStorage;
        protected CancellationTokenSource CancellationTokenSource;

        [SerializeField] private LootType _lootType;
        [SerializeField] private float _timeToTrigger;
        [SerializeField] private Outline _outline;
        [SerializeField] private Collider _lootboxCollider;
        [SerializeField] private KeyHintCanvas _keyHintCanvas;
        [SerializeField] private LootboxRarenessType _lootboxRarenessType;

        private bool _canInteract = true;
        private CompositeDisposable _compositeDisposable;

        [Inject]
        public void Construct(InteractableStorage interactableStorage, 
            KeyGraphicsStorage keyGraphicsStorage, KeyCodeService keyCodeService, LootboxService lootboxService)
        {
            KeyCodeService = keyCodeService;
            LootboxService = lootboxService;
            KeyGraphicsStorage = keyGraphicsStorage;
            InteractableStorage = interactableStorage;
        }

        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();
            CancellationTokenSource = new CancellationTokenSource();
        }

        private void OnEnable()
        {
            InteractableStorage.AddItem(this);
        }

        public bool CanInteract(Vector3 interactPosition, out float interactDistance)
        {
            var closestPoint = _lootboxCollider.ClosestPointOnBounds(interactPosition);
            interactDistance = Vector3.Distance(interactPosition, closestPoint);
            return _canInteract;
        }

        public async UniTask Interact(CancellationToken externalToken)
        {
            _canInteract = false;
            _outline.enabled = true;
            _outline.OutlineColor = LootboxService.GetLootColor(_lootboxRarenessType);

            CancellationTokenSource = new CancellationTokenSource();

            var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(externalToken, CancellationTokenSource.Token);

            var actionKey = KeyCodeService.GetKeyBind(KeyType.ActionKey);
            
            var keyUntappedGraphics = KeyGraphicsStorage.GetUntappedKeySprite(actionKey);
            var keyTappedGraphics = KeyGraphicsStorage.GetTappedKeySprite(actionKey);
            
            _keyHintCanvas.SetHintImage(keyUntappedGraphics);
            _keyHintCanvas.SetHintImageActive(true);

            

            while (!linkedCts.Token.IsCancellationRequested)
            {
                float elapsedTime = 0;
                float completionPercent;
                
                var actionKeyPressedCompletionSource = new UniTaskCompletionSource();
                _compositeDisposable = new CompositeDisposable();
                
                Observable
                    .EveryUpdate()
                    .Subscribe(_ =>
                    {
                        if (linkedCts.Token.IsCancellationRequested)
                            return;
                    
                        if (Input.GetKeyDown(actionKey))
                        {
                            if (_lootType == LootType.Instant)
                                actionKeyPressedCompletionSource.TrySetResult();

                            _keyHintCanvas.SetHintImage(keyTappedGraphics);
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
                            _keyHintCanvas.SetHintImage(keyUntappedGraphics);
                            _keyHintCanvas.SetProgressActive(false);
                            elapsedTime = 0;
                        }
                    })
                    .AddTo(_compositeDisposable);

                await actionKeyPressedCompletionSource.Task; 
                OnTaskCompleted();
            }
            
            await StopInteraction();
        }

        public async UniTask StopInteraction()
        {
            _canInteract = true;
            _outline.enabled = false;
            _keyHintCanvas.SetHintImageActive(false);
            _keyHintCanvas.SetProgressActive(false);

            _compositeDisposable?.Dispose();
            CancellationTokenSource?.Cancel();
        }

        protected virtual void OnTaskCompleted()
        {
            gameObject.SetActive(false);
            CancellationTokenSource?.Cancel();
        }

        private void OnDisable()
        {
            InteractableStorage.RemoveItem(this);
        }

        public void Dispose()
        {
            InteractableStorage.RemoveItem(this);
        }
    }
}