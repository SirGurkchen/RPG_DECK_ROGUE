using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    private List<ItemController> _currentRewards = new List<ItemController>();
    
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

    public ItemController GetRewardItem(int index)
    {
        if (index < 0 || index >= _currentRewards.Count)
        {
            return null;
        }
        return _currentRewards[index];
    }

    public void ClearRewards()
    {
        _currentRewards.Clear();
    }
}
