using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private Image _effectVolumeBar;

    public void ToggleSettingsMenu(bool isOn)
    {
        _defaultMenu.SetActive(!isOn);
        _settingsMenu.SetActive(isOn);
    }

    public void ChangeEffectBarFill(float fillAmount)
    {
        _effectVolumeBar.fillAmount = fillAmount;
    }
}
