using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ItemBase _test_item;
    [SerializeField] private EnemyController _test_enemy;

    void Start()
    {
        _playerManager.GiveItemToPlayer(_test_item);
        print(_test_enemy.GetEnemyStats());
        _playerManager.UseCurrentlySelectedItem(_test_enemy);
        print(_test_enemy.GetEnemyStats());
    }
}
