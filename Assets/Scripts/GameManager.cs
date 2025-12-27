using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ItemBase _testItem;
    [SerializeField] private EnemyController _testEnemy;
    [SerializeField] private EnemyController _2testEnemy;

    void Start()
    {
        _playerManager.GiveItemToPlayer(_testItem);
        _playerManager.EquipItem(0);
        Debug.Log(_testEnemy.GetEnemyStats());
        Debug.Log(_2testEnemy.GetEnemyStats());
        _playerManager.UseCurrentlySelectedItem(_testEnemy);
        _playerManager.UseCurrentlySelectedItem(_2testEnemy);
        Debug.Log(_testEnemy.GetEnemyStats());
        Debug.Log(_2testEnemy.GetEnemyStats());
    }
}
