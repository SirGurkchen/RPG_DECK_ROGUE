using UnityEngine;

/// <summary>
/// Contains data for enemies of the human type.
/// Extends EnemyBase to have necessary base data.
/// Implements IBlock as part of the enemy type.
/// </summary>
[CreateAssetMenu(fileName = "New Human Enemy", menuName = "Enemy/Human")]
public class HumanData : EnemyBase, IBlock
{

    [SerializeField] private int _block;

    public int Block => _block;

    public override string GetEnemyStats()
    {
        return base.GetEnemyStats() + " Block: " + _block;
    }
}
