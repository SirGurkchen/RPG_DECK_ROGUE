using DG.Tweening;
using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private const float ATTACK_ANIMATION_HEIGHT = 0.6f;
    private const float ATTACK_TIME = 0.2f;
    private const float SWAY_TIME = 0.05f;
    private const float DAMAGING_SWAY = 0.1f;
    private Vector3 _originalPos;

    public void SetOriginalPos(Vector3 pos)
    {
        _originalPos = pos;
    }

    public void PlayAttackAnimation()
    {
        if (gameObject != null)
        {
            transform.DOKill();
            Vector3 attackPos = _originalPos + new Vector3(0, -ATTACK_ANIMATION_HEIGHT, 0);
            Vector3 endPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            StartCoroutine(ExecuteAttackAnimation(attackPos, endPos));
        }
    }

    public void PlayDamagingAnimation()
    {
        transform.DOKill();
        Vector3 currentPos = gameObject.transform.position;
        Vector3 positiveSway = new Vector3(currentPos.x + DAMAGING_SWAY, currentPos.y, 0);
        Vector3 negativeSway = new Vector3(currentPos.x - DAMAGING_SWAY, currentPos.y, 0);
        StartCoroutine(ExecuteDamagingAnimation(currentPos, positiveSway, negativeSway));
    }

    private IEnumerator ExecuteAttackAnimation(Vector3 attackPos, Vector3 startPos)
    {
        gameObject.transform.DOMove(attackPos, ATTACK_TIME);
        yield return new WaitForSeconds(ATTACK_TIME);
        gameObject.transform.DOMove(startPos, ATTACK_TIME);
    }

    private IEnumerator ExecuteDamagingAnimation(Vector3 startPos, Vector3 positive, Vector3 negative)
    {
        for (int i = 0; i < 2; i++)
        {
            gameObject.transform.DOMove(positive, SWAY_TIME);
            yield return new WaitForSeconds(SWAY_TIME);
            gameObject.transform.DOMove(negative, SWAY_TIME);
            yield return new WaitForSeconds(SWAY_TIME);
        }
        gameObject.transform.DOMove(startPos, SWAY_TIME);
    }
}
