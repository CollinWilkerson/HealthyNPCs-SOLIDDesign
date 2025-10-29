using System;
using System.Collections;
using UnityEngine;

public class RegenBoss : MonoBehaviour, IHealth
{
    [SerializeField]
    private int startingHp = 100;
    [SerializeField] private float healTime;
    private bool firstHit = true;

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
        if(firstHit)
        {
            firstHit = false;
            StartCoroutine(Heal());
        }

        if (amount <= 0)
            throw new ArgumentOutOfRangeException("Invalid Damage amount specified: " + amount);

        currentHp -= amount;

        OnHPPctChanged(CurrentHpPct);

        if (currentHp <= 0)
            Die();
    }

    private IEnumerator Heal()
    {
        yield return new WaitForSeconds(healTime);

        currentHp = startingHp;
        firstHit = true;
    }

    private void Die()
    {
        OnDied();
        Destroy(gameObject);
    }
}
