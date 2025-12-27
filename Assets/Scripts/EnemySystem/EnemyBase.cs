using UnityEngine;

public abstract class EnemyBase : ScriptableObject
{
    [Header("Basic Info")]
    [SerializeField] private string _enemy_name;
    [TextArea(3, 6)]
    [SerializeField] private string _description;
    [SerializeField] private EnemyType _type;
    [SerializeField] private FightPosition _position;

    public abstract void Attack(PlayerStats player);
    public abstract void TakeDamage(int damage);
    public virtual string GetEnemyStats()
    {
        return "Name: " + _enemy_name + " Type: " + _type.ToString() + " Position: " + _position.ToString();
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
