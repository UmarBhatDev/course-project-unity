using System;
using System.Threading;
using Bootstrap.CanvasBootstrap.Data;
using Cysharp.Threading.Tasks;
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

        private readonly CompositeDisposable _compositeDisposable;
        public JourneyController(Stage stage, NodeService nodeService, WinViewFactory winViewFactory)
        {
            _stage = stage;
            _nodeService = nodeService;
            _winViewFactory = winViewFactory;
            _compositeDisposable = new CompositeDisposable();
        }

        public async UniTask Play(CancellationToken cancellationToken)
        {
            var script = _nodeService.CreateScript(_stage.Id);
            script?.Play();
            
            //find BOLT component and run

            await UniTask.WaitUntil(ShouldExit, cancellationToken: cancellationToken);
        }

        private WinView _view;
        
        private bool ShouldExit()
        {
            if (_view != null) 
                return _view.ButtonTapped;
            
            _view = _winViewFactory.Create();
            _view.AddTo(_compositeDisposable);

            return _view.ButtonTapped;
        }

        public void Dispose()
        {
            _compositeDisposable.Dispose();
        }
    }
}