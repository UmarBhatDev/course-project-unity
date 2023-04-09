using UnityEngine;

namespace Bootstrap.CanvasBootstrap
{
    public struct CanvasData
    {
        public Canvas Canvas => _canvas;

        private readonly Canvas _canvas;

        public CanvasData(Canvas canvas)
        {
            _canvas = canvas;
        }
    }
}