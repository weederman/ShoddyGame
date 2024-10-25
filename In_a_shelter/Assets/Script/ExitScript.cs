using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitScript : MonoBehaviour
{
    public GameObject interactionKeyImage; // F 키 이미지를 위한 UI Image
    private bool isPlayerInRange = false; // 플레이어가 범위 내에 있는지 확인
    public GameObject ExitPanel;
    void Start()
    {
        interactionKeyImage.gameObject.SetActive(false); // F 키 이미지를 비활성화
        ExitPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            ExitPanel.SetActive(true);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어가 콜라이더에 들어올 때
        if (collision.CompareTag("Player")) // 플레이어 태그가 필요
        {
            isPlayerInRange = true;
            interactionKeyImage.gameObject.SetActive(true); // F 키 이미지 표시
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // 플레이어가 콜라이더에서 나갈 때
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ExitPanel.SetActive(false);
            interactionKeyImage.gameObject.SetActive(false); // F 키 이미지 숨기기
        }
    }
    public void Exit()
    {
        ExitPanel.SetActive(false );
        SceneManager.LoadScene("Shelter");
    }
    public void No()
    {
        ExitPanel.SetActive(false);
    }
}
