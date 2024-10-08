using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerr : MonoBehaviour
{
    // ���� ��ư
    public void StartGame()
    {
        Debug.Log("StartGame ��ư Ŭ����.");
        SceneManager.LoadScene("Shelter");
    }

    // ���� �ε� ��ư
    public void LoadGame()
    {
        Debug.Log("LoadGame ��ư Ŭ����.");
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        Debug.Log("�� �ε� ����: Shelter");

        // ���� �񵿱������� �ε�
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Shelter");

        // �� �ε尡 �Ϸ�� ������ ���
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("�� �ε� �Ϸ�: Shelter");

        // GameManager�� �ʱ�ȭ�Ǿ����� Ȯ��
        if (GameManager.Instance != null)
        {
            DataManager.Instance.LoadGameData();
            Debug.Log("���� �ε� �Ϸ�");
        }
        else
        {
            Debug.LogError("GameManager �ν��Ͻ��� �� �ε� �Ŀ��� �������� �ʽ��ϴ�.");
        }
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame ��ư Ŭ����. ���� ����.");
        Application.Quit();
    }
}
