using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private MainMenuUI _menuUI;
    [SerializeField] private AudioClip _changeAnimationSound;

    private void Start()
    {
        _menuUI.PlayStartAnimation();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        Application.Quit();
    }

    public void PlayGame()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        LoadingScreenManager.Instance.PlayLoadAnimation("MainScene");
        AudioManager.Instance.PlayAudioClip(_changeAnimationSound);
    }

    public void ChangeToSettings()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        _menuUI.ToggleSettingsMenu(true, _changeAnimationSound);
        AudioManager.Instance.PlayAudioClip(_changeAnimationSound);
    }

    public void ChangeToMain()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        _menuUI.ToggleSettingsMenu(false, _changeAnimationSound);
        AudioManager.Instance.PlayAudioClip(_changeAnimationSound);
    }

    public void IncreaseEffectVolume()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        SettingsManager.Instance.ChangeEffectVolumePlus();
        _menuUI.ChangeEffectBarFill(AudioManager.Instance.EffectVolume);
    }

    public void DecreaseEffectVolume()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        SettingsManager.Instance.ChangeEffectVolumeMinus();
        _menuUI.ChangeEffectBarFill(AudioManager.Instance.EffectVolume);
    }

    public void IncreaseMusicVolume()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        SettingsManager.Instance.ChangeMusicVolumePlus();
        _menuUI.ChangeMusicBarFill(AudioManager.Instance.MusicVolume);
    }

    public void DecreaseMusicVolume()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        SettingsManager.Instance.ChangeMusicVolumeMinus();
        _menuUI.ChangeMusicBarFill(AudioManager.Instance.MusicVolume);
    }
}
