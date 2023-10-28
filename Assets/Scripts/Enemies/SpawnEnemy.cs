using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy;
    public float spawnCooldown = 10f;
    public float timeToSpawn;
    public bool touchCamera;
    public GameObject currentEnemy;

    void Start()
    {
        timeToSpawn = spawnCooldown;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            touchCamera = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            touchCamera = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToSpawn > 0 && currentEnemy.IsUnityNull())
        {
            timeToSpawn -= Time.deltaTime;
        }
        else if (timeToSpawn <= 0)
        {
            if (!touchCamera)
            {
                currentEnemy = Instantiate(enemy, transform.position, transform.rotation);
                timeToSpawn = spawnCooldown;
            }
        }
    }
}