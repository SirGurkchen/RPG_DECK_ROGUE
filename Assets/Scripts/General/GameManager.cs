using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private EnemyBoard _enemyBoard;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CardManager _cardManager;
    [SerializeField] private CardController _testCard;

    private void Start()
    {
        _player.OnPlayerTurnEnded += PlayerTurnEnd;
        _player.OnItemSelected += PlayerItemSelect;
        _player.OnCardUse += PlayerCardUse;
        _player.OnCardSelected += PlayerCardSelection;

        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
        AddCardToPlayer(_testCard);
    }

    private void PlayerCardSelection(CardController card)
    {
        if (card == null) return;
        _UIManager.UpdateCardUI(card.GetCard());
        card.SelectCard();
    }

    private void AddCardToPlayer(CardController card)
    {
        CardController newCard = Instantiate(card);
        _player.GiveCard(newCard);
        _UIManager.AddCardUI(newCard);
    }

    private void PlayerCardUse(CardController card)
    {
        if (card == null) return;
        _cardManager.PlayCard(card, _player, _player.GetPlayerInventory(), _enemyBoard);
        _UIManager.ClearCardUI();
        _UIManager.RemoveCardUI(card);
    }

    private void PlayerTurnEnd()
    {
        foreach (EnemyController enemy in _enemyBoard.GetEnemies())
        {
            enemy.Attack(_player);
        }

        _player.DeselectAllItems();
        _UIManager.ClearWeaponUI();
        _UIManager.UpdateHealthText(_player.GetPlayerStats().Health, _player.GetPlayerStats().MaxHealth);
    }

    private void PlayerItemSelect(ItemController item)
    {
        if (item == null)
        {
            Debug.Log("No Item");
            return;
        }
        _UIManager.UpdateWeaponUI(item);
    }

    private void OnDisable()
    {
        _player.OnPlayerTurnEnded -= PlayerTurnEnd;
        _player.OnItemSelected -= PlayerItemSelect;
        _player.OnCardUse -= PlayerCardUse;
    }
}
