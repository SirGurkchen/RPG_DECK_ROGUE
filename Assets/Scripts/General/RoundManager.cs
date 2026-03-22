using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    [SerializeField] private RewardManager _rewardManager;
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private HordeLogic _hordeLogic;

    private Coroutine _roundBufferRoutine;
    private const float ROUND_BUFFER_TIMER = 0.75f;
    private const float DAMAGE_TIMER = 0.25f;
    private int _rounds;

    private void Start()
    {
        _rounds = 1;
    }

    public void StartBufferedRound(UIManager UI, PlayerManager player, EnemyBoard board)
    {
        GameInput.Instance.ChangePlayerActive(false);
        UI.ToggleSelectionPrompts(false);
        _roundBufferRoutine = StartCoroutine(ExecuteBufferedRound(UI, player, board));
    }

    private IEnumerator ExecuteBufferedRound(UIManager UI, PlayerManager player, EnemyBoard board)
    {
        if (_roundBufferRoutine != null)
        {
            Debug.Log("Routine was running! Stopped!");
            StopCoroutine(_roundBufferRoutine);
            _roundBufferRoutine = null;
        }

        yield return new WaitForSeconds(ROUND_BUFFER_TIMER);

        foreach (EnemyController enemy in board.GetEnemies().ToList())
        {
            if (enemy == null || !board.GetEnemies().Contains(enemy)) continue;

            enemy.Attack(player);
            yield return new WaitForSeconds(DAMAGE_TIMER - 0.1f);
            UI.ToggleDamageVisual(true);
            UI.UpdateHealthBar(player.GetPlayerStats().Health, player.GetPlayerStats().MaxHealth);
            yield return new WaitForSeconds(DAMAGE_TIMER);
            UI.ToggleDamageVisual(false);
            yield return new WaitForSeconds(ROUND_BUFFER_TIMER);
        }

        player.GetPlayerStats().AddMana(1);
        UI.UpdateManaUI(player.GetPlayerStats().Mana, player.GetPlayerStats().MaxMana);

        if (board.GetEnemies().Count > 0)
        {
            GameInput.Instance.ChangePlayerActive(true);
            yield return null; 
            UI.ToggleSelectionPrompts(true);
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

        if (player.GetPlayerInventory().CanAddItem())
        {
            GameInput.Instance.ChangePlayerActive(false);
            UI.ToggleSelectionPrompts(false);
            UI.ToggleLeavePrompt();
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
        else
        {
            _rounds++;
            UI.ToggleSelectionPrompts(false);
            UI.UpdateRoundCounter(_rounds);
            GameInput.Instance.ChangePlayerActive(false);
            StartNextRound(UI);
        }
    }

    private void ShowRewardScreen(PlayerManager player, UIManager UI)
    {
        List<ItemController> rewards = _rewardManager.GetRandomRewardItems();
        UI.FillRewardUI(rewards[0], rewards[1]);
        GameInput.Instance.ChangeRewardActive(true);
    }

    private void ShowShopScreen()
    {
        _shopManager.FillShop();
        GameInput.Instance.ChangeShopActive(true);
    }

    public void HandleShopSelect(int index, UIManager UI)
    {
        _shopManager.HandleShopSelection(index, UI);
    }

    public void HandleShopConfirmation(PlayerManager player, UIManager UI)
    {
        if (!_shopManager.IsRewardSelected()) return;

        if (_shopManager.HandleShopConfirm(player, UI))
        {
            GameInput.Instance.ChangeShopActive(false);
            player.GetPlayerStats().AddMana(1);
            _rounds++;
            UI.UpdateManaUI(player.GetPlayerStats().Mana, player.GetPlayerStats().MaxMana);
            UI.UpdateRoundCounter(_rounds);
            StartNextRound(UI);
        }
    }

    public void HandleRewardConfirm(PlayerManager player, UIManager UI)
    {
        if (_rewardManager.GetSelectReward() == null) return;

        _rewardManager.GivePlayerRewardItem(_rewardManager.GetSelectReward(), player, UI);
        _rewardManager.ClearRewards();
        UI.ClearRewardUI();
        UI.RemoveItemDescription();
        GameInput.Instance.ChangeRewardActive(false);
        player.GetPlayerStats().AddMana(1);
        _rounds++;
        UI.UpdateRoundCounter(_rounds);
        UI.UpdateManaUI(player.GetPlayerStats().Mana, player.GetPlayerStats().MaxMana);
        StartNextRound(UI);
    }

    private void StartNextRound(UIManager UI)
    {
        if (_rounds % 10 == 0)
        {
            _hordeLogic.RefillBoardWithBoss();
        }
        else
        {
            _hordeLogic.RefillBoardRandomly(_rounds);
        }
        StartCoroutine(WaitForSpawnThenActivate(UI));
    }

    public void CancelRoundBuffer(UIManager UI)
    {
        _rewardManager.ClearRewards();
        _shopManager.EmptyShop();
        UI.ToggleLeavePrompt();
        UI.RemoveItemDescription();
        _rounds++;
        UI.UpdateRoundCounter(_rounds);
        GameInput.Instance.ChangeShopActive(false);
        GameInput.Instance.ChangeRewardActive(false);
        StartNextRound(UI);
    }

    public void HandleRewardSelection(int rewardIndex, UIManager UI)
    {
        _rewardManager.SetSelectReward(rewardIndex);
        UI.ShowRewardItemDescription(_rewardManager.GetSelectReward(), rewardIndex);
    }

    public void StartPredeterminedRound(string enemyOne, string enemyTwo = null)
    {
        _hordeLogic.RefillBoardPredetermined(enemyOne, enemyTwo); 
    }

    private IEnumerator WaitForSpawnThenActivate(UIManager UI)
    {
        yield return new WaitUntil(() => !_hordeLogic.isSpawning);
        GameInput.Instance.ChangePlayerActive(true);
        UI.ToggleSelectionPrompts(true);
    }
}
