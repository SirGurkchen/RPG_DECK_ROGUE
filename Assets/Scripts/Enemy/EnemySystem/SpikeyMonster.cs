using UnityEngine;
using static Unity.Collections.AllocatorManager;

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
