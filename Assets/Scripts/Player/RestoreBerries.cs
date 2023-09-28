using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreBerries : MonoBehaviour
{
    // Start is called before the first frame update
    public float restoreCooldown;
    private float timeToRestore;
    public int currentBerriesCount;

    void Start()
    {
        timeToRestore = restoreCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeToRestore > 0 && currentBerriesCount < 5)
        {
            timeToRestore -= Time.deltaTime;
        }

        if (timeToRestore <= 0 && currentBerriesCount < 5)
        {
            gameObject.transform.GetChild(currentBerriesCount).gameObject.SetActive(true);
            currentBerriesCount += 1;
            timeToRestore = restoreCooldown;
        }
    }
}