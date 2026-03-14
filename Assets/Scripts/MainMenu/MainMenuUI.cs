using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject _defaultMenu;
    [SerializeField] private GameObject _settingsMenu;
    [SerializeField] private Image _effectVolumeBar;
    [SerializeField] private Image _musicVolumeBar;
    [SerializeField] private MainMenuAnimator _animator;

    public void ToggleSettingsMenu(bool isOn)
    {
        _animator.ToggleSettingsAnimation(isOn);
    }

    public void StartGame()
    {
        _animator.StartGameAnimation();
    }

    public void ChangeEffectBarFill(float fillAmount)
    {
        _effectVolumeBar.fillAmount = fillAmount;
    }

    public void ChangeMusicBarFill(float fillAmount)
    {
        _musicVolumeBar.fillAmount = fillAmount;
    }
}
