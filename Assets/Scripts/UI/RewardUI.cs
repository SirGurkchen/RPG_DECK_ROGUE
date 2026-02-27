using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the visualization of the Reward system after every round.
/// </summary>
public class RewardUI : MonoBehaviour
{
    [SerializeField] private Image[] _rewardUI;

    private void Start()
    {
        foreach (Image item in  _rewardUI)
        {
            item.gameObject.SetActive(false);
        }
    }

    public void ClearRewardUI()
    {
        DemarkAllRewards();
        foreach (Image item in _rewardUI)
        {
            item.gameObject.SetActive(false);
        }
        _rewardUI[0].sprite = null;
        _rewardUI[1].sprite = null;
    }

    public void FillRewardUI(ItemController itemOne, ItemController itemTwo)
    {
        _rewardUI[0].sprite = itemOne.GetItemBase().Icon;
        _rewardUI[1].sprite = itemTwo.GetItemBase().Icon;
        foreach (Image item in _rewardUI)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void DemarkAllRewards()
    {
        foreach (Image item in _rewardUI)
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
