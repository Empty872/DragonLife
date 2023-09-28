using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public GameObject player;
    public float damage;

    void Start()
    {
        player = GameObject.Find("Dragon");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            Destroy(gameObject);
            DealDamage(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MainCamera"))
        {
            Destroy(gameObject);
        }
    }

    public void DealDamage(float dealedDamage)
    {
        player.GetComponent<DragonState>().TakeDamage(dealedDamage);
    }
}