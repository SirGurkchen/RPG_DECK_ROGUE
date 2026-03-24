/// <summary>
/// Enemy Controller for enemies which have thorn damage.
/// </summary>
public class SpikeyEnemyController : EnemyController
{
    private int _currentThorns;

    protected override void InitializeEnemy()
    {
        base.InitializeEnemy();
        if (_enemyData is SpikeyMonster thorn)
        {
            _currentThorns = thorn.ThornsDamage;
        }
    }

    public override void TakeDamage(int damage, AttackType attack)
    {
        base.TakeDamage(damage, attack);
        if (attack != AttackType.Range)
        {
            IncreaseThorns();
            _myUI.UpdateThornsDisplay(_currentThorns);
        }
    }

    public override void Attack(PlayerManager player)
    {
        base.Attack(player);
        player.TakeDamage(_currentThorns);
    }

    private void IncreaseThorns()
    {
        _currentThorns++;
    }
}
