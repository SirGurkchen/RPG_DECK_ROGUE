using UnityEngine;

/// <summary>
/// Contains overwrites for the Knight Enemy.
/// </summary>
public class Knight : EnemyController
{
    public override void TakeDamage(int damage, AttackType attack)
    {
        if (attack == AttackType.Range)
        {
            Debug.Log("Immune!");
        }
        else
        {
            base.TakeDamage(damage, attack);
        }
    }
}
