using System;
using Features.Hints.Data;
using Features.Hints.Services;
using Features.Inventory.Data;
using Features.Inventory.Factories;
using Features.PressableButtons.Views;
using UniRx;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Features.Inventory.Services
{
    public class InventoryService : IInitializable
    {
        private readonly KeyCodeService _keyCodeService;
        private readonly InventoryControllerFactory _inventoryControllerFactory;

        public InventoryService(KeyCodeService keyCodeService, InventoryControllerFactory inventoryControllerFactory)
        {
            _keyCodeService = keyCodeService;
            _inventoryControllerFactory = inventoryControllerFactory;
        }

        private IDisposable _exitDisposable;

        public void Initialize()
        {
            var inventoryKey = _keyCodeService.GetKeyBind(KeyType.InventoryKey);
            var inventoryController = _inventoryControllerFactory.Create();

            var keyHint = Object.FindObjectOfType<PressableButtonBaseView>();

            keyHint
                .Interacted
                .Subscribe(_ =>
                {
                    inventoryController.ToggleInventory();
                    
                    _exitDisposable = Observable
                        .EveryUpdate()
                        .Subscribe(_ =>
                        {
                            if (!Input.GetKeyDown(KeyCode.Escape)) return;
                            
                            inventoryController.ToggleInventory();
                            _exitDisposable?.Dispose();
                        });
                });
        }
    }
}