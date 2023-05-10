using Features.Actor.Views;
using Features.EndPoint.Views;
using Features.MainMenu.Views;
using Features.Win;
using UnityEngine;

namespace Bootstrap.CanvasBootstrap.Data
{
    [CreateAssetMenu(fileName = "ViewRegistry", menuName = "Registries/ViewRegistry")]
    public class ViewRegistry : ScriptableObject
    {
        [SerializeField] private MainMenuView _mainMenuPanel;
        [SerializeField] private EndPointView _endPointView;
        [SerializeField] private Canvas _gamePlayCanvas;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private WinView _winView;

        public WinView WinView => _winView;
        public PlayerView PlayerView => _playerView;
        public Canvas GamePlayCanvas => _gamePlayCanvas;
        public EndPointView EndPointView => _endPointView;
        public MainMenuView MainMenuPanel => _mainMenuPanel;
    }
}

