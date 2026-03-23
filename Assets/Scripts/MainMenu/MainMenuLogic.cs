using UnityEngine;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private MainMenuUI _menuUI;

    private void Start()
    {
        _menuUI.PlayStartAnimation();
        AudioManager.Instance.PlayChainSound();
        AudioManager.Instance.PlayDefaultMusic();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonPress();
        Application.Quit();
    }

    public void PlayGame()
    {
        _menuUI.KillAllAnimations();
        AudioManager.Instance.PlayButtonPress();
        LoadingScreenManager.Instance.PlayLoadAnimation("MainScene");
    }

    public void ChangeToSettings()
    {
        AudioManager.Instance.PlayButtonPress();
        _menuUI.ToggleSettingsMenu(true);
        AudioManager.Instance.PlayChainSound();
    }

    public void ChangeToMain()
    {
        AudioManager.Instance.PlayButtonPress();
        _menuUI.ToggleSettingsMenu(false);
        AudioManager.Instance.PlayChainSound();
    }

    public void IncreaseEffectVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeEffectVolumePlus();
        _menuUI.ChangeEffectBarFill(AudioManager.Instance.EffectVolume);
    }

    public void DecreaseEffectVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeEffectVolumeMinus();
        _menuUI.ChangeEffectBarFill(AudioManager.Instance.EffectVolume);
    }

    public void IncreaseMusicVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeMusicVolumePlus();
        _menuUI.ChangeMusicBarFill(AudioManager.Instance.MusicVolume);
    }

    public void DecreaseMusicVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeMusicVolumeMinus();
        _menuUI.ChangeMusicBarFill(AudioManager.Instance.MusicVolume);
    }
}
