public class AnimalEnemyController : EnemyController
{
    public override void TakeDamage(int damage, AttackType attack)
    {
        if (CheckDodge()) return;

       base.TakeDamage(damage, attack);
    }
    private bool CheckDodge()
    {
        if (_enemyData is AnimalEnemy animal && TryDodge())
        {
            AudioManager.Instance.PlayAudioClip(animal.MissSound);
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool TryDodge()
    {
        return _enemyData is IDodge dodge && UnityEngine.Random.Range(0, dodge.DodgeChance) == 0;
    }
}
