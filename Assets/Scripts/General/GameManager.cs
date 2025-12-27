using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ItemBase _testItem;
    [SerializeField] private EnemyController _testEnemy;

    void Start()
    {
        _playerManager.GiveItemToPlayer(_testItem);
        _playerManager.EquipItem(0);
        Debug.Log(_testEnemy.GetEnemyStats());
    }
}
