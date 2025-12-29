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

        Debug.Log(_currentEndurance);
    }

    public void Use(PlayerStats stats, EnemyController target = null)
    {
        _itemData.Use(stats, target);
        _currentEndurance--;
        Debug.Log(_currentEndurance);
        CheckDestroy();
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
