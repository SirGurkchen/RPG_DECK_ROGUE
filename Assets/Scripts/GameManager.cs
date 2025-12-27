using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ItemBase _test_item;
    [SerializeField] private ItemBase _2test_item;
    [SerializeField] private EnemyBase _test_enemy;

    void Start()
    {
        _playerManager.GiveItemToPlayer(_test_item);
        print(_test_enemy.GetEnemyStats());
        _playerManager.AttackWithItem(_test_enemy);
        print(_test_enemy.GetEnemyStats());
    }
}
