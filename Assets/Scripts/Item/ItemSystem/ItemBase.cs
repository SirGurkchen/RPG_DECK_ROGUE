using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _itemName;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private ItemType _itemType;

    [Header("Item Properties")]
    [SerializeField] private Rarity _rarity;

    public string ItemName => _itemName;
    public string Description => _description;

    public abstract bool Use(PlayerStats player, EnemyController target = null);

    public virtual string GetItemToString()
    {
        return "Name: " + _itemName;
    }
    
    public ItemType GetItemType()
    {
        return _itemType;
    }
}

public enum ItemType
{
    Weapon,
    Shield,
    Potion,
    Misc
}

public enum Rarity
{
    Common,
    Uncommon,
    Rare,
    Epic,
    Legendary,
    Mythical
}
