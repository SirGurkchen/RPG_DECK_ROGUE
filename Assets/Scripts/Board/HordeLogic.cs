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

    public bool IsSpawning { get; private set; }

    /// <summary>
    /// Starts refilling the board randomly
    /// </summary>
    /// <param name="round">The current round number</param>
    public void RefillBoardRandomly(int round)
    {
        StartCoroutine(SpawnRandomEnemiesWithDelay(round));
    }

    /// <summary>
    /// Starts refilling the board with predetermined enemies
    /// </summary>
    /// <param name="enemyOne">The name of the first enemy</param>
    /// <param name="enemyTwo">The name of the second enemy. Can be left empty.</param>
    public void RefillBoardPredetermined(string enemyOne, string enemyTwo = null)
    {
        StartCoroutine(SpawnPredeterminedEnemiesWithDelay(enemyOne, enemyTwo));
    }

    /// <summary>
    /// Starts refilling the board with the boss enemy.
    /// </summary>
    public void RefillBoardWithBoss()
    {
        StartCoroutine(SpawnBossEnemy());
    }

    private IEnumerator SpawnPredeterminedEnemiesWithDelay(string enemyOne, string enemyTwo)
    {
        IsSpawning = true;
        yield return new WaitForSeconds(SPAWN_TIMER);
        AddPredeterminedEnemy(enemyOne);
        if (enemyTwo != null)
        {
            yield return new WaitForSeconds(SPAWN_TIMER);
            AddPredeterminedEnemy(enemyTwo);
        }
        IsSpawning = false;
    }

    private IEnumerator SpawnRandomEnemiesWithDelay(int round)
    {
        IsSpawning = true;

        yield return new WaitForSeconds(SPAWN_TIMER);

        int amount = Random.Range(MIN_ENEMIES, MAX_ENEMIES + 1);

        for (int i = 0; i < amount; i++)
        {
            AddRandomEnemy(round);
            yield return new WaitForSeconds(SPAWN_TIMER);
        }
        IsSpawning = false;
    }

    private IEnumerator SpawnBossEnemy()
    {
        IsSpawning = true;
        yield return new WaitForSeconds(SPAWN_TIMER);
        AddBossEnemy();
        yield return new WaitForSeconds(SPAWN_TIMER);
        IsSpawning = false;
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
