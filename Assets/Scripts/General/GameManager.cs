using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private EnemyBoard _enemyBoard;

    private void Start()
    {
        _player.OnPlayerTurnEnded += PlayerTurnEnd;
    }

    private void PlayerTurnEnd()
    {
        foreach (EnemyController enemy in _enemyBoard.GetEnemies())
        {
            enemy.Attack(_player);
        }
    }
}
