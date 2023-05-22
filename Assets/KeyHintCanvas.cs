using UniRx;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class KeyHintCanvas : MonoBehaviour
{
    [SerializeField] private Image _hintImage;
    [SerializeField] private Image _progressImage;

    private Canvas _canvas;
    private CompositeDisposable _compositeDisposable;

    private void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.worldCamera = Camera.current;
    }

    public void SetHintImage(Sprite hintSprite)
    {
        _hintImage.sprite = hintSprite;
    }

    public void SetProgress(float progress)
    {
        _progressImage.fillAmount = 1 - progress;
    }
    
    public void SetProgressActive(bool isActive)
    {
        //todo: animations
        _progressImage.gameObject.SetActive(isActive);
    }

    public void SetHintImageActive(bool isActive, bool followCamera = true)
    {
        //todo: animations
        _compositeDisposable?.Dispose();

        _compositeDisposable = new CompositeDisposable();

        if (followCamera)
        {
            Observable
                .EveryUpdate()
                .Subscribe(_ =>
                {
                    if (Camera.current == null)
                        return;

                    var cameraPosition = Camera.current.transform.position;
                    _canvas.transform.LookAt(new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z));
                })
                .AddTo(_compositeDisposable);
        }

        _hintImage.gameObject.SetActive(isActive);
    }
}