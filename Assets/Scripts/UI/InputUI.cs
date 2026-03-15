using UnityEngine;

public class InputUI : MonoBehaviour
{
    [SerializeField] private GameObject _leavePrompt;
    [SerializeField] private GameObject _selectionPrompts;
    [SerializeField] private GameObject _leftOnPrompt;
    [SerializeField] private GameObject _rightOnPrompt;
    [SerializeField] private GameObject _cardOnPrompt;
    [SerializeField] private GameObject _cardMenuPrompt;

    public void ToggleLeavePrompt()
    {
        _leavePrompt.SetActive(!_leavePrompt.activeSelf);
    }

    public void ToggleSelectionPrompts(bool isOn)
    {
        _selectionPrompts.gameObject.SetActive(isOn);
    }

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

    public void ResetInputPrompt()
    {
        _rightOnPrompt.SetActive(false);
        _leftOnPrompt.SetActive(false);
    }

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