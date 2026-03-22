using UnityEngine;

[CreateAssetMenu(fileName = "New Animal Enemy", menuName = "Enemy/Animal")]
public class AnimalEnemy : EnemyBase, IDodge
{
    [Tooltip("Number equals chance. Example: 5 = 1/5th")]
    [SerializeField] private int _dodgeChance; // Number equals chance. Example: 5 = 1/5th
    [SerializeField] private AudioClip _dodgeAudio;
    public int DodgeChance => _dodgeChance;
    public AudioClip MissSound => _dodgeAudio;

    public override string GetEnemyStats()
    {
        return base.GetEnemyStats() + " Dodge Chance: " + _dodgeChance;
    }
}
