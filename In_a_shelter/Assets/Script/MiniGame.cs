using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    public GameObject MinigamePanel;
    void Start()
    {
        MinigamePanel = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Time.timeScale = 1.0f;
            MinigamePanel.SetActive(false);

        }
    }
}
