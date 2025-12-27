using UnityEngine;

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
