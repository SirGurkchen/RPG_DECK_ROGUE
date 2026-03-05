using UnityEngine;
using static Unity.Collections.AllocatorManager;

[CreateAssetMenu(fileName = "New Animal Enemy", menuName = "Enemy/Animal")]
public class AnimalEnemy : EnemyBase, IDodge
{
    [Tooltip("Number equals chance. Example: 5 = 1/5th")]
    [SerializeField] private int _dodgeChance; // Number equals chance. Example: 5 = 1/5th
    public int DodgeChance => _dodgeChance;

    public override string GetEnemyStats()
    {
        return base.GetEnemyStats() + " Dodge Chance: " + _dodgeChance;
    }
}
