using UnityEngine;

/// <summary>
/// Contains data for the Spikey Enemy type.
/// Extends EnemyBase for necessary data.
/// Implements IThorns as part of the enemy type.
/// </summary>
[CreateAssetMenu(fileName = "New Spikey Enemy", menuName = "Enemy/Spikey Monster")]
public class SpikeyMonster : EnemyBase, IThorns
{
    [SerializeField] private int _thornsDamage;

    public int ThornsDamage => _thornsDamage;

    public override string GetEnemyStats()
    {
        return base.GetEnemyStats() + " Thorns: " + _thornsDamage;
    }
}
