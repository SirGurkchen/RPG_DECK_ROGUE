using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : ItemBase
{
    [Header("Weapon Stats")]
    [SerializeField] private int _damage;
    [SerializeField] private AttackType _attackType;

    public override void Use(PlayerStats player, EnemyBase target = null)
    {
        if (target == null)
        {
            Debug.Log("Cannot attack without target!");
        }
        target.TakeDamage(_damage);
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
    Magic
}
