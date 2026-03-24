using UnityEngine;

/// <summary>
/// Handles the visibilty of all UI Input prompts.
/// </summary>
public class InputUI : MonoBehaviour
{
    [SerializeField] private GameObject _leavePrompt;
    [SerializeField] private GameObject _selectionPrompts;
    [SerializeField] private GameObject _leftOnPrompt;
    [SerializeField] private GameObject _rightOnPrompt;
    [SerializeField] private GameObject _cardOnPrompt;
    [SerializeField] private GameObject _cardMenuPrompt;

    /// <summary>
    /// Toggles the leave prompt.
    /// </summary>
    public void ToggleLeavePrompt()
    {
        _leavePrompt.SetActive(!_leavePrompt.activeSelf);
    }

    /// <summary>
    /// Sets activity of selection prompts.
    /// </summary>
    /// <param name="isOn">Activity of selection prompts.</param>
    public void ToggleSelectionPrompts(bool isOn)
    {
        _selectionPrompts.gameObject.SetActive(isOn);
    }

    /// <summary>
    /// Toggle On/Off images of selection prompts.
    /// </summary>
    /// <param name="type">Input type.</param>
    public void ToggleInputPrompt(Input type)
    {
        switch (type)
        {
            case Input.Q:
                _rightOnPrompt.SetActive(false);
                _leftOnPrompt.SetActive(!_leftOnPrompt.activeSelf);
                break;
            case Input.E:
                _leftOnPrompt.SetActive(false);
                _rightOnPrompt.SetActive(!_rightOnPrompt.activeSelf);
                break;
            case Input.Control:
                _cardOnPrompt.SetActive(!_cardOnPrompt.activeSelf);
                break;

        }
    }

    /// <summary>
    /// Resets all On input prompts.
    /// </summary>
    public void ResetInputPrompt()
    {
        _rightOnPrompt.SetActive(false);
        _leftOnPrompt.SetActive(false);
    }

    /// <summary>
    /// Sets activity of card menu prompt.
    /// </summary>
    /// <param name="isOn">Activity of card menu prompt.</param>
    public void ToggleCardMenuPrompt(bool isOn)
    {
        _cardMenuPrompt.SetActive(isOn);
    }
}

public enum Input
{
    Q,
    E,
    Control
}