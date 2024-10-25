using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventBox : MonoBehaviour
{
    public GameObject interactionKeyImage; // F 키 이미지를 위한 UI Image
    public GameObject miniGamePanel; // 미니게임 패널
    private bool isPlayerInRange = false; // 플레이어가 범위 내에 있는지 확인
    public Texture2D cursor;
    void Start()
    {
        // 자식 오브젝트에서 F 키 이미지를 찾음
        //interactionKeyImage = GetComponentInChildren<GameObject>();
        interactionKeyImage.gameObject.SetActive(false); // F 키 이미지를 비활성화
        miniGamePanel.SetActive(false); // 미니게임 패널 비활성화
    }

    void Update()
    {
        if (miniGamePanel == null)
        {
            //Destroy(gameObject);
        }
        else
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
            {
                // 시간을 멈춤
                Time.timeScale = 0;
                // 미니게임 패널 활성화
                miniGamePanel.SetActive(true);
                Cursor.SetCursor(cursor, new Vector2(0, 0), 0);
            }
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
            interactionKeyImage.gameObject.SetActive(false); // F 키 이미지 숨기기
        }
    }
}
