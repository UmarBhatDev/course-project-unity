using System;
using Features.Inventory.Data;
using Features.Lootboxes.Data;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Inventory.Views
{
    public class SlotView : MonoBehaviour, IDisposable
    {
        public event Action<SlotView> SlotSelected;
        
        [NonSerialized] public InventoryItemType InventoryItemType;
        [NonSerialized] public bool IsBusy;
        
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _innerOutlineImage;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private Button _slotButton;
        [SerializeField] private GameObject _outLineImage;

        [Inject] private OutlineByRarenessRegistry _outlinesByRarenessRegistry;

        private CompositeDisposable _compositeDisposable;
        
        private void Start()
        {
            _compositeDisposable = new CompositeDisposable();

            _slotButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    _outLineImage.gameObject.SetActive(true);
                    _slotButton.interactable = false;

                    SlotSelected?.Invoke(this);
                })
                .AddTo(_compositeDisposable);
        }

        public void SetItem(InventoryItemData itemData, int count, bool isBusy = true)
        {
            var innerOutlineSprite = _outlinesByRarenessRegistry.GetOutlineByRareness(itemData.ItemRareness);
            
            _innerOutlineImage.sprite = innerOutlineSprite;
            _itemImage.sprite = itemData.ItemIcon;
            InventoryItemType = itemData.ItemType;
            _countText.text = count.ToString();
            
            IsBusy = isBusy;
        }

        public void SetOutlineActive(bool activeState)
        {
            _outLineImage.gameObject.SetActive(activeState);
            _slotButton.interactable = !activeState;
        }

        public void SetCount(int count)
        {
            _countText.text = count.ToString();
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}