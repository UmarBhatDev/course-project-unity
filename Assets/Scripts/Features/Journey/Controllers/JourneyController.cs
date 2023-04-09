using System;
using System.Threading;
using Bootstrap.CanvasBootstrap.Data;
using Cysharp.Threading.Tasks;
using Features.Roadmap.Data;
using Features.Win;

namespace Features.Journey.Controllers
{
    public class JourneyController : IDisposable
    {
        private readonly Stage _stage;
        private readonly WinViewFactory _winViewFactory;

        public JourneyController(Stage stage, WinViewFactory winViewFactory)
        {
            _stage = stage;
            _winViewFactory = winViewFactory;
        }

        public async UniTask Play(CancellationToken cancellationToken)
        {
            //find BOLT component and run

            await UniTask.WaitUntil(ShouldExit, cancellationToken: cancellationToken);
        }

        private WinView _view;
        private bool ShouldExit()
        {
            if (_view == null)
            {
                _view = _winViewFactory.Create();
            }

            return _view.ButtonTapped;
        }

        public void Dispose()
        {
        }
    }
}