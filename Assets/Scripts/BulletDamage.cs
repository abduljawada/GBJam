using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [HideInInspector] public int damage;
    DestroyObject destroyObject => GetComponent<DestroyObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();

        if (enemyHealth)
        {
            enemyHealth.TakeDamage(damage);
        }

        destroyObject.Destroy();
    }
}
