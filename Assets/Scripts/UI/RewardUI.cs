using TMPro;
using UnityEngine;

/// <summary>
/// Handles the visualization of the Reward system after every round.
/// </summary>
public class RewardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _rewardUI;

    public void ClearRewardUI()
    {
        DemarkAllRewards();
        _rewardUI[0].text = "";
        _rewardUI[1].text = "";
    }

    public void FillRewardUI(ItemController itemOne, ItemController itemTwo)
    {
        _rewardUI[0].text = itemOne.GetItemName();
        _rewardUI[1].text = itemTwo.GetItemName();
    }

    public void DemarkAllRewards()
    {
        foreach (TextMeshProUGUI item in _rewardUI)
        {
            item.color = Color.white;
        }
    }

    public void ShowRewardItemDescription(ItemController item, int index, ItemUI itemUI)
    {
        itemUI.ShowRewardItemDescription(item);
        DemarkAllRewards();
        _rewardUI[index].color = Color.red;
    }
}
