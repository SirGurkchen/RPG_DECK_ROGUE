using UnityEngine;

/// <summary>
/// Enemy Data for enemies which do not have any special effects.
/// </summary>
[CreateAssetMenu(fileName = "New Normal Enemy", menuName = "Enemy/Normal")]
public class NormalEnemy : EnemyBase
{
    public override string GetEnemyStats()
    {
        return base.GetEnemyStats();
    }
}
