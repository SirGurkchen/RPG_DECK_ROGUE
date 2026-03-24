using UnityEngine;

/// <summary>
/// Controls the In Game Card and its visualization.
/// This is achieved by orchestration of CardBase Data and the CardUI class.
/// </summary>
public class CardController : MonoBehaviour
{
    [SerializeField] private CardBase _card;
    [SerializeField] private CardUI _myUI;

    private void Start()
    {
        _myUI.InitCard(_card);
    }

    /// <summary>
    /// Return the Data of the card.
    /// </summary>
    /// <returns>CardBase of the card.</returns>
    public CardBase GetCard()
    {
        return _card;
    }

    /// <summary>
    /// Starts the selection visualization of the card.
    /// </summary>
    public void SelectCard()
    {
        _myUI.VisualizeCardSelect();
    }

    /// <summary>
    /// Starts the deselection visualization of the card.
    /// </summary>
    public void DeselectCard()
    {
        _myUI.VisualizeCardDeselect();
    }

    /// <summary>
    /// Marks the card in red.
    /// Used for marking the card in the shop.
    /// </summary>
    /// <param name="isOn">Should card be marked.</param>
    public void ToggleShopSelection(bool isOn)
    {
        _myUI.ToggleMarkCard(isOn);
    }
}
