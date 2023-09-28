using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject textPanel;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void CloseTextPanel()
    {
        textPanel.SetActive(false);
    }
}