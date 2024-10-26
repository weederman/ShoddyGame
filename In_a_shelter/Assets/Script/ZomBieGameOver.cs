using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomBieGameOver : MonoBehaviour
{
    private GameObject player; // �÷��̾� ��ü
    public GameObject gameoverpanel;
    void Start() // Start �޼��� �߰�
    {
        player = GameObject.FindWithTag("Player"); // �±׷� �÷��̾� ã��
        //gameoverpanel = GameObject.Find("GameOverPanel");
    }

    private void OnTriggerEnter2D(Collider2D other) // �Ű����� �̸� ����
    {
        if (other.gameObject == player) // �� ������ ����
        {
            Debug.Log("����");
            Time.timeScale = 0f;
            gameoverpanel.SetActive(true);
        }
    }
}
