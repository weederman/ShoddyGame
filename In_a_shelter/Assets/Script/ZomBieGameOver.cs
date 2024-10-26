using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomBieGameOver : MonoBehaviour
{
    private GameObject player; // 플레이어 객체
    public GameObject gameoverpanel;
    void Start() // Start 메서드 추가
    {
        player = GameObject.FindWithTag("Player"); // 태그로 플레이어 찾기
        //gameoverpanel = GameObject.Find("GameOverPanel");
    }

    private void OnTriggerEnter2D(Collider2D other) // 매개변수 이름 수정
    {
        if (other.gameObject == player) // 비교 연산자 수정
        {
            Debug.Log("죽음");
            Time.timeScale = 0f;
            gameoverpanel.SetActive(true);
        }
    }
}
