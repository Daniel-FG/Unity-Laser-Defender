using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//摧毀器
//附加在摧毀器上

public class Shredder : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
