using TMPro;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _itemList;

    public void SetItemUI(string itemName, int index)
    {
        if (index >= 0 && index < _itemList.Length)
        {
            _itemList[index].text = itemName;
        }
    }

    public void RemoveItemUI(int index)
    {
        if (_itemList[index] != null)
        {
            _itemList[index].text = "";
        }
    }

    public void MarkItem(int index)
    {
        DemarkAll();
        _itemList[index].color = Color.red;
    }

    public void DemarkAll()
    {
        foreach (TextMeshProUGUI item in _itemList)
        {
            if (item != null && item.text != "")
            {
                item.color = Color.white;
            }
        }
    }
}
