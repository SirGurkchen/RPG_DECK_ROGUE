using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private ItemBase _test_item;

    void Start()
    {
        _playerManager.GiveItemToPlayer(_test_item);
    }
}
