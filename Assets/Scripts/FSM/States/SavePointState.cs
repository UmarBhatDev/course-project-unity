using Bootstrap.GlobalDisposable.Services;
using Cysharp.Threading.Tasks;
using Features.SavePointMenu.Factories;
using Features.SceneTransitions;
using Features.SceneTransitions.Factories;
using FSM.Data;
using FSM.Interfaces;
using UnityEngine;
using UnityEngine.Scripting;

namespace FSM.States
{
    [Preserve]
    public class SavePointState : IGameState<SavePointState.PayLoad>
    {
        private const string SCENE_NAME = "SavePointMenu";

        private readonly CurtainViewFactory _curtainViewFactory;
        private readonly GlobalCompositeDisposable _globalCompositeDisposable;
        private readonly SavePointMenuControllerFactory _savePointMenuControllerFactory;

        public SavePointState(CurtainViewFactory curtainViewFactory, GlobalCompositeDisposable globalCompositeDisposable,
            SavePointMenuControllerFactory savePointMenuControllerFactory)
        {
            _curtainViewFactory = curtainViewFactory;
            _globalCompositeDisposable = globalCompositeDisposable;
            _savePointMenuControllerFactory = savePointMenuControllerFactory;
        }
        
        public async UniTaskVoid Enter(PayLoad payload)
        {
            var curtainType = payload.CurtainOverride ?? CurtainType.BlackFade;

            await Transition.ToScene(SCENE_NAME, _curtainViewFactory, _globalCompositeDisposable, curtainType, payload.AdditionalAction);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            var savePointMenuController = _savePointMenuControllerFactory.Create();
            
            savePointMenuController.Initialize();
        }
        
        public struct PayLoad
        {
            public AdditionalTask AdditionalAction;
            public CurtainType? CurtainOverride { get; set; }

            public PayLoad(CurtainType curtainOverride, AdditionalTask additionalAction)
            {
                CurtainOverride = curtainOverride;
                AdditionalAction = additionalAction;
            }
        }

        public void Exit()
        {
        }
    }
    
    public static partial class StateMachineExtensions
    {
        public static void GoSavePointState(this IStateMachine stateMachine, CurtainType? curtainType = null, AdditionalTask additionalTask = null)
            => stateMachine.EnterState<SavePointState, SavePointState.PayLoad>(new SavePointState.PayLoad
            {
                CurtainOverride = curtainType,
                AdditionalAction = additionalTask
            });
    }
}