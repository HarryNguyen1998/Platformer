using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] SettingsDialog _settingsDialog;
    [SerializeField] Button _openSettingsBtn;

    public void OpenSettingsMenu()
    {
        _settingsDialog.Open();
    }

}
