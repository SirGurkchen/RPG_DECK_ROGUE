using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event Action OnConfirmPress;
    public event Action OnSelectRightPress;
    public event Action OnSelectLeftPress;
    public event Action<int> OnItemSelect;

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
        _inputActions.Player.SelectLeft.performed += SelectLeft_performed;
        _inputActions.Player.SelectItemOne.performed += SelectItemOne_performed;
        _inputActions.Player.SelectItemTwo.performed += SelectItemTwo_performed;
        _inputActions.Player.SelectItemThree.performed += SelectItemThree_performed;
        _inputActions.Player.SelectItemFour.performed += SelectItemFour_performed;
    }

    private void SelectRight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSelectRightPress?.Invoke();
    }

    private void Confirm_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnConfirmPress?.Invoke();
    }

    private void SelectLeft_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSelectLeftPress?.Invoke();
    }

    private void SelectItemOne_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnItemSelect?.Invoke(1);
    }

    private void SelectItemTwo_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnItemSelect?.Invoke(2);
    }

    private void SelectItemThree_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnItemSelect?.Invoke(3);
    }

    private void SelectItemFour_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnItemSelect?.Invoke(4);
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
            _inputActions.Player.SelectLeft.performed -= SelectLeft_performed;
            _inputActions.Player.SelectItemOne.performed -= SelectItemOne_performed;
            _inputActions.Player.SelectItemTwo.performed -= SelectItemTwo_performed;
            _inputActions.Player.SelectItemThree.performed -= SelectItemThree_performed;
            _inputActions.Player.SelectItemFour.performed -= SelectItemFour_performed;
        }

        OnConfirmPress = null;
        OnSelectRightPress = null;
        OnSelectLeftPress = null;
        OnItemSelect = null;

        if (Instance == this)
        {
            Instance = null;
        }
    }
}
