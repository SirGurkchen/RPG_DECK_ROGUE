using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Image _effectVolumeBar;
    [SerializeField] private Image _musicVolumeBar;
    [SerializeField] private MainMenuAnimator _animator;

    public void ToggleSettingsMenu(bool isOn, AudioClip sound)
    {
        _animator.ToggleSettingsAnimation(isOn, sound);
    }

    public void PlayStartAnimation(AudioClip sound)
    {
        _animator.AnimateStartUp();
        AudioManager.Instance.PlayAudioClip(sound);
    }

    public void ChangeEffectBarFill(float fillAmount)
    {
        _effectVolumeBar.fillAmount = fillAmount;
    }

    public void ChangeMusicBarFill(float fillAmount)
    {
        _musicVolumeBar.fillAmount = fillAmount;
    }

    public void KillAllAnimations()
    {
        _animator.KillAll();
    }
}
