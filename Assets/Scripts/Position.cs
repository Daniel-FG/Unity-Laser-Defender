using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵人位置
//附加在Position裡

public class Position : MonoBehaviour
{
    private void OnDrawGizmos()  //顯示圓框在Scene View裡
    {
        float radius = 1;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
