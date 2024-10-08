using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerr : MonoBehaviour
{
    // 시작 버튼
    public void StartGame()
    {
        Debug.Log("StartGame 버튼 클릭됨.");
        SceneManager.LoadScene("Shelter");
    }

    // 게임 로드 버튼
    public void LoadGame()
    {
        Debug.Log("LoadGame 버튼 클릭됨.");
        StartCoroutine(LoadGameCoroutine());
    }

    private IEnumerator LoadGameCoroutine()
    {
        Debug.Log("씬 로드 시작: Shelter");

        // 씬을 비동기적으로 로드
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Shelter");

        // 씬 로드가 완료될 때까지 대기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Debug.Log("씬 로드 완료: Shelter");

        // GameManager가 초기화되었는지 확인
        if (GameManager.Instance != null)
        {
            DataManager.Instance.LoadGameData();
            Debug.Log("게임 로드 완료");
        }
        else
        {
            Debug.LogError("GameManager 인스턴스가 씬 로드 후에도 존재하지 않습니다.");
        }
    }

    public void ExitGame()
    {
        Debug.Log("ExitGame 버튼 클릭됨. 게임 종료.");
        Application.Quit();
    }
}
