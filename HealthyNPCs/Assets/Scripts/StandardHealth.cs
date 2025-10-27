using System;
using UnityEngine;

public class StandardHealth : MonoBehaviour, IHealth
{
    [SerializeField]
    private int startingHp = 100;

    private int currentHp;

    private float CurrentHpPct 
    { 
        get { return (float)currentHp / (float)startingHp; } 
    }

    public event Action<float> OnHPPctChanged = delegate { };
    public event Action OnDied = delegate { };

    private void Start()
    {
        currentHp = startingHp;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        currentHp -= amount;

        OnHPPctChanged(CurrentHpPct);

        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        OnDied();
        Destroy(gameObject);
    }
}