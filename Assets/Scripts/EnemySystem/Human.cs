using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[CreateAssetMenu(fileName = "New Human Enemy", menuName = "Enemy/Human")]
public class Human : EnemyBase
{
    [Header("Human Stats")]
    [SerializeField] private int _damage;
    [SerializeField] private int _health;
    [SerializeField] private int _block;

    public override void Attack(PlayerStats player)
    {
        Debug.Log("Damage: " +  _damage);
    }

    public override void TakeDamage(int damage)
    {
        _health -= damage - _block;
    }

    public override string GetEnemyStats()
    {
        return base.GetEnemyStats() + " Health: " + _health;
    }
}
