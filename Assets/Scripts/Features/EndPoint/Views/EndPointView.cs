using System;
using UniRx;
using UnityEngine;

namespace Features.EndPoint.Views
{
    public class EndPointView : MonoBehaviour, IDisposable
    {
        public readonly ReactiveCommand EndPointReached = new();
        
        private void OnTriggerEnter(Collider other)
        {
            EndPointReached.Execute();
        }

        public void Dispose()
        {
            EndPointReached?.Dispose();
            Destroy(gameObject);
        }
    }
}