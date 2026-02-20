using UnityEngine;

/// <summary>
/// Contains data for items of the Weapon type.
/// Extends ItemBase for necessary base data.
/// Implements IDurable for item type purposes (Durabilty).
/// </summary>
[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : ItemBase, IDurable
{
    [Header("Weapon Stats")]
    [SerializeField] private int _damage;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private int _endurance;
    [Tooltip("Leave at 0 if weapon is not Magic")]
    [SerializeField] private int _manaCost = 0;

    public int MaxDurability => _endurance;
    public int Damage => _damage;
    public AttackType attackType => _attackType;

    public override bool Use(PlayerStats player, EnemyController target = null)
    {
        if (target == null)
        {
            Debug.Log("Cannot attack without target!");
            return false;
        }
        else
        {
            if (_attackType == AttackType.Magic)
            {
                if (player.Mana >= _manaCost)
                {
                    player.RemoveMana(_manaCost);
                }
                else
                {
                    return false;
                }
            }
            target.TakeDamage(_damage, _attackType);
            return true;
        }
    }

    public override string GetItemToString()
    {
        return base.GetItemToString() + " Damage: " + _damage + " Weapon Type: " + _attackType.ToString();
    }
}

public enum AttackType
{
    Melee,
    Range,
    Magic,
    Blade,
    None
}
