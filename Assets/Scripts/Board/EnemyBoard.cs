using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Manages the visual representation of the enemy wave.
/// Visualization includes adding, removing and moving enemies on the field.
/// The DOTween Library is used for achieving the visualization goals.
/// </summary>
public class EnemyBoard : MonoBehaviour
{
    [SerializeField] private List<EnemyController> _enemiesOnField;
    [SerializeField] private Vector3 _startPos = new Vector3(0, 1.5f, 0);
    [SerializeField] private Transform[] _enemyPositions;

    private const int MAX_ENEMIES = 2;

    public event Action OnBoardClear;

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
        CheckBoardClear();
    }

    public EnemyController GetMostLefternEnemy()
    {
        if (_enemiesOnField.Count == 0)
        {
            Debug.Log("No Enemies On Field!");
            return null;
        }
        return _enemiesOnField[0];
    }

    public EnemyController GetMostRighternEnemy()
    {
        if (_enemiesOnField.Count == 0)
        {
            Debug.Log("No Enemies On Field!");
            return null;
        }
        return _enemiesOnField[_enemiesOnField.Count - 1];
    }

    private void SetUpEnemyVisuals()
    {
        for (int count = 0; count < _enemiesOnField.Count; count++)
        {
            Vector3 pos = _startPos + _enemyPositions[count].position;
            _enemiesOnField[count].transform.DOMove(pos, 0.3f);
        }
    }

    private void CheckBoardClear()
    {
        if (_enemiesOnField.Count == 0)
        {
            OnBoardClear?.Invoke();
        }
        else
        {
            SetUpEnemyVisuals();
        }
    }

    public List<EnemyController> GetEnemies()
    {
        return _enemiesOnField;
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

        OnBoardClear = null;
    }
}
