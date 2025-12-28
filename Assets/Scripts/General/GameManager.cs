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

        _playerManager.OnEnemyLeftSelect += HandleEnemyLeftSelect;
        _playerManager.OnEnemyRightSelect += HandleEnemyRightSelect;
    }

    private void HandleEnemyLeftSelect()
    {
        EnemyController selectEnemy = _enemyBoard.GetMostRighternEnemy();
        _playerManager.SetSelectTarget(selectEnemy);
        Debug.Log("Left Enemy selected!");
    }

    private void HandleEnemyRightSelect()
    {
        EnemyController selectEnemy = _enemyBoard.GetMostLefternEnemy();
        _playerManager.SetSelectTarget(selectEnemy);
        Debug.Log("Right Enemy Selected!");
    }

    private void AddEnemy(EnemyController enemy)
    {
        EnemyController newEnemy = Instantiate(enemy);
        _enemyBoard.AddEnemyToField(newEnemy);
    }

    private void OnDestroy()
    {
        _playerManager.OnEnemyLeftSelect -= HandleEnemyLeftSelect;
    }
}
