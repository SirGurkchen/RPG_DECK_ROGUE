using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _item_name;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private ItemType _itemType;

    [Header("Item Properties")]
    [SerializeField] private Rarity _rarity;
    [SerializeField] private int _endurance;


    public abstract void Use(PlayerStats player);
    public virtual string GetItemToString()
    {
        return "Name: " + _item_name;
    }
}

public enum ItemType
{
    Weapon,
    Shield,
    Wand,
    Health,
    Mana,
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
