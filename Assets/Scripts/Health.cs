using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//血量
//附加在敵人與玩家上

public class Health : MonoBehaviour
{
    private float entityHealth;
    public float EntityHealth
    {
        get
        {
            return entityHealth;
        }
        set
        {
            entityHealth = value;
            if(entityHealth <= 0)
            {
                SendMessage("Die");
                Destroy(gameObject);
            }
        }
    }
}
