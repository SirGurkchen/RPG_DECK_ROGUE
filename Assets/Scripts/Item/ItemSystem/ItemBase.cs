using UnityEngine;

/// <summary>
/// Contains base configuration data for new items.
/// </summary>
public abstract class ItemBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _itemName;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private bool _isUnrewardable;
    [SerializeField] private Sprite _itemIcon;

    [Header("Item Properties")]
    [SerializeField] private Rarity _rarity;
    [SerializeField] CardController _unlockableCard;

    public string ItemName => _itemName;
    public string Description => _description;
    public CardController UnlockCard => _unlockableCard;
    public bool IsNotReward => _isUnrewardable;
    public Sprite Icon => _itemIcon;

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
