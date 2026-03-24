using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the Reward System which is triggered after every round.
/// </summary>
public class RewardManager : MonoBehaviour
{
    private List<ItemController> _currentRewards = new List<ItemController>();
    private List<ItemController> _availableRewards = new List<ItemController>();
    private ItemController _selectReward;

    /// <summary>
    /// Builds a list of random reward items and returns it.
    /// </summary>
    /// <returns>List of ItemControllers of chosen items.</returns>
    public List<ItemController> GetRandomRewardItems()
    {
        ItemController itemOne = ItemsDataBase.Instance.GetRandomItem();
        _availableRewards.Add(itemOne);

        ItemController itemTwo = ItemsDataBase.Instance.GetRandomItem();
        _availableRewards.Add(itemTwo);

        _currentRewards = _availableRewards;
        return _currentRewards;
    }

    /// <summary>
    /// Sets the selected reward.
    /// </summary>
    /// <param name="index">Index of reward.</param>
    public void SetSelectReward(int index)
    {
        _selectReward = _currentRewards[index];
    }

    /// <summary>
    /// Clears the rewards.
    /// </summary>
    public void ClearRewards()
    {
        _currentRewards.Clear();
        _availableRewards.Clear();
        _selectReward = null;
    }

    /// <summary>
    /// Return selected reward
    /// </summary>
    /// <returns>ItemController of selected reward.</returns>
    public ItemController GetSelectReward()
    {
        return _selectReward;
    }

    /// <summary>
    /// Gives the player a reward item.
    /// </summary>
    /// <param name="item">Item to reward.</param>
    /// <param name="player">Player main class.</param>
    /// <param name="UI">UI.</param>
    public void GivePlayerRewardItem(ItemController item, PlayerManager player, UIManager UI)
    {
        if (player.GetPlayerInventory().CanAddItem())
        {
            player.GetPlayerInventory().GiveItemToInventory(Instantiate(item));
            UI.UpdateInventoryUI(player.GetPlayerInventory().GetInventory());
        }
    }
}
