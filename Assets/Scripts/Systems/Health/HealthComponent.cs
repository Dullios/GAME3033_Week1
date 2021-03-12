using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, IDamageable
{
    public float Health => currentHealth;
    public float MaxHealth => maxHealth;

    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Destroy();
    }

    public virtual void Destroy()
    {
        Destroy(gameObject);
    }

}
