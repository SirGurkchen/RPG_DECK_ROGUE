using UnityEngine;

public abstract class EnemyBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _enemy_name;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private EnemyType _type;
    [SerializeField] private FightPosition _position;
    [SerializeField] private int _damage;
    [SerializeField] private int _health;

    public int Damage => _damage;
    public int Health => _health;

    public virtual string GetEnemyStats()
    {
        return "Name: " + _enemy_name + " Type: " + _type.ToString() + " Position: " + _position.ToString() + " Damage: " + _damage;
    }
}

public enum EnemyType
{
    Human,
    Ghost,
    Orc,
    Beast
}

public enum FightPosition
{
    Standing,
    Flying,
    Swimming,
    Stealthing
}
