using UnityEngine;

/// <summary>
/// Contains configuration data for enemies.
/// New enemy types use this SO to have necessary base data.
/// </summary>
public abstract class EnemyBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _enemyName;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private int _coins;
    [SerializeField] private Sprite _enemySprite;

    [Header("Combat Info")]
    [SerializeField] private EnemyType _type;
    [SerializeField] private FightPosition _position;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private AttackType _weakness;
    [Tooltip("Round from which this enemy will spawn onward. Keep at 0 or 1 to have available from beginning")]
    [SerializeField] private int _roundAppear;
    [Tooltip("Round from which this enemy will not spawn anymore onward. Keep at 0 to have always available")]
    [SerializeField] private int _roundDisappear;

    public int Damage => _damage;
    public int Health => _health;
    public string Name => _enemyName;
    public AttackType Weakness => _weakness;
    public int Coins => _coins;
    public Sprite Icon => _enemySprite;
    public EnemyType Type => _type;
    public int RoundAppear => _roundAppear;
    public int RoundDisappear => _roundDisappear;

    /// <summary>
    /// Returns important enemy data of an enemy as a string.
    /// </summary>
    /// <returns>String of enemy data.</returns>
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
    Monster,
    Animal
}

public enum FightPosition
{
    Standing,
    Flying,
    Swimming,
    Stealthing
}
