using System;
using UnityEngine;

/// <summary>
/// Handles the New Unity Input System.
/// Inputs are handled as events.
/// </summary>
public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }

    public event Action OnConfirmPress;
    public event Action OnSelectRightPress;
    public event Action OnSelectLeftPress;
    public event Action<int> OnItemSelect;
    public event Action OnCardMenuSelect;
    public event Action<int> OnRewardSelect;
    public event Action OnRewardConfirm;

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
        _inputActions.Player.SwitchToCard.performed += SwitchToCard_performed;
        _inputActions.Player.SelectFists.performed += SelectFists_performed;
        _inputActions.ItemReward.ItemOne.performed += ItemOneReward;
        _inputActions.ItemReward.ItemTwo.performed += ItemTwoReward;
        _inputActions.ItemReward.Confirm.performed += RewardConfirmed;
    }

    private void SelectFists_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnItemSelect?.Invoke(5);
    }

    private void RewardConfirmed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRewardConfirm?.Invoke();
    }

    private void ItemOneReward(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRewardSelect?.Invoke(0);
    }

    private void ItemTwoReward(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnRewardSelect?.Invoke(1);
    }

    private void SwitchToCard_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCardMenuSelect?.Invoke();
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

    public void ChangePlayerActive(bool isActive)
    {
        if (isActive)
        {
            _inputActions.Player.Enable();
        }
        else
        {
            _inputActions.Player.Disable();
        }
    }

    public void ChangeRewardActive(bool isActive)
    {
        if (isActive)
        {
            _inputActions.ItemReward.Enable();
        }
        else
        {
            _inputActions.ItemReward.Disable();
        }
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
            _inputActions.Player.SwitchToCard.performed -= SwitchToCard_performed;
            _inputActions.ItemReward.ItemOne.performed -= ItemOneReward;
            _inputActions.ItemReward.ItemTwo.performed -= ItemTwoReward;
            _inputActions.ItemReward.Confirm.performed -= RewardConfirmed;
            _inputActions.Player.SelectFists.performed -= SelectFists_performed;
        }

        OnConfirmPress = null;
        OnSelectRightPress = null;
        OnSelectLeftPress = null;
        OnItemSelect = null;
        OnCardMenuSelect = null;
        OnRewardSelect = null;
        OnRewardConfirm = null;

        if (Instance == this)
        {
            Instance = null;
        }
    }
}
