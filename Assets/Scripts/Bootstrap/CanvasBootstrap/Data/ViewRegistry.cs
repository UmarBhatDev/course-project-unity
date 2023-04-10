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
        [SerializeField] private Canvas _gamePlayCanvas;
        [SerializeField] private WinView _winView;
        [SerializeField] private EndPointView _endPointView;

        public WinView WinView => _winView;

        public MainMenuView MainMenuPanel => _mainMenuPanel;

        public Canvas GamePlayCanvas => _gamePlayCanvas;
        public EndPointView EndPointView => _endPointView;
    }
}

