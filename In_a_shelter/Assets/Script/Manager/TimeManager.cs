using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text Timer;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            int hour = GameManager.Instance.hour;
            int minute = GameManager.Instance.minute;
            UpdateTimerText(hour, minute);
        }
    }

    private void UpdateTimerText(int hour, int minute)
    {
        if (Timer != null)
        {
            Timer.text = hour.ToString("D2") + ":" + minute.ToString("D2");
        }
    }
}