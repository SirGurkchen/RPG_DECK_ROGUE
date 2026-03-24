using System;
using UnityEngine;

/// <summary>
/// Handles the Pause menu logic of the game.
/// This includes showing the warning, quitting and return to the main menu via buttons.
/// </summary>
public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _warningScreen;

    private Action _confirmAction;

    /// <summary>
    /// Quits the game.
    /// </summary>
    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonPress();
        ShowWarning(WarningType.Quit);
    }

    /// <summary>
    /// Returns to the main menu.
    /// </summary>
    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlayButtonPress();
        ShowWarning(WarningType.MainMenu);
    }

    /// <summary>
    /// Confirms a decision in the pause menu.
    /// </summary>
    public void ConfirmDecision()
    {
        AudioManager.Instance.PlayButtonPress();
        _confirmAction?.Invoke();
        _confirmAction = null;
    }

    /// <summary>
    /// Cancels a decision in the pause menu.
    /// </summary>
    public void CancelDecision()
    {
        AudioManager.Instance.PlayButtonPress();
        _confirmAction = null;
        GameInput.Instance.TogglePauseInput(true);
        _warningScreen.SetActive(false);
    }

    private void ShowWarning(WarningType type)
    {
        GameInput.Instance.TogglePauseInput(false);
        _warningScreen.SetActive(true);
        switch(type)
        {
            case WarningType.MainMenu:
                _confirmAction = () => LoadingScreenManager.Instance.PlayLoadAnimation("MainMenu");
                break;
            case WarningType.Quit:
                _confirmAction = () => Application.Quit();
                break;
        }
    }

    private enum WarningType
    {
        MainMenu,
        Quit
    }
}
