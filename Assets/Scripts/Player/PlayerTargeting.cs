using UnityEngine;

public class PlayerTargeting : MonoBehaviour
{
    private EnemyController _currentTarget;

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
        }
        else
        {
            _currentTarget = null;
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
