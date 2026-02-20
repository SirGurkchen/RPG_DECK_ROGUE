using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Updates the visuals of status effects an enemy can have.
/// </summary>
public class EnemyStatusUI : MonoBehaviour
{
    [System.Serializable]
    public class StatusIcon
    {
        public string StatusName;
        public GameObject Container;
        public TextMeshProUGUI Value;
    }

    [SerializeField] private List<StatusIcon> _statusIcons;

    public void UpdateStatus(EnemyBase enemyData)
    {
        UpdateStatusDisplay("Block", enemyData is IBlock block ? block.Block : -1);
        UpdateStatusDisplay("Thorns", enemyData is IThorns thorn ? thorn.ThornsDamage : -1);
    }

    private void UpdateStatusDisplay(string statusName, int value)
    {
        StatusIcon icons = _statusIcons.Find(s => s.StatusName == statusName);

        if (icons != null)
        {
            if (value > 0)
            {
                icons.Container.SetActive(true);
                icons.Value.text = value.ToString();
            }
            else
            {
                icons.Container.SetActive(false);
            }
        }
    }
}
