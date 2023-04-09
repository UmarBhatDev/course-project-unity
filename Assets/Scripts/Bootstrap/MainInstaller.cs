using Bootstrap.CanvasBootstrap.Bootstrap;
using Features.Journey.Bootstrap;
using Features.MainMenu.Bootstrap;
using Features.Persistence.Bootstrap;
using Features.SceneTransitions.Bootstrap;
using Features.Win;
using FSM.Bootstrap;
using Utilities;
using Zenject;

namespace Bootstrap
{
   public class MainInstaller : MonoInstaller
   {
      public override void InstallBindings()
      { 
         Container.Install<CanvasInstaller>();
         Container.Install<JourneyInstaller>();
         Container.Install<CurtainInstaller>();
         Container.Install<MainMenuInstaller>();
         Container.Install<PersistenceInstaller>();
         Container.Install<StateMachineInstaller>();

         Container.InstallService<WinViewFactory>();
      }
   }
}