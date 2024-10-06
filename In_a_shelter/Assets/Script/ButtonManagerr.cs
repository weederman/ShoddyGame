using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerr : MonoBehaviour
{
    // 시작 버튼
    public void StartGame()
    {
        SceneManager.LoadScene("Shelter");
    }

    // 게임 로드 버튼
    public void LoadGame()
    {
        DateManager.Instance.LoadGameData();
        SceneManager.LoadScene("Shelter");
        Debug.Log("게임 로드");
    }

    public void ExitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
