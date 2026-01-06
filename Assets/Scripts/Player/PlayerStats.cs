using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _health;
    [SerializeField] private int _mana;
    [SerializeField] private int _armor;

    public event Action OnPlayerDeath;
    public event Action OnPlayerHeal;

    public int MaxHealth => _maxHealth;
    public int Health => _health;

    public void ReceiveDamage(int damage)
    {
        int finalDamage = damage - _armor;

        if (finalDamage > 0)
        {
            _health -= finalDamage;
        }

        CheckDeath();
    }

    public void HealPlayer(int healAmount)
    {
        _health += healAmount;
        OnPlayerHeal?.Invoke();
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    private void OnDestroy()
    {
        OnPlayerDeath = null;
        OnPlayerHeal = null;
    }
}
