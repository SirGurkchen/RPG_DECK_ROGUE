using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;
    [SerializeField] private EnemyBoard _enemyBoard;
    [SerializeField] private UIManager _UIManager;
    [SerializeField] private CardManager _cardManager;

    private void Start()
    {
        _player.OnPlayerTurnEnded += PlayerTurnEnd;
        _player.OnItemSelected += PlayerItemSelect;
        _player.OnCardUse += PlayerCardUse;
    }

    private void PlayerCardUse(CardController card)
    {
        _cardManager.PlayCard(card, _player, _player.GetPlayerInventory(), _enemyBoard);
    }

    private void PlayerTurnEnd()
    {
        foreach (EnemyController enemy in _enemyBoard.GetEnemies())
        {
            enemy.Attack(_player);
        }

        _player.DeselectAllItems();
        _UIManager.ClearWeaponUI();
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
