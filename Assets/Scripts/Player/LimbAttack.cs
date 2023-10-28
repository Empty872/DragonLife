using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LimbAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public bool canDamage;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitBox") && canDamage)
        {
            Attack(other.gameObject, damage);
            canDamage = false;
        }
    }


    public void Attack(GameObject enemy, int damage)
    {
        enemy.GetComponentInParent<EnemyState>().TakeDamage(damage);
    }
}