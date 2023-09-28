using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canDrink;
    public GameObject player;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canDrink && Input.GetKeyDown(KeyCode.D))
        {
            Drink();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Lake"))
        {
            canDrink = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Lake"))
        {
            canDrink = false;
        }
    }

    public void Drink()
    {
        player.GetComponent<DragonState>().Drink(10000);
    }
}