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
        Debug.Log("���ӿ�����Ʈ����");
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

        // �� 5�ʸ��� �ð� ����
        if (timeElapsed >= 2f)
        {
            timeElapsed = 0f;
            minute += 10;

            if (minute >= 60)
            {
                minute = 0;
                hour += 1;
            }

            Debug.Log($"�ð�: {hour}�� {minute}��");
        }

        // 22�ð� �Ǹ� ���ο� ���� �Ѿ�� �ð� �ʱ�ȭ
        if (hour == 22)
        {
            FadeInOutManager.NextDay();//���� ���������� �ķ� �پ���, �ķ��� 0�� ���·� ��Ʋ�� ������ ���ӿ���
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