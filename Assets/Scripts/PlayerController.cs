using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家
//附加在玩家上

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 15f;
    public float firingRate;
    public GameObject laserPrefab;
    public float playerHealth = 250f;
    public float laserSpeed = 2f;
    public AudioClip shootingSound;

    private LevelManager levelManager;
    private float xMax, xMin;
    private float yMax, yMin;
    void Start ()
    {
        FindBoundary();
        GetComponent<Health>().EntityHealth = playerHealth;
        levelManager = FindObjectOfType<LevelManager>();
    }
	
	void Update ()
    {
        //移動方向
		if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = transform.position + Vector3.up * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = transform.position + Vector3.down * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = transform.position + Vector3.left * playerSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = transform.position + Vector3.right * playerSpeed * Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
        float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
        transform.position = new Vector3(newX, newY, transform.position.z);

        //發射雷射
        if(Input.GetKeyDown("space"))
        {
            InvokeRepeating("ShootLaser", 0.00001f, firingRate);  //在第一次呼叫時建議使用極小的數字來代表0
        }
        if(Input.GetKeyUp("space"))
        {
            CancelInvoke();
        }

    }
    void FindBoundary()
    {
        float cameraToPlayer = transform.position.z - Camera.main.transform.position.z;
        xMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraToPlayer)).x + 0.5f;
        xMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, cameraToPlayer)).x - 0.5f;
        yMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, cameraToPlayer)).y + 0.5f;
        yMax = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, cameraToPlayer)).y - 0.5f;
    }
    void ShootLaser()
    {
        GameObject playerLaser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        playerLaser.GetComponent<Rigidbody2D>().velocity = Vector2.up * laserSpeed;
        AudioSource.PlayClipAtPoint(shootingSound, transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Health health = GetComponent<Health>();
            health.EntityHealth = health.EntityHealth - 150f;
        }
    }
    void Die()
    {
        Destroy(gameObject);
        levelManager.LoadScene("Game Over");
    }
}
