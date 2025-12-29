using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Items/Potion")]
public class Potion : ItemBase
{
    [Header("Potion Stats")]
    [SerializeField] private int _amount;
    [SerializeField] private PotionType _type;

    public override bool Use(PlayerStats player, EnemyController target = null)
    {
        if (player == null)
        {
            return false;
        }
        else
        {
            switch (_type)
            {
                case PotionType.Health:
                    //heal
                    return true;
                case PotionType.Mana:
                    //add mana
                    return true;
                default:
                    return false;
            }
        }
    }
}

public enum PotionType
{
    Mana,
    Health
}
