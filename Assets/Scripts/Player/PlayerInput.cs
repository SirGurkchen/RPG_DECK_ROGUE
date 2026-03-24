using System;
using UnityEngine;

/// <summary>
/// Handles the Player Input received by the GameInput class.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private HordeLogic _horde;

    public event Action OnConfirm;
    public event Action OnEnemyLeftSelect;
    public event Action OnEnemyRightSelect;
    public event Action<int> OnItemSelect;
    public event Action OnCardSwitch;
    public event Action<int> OnRewardSelect;
    public event Action OnRewardConfirm;
    public event Action OnShopConfirmation;
    public event Action<int> OnShopSelection;
    public event Action OnPlayerCancel;
    public event Action OnPlayerPause;


    private void Start()
    {
        GameInput.Instance.OnConfirmPress += Instance_OnConfirmPress;
        GameInput.Instance.OnSelectLeftPress += Instance_OnSelectLeftPress;
        GameInput.Instance.OnSelectRightPress += Instance_OnSelectRightPress;
        GameInput.Instance.OnItemSelect += Instance_OnItemSelect;
        GameInput.Instance.OnCardMenuSelect += Instance_OnCardMenuSelect;
        GameInput.Instance.OnRewardSelect += RewardSelected;
        GameInput.Instance.OnRewardConfirm += RewardConfirmed;
        GameInput.Instance.OnShopItemSelect += ShopSelect;
        GameInput.Instance.OnShopItemConfirm += ShopConfirm;
        GameInput.Instance.OnCancel += PlayerCancel;
        GameInput.Instance.OnPause += PlayerPause;
    }

    private void PlayerPause()
    {
        OnPlayerPause?.Invoke();
    }

    private void PlayerCancel()
    {
        OnPlayerCancel?.Invoke();
    }

    private void ShopConfirm()
    {
        OnShopConfirmation?.Invoke();
    }

    private void ShopSelect(int itemIndex)
    {
        OnShopSelection?.Invoke(itemIndex);
    }

    private void RewardConfirmed()
    {
        OnRewardConfirm?.Invoke();
    }

    private void RewardSelected(int rewardIndex)
    {
        OnRewardSelect?.Invoke(rewardIndex);
    }

    private void Instance_OnCardMenuSelect()
    {
        OnCardSwitch?.Invoke();
    }

    private void Instance_OnSelectLeftPress()
    {
        if (_horde.IsSpawning)
        {
            return;
        }

        OnEnemyLeftSelect?.Invoke();
    }

    private void Instance_OnSelectRightPress()
    {
        if (_horde.IsSpawning)
        {
            return;
        }

        OnEnemyRightSelect?.Invoke();
    }

    private void Instance_OnConfirmPress()
    {
        if (_horde.IsSpawning)
        {
            return;
        }

        OnConfirm?.Invoke();
    }

    private void Instance_OnItemSelect(int item_numb)
    {
        if (_horde.IsSpawning)
        {
            return;
        }

        switch (item_numb)
        {
            case 1:
                OnItemSelect?.Invoke(0);
                break;
            case 2:
                OnItemSelect?.Invoke(1);
                break;
            case 3:
                OnItemSelect?.Invoke(2);
                break;
            case 4:
                OnItemSelect?.Invoke(3);
                break;
            case 5:
                OnItemSelect?.Invoke(4);
                break;
        }
    }

    private void OnDisable()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnConfirmPress -= Instance_OnConfirmPress;
            GameInput.Instance.OnSelectRightPress -= Instance_OnSelectRightPress;
            GameInput.Instance.OnSelectLeftPress -= Instance_OnSelectLeftPress;
            GameInput.Instance.OnItemSelect -= Instance_OnItemSelect;
            GameInput.Instance.OnCardMenuSelect -= Instance_OnCardMenuSelect;
            GameInput.Instance.OnRewardSelect -= RewardSelected;
            GameInput.Instance.OnRewardConfirm -= RewardConfirmed;
            GameInput.Instance.OnShopItemConfirm -= ShopConfirm;
            GameInput.Instance.OnShopItemSelect -= ShopSelect;
            GameInput.Instance.OnCancel -= PlayerCancel;
            GameInput.Instance.OnPause -= PlayerPause;
        }
    }

    private void OnDestroy()
    {
        OnConfirm = null;
        OnEnemyLeftSelect = null;
        OnEnemyRightSelect = null;
        OnItemSelect = null;
        OnCardSwitch = null;
        OnRewardSelect = null;
        OnRewardConfirm = null;
        OnShopConfirmation = null;
        OnShopSelection = null;
        OnPlayerCancel = null;
        OnPlayerPause = null;
    }
}
