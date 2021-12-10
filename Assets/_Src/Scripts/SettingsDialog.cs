using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingsDialog : MonoBehaviour
{
    [SerializeField] Transform _settingsModalWindow;
    [SerializeField] Button _closeBtn;

    public void Start()
    {
        _settingsModalWindow.localScale = Vector2.zero;
        gameObject.SetActive(false);
    }

    public void Open()
    {
        gameObject.SetActive(true);
        _settingsModalWindow.DOScale(1.0f, 0.5f);
        _closeBtn.interactable = true;
    }
    public void Close()
    {
        _closeBtn.interactable = false;
        _settingsModalWindow.DOScale(Vector2.zero, 0.8f).SetEase(Ease.InBack).OnComplete(() => {
            gameObject.SetActive(false);
        });
    }

}
