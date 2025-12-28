using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerTargeting _targeting;
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private EnemyBoard _board;

    private void Start()
    {
        var input = GetComponent<PlayerInput>();

        if (input != null )
        {
            input.OnConfirm += HandleConfirm;
            input.OnEnemyLeftSelect += Input_OnEnemyLeftSelect;
            input.OnEnemyRightSelect += Input_OnEnemyRightSelect;
        }
    }

    private void Input_OnEnemyRightSelect()
    {
        _targeting.SetSelectTarget(_board.GetMostRighternEnemy());
    }

    private void Input_OnEnemyLeftSelect()
    {
        _targeting.SetSelectTarget(_board.GetMostLefternEnemy());
    }

    private void HandleConfirm()
    {
        _combat.Use(_targeting.GetCurrentTarget());
        _targeting.DeselectAll();
    }
}
