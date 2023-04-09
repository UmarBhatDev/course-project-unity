using System;
using Cysharp.Threading.Tasks;
using Features.SceneTransitions.Controllers;
using Features.SceneTransitions.Factories;
using FSM.Data;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Features.SceneTransitions
{
    public delegate UniTask AdditionalTask();
    public static class Transition
    {
        public static UniTask ToScene(string sceneName, CurtainViewFactory curtainViewFactory, CurtainType curtain = CurtainType.BlackFade, AdditionalTask additionalTask = default)
            => ToScene(
                handle: Addressables.LoadSceneAsync(sceneName, LoadSceneMode.Single, false),
                curtainViewFactory,
                curtain,
                additionalTask);
        
        private static async UniTask ToScene(AsyncOperationHandle<SceneInstance> handle, CurtainViewFactory curtainViewFactory, CurtainType curtain = CurtainType.BlackFade, AdditionalTask additionalTask = default)
        {
            var curtainView = curtainViewFactory.Create(curtain);
            
            using var transition = new CurtainScope(curtainView);
            
            await transition.FadeIn;
            var task = new UniTaskCompletionSource();
            
            if (additionalTask != null) 
                await additionalTask.Invoke();

            handle.Completed += _ => task.TrySetResult();

            await task.Task;

            await handle.Result.ActivateAsync();
        }
    }
}