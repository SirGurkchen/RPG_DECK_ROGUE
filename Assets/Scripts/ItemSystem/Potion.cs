using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Items/Potion")]
public class Potion : ItemBase
{
    [Header("Potion Stats")]
    [SerializeField] private int _amount;
    [SerializeField] private PotionType _type;

    public override void Use(PlayerStats player, EnemyBase target = null)
    {
        Debug.Log("Cannot attack without target!");
    }
}

public enum PotionType
{
    Mana,
    Health
}
