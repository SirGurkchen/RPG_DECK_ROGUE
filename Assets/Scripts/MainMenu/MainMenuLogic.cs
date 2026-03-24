using UnityEngine;

/// <summary>
/// Handles the logic of the Main Menu.
/// This includes the buttons of the main menu.
/// </summary>
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

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonPress();
        Application.Quit();
    }

    /// <summary>
    /// Starts a game round.
    /// </summary>
    public void PlayGame()
    {
        _menuUI.KillAllAnimations();
        AudioManager.Instance.PlayButtonPress();
        LoadingScreenManager.Instance.PlayLoadAnimation("MainScene");
    }

    /// <summary>
    /// Changes to settings screen.
    /// </summary>
    public void ChangeToSettings()
    {
        AudioManager.Instance.PlayButtonPress();
        _menuUI.ToggleSettingsMenu(true);
        AudioManager.Instance.PlayChainSound();
    }

    /// <summary>
    /// Changes to main menu screen.
    /// </summary>
    public void ChangeToMain()
    {
        AudioManager.Instance.PlayButtonPress();
        _menuUI.ToggleSettingsMenu(false);
        AudioManager.Instance.PlayChainSound();
    }

    /// <summary>
    /// Increases the effect volume.
    /// </summary>
    public void IncreaseEffectVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeEffectVolumePlus();
        _menuUI.ChangeEffectBarFill(AudioManager.Instance.EffectVolume);
    }

    /// <summary>
    /// Decreases the effect volume.
    /// </summary>
    public void DecreaseEffectVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeEffectVolumeMinus();
        _menuUI.ChangeEffectBarFill(AudioManager.Instance.EffectVolume);
    }

    /// <summary>
    /// Increases the music volume.
    /// </summary>
    public void IncreaseMusicVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeMusicVolumePlus();
        _menuUI.ChangeMusicBarFill(AudioManager.Instance.MusicVolume);
    }

    /// <summary>
    /// Decreases the music volume.
    /// </summary>
    public void DecreaseMusicVolume()
    {
        AudioManager.Instance.PlayButtonPress();
        SettingsManager.Instance.ChangeMusicVolumeMinus();
        _menuUI.ChangeMusicBarFill(AudioManager.Instance.MusicVolume);
    }
}
