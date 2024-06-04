using Features.PaperHint.Factories;
using UnityEngine.Scripting;

namespace Features.PaperHint.Controllers
{
    [Preserve]
    public class PaperHintController
    {
        private readonly PaperHintViewFactory _paperHintViewFactory;

        public PaperHintController(PaperHintViewFactory paperHintViewFactory)
        {
            _paperHintViewFactory = paperHintViewFactory;
        }

        public void StartFlow()
        {
            var view = _paperHintViewFactory.Create();

            view.OnContinueClicked += () => view.Dispose();
        }
    }
}