using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _warningScreen;

    private Action _confirmAction;

    public void QuitGame()
    {
        AudioManager.Instance.PlayButtonPress();
        ShowWarning(WarningType.Quit);
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlayButtonPress();
        ShowWarning(WarningType.MainMenu);
    }

    public void ConfirmDecision()
    {
        AudioManager.Instance.PlayButtonPress();
        _confirmAction?.Invoke();
        _confirmAction = null;
    }

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
