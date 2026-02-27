using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private HordeLogic _hordeLogic;

    private Coroutine _roundBufferRoutine;
    private const float ROUND_BUFFER_TIMER = 0.75f;

    public void StartBufferedRound(UIManager UI, PlayerManager player, EnemyBoard board)
    {
        _roundBufferRoutine = StartCoroutine(ExecuteBufferedRound(UI, player, board));
    }

    private IEnumerator ExecuteBufferedRound(UIManager UI, PlayerManager player, EnemyBoard board)
    {
        GameInput.Instance.ChangePlayerActive(false);
        UI.RemoveItemDescription();
        UI.ShowEnemyInfo(null);
        player.DeselectAllItems();
        UI.UpdateWeaponUI(player.GetPlayerInventory().GetInventory());

        yield return new WaitForSeconds(ROUND_BUFFER_TIMER);

        foreach (EnemyController enemy in board.GetEnemies())
        {
            enemy.Attack(player);
        }

        player.GetPlayerStats().AddMana(1);
        UI.UpdateHealthText(player.GetPlayerStats().Health, player.GetPlayerStats().MaxHealth);
        UI.UpdateManaUI(player.GetPlayerStats().Mana, player.GetPlayerStats().MaxMana);

        if (board.GetEnemies().Count > 0)
        {
            GameInput.Instance.ChangePlayerActive(true);
        }
        _roundBufferRoutine = null;
    }

    public void BoardClear(PlayerManager player, UIManager UI)
    {
        if (_roundBufferRoutine != null)
        {
            StopCoroutine(_roundBufferRoutine);
            _roundBufferRoutine = null;
        }
        GameInput.Instance.ChangePlayerActive(false);

        switch (RoundBufferPool.Instance.GetRandomRoundBuffer())
        {
            case RoundBufferPool.BufferType.Shop:
                ShowShopScreen();
                break;
            case RoundBufferPool.BufferType.Reward:
                ShowRewardScreen(player, UI);
                break;
        }
    }

    private void ShowRewardScreen(PlayerManager player, UIManager UI)
    {
        if (player.GetPlayerInventory().CanAddItem())
        {
            List<ItemController> rewards = _rewardManager.GetRandomRewardItems();
            UI.FillRewardUI(rewards[0], rewards[1]);
            GameInput.Instance.ChangeRewardActive(true);
        }
        else
        {
            GameInput.Instance.ChangeRewardActive(false);
            _hordeLogic.RefillBoard();
        }
    }

    private void ShowShopScreen()
    {
        Debug.Log("Shop Screen!");
        GameInput.Instance.ChangeRewardActive(false);
        _hordeLogic.RefillBoard();
    }

    public void HandleRewardConfirm(PlayerManager player, UIManager UI)
    {
        if (_rewardManager.GetSelectReward()  == null)
        {
            return;
        }

        _rewardManager.GivePlayerRewardItem(_rewardManager.GetSelectReward(), player, UI);
        _rewardManager.ClearRewards();
        UI.ClearRewardUI();
        UI.RemoveItemDescription();
        GameInput.Instance.ChangeRewardActive(false);
        GameInput.Instance.ChangePlayerActive(true);
        _hordeLogic.RefillBoard();
    }

    public void HandleRewardSelection(int rewardIndex, UIManager UI)
    {
        _rewardManager.SetSelectReward(rewardIndex);
        UI.ShowRewardItemDescription(_rewardManager.GetSelectReward(), rewardIndex);
    }
}
