using Bootstrap.CanvasBootstrap.Data;
using UnityEngine;
using Zenject;

namespace Bootstrap.CanvasBootstrap.Bootstrap
{
    public class CanvasInstaller : Installer
    {
        private readonly ViewRegistry _viewRegistry;

        public CanvasInstaller(ViewRegistry viewRegistry)
        {
            _viewRegistry = viewRegistry;
        }


        public override void InstallBindings()
        {
            var canvas = Container.InstantiatePrefab(_viewRegistry.GamePlayCanvas);
            var canvasData = new CanvasData(canvas.GetComponent<Canvas>());
            
            Container.Bind<CanvasData>().FromInstance(canvasData).AsSingle();
        }
    }
}