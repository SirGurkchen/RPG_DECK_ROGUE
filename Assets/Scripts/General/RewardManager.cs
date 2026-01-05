using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private List<ItemController> _currentRewards = new List<ItemController>();

    private ItemController _selectReward;

    public List<ItemController> GetRandomRewardItems(ItemsDataBase database)
    {
        List<ItemController> rewardItems = new List<ItemController>();
        ItemController itemOne = database.GetRandomItem();
        rewardItems.Add(itemOne);

        ItemController itemTwo = database.GetRandomItem();
        rewardItems.Add(itemTwo);

        _currentRewards = rewardItems;
        return rewardItems;
    }

    public void SetSelectReward(int index)
    {
        _selectReward = _currentRewards[index];
    }

    public void ClearRewards()
    {
        _currentRewards.Clear();
    }

    public ItemController GetSelectReward()
    {
        return _selectReward;
    }
}
