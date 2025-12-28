using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private EnemyBoard _enemyBoard;
    [SerializeField] private ItemBase _testItem;
    [SerializeField] private EnemyController _testEnemy;
    [SerializeField] private EnemyController _2testEnemy;


    void Start()
    {
        _playerManager.GiveItemToPlayer(_testItem);
        _playerManager.EquipItem(0);
        AddEnemy(_testEnemy);
        AddEnemy(_2testEnemy);

        _playerManager.OnEnemySelect += HandleEnemyRightSelect;
    }

    private void HandleEnemyRightSelect()
    {
        EnemyController selectEnemy = _enemyBoard.GetMostRighternEnemy();
        _playerManager.SetSelectTarget(selectEnemy);
        Debug.Log("Enemy selected");
    }

    private void AddEnemy(EnemyController enemy)
    {
        EnemyController newEnemy = Instantiate(enemy);
        _enemyBoard.AddEnemyToField(newEnemy);
    }

    private void OnDestroy()
    {
        _playerManager.OnEnemySelect -= HandleEnemyRightSelect;
    }
}
