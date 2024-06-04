using Features.Actor.Views;
using Features.GameOverMenu.Views;
using Features.Inventory.Views;
using Features.MainMenu.Views;
using Features.PaperHint.Views;
using Features.SavePointMenu.Views;
using UnityEngine;

namespace Bootstrap.CanvasBootstrap.Data
{
    [CreateAssetMenu(fileName = "ViewRegistry", menuName = "Registries/ViewRegistry")]
    public class ViewRegistry : ScriptableObject
    {
        [SerializeField] private GameOverMenuView _gameOverMenuView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private MainMenuView _mainMenuPanel;
        [SerializeField] private SavePointMenuView _savePointMenuPanel;
        [SerializeField] private MoveToLocationElementView _moveToLocationElementView;
        [SerializeField] private PaperHintView _paperHintView;
        [SerializeField] private Canvas _gamePlayCanvas;
        [SerializeField] private PlayerView _playerView;

        public PlayerView PlayerView => _playerView;
        public Canvas GamePlayCanvas => _gamePlayCanvas;
        public MainMenuView MainMenuPanel => _mainMenuPanel;
        public SavePointMenuView SavePointMenuPanel => _savePointMenuPanel;
        public PaperHintView PaperHintView => _paperHintView;
        public MoveToLocationElementView MoveToLocationElementView => _moveToLocationElementView;
        public InventoryView InventoryView => _inventoryView;
        public GameOverMenuView GameOverMenuView => _gameOverMenuView;
    }
}

