using System;
using Cysharp.Threading.Tasks;
using Features.MainMenu.Factories;
using Features.MainMenu.Views;
using FSM;
using FSM.Data;
using FSM.States;
using UnityEngine.Device;

namespace Features.MainMenu.Controllers
{
    public class MainMenuController : IDisposable
    {
        private MainMenuView _mainMenuView;
        private readonly IStateMachine _stateMachine;
        private readonly MainMenuViewFactory _mainMenuViewFactory;

        public MainMenuController(MainMenuViewFactory mainMenuViewFactory, IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _mainMenuViewFactory = mainMenuViewFactory;
        }

        public void Initialize()
        {
            _mainMenuView = _mainMenuViewFactory.Create();

            _mainMenuView.PlayButtonPressed += () =>
            {
                _stateMachine.GoJourney(CurtainType.BlackFade, Close);
            };
            
            _mainMenuView.ExitButtonPressed += Application.Quit;

            UniTask Close()
            {
                Dispose();
                return UniTask.CompletedTask;
            }
        }

        public void Dispose()
        {
            _mainMenuView.Dispose();
        }
    }
}