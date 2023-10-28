using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalking : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    void Start()
    {
        player = GameObject.Find("Dragon");
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        var difference = player.transform.position - transform.position;
        transform.rotation =
            Quaternion.Euler(0, 0, (float)(Math.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) - 90);
    }
}