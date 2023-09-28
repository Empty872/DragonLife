using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool canEat;
    public float food;
    public GameObject player;
    public GameObject foodSource;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (canEat && Input.GetKeyDown(KeyCode.D))
        {
            Eat();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FoodSource"))
        {
            canEat = true;
            foodSource = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("FoodSource"))
        {
            canEat = false;
            foodSource = null;
        }
    }

    public void Eat()
    {
        for (int i = 0; i < foodSource.transform.childCount; i++)
        {
            var foodObject = foodSource.transform.GetChild(i).gameObject;
            if (foodObject.activeInHierarchy)
            {
                player.GetComponent<DragonState>().Eat(food);
            }

            foodObject.SetActive(false);
            foodSource.gameObject.GetComponent<RestoreBerries>().currentBerriesCount = 0;
        }
    }
}