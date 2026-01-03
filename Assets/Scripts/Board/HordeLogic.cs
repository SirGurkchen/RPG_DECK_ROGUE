using System.Collections;
using UnityEngine;

public class HordeLogic : MonoBehaviour
{
    [SerializeField] private EnemyController[] _enemyPool;
    [SerializeField] private EnemyBoard _board;

    private const float SPAWN_TIMER = 0.5f;
    private const int MIN_ENEMIES = 1;
    private const int MAX_ENEMIES = 2;

    public bool isSpawning {  get; private set; }

    private void Start()
    {
        RefillBoard();
    }

    public void RefillBoard()
    {
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        isSpawning = true;

        yield return new WaitForSeconds(SPAWN_TIMER);

        int amount = Random.Range(MIN_ENEMIES, MAX_ENEMIES + 1);

        for (int i = 0; i < amount; i++)
        {
            AddEnemy();
            yield return new WaitForSeconds(SPAWN_TIMER);
        }
        isSpawning = false;
    }

    private void AddEnemy()
    {
        EnemyController newEnemy = Instantiate(_enemyPool[Random.Range(0, _enemyPool.Length)]);
        _board.AddEnemyToField(newEnemy);
    }
}
