using System;
using System.Threading;
using Bootstrap.CanvasBootstrap.Data;
using Cysharp.Threading.Tasks;
using Features.Actor.Rules;
using Features.Roadmap.Data;
using Features.StoryNodes.Services;
using Features.Win;
using UniRx;

namespace Features.Journey.Controllers
{
    public class JourneyController : IDisposable
    {
        private readonly Stage _stage;
        private readonly NodeService _nodeService;
        private readonly WinViewFactory _winViewFactory;

        private ActorRule _actorRule;

        private readonly CompositeDisposable _compositeDisposable;
        public JourneyController(Stage stage, NodeService nodeService, WinViewFactory winViewFactory, ActorRule actorRule)
        {
            _stage = stage;
            _actorRule = actorRule;
            _nodeService = nodeService;
            _winViewFactory = winViewFactory;
            _compositeDisposable = new CompositeDisposable();
        }

        public async UniTask Play(CancellationToken cancellationToken)
        {
            var script = _nodeService.CreateScript(_stage.Id, _stage.Script);
            script?.Play();
            
            //find BOLT component and run

            await UniTask.WaitUntil(ShouldExit, cancellationToken: cancellationToken);
        }

        private WinView _view;
        
        private bool ShouldExit()
        {
            if (_view != null) 
                return _view.ButtonTapped || _levelCompletionRequested;
            
            _view = _winViewFactory.Create();
            _view.AddTo(_compositeDisposable);

            return _view.ButtonTapped || _levelCompletionRequested;
        }

        private bool _levelCompletionRequested = false;

        public void RequestLevelCompletion()
        {
            _levelCompletionRequested = true;
        }

        public void Dispose()
        {
            _actorRule.DisposeActor();
            _compositeDisposable.Dispose();
        }
    }
}