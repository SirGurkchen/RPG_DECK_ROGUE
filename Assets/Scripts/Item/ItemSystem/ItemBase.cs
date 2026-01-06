using UnityEngine;

public abstract class ItemBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _itemName;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private int _price;
    [SerializeField] private ItemType _itemType;
    [SerializeField] private bool _isUnrewardable;

    [Header("Item Properties")]
    [SerializeField] private Rarity _rarity;
    [SerializeField] CardController _unlockableCard;
    [SerializeField] private bool _unlockedCard = false;

    public string ItemName => _itemName;
    public string Description => _description;
    public CardController UnlockCard => _unlockableCard;
    public bool UnlockedCard => _unlockedCard;
    public bool IsNotReward => _isUnrewardable;

    public abstract bool Use(PlayerStats player, EnemyController target = null);

    public virtual string GetItemToString()
    {
        return "Name: " + _itemName;
    }
    
    public ItemType GetItemType()
    {
        return _itemType;
    }

    public void SetUnlocked()
    {
        _unlockedCard = true;
    }

    public void ResetUnlock()
    {
        _unlockedCard = false;
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
