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
    public event Action<int> OnShopItemSelect;
    public event Action OnShopItemConfirm;
    public event Action OnCancel;
    public event Action OnPause;

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
    }

    private void Start()
    {
        _inputActions.Pause.Enable();
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
        _inputActions.ItemReward.Cancel.performed += CancelPerformed;
        _inputActions.ShopInteract.Cancel.performed += CancelPerformed;
        _inputActions.ShopInteract.ItemOne.performed += ShopOne_performed;
        _inputActions.ShopInteract.ItemTwo.performed += ShopTwo_performed;
        _inputActions.ShopInteract.Confirm.performed += ShopConfirm_performed;
        _inputActions.Pause.PauseKey.performed += PauseKey_performed;
    }

    private void PauseKey_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPause?.Invoke();
    }

    public bool IsInputActive()
    {
        return _inputActions.Player.enabled;
    }

    public void TogglePauseInput(bool isOn)
    {
        if (isOn)
        {
            _inputActions.Pause.Enable();
        }
        else
        {
            _inputActions.Pause.Disable();
        }
    }

    private void CancelPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnCancel?.Invoke();
    }

    private void ShopTwo_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShopItemSelect?.Invoke(1);
    }

    private void ShopOne_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShopItemSelect?.Invoke(0);
    }

    private void ShopConfirm_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnShopItemConfirm?.Invoke();
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
            _inputActions.Player.Disable();
            _inputActions.ItemReward.Enable();
        }
        else
        {
            _inputActions.ItemReward.Disable();
            _inputActions.Player.Enable();
        }
    }

    public void ChangeShopActive(bool isActive)
    {
        if (isActive)
        {
            _inputActions.ShopInteract.Enable();
        }
        else
        {
            _inputActions.ShopInteract.Disable();
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
            _inputActions.ShopInteract.ItemOne.performed -= ShopOne_performed;
            _inputActions.ShopInteract.ItemTwo.performed -= ShopTwo_performed;
            _inputActions.ShopInteract.Confirm.performed -= ShopConfirm_performed;
            _inputActions.ItemReward.Cancel.performed -= CancelPerformed;
            _inputActions.ShopInteract.Cancel.performed -= CancelPerformed;
            _inputActions.Pause.PauseKey.performed += PauseKey_performed;
        }

        OnConfirmPress = null;
        OnSelectRightPress = null;
        OnSelectLeftPress = null;
        OnItemSelect = null;
        OnCardMenuSelect = null;
        OnRewardSelect = null;
        OnRewardConfirm = null;
        OnShopItemConfirm = null;
        OnShopItemSelect = null;
        OnCancel = null;
        OnPause = null;


        if (Instance == this)
        {
            Instance = null;
        }
    }
}
