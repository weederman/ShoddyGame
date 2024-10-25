using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int survivalDays = 0;  // 생존일 수
    public int Food = 0;
    public int Material = 0;
    public int Medical = 0;
    public bool[] NPC;

    public Text Timer;
    private int hour = 0;
    private int minute = 0;

    private FadeInOutManager FadeInOutManager;
    private string currentSceneName;

    private float timeElapsed = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("게임오브젝트생성");
    }

    private void Start()
    {
        FadeInOutManager = FindObjectOfType<FadeInOutManager>();
        currentSceneName = SceneManager.GetActiveScene().name;

        Time.timeScale = 1;
        hour = 6;
        UpdateTimerText();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= 5f)
        {
            timeElapsed = 0f;
            minute += 10;

            if (minute >= 60)
            {
                minute = 0;
                hour += 1;
            }

            UpdateTimerText();
            Debug.Log($"시간: {hour}시 {minute}분");
        }

        if(hour == 22)
        {
            FadeInOutManager.NextDay();
            hour = 6;
        }

        if(currentSceneName != "Shelter")
        {
            FadeInOutManager.NextDay();
            hour = 6;
        }
    }

    private void UpdateTimerText()
    {
        Timer.text = hour.ToString("D2") + ":" + minute.ToString("D2");
    }

    public void gameOver()
    {

    }
}