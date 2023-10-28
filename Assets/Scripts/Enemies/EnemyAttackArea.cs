using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    // Start is called before the first frame update
    public bool touchPlayer;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            touchPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("HitBox"))
        {
            touchPlayer = false;
        }
    }

    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("HitBox"))
    //     {
    //         touchPlayer = true;
    //     }
    //
    //     Debug.Log(other.gameObject);
    // }
    //
    // private void OnCollisionExit2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("HitBox"))
    //     {
    //         touchPlayer = false;
    //     }
    // }
}