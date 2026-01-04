using System.Collections.Generic;
using UnityEngine;

public class ItemsDataBase : MonoBehaviour
{
    [SerializeField] private List<ItemController> _itemDatabase;


    public ItemController GetRandomItem()
    {
        int rng = Random.Range(0, _itemDatabase.Count);
        Debug.Log("Given Item: " + _itemDatabase[rng]);
        return _itemDatabase[rng];
    }

    public ItemController GetItemByName(string itemName)
    {
        return _itemDatabase.Find(item => item.name == itemName);
    }
}
