using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnConfirm;
    public event Action OnEnemyLeftSelect;
    public event Action OnEnemyRightSelect;
    public event Action<int> OnItemSelect;

    private void Start()
    {
        GameInput.Instance.OnConfirmPress += Instance_OnConfirmPress;
        GameInput.Instance.OnSelectLeftPress += Instance_OnSelectLeftPress;
        GameInput.Instance.OnSelectRightPress += Instance_OnSelectRightPress;
        GameInput.Instance.OnItemSelect += Instance_OnItemSelect;
    }

    private void Instance_OnSelectLeftPress()
    {
        OnEnemyLeftSelect?.Invoke();
    }

    private void Instance_OnSelectRightPress()
    {
        OnEnemyRightSelect?.Invoke();
    }

    private void Instance_OnConfirmPress()
    {
        OnConfirm?.Invoke();
    }

    private void Instance_OnItemSelect(int item_numb)
    {
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

    private void OnDisable()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnConfirmPress -= Instance_OnConfirmPress;
            GameInput.Instance.OnSelectRightPress -= Instance_OnSelectRightPress;
            GameInput.Instance.OnSelectLeftPress -= Instance_OnSelectLeftPress;
            GameInput.Instance.OnItemSelect -= Instance_OnItemSelect;
        }
    }

    private void OnDestroy()
    {
        OnConfirm = null;
        OnEnemyLeftSelect = null;
        OnEnemyRightSelect = null;
        OnItemSelect = null;
    }
}
