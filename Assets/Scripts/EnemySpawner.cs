using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵人產生器
//附加於EnemyFormation上

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;  //敵人
    public float width;  //陣行長
    public float height;  //陣行寬
    public float speed;  //陣行移動速度
    public float spawnDelay = 1f;

    private bool movingRight = true;  //陣行是否往右邊移動
    private float xMin, xMax;  //X方向邊界

    private void Start()
    {
        SpawnEnemies();
        float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distanceToCamera)).x;
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distanceToCamera)).x;
    }
    private void Update()
    {
        if(movingRight)
        {
            transform.position = transform.position + Vector3.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position = transform.position + Vector3.left * speed * Time.deltaTime;
        }

        float leftEdgeOfFormation = transform.position.x - (0.5f * width);
        float rightEdgeOfFormation = transform.position.x + (0.5f * width);
        if(leftEdgeOfFormation < xMin)
        {
            movingRight = true;
        }
        else if(rightEdgeOfFormation > xMax)
        {
            movingRight = false;
        }

        if(NoMoreEnemy())
        {
            SpawnOneEnemyUntilFull();
        }
    }
    void SpawnEnemies()
    {
        foreach (Transform enemyPosition in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, enemyPosition.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = enemyPosition;  //將產生出來的Enemy指派為每一個position的子物件
        }
    }
    void SpawnOneEnemyUntilFull()
    {
        Transform freePosition = NextFreePosition();
        if(freePosition)
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = freePosition;  //將產生出來的Enemy指派為每一個position的子物件
        }
        if(NextFreePosition())
        {
            Invoke("SpawnOneEnemyUntilFull", spawnDelay);
        }
    }
    bool NoMoreEnemy()
    {
        foreach(Transform position in transform)
        {
            if(position.childCount > 0)
            {
                return false;
            }
        }
        return true;
    }
    Transform NextFreePosition()
    {
        foreach (Transform position in transform)
        {
            if (position.childCount == 0)
            {
                return position;
            }
        }
        return null;
    }
    private void OnDrawGizmos()  //顯示方塊在Scene View裡
    {
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + 2f, 0), new Vector3(width, height, 0));
    }
}
