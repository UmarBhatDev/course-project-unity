using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Features.SceneTransitions.Views
{
    public abstract class CurtainViewBase : MonoBehaviour
    {
        public abstract UniTask FadeInCompletionSource { get; }
        public abstract void FadeIn();
        public abstract void FadeOut();
    }
}