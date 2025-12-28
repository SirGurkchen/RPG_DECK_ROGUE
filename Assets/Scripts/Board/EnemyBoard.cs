using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBoard : MonoBehaviour
{
    [SerializeField] private List<EnemyController> _enemiesOnField;
    [SerializeField] private float _horizontalSpacing = 2f;
    [SerializeField] private Vector3 _startPos = Vector3.zero;

    private const int MAX_ENEMIES = 2;

    private void Awake()
    {
        _enemiesOnField = new List<EnemyController>();
    }

    public void AddEnemyToField(EnemyController enemy)
    {
        if (_enemiesOnField.Count < MAX_ENEMIES)
        {
            _enemiesOnField.Add(enemy);
            SetUpEnemyVisuals();
            enemy.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(EnemyController enemy)
    {
        _enemiesOnField.Remove(enemy);
        enemy.OnEnemyDeath -= HandleEnemyDeath;
        Destroy(enemy.gameObject);
        SetUpEnemyVisuals();
    }

    public EnemyController GetMostRighternEnemy()
    {
        if (_enemiesOnField.Count == 0)
        {
            Debug.Log("No Enemies On Field!");
            return null;
        }
        return _enemiesOnField[0];
    }

    public EnemyController GetMostLefternEnemy()
    {
        if (_enemiesOnField.Count == 0)
        {
            Debug.Log("No Enemies ON Field!");
            return null;
        }
        return _enemiesOnField[_enemiesOnField.Count - 1];
    }

    private void SetUpEnemyVisuals()
    {
        for (int count = 0; count < _enemiesOnField.Count; count++)
        {
            Vector3 pos = _startPos + new Vector3(_horizontalSpacing * count, 0, 0);
            _enemiesOnField[count].transform.position = pos;
        }
    }

    private void OnDestroy()
    {
        foreach (EnemyController enemy in _enemiesOnField)
        {
            if (enemy != null)
            {
                enemy.OnEnemyDeath -= HandleEnemyDeath;
            }
        }
    }
}
