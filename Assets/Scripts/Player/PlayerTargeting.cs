using System;
using UnityEngine;

/// <summary>
/// Handles the enemy targeting system as part of the combat.
/// </summary>
public class PlayerTargeting : MonoBehaviour
{
    private EnemyController _currentTarget;

    public EnemyController GetCurrentTarget() => _currentTarget;

    public event Action<EnemyController, Input> OnEnemyTargeted;
    private Input _currentSelectPos;

    public void SetSelectTarget(EnemyController enemy, Input input)
    {
        if (enemy == null) return;

        if (_currentTarget != null)
        {
            _currentTarget.SetEnemeyMarker(false);
        }

        if (_currentTarget != enemy)
        {
            _currentTarget = enemy;
            _currentSelectPos = input;
            enemy.SetEnemeyMarker(true);
            OnEnemyTargeted?.Invoke(enemy, _currentSelectPos);
        }
        else
        {
            _currentTarget = null;
            enemy.SetEnemeyMarker(false);
            OnEnemyTargeted?.Invoke(null, input);
        }
    }

    public void DeselectAll()
    {
        if (_currentTarget != null)
        {
            _currentTarget.SetEnemeyMarker(false);
        }
        _currentTarget = null;
    }
}
