using UniRx;
using UnityEngine;

namespace Features.EndPoint.Views
{
    public class EndPointView : MonoBehaviour
    {
        public readonly ReactiveCommand EndPointReached = new();
        
        private void OnTriggerEnter(Collider other)
        {
            EndPointReached.Execute();
        }
    }
}