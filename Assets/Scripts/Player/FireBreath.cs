using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    private float timer = 1f;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitBox"))
        {
            other.GetComponentInParent<EnemyState>().TakeDamage(damage);
        }
    }
}