using UnityEngine;

/// <summary>
/// Manages the Usage of Cards.
/// Each card has this script as part of its components.
/// </summary>
public class CardManager : MonoBehaviour
{
    /// <summary>
    /// Plays a given card.
    /// Builds the card context so the card can be played correctly.
    /// </summary>
    /// <param name="card">Card to be played.</param>
    /// <param name="player">Player Main class.</param>
    /// <param name="playerInventory">Inventory of the player.</param>
    /// <param name="board">Enemyboard of the game.</param>
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
