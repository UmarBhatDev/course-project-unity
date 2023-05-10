using Bootstrap;
using Bootstrap.GlobalDisposable.Services;
using Features.StoryNodes.Tutorials.Views;
using Zenject;

namespace Features.StoryNodes.Tutorials.Factories
{
    public class TutorialViewFactory : IFactory<TutorialViewBase, TutorialViewBase>
    {
        private readonly DiContainer _diContainer;
        private readonly GlobalCompositeDisposable _globalCompositeDisposable;

        public TutorialViewFactory(DiContainer diContainer, GlobalCompositeDisposable globalCompositeDisposable)
        {
            _diContainer = diContainer;
            _globalCompositeDisposable = globalCompositeDisposable;
        }

        public TutorialViewBase Create(TutorialViewBase tutorialViewBase)
        {
            var tutorial = _diContainer.InstantiatePrefabForComponent<TutorialViewBase>(tutorialViewBase);
            _globalCompositeDisposable.AddDisposable(tutorial);
            return tutorial;
        }
    }
}