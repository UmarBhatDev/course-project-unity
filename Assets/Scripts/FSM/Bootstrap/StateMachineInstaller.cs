using FSM.Interfaces;
using Utilities;
using Zenject;

namespace FSM.Bootstrap
{
    public class StateMachineInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<StateMachine>();

            Container
                .Bind<IStateBase>()
                .To(x => x.AllClasses().DerivingFrom<IStateBase>()).AsSingle();
        }
    }
}