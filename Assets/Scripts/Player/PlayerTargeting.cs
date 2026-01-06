using System;
using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    private EnemyController _currentTarget;

    public event Action<EnemyController> OnEnemyTargeted;

    public void SetSelectTarget(EnemyController enemy)
    {
        if (_currentTarget != null)
        {
            _currentTarget.SetEnemeyMarker(false);
        }

        if (_currentTarget !=  enemy)
        {
            _currentTarget = enemy;
            enemy.SetEnemeyMarker(true);
            OnEnemyTargeted?.Invoke(enemy);
        }
        else
        {
            _currentTarget = null;
            enemy.SetEnemeyMarker(false);
            OnEnemyTargeted?.Invoke(null);
        }
    }

    public void DeselectAll()
    {
        if (_currentTarget != null )
        {
            _currentTarget.SetEnemeyMarker(false);
        }
        _currentTarget = null;
    }

    public EnemyController GetCurrentTarget()
    {
        return _currentTarget;
    }
}
