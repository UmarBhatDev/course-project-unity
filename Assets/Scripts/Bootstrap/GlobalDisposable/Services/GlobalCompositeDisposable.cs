using System;
using UniRx;
using UnityEngine;

namespace Bootstrap.GlobalDisposable.Services
{
    public class GlobalCompositeDisposable : IDisposable
    {
        private CompositeDisposable _compositeDisposable;

        public GlobalCompositeDisposable()
        {
            _compositeDisposable = new CompositeDisposable();
        }

        public void AddDisposable(IDisposable disposable)
            => _compositeDisposable.Add(disposable);

        public void Dispose()
        {
            try
            {
                _compositeDisposable?.Dispose();
            }
            catch (InvalidOperationException)
            {
                Debug.Log("nothing to dispose");
            }
            
            _compositeDisposable = new CompositeDisposable();
        }
    }
}