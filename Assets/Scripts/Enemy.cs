using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敵人
//附加在敵人上

public class Enemy : MonoBehaviour
{
    public float firingRate;
    public float laserSpeed;
    public float enemyHealth = 100f;
    public GameObject enemyLaserPrefab;
    public int scoreValue;
    public AudioClip shootingSound;
    public AudioClip deathSound;
    
    private ScoreKeeper scoreKeeper;
    //private particlesystem starfield1;
    //private particlesystem starfield2;

    private void Start ()
    {
        GetComponent<Health>().EntityHealth = enemyHealth;
        InvokeRepeating("ShootEnemyLaser", 2f, firingRate);
        scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }
    private void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position);
        Destroy(gameObject);
        scoreKeeper.AddScore(scoreValue);
    }
    void ShootEnemyLaser()
    {
        float shootProbability = 0.6f;
        if (Random.value <= shootProbability)
        {
            GameObject enemyLaser = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
            enemyLaser.GetComponent<Rigidbody2D>().velocity = Vector2.down * laserSpeed;
            AudioSource.PlayClipAtPoint(shootingSound, transform.position);
        }
    }
}
