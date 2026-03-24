using DG.Tweening;
using UnityEngine;

/// <summary>
/// Handles the animation of the Main Menu visual elements.
/// Animations include Start UP and sub menu switching.
/// </summary>
public class MainMenuAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _selectionEndPosition;
    [SerializeField] private GameObject _settingsEndPostion;
    [SerializeField] private GameObject _selectionObject;
    [SerializeField] private GameObject _settingsObject;

    private const float START_ANIMATION_TIME = 1.2f;
    private const float CHANGE_ANIMATION_TIME = 0.9f;
    private Vector3 _selectionStartPos;
    private Vector3 _settingsStartPos;

    private void Start()
    {
        _selectionStartPos = _selectionObject.transform.position;
        _settingsStartPos = _settingsObject.transform.position;
    }

    /// <summary>
    /// Animates the main menu start up.
    /// </summary>
    public void AnimateStartUp()
    {
        if (_selectionObject == null) return;
        _selectionObject.transform.DOKill();
        _selectionObject.gameObject.transform.DOMove(_selectionEndPosition.transform.position, START_ANIMATION_TIME);
    }

    /// <summary>
    /// Toggles the settings/main menu animation
    /// </summary>
    /// <param name="isOn">Activity of main menu.</param>
    public void ToggleSettingsAnimation(bool isOn)
    {
        if (isOn)
        {
            ChangeToSettingsMenu();
        }
        else
        {
            ChangeToMainMenu();
        }
    }

    private void ChangeToSettingsMenu()
    {
        if (_selectionObject == null) return;
        if (_settingsObject == null) return;
        _selectionObject.transform.DOKill();
        _settingsObject.transform.DOKill();
        AudioManager.Instance.PlayChainSound();
        _selectionObject.gameObject.transform.DOMove(_selectionStartPos, CHANGE_ANIMATION_TIME).OnComplete(() =>
        {
            AudioManager.Instance.PlayChainSound();
            _settingsObject.gameObject.transform.DOMove(_settingsEndPostion.transform.position, CHANGE_ANIMATION_TIME);
        });
    }

    private void ChangeToMainMenu()
    {
        if (_selectionObject == null) return;
        if (_settingsObject == null) return;
        _selectionObject.transform.DOKill();
        _settingsObject.transform.DOKill();
        AudioManager.Instance.PlayChainSound();
        _settingsObject.gameObject.transform.DOMove(_settingsStartPos, CHANGE_ANIMATION_TIME).OnComplete(() =>
        {
            AudioManager.Instance.PlayChainSound();
            _selectionObject.gameObject.transform.DOMove(_selectionEndPosition.transform.position, CHANGE_ANIMATION_TIME);
        });
    }

    /// <summary>
    /// Kills all animation in the main menu.
    /// </summary>
    public void KillAll()
    {
        _selectionObject.transform.DOKill();
        _settingsObject.transform.DOKill();
    }
}
