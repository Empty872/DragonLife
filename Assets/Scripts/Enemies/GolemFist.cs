using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemFist : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    public bool canAttack;
    public GameObject player;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox") && canAttack)
        {
            player.GetComponent<DragonState>().TakeDamage(damage);
            canAttack = false;
        }
    }

    public void StartAttack()
    {
        canAttack = true;
    }

    public void StopAttack()
    {
        canAttack = false;
    }
}