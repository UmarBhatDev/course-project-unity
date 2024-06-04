using Bootstrap.CanvasBootstrap.Bootstrap;
using Bootstrap.GlobalDisposable.Installers;
using Features.Actor.Installers;
using Features.GameOverMenu.Bootstrap;
using Features.Hints.Installers;
using Features.Interactables.Installers;
using Features.Inventory.Bootstrap;
using Features.Journey.Bootstrap;
using Features.Lootboxes.Bootstrap;
using Features.MainMenu.Bootstrap;
using Features.PaperHint.Bootstrap;
using Features.Persistence.Bootstrap;
using Features.SavePointMenu.Bootstrap;
using Features.SceneTransitions.Bootstrap;
using Features.StoryNodes.Bootstrap;
using Features.StoryNodes.Tutorials.Installers;
using FSM.Bootstrap;
using Zenject;

namespace Bootstrap
{
    public class MainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Install<KeyInstaller>();
            Container.Install<NodeInstaller>();
            Container.Install<ActorInstaller>();
            Container.Install<CanvasInstaller>();
            Container.Install<JourneyInstaller>();
            Container.Install<PaperHintInstaller>();
            Container.Install<CurtainInstaller>();
            Container.Install<LootboxInstaller>();
            Container.Install<GameOverInstaller>();
            Container.Install<MainMenuInstaller>();
            Container.Install<SavePointMenuInstaller>();
            Container.Install<TutorialInstaller>();
            Container.Install<InventoryInstaller>();
            Container.Install<PersistenceInstaller>();
            Container.Install<InteractableInstaller>();
            Container.Install<StateMachineInstaller>();
            Container.Install<GlobalDisposableInstaller>();
        }
    }
}