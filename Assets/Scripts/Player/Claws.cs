using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Claws : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    public bool canAttack;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitBox") && canAttack)
        {
            Attack(other.gameObject, damage);
            canAttack = false;
        }
    }


    public void Attack(GameObject enemy, int damage)
    {
        enemy.GetComponentInParent<EnemyState>().TakeDamage(damage);
    }
}
