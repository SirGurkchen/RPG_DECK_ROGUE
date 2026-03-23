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

    public EnemyController GetRandomEnemy(int currentRound)
    {
        List<EnemyController> validEnemies = new List<EnemyController>();

        foreach (EnemyController enemy in _enemyDatabase)
        {
            if (enemy.AppearRound <= currentRound) // Check if enemy round conditions are met, e.g. the current round is between Appear and Disappear Round
            {
                if (enemy.DisappearRound >= currentRound || enemy.DisappearRound == 0)
                {
                    validEnemies.Add(enemy);
                }
            }
        }

        return validEnemies[Random.Range(0, validEnemies.Count)];
    }

    public EnemyController GetEnemyByName(string enemyName)
    {
        EnemyController enemy = _enemyDatabase.Find(enemy => enemy.GetEnemyName() == enemyName);
        if (!enemy) Debug.Log("No enemy found by given name!");
        return enemy;
    }
}
