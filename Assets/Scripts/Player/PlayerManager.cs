using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private PlayerTargeting _targeting;
    [SerializeField] private EnemyBoard _board;
    [SerializeField] private PlayerStats _stats;

    public event Action OnPlayerTurnEnded;

    private void Start()
    {
        var input = GetComponent<PlayerInput>();

        if (input != null )
        {
            input.OnConfirm += HandleConfirm;
            input.OnEnemyLeftSelect += Input_OnEnemyLeftSelect;
            input.OnEnemyRightSelect += Input_OnEnemyRightSelect;
            _stats.OnPlayerDeath += PlayerDead;
        }
    }

    private void PlayerDead()
    {
        Debug.Log("Dead!");
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
        _combat.Use(_stats, _targeting.GetCurrentTarget());
        _targeting.DeselectAll();
        OnPlayerTurnEnded?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _combat.TakeDamage(_stats, damage);
    }

    private void OnDisable()
    {
        var input = GetComponent<PlayerInput>();

        input.OnConfirm -= HandleConfirm;
        input.OnEnemyLeftSelect -= Input_OnEnemyLeftSelect;
        input.OnEnemyRightSelect -= Input_OnEnemyRightSelect;
        _stats.OnPlayerDeath -= PlayerDead;
    }

    private void OnDestroy()
    {
        OnPlayerTurnEnded = null;
    }
}
