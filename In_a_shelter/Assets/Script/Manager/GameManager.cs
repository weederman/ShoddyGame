using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool tutorial = false;
    public int survivalDays = 0;
    public int Food = 0;
    public int Material = 0;
    public int Medical = 0;
    public bool[] NPC;

    private FadeInOutManager FadeInOutManager;
    private string currentSceneName;
    private float timeElapsed = 0f;

    public int hour = 6;
    public int minute = 0;

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
        ResetTimeToMorning();
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        // 매 5초마다 시간 증가
        if (timeElapsed >= 2f)
        {
            timeElapsed = 0f;
            minute += 10;

            if (minute >= 60)
            {
                minute = 0;
                hour += 1;
            }

            Debug.Log($"시간: {hour}시 {minute}분");
        }

        // 22시가 되면 새로운 날로 넘어가고 시간 초기화
        if (hour == 22)
        {
            FadeInOutManager.NextDay();//날이 넘을때마다 식량 줄어들기, 식량이 0인 상태로 이틀이 지나면 게임오버
            ResetTimeToMorning();
        }
    }

    public void GameOver()
    {
        tutorial = false;
        survivalDays = 0;
        Food = 0;
        Material = 0;
        Medical = 0;
        ResetTimeToMorning();
    }

    public void ResetTimeToMorning()
    {
        hour = 6;
        minute = 0;
    }
}