using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] int damageAmount = 10;

    public void TakeDamage(int amount)
    {
        GetComponent<IHealth>().TakeDamage(amount);
    }

    public void Interact()
    {
        TakeDamage(damageAmount);
    }
}