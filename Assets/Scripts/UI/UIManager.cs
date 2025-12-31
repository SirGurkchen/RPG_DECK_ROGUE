using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _weaponUI;

    public void UpdateWeaponUI(ItemController item)
    {
        _weaponUI.text = item.GetItemName();
    }
}
