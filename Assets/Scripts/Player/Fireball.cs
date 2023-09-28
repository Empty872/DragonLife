using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float damage;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyHitBox"))
        {
            Destroy(gameObject);
            DealDamage(other, damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
        }
    }

    public void DealDamage(Collider2D col, float dealedDamage)
    {
        col.GetComponentInParent<EnemyState>().TakeDamage(dealedDamage);
    }
}