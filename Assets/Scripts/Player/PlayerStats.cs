using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _armor;
    [SerializeField] private int _maxMana;

    private int _health;
    private int _mana;

    public event Action OnPlayerDeath;
    public event Action OnPlayerHeal;
    public event Action OnManaChange;

    public int MaxHealth => _maxHealth;
    public int Health => _health;
    public int MaxMana => _maxMana;
    public int Mana => _mana;

    private void Start()
    {
        _health = _maxHealth;
        _mana = _maxMana;
    }

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
        if (_health < _maxHealth)
        {
            _health += healAmount;
            if (_health > _maxHealth)
            {
                _health = _maxHealth;
            }
            OnPlayerHeal?.Invoke();
        }
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            OnPlayerDeath?.Invoke();
        }
    }

    public void RemoveMana(int amount)
    {
        _mana -= amount;
        OnManaChange?.Invoke();
    }

    public void AddMana(int amount)
    {
        if (_mana < _maxMana)
        {
            _mana += amount;

            if (_mana > _maxMana)
            {
                _mana = _maxMana;
            }
        }
    }

    private void OnDestroy()
    {
        OnPlayerDeath = null;
        OnPlayerHeal = null;
        OnManaChange = null;
    }
}
