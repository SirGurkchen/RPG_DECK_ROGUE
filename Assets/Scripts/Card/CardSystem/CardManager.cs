using UnityEngine;

/// <summary>
/// Manages the Usage of Cards.
/// Each card has this script as part of its components.
/// </summary>
public class CardManager : MonoBehaviour
{
    public void PlayCard(CardController card, PlayerManager player, PlayerInventory playerInventory, EnemyBoard board)
    {
        ItemBase equippedItem = null;
        if (playerInventory.GetEquippedItem() != null)
        {
            equippedItem = playerInventory.GetEquippedItem().GetItemBase();
        }
        AttackType attackType = AttackType.None;

        if (equippedItem is Weapon weapon)
        {
            attackType = weapon.attackType;
        }

        CardContext context = new CardContext
        {
            usedItem = equippedItem,
            attackType = attackType,
            enemies = board.GetEnemies().ToArray(),
            player = player
        };

        card.GetCard().Play(context);
    }
}
