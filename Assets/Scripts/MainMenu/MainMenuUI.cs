using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the UI of the Main Menu.
/// This includes playing the start up animation and updating the fill bars of the sound settings.
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Image _effectVolumeBar;
    [SerializeField] private Image _musicVolumeBar;
    [SerializeField] private MainMenuAnimator _animator;

    /// <summary>
    /// Toggles the setting menu.
    /// </summary>
    /// <param name="isOn">Activitu of settings menu.</param>
    public void ToggleSettingsMenu(bool isOn)
    {
        _animator.ToggleSettingsAnimation(isOn);
    }

    /// <summary>
    /// Plays the start up animation.
    /// </summary>
    public void PlayStartAnimation()
    {
        _animator.AnimateStartUp();
    }

    /// <summary>
    /// Changes fill of sound effect bar.
    /// </summary>
    /// <param name="fillAmount">The fill amount.</param>
    public void ChangeEffectBarFill(float fillAmount)
    {
        _effectVolumeBar.fillAmount = fillAmount;
    }

    /// <summary>
    /// Changes fill of music effect bar.
    /// </summary>
    /// <param name="fillAmount">The fill amount.</param>
    public void ChangeMusicBarFill(float fillAmount)
    {
        _musicVolumeBar.fillAmount = fillAmount;
    }

    /// <summary>
    /// Kills all animation animations.
    /// </summary>
    public void KillAllAnimations()
    {
        _animator.KillAll();
    }
}
