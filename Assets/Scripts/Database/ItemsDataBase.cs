using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains a List of all obtainable items in the game.
/// Used to get info for which item to create.
/// </summary>
public class ItemsDataBase : MonoBehaviour
{
    [SerializeField] private List<ItemController> _itemDatabase;
    private List<ItemController> _validItems = new List<ItemController>();


    public ItemController GetRandomItem()
    {
        foreach (ItemController item in _itemDatabase)
        {
            if (!item.GetItemBase().IsNotReward)
            {
                _validItems.Add(item);
            }
        }

        int rng = Random.Range(0, _validItems.Count);
        ItemController reward = _validItems[rng];
        _validItems.Clear();
        return reward;
    }

    public ItemController GetItemByName(string itemName)
    {
        return _itemDatabase.Find(item => item.name == itemName);
    }
}
