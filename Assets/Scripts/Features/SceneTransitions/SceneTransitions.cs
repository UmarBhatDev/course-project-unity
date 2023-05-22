using System;
using Bootstrap.GlobalDisposable.Services;
using Cysharp.Threading.Tasks;
using Features.SceneTransitions.Controllers;
using Features.SceneTransitions.Factories;
using FSM.Data;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Features.SceneTransitions
{
    public delegate UniTask AdditionalTask();
    public delegate AsyncOperationHandle<SceneInstance> AsyncLoadSceneAction();
    public static class Transition
    {
        public static UniTask ToScene(string sceneName, CurtainViewFactory curtainViewFactory, GlobalCompositeDisposable globalCompositeDisposable, 
            CurtainType curtain = CurtainType.BlackFade, AdditionalTask additionalTask = default)
            => ToScene(
                () => Addressables.LoadSceneAsync(sceneName),
                curtainViewFactory,
                globalCompositeDisposable,
                curtain,
                additionalTask);
        
        private static async UniTask ToScene(AsyncLoadSceneAction action, CurtainViewFactory curtainViewFactory, IDisposable globalCompositeDisposable, 
            CurtainType curtain = CurtainType.BlackFade, AdditionalTask additionalTask = default)
        {
            var curtainView = curtainViewFactory.Create(curtain);
            
            using var transition = new CurtainScope(curtainView);
            
            await transition.FadeIn;

            globalCompositeDisposable?.Dispose();
            
            var handle = action.Invoke();
            var task = new UniTaskCompletionSource();
            
            if (additionalTask != null) 
                await additionalTask.Invoke();

            handle.Completed += _ => task.TrySetResult();

            await task.Task;

            await handle.Result.ActivateAsync();
        }
    }
}