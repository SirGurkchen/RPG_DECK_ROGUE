using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the Animation of the Shop.
/// Animates and enter and exit animation.
/// </summary>
public class ShopAnimator : MonoBehaviour
{
    [SerializeField] private Image _shopSign;
    [SerializeField] private GameObject _shopSignEndPos;
    [SerializeField] private AudioClip _shopSound;

    private const float SIGN_MOVE_TIME = 0.75f;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = _shopSign.transform.position;
    }

    /// <summary>
    /// Plays the shop enter animation
    /// </summary>
    public Tween PlayShopEnterAnimation()
    {
        if (_shopSignEndPos == null) return null;
        _shopSign.transform.DOKill();
        AudioManager.Instance.PlayAudioClip(_shopSound);
        return _shopSign.transform.DOMove(_shopSignEndPos.transform.position, SIGN_MOVE_TIME)
            .SetLink(_shopSign.gameObject);
    }

    /// <summary>
    /// Plays the shop leave animation.
    /// </summary>
    public Tween PlayShopLeaveAnimation()
    {
        if (_shopSign == null) return null;
        _shopSign.transform.DOKill();
        AudioManager.Instance.PlayAudioClip(_shopSound);
        return _shopSign.transform.DOMove(_startPos, SIGN_MOVE_TIME)
            .SetLink(_shopSign.gameObject);
    }
}
