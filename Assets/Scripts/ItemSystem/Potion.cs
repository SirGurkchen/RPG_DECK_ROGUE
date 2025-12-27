using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Items/Potion")]
public class Potion : ItemBase
{
    [Header("Potion Stats")]
    [SerializeField] private int _amount;
    [SerializeField] private PotionType _type;

    public override void Use(PlayerStats player, EnemyController target = null)
    {
        if (player == null)
        {
            return;
        }

        switch (_type)
        {
            case PotionType.Health:
                //heal
                break;
            case PotionType.Mana:
                //add mana
                break;
        }
    }
}

public enum PotionType
{
    Mana,
    Health
}
