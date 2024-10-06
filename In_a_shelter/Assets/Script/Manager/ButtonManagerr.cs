using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerr : MonoBehaviour
{
    // ���� ��ư
    public void StartGame()
    {
        SceneManager.LoadScene("Shelter");
    }

    // ���� �ε� ��ư
    public void LoadGame()
    {
        DateManager.Instance.LoadGameData();
        SceneManager.LoadScene("Shelter");
        Debug.Log("���� �ε�");
    }

    public void ExitGame()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}
