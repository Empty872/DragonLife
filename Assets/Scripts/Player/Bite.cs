using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public bool canBite;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitBox") && canBite)
        {
            Attack(other.gameObject, damage);
            canBite = false;
        }
    }


    public void Attack(GameObject enemy, int damage)
    {
        enemy.GetComponentInParent<EnemyState>().TakeDamage(damage);
    }
}