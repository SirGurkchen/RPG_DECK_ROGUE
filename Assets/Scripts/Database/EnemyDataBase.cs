using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBase : MonoBehaviour
{
    public static EnemyDataBase Instance { get; private set; }

    [SerializeField] private List<EnemyController> _enemyDatabase;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are multiple Enemy Databases!");
            return;
        }
        Instance = this;
    }

    public EnemyController GetRandomEnemy()
    {
        return _enemyDatabase[Random.Range(0, _enemyDatabase.Count)];
    }

    public EnemyController GetEnemyByName(string enemyName)
    {
        EnemyController enemy = _enemyDatabase.Find(enemy => enemy.GetEnemyName() == enemyName);
        if (!enemy) Debug.Log("No enemy found by given name!");
        return enemy;
    }
}
