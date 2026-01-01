using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] private ItemBase _itemData;
    private int _currentEndurance = 1;

    private void Awake()
    {
        InitializeItem();
    }

    private void InitializeItem()
    {
        if (_itemData is IDurable item)
        {
            _currentEndurance = item.MaxDurability;
        }
    }

    public bool Use(PlayerStats stats, EnemyController target = null)
    {
        bool suc = _itemData.Use(stats, target);

        if (suc)
        {
            _currentEndurance--;
            Debug.Log(_currentEndurance);
            CheckDestroy();
        }

        return suc;
    }

    public string GetItemName()
    {
        return _itemData.ItemName;
    }

    public ItemBase GetItemBase()
    {
        return _itemData;
    }

    private void CheckDestroy()
    {
        if (_currentEndurance <= 0)
        {
            Debug.Log("Destroy!");
            Destroy(gameObject);
        }
    }
}
