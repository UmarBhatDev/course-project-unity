using Features.PaperHint.Controllers;
using Features.PaperHint.Factories;
using UnityEngine.Scripting;
using Utilities;
using Zenject;

namespace Features.PaperHint.Bootstrap
{
    [Preserve]
    public class PaperHintInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<PaperHintViewFactory>();
            Container.BindFactory<PaperHintController, PaperHintControllerFactory>();
        }
    }
}