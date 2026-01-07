using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private List<ItemController> _currentRewards = new List<ItemController>();
    private List<ItemController> _availableRewards = new List<ItemController>();
    private ItemController _selectReward;

    public List<ItemController> GetRandomRewardItems(ItemsDataBase database)
    {
        ItemController itemOne = database.GetRandomItem();
        _availableRewards.Add(itemOne);

        ItemController itemTwo = database.GetRandomItem();
        _availableRewards.Add(itemTwo);

        _currentRewards = _availableRewards;
        return _currentRewards;
    }

    public void SetSelectReward(int index)
    {
        _selectReward = _currentRewards[index];
    }

    public void ClearRewards()
    {
        _currentRewards.Clear();
        _availableRewards.Clear();
    }

    public ItemController GetSelectReward()
    {
        return _selectReward;
    }
}
