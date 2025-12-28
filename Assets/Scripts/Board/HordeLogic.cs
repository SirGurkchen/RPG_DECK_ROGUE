using System.Collections;
using UnityEngine;

public class HordeLogic : MonoBehaviour
{
    [SerializeField] private EnemyController[] _enemyPool;
    [SerializeField] private EnemyBoard _board;

    private float _spawnTimer = 0.5f;

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
        yield return new WaitForSeconds(_spawnTimer);

        int amount = Random.Range(1, 3);

        for (int i = 0; i < amount; i++)
        {
            AddEnemy();

            if (i < amount - 1)
            {
                yield return new WaitForSeconds(_spawnTimer);
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
