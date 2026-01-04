using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private HordeLogic _horde;

    public event Action OnConfirm;
    public event Action OnEnemyLeftSelect;
    public event Action OnEnemyRightSelect;
    public event Action<int> OnItemSelect;
    public event Action OnCardSwitch;
    public event Action<int> OnRewardSelect;

    private void Start()
    {
        GameInput.Instance.OnConfirmPress += Instance_OnConfirmPress;
        GameInput.Instance.OnSelectLeftPress += Instance_OnSelectLeftPress;
        GameInput.Instance.OnSelectRightPress += Instance_OnSelectRightPress;
        GameInput.Instance.OnItemSelect += Instance_OnItemSelect;
        GameInput.Instance.OnCardMenuSelect += Instance_OnCardMenuSelect;
        GameInput.Instance.OnRewardSelect += RewardSelected;
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
        if (_horde.isSpawning)
        {
            return;
        }

        OnEnemyLeftSelect?.Invoke();
    }

    private void Instance_OnSelectRightPress()
    {
        if (_horde.isSpawning)
        {
            return;
        }

        OnEnemyRightSelect?.Invoke();
    }

    private void Instance_OnConfirmPress()
    {
        if (_horde.isSpawning)
        {
            return;
        }

        OnConfirm?.Invoke();
    }

    private void Instance_OnItemSelect(int item_numb)
    {
        if (_horde.isSpawning)
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
        }
    }

    public void EnablePlayerControls()
    {
        GameInput.Instance.ChangeRewardActive(false);
        GameInput.Instance.ChangePlayerActive(true);
    }

    public void EnableRewardControls()
    {
        GameInput.Instance.ChangePlayerActive(false);
        GameInput.Instance.ChangeRewardActive(true);
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
    }
}
