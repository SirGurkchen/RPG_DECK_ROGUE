using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : ItemBase, IDurable
{
    [Header("Weapon Stats")]
    [SerializeField] private int _damage;
    [SerializeField] private AttackType _attackType;
    [SerializeField] private int _endurance;

    public int MaxDurability => _endurance;

    public override bool Use(PlayerStats player, EnemyController target = null)
    {
        if (target == null)
        {
            Debug.Log("Cannot attack without target!");
            return false;
        }
        else
        {
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
    None
}
