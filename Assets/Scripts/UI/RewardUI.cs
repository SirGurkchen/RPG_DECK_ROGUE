using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the visualization of the Reward system after every round.
/// </summary>
public class RewardUI : MonoBehaviour
{
    [SerializeField] private Image[] _rewardUI;
    [SerializeField] private TextMeshProUGUI _oneText;
    [SerializeField] private TextMeshProUGUI _twoText;

    private void Start()
    {
        foreach (Image item in  _rewardUI)
        {
            item.gameObject.SetActive(false);
        }

        _oneText.text = string.Empty;
        _twoText.text = string.Empty;
    }

    /// <summary>
    /// Clears reward UI.
    /// </summary>
    public void ClearRewardUI()
    {
        DemarkAllRewards();
        foreach (Image item in _rewardUI)
        {
            item.gameObject.SetActive(false);
        }
        _rewardUI[0].sprite = null;
        _rewardUI[1].sprite = null;

        _oneText.text = string.Empty;
        _twoText.text = string.Empty;
    }

    /// <summary>
    /// Fills reward UI with Items.
    /// </summary>
    /// <param name="itemOne">Item one.</param>
    /// <param name="itemTwo">Item two.</param>
    public void FillRewardUI(ItemController itemOne, ItemController itemTwo)
    {
        _rewardUI[0].sprite = itemOne.GetItemBase().Icon;
        _rewardUI[1].sprite = itemTwo.GetItemBase().Icon;
        foreach (Image item in _rewardUI)
        {
            item.gameObject.SetActive(true);
        }
        _oneText.text = "1";
        _twoText.text = "2";
    }

    /// <summary>
    /// Demarks all items.
    /// </summary>
    public void DemarkAllRewards()
    {
        foreach (Image item in _rewardUI)
        {
            item.color = Color.white;
        }
    }

    /// <summary>
    /// Shows description of reward Item.
    /// </summary>
    /// <param name="item">Item to show description for.</param>
    /// <param name="index">Index of item.</param>
    /// <param name="itemUI">UI of item.</param>
    public void ShowRewardItemDescription(ItemController item, int index, ItemUI itemUI)
    {
        itemUI.ShowRewardItemDescription(item);
        DemarkAllRewards();
        _rewardUI[index].color = Color.red;
    }
}
