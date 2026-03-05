using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private GameObject _settingsMenu;

    public void ToggleSettingsMenu(bool isOn)
    {
        _defaultMenu.SetActive(!isOn);
        _settingsMenu.SetActive(isOn);
    }
}
