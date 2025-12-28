using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event Action OnConfirmPress;
    public event Action OnSelectRightPress;

    private InputActions _inputActions;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _inputActions = new InputActions();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _inputActions.Player.Enable();
        _inputActions.Player.Confirm.performed += Confirm_performed;
        _inputActions.Player.SelectRight.performed += SelectRight_performed;
    }

    private void SelectRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSelectRightPress?.Invoke();
    }

    private void Confirm_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnConfirmPress?.Invoke();
    }

    private void OnDisable()
    {
        if (_inputActions != null)
        {
            _inputActions.Player.Disable();
        }
    }

    private void OnDestroy()
    {
        if (_inputActions != null)
        {
            _inputActions.Player.Confirm.performed -= Confirm_performed;
            _inputActions.Player.SelectRight.performed -= SelectRight_performed;
        }

        OnConfirmPress = null;
        OnSelectRightPress = null;

        if (Instance == this)
        {
            Instance = null;
        }
    }
}
