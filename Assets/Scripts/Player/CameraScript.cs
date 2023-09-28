using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;

    void Start()
    {
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var playerPosition = player.transform.position;
        transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
    }
}