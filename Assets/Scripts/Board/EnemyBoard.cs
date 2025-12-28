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
            SetUpEnemyVisual(enemy);
            enemy.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(EnemyController enemy)
    {
        _enemiesOnField.Remove(enemy);
        enemy.OnEnemyDeath -= HandleEnemyDeath;
        Destroy(enemy.gameObject);
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

    private void SetUpEnemyVisual(EnemyController newEnemy)
    {
        int index = _enemiesOnField.IndexOf(newEnemy);
        Vector3 pos = _startPos + new Vector3(_horizontalSpacing * index, 0, 0);
        newEnemy.transform.position = pos;
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
