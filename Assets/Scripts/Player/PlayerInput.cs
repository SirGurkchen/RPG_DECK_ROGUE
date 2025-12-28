using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action OnConfirm;
    public event Action OnEnemyLeftSelect;
    public event Action OnEnemyRightSelect;

    private void Start()
    {
        GameInput.Instance.OnConfirmPress += Instance_OnConfirmPress;
        GameInput.Instance.OnSelectLeftPress += Instance_OnSelectLeftPress;
        GameInput.Instance.OnSelectRightPress += Instance_OnSelectRightPress;
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

    private void OnDisable()
    {
        if (GameInput.Instance != null)
        {
            GameInput.Instance.OnConfirmPress -= Instance_OnConfirmPress;
            GameInput.Instance.OnSelectRightPress -= Instance_OnSelectRightPress;
            GameInput.Instance.OnSelectLeftPress -= Instance_OnSelectLeftPress;
        }
    }

    private void OnDestroy()
    {
        OnConfirm = null;
        OnEnemyLeftSelect = null;
        OnEnemyRightSelect = null;
    }
}
