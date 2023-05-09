using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    public float health = 50f;
    public float MaxHealth;
    private void Awake()
    {
        MaxHealth = health;
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }
    void Die()
    {

        Destroy(gameObject);
    }
}
