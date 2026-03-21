using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a List of all obtainable items in the game.
/// Used to get info for which item to create.
/// </summary>
public class ItemsDataBase : MonoBehaviour
{
    public static ItemsDataBase Instance { get; private set; }


    [SerializeField] private List<ItemController> _itemDatabase;
    private List<ItemController> _validItems = new List<ItemController>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There are multiple Item Databases!");
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public ItemController GetRandomItem()
    {
        foreach (ItemController item in _itemDatabase)
        {
            if (!item.GetItemBase().IsNotReward)
            {
                _validItems.Add(item);
            }
        }

        ItemController reward = _validItems[Random.Range(0, _validItems.Count)];
        _validItems.Clear();
        return reward;
    }

    public ItemController GetItemByName(string itemName)
    {
        return _itemDatabase.Find(item => item.GetItemName() == itemName);
    }
}
