using UnityEngine;

/// <summary>
/// Contains configuration data for enemies.
/// New enemy types use this SO to have necessary base data.
/// </summary>
[CreateAssetMenu(fileName = "New Base Enemy", menuName = "Enemy/Enemy")]
public abstract class EnemyBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _enemyName;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private EnemyType _type;
    [SerializeField] private FightPosition _position;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private AttackType _weakness;
    [SerializeField] private int _coins;

    public int Damage => _damage;
    public int Health => _health;
    public string Name => _enemyName;
    public AttackType Weakness => _weakness;
    public int Coins => _coins;

    public virtual string GetEnemyStats()
    {
        return "Name: " + _enemyName + " Type: " + _type.ToString() + " Position: " + _position.ToString() + " Damage: " + _damage;
    }
}

public enum EnemyType
{
    Human,
    Ghost,
    Orc,
    Beast,
    Monster
}

public enum FightPosition
{
    Standing,
    Flying,
    Swimming,
    Stealthing
}
