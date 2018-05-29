using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//雷射
//附加在所有雷射上

public class Laser : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.gameObject.GetComponent<Health>();
        if(health)
        {
            health.EntityHealth = health.EntityHealth - damage;
            Destroy(gameObject);
        }
    }
}
