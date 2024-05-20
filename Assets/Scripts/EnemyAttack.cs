using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] float attackDelay = .5f;
    float nextAttackTime;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AttackPlayer(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        AttackPlayer(collision);
    }

    void AttackPlayer(Collision2D collision)
    {
        PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

        if (playerHealth)
        {
            if (nextAttackTime < Time.time)
            {
                playerHealth.TakeDamage();
                nextAttackTime = Time.time + attackDelay;
            }
        }
        
    }
}
