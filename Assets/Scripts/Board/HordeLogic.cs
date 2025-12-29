using System.Collections;
using UnityEngine;

public class HordeLogic : MonoBehaviour
{
    [SerializeField] private EnemyController[] _enemyPool;
    [SerializeField] private EnemyBoard _board;

    private const float SPAWN_TIMER = 0.5f;
    private const int MIN_ENEMIES = 1;
    private const int MAX_ENEMIES = 2;

    private void Start()
    {
        _board.OnBoardClear += RefillBoard;
        RefillBoard();
    }

    private void RefillBoard()
    {
        StartCoroutine(SpawnEnemiesWithDelay());
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        yield return new WaitForSeconds(SPAWN_TIMER);

        int amount = Random.Range(MIN_ENEMIES, MAX_ENEMIES + 1);

        for (int i = 0; i < amount; i++)
        {
            AddEnemy();

            if (i < amount - 1)
            {
                yield return new WaitForSeconds(SPAWN_TIMER);
            }
        }
    }

    private void AddEnemy()
    {
        EnemyController newEnemy = Instantiate(_enemyPool[Random.Range(0, _enemyPool.Length)]);
        _board.AddEnemyToField(newEnemy);
    }

    private void OnDestroy()
    {
        if (_board != null)
        {
            _board.OnBoardClear -= RefillBoard;
        }
    }
}
