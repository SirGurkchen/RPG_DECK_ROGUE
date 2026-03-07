using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private AudioClip _buttonSound;
    [SerializeField] private MainMenuUI _menuUI;

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
        SceneManager.LoadScene("MainScene");
    }

    public void ChangeToSettings()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        _menuUI.ToggleSettingsMenu(true);
    }

    public void ChangeToMain()
    {
        AudioManager.Instance.PlayAudioClip(_buttonSound);
        _menuUI.ToggleSettingsMenu(false);
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
}
