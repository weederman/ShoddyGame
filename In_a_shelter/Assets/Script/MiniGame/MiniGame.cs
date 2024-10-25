using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    public GameObject MinigamePanel;
    public Slider noisebar;
    private double slideValue = 0f;
    public double gaugeSpeedMultiplier = 0.001f;
    public Vector3 previousMousePosition;
    void Start()
    {
        previousMousePosition = Input.mousePosition;
        MinigamePanel = this.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            slideValue = 0f;
            Time.timeScale = 1.0f;
            MinigamePanel.SetActive(false);
            Cursor.SetCursor(default, new Vector2(0, 0), 0);
        }
        Vector3 currentMousePosition = Input.mousePosition;
        
        float distance = Vector3.Distance(previousMousePosition, currentMousePosition);
        Debug.Log(distance);
        slideValue += distance * gaugeSpeedMultiplier;
        slideValue = Mathf.Clamp((float)slideValue,0,1000);
        slideValue -= 0.8f;
        noisebar.value = (float)slideValue;
        if(noisebar.value >= 999)
        {
            End_Minigame(3);
        }
        previousMousePosition = currentMousePosition;

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
            case 3:
                ZombieManager.Instance.TriggerChaseAll(10f);
                break;
        }
        Destroy(this.gameObject);
    }
}
