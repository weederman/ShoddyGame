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
            Cursor.SetCursor(default, new Vector2(0, 0), 0);
        }

    }
    public void End_Minigame(int reward)
    {
        Time.timeScale = 1.0f;
        MinigamePanel.SetActive(false);
        Cursor.SetCursor(default, new Vector2(0,0),0);
        switch (reward)
        {
            case 0:
                GameManager.Instance.Food += 100;
                break;
            case 1:
                GameManager.Instance.Material += 100;
                break;
            case 2:
                GameManager.Instance.Medical += 100;
                break;
        }
    }
}
