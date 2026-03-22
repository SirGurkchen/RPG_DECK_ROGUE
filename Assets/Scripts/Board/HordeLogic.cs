using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the logical side of spawning new enemies onto the board.
/// Works with EnemyBoard to also simultaneously show visuals.
/// </summary>
public class HordeLogic : MonoBehaviour
{
    [SerializeField] private EnemyBoard _board;

    private const float SPAWN_TIMER = 0.5f;
    private const int MIN_ENEMIES = 1;
    private const int MAX_ENEMIES = 2;

    public bool isSpawning {  get; private set; }

    public void RefillBoardRandomly(int round)
    {
        StartCoroutine(SpawnRandomEnemiesWithDelay(round));
    }

    public void RefillBoardPredetermined(string enemyOne, string enemyTwo)
    {
        StartCoroutine(SpawnPredeterminedEnemiesWithDelay(enemyOne, enemyTwo));
    }

    public void RefillBoardWithBoss()
    {
        StartCoroutine(SpawnBossEnemy());
    }

    private IEnumerator SpawnPredeterminedEnemiesWithDelay(string enemyOne, string enemyTwo)
    {
        isSpawning = true;
        yield return new WaitForSeconds(SPAWN_TIMER);
        AddPredeterminedEnemy(enemyOne);
        if (enemyTwo != null)
        {
            yield return new WaitForSeconds(SPAWN_TIMER);
            AddPredeterminedEnemy(enemyTwo);
        }
        isSpawning = false;
    }

    private IEnumerator SpawnRandomEnemiesWithDelay(int round)
    {
        isSpawning = true;

        yield return new WaitForSeconds(SPAWN_TIMER);

        int amount = Random.Range(MIN_ENEMIES, MAX_ENEMIES + 1);

        for (int i = 0; i < amount; i++)
        {
            AddRandomEnemy(round);
            yield return new WaitForSeconds(SPAWN_TIMER);
        }
        isSpawning = false;
    }

    private IEnumerator SpawnBossEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(SPAWN_TIMER);
        AddBossEnemy();
        yield return new WaitForSeconds(SPAWN_TIMER);
        isSpawning = false;
    }

    private void AddRandomEnemy(int currentRound)
    {
        EnemyController newEnemy = Instantiate(EnemyDataBase.Instance.GetRandomEnemy(currentRound));
        _board.AddEnemyToField(newEnemy);
    }

    private void AddPredeterminedEnemy(string enemy)
    {
        EnemyController newEnemy = Instantiate(EnemyDataBase.Instance.GetEnemyByName(enemy));
        _board.AddEnemyToField(newEnemy);
    }

    private void AddBossEnemy()
    {
        EnemyController newBoss = Instantiate(EnemyDataBase.Instance.GetEnemyByName("Slime King"));
        _board.AddBossToField(newBoss);
    }
}
