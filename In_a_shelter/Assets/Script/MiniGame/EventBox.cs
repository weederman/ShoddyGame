using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventBox : MonoBehaviour
{
    public GameObject interactionKeyImage; // F Ű �̹����� ���� UI Image
    public GameObject miniGamePanel; // �̴ϰ��� �г�
    private bool isPlayerInRange = false; // �÷��̾ ���� ���� �ִ��� Ȯ��
    public Texture2D cursor;
    void Start()
    {
        // �ڽ� ������Ʈ���� F Ű �̹����� ã��
        //interactionKeyImage = GetComponentInChildren<GameObject>();
        interactionKeyImage.gameObject.SetActive(false); // F Ű �̹����� ��Ȱ��ȭ
        miniGamePanel.SetActive(false); // �̴ϰ��� �г� ��Ȱ��ȭ
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
                // �ð��� ����
                Time.timeScale = 0;
                // �̴ϰ��� �г� Ȱ��ȭ
                miniGamePanel.SetActive(true);
                Cursor.SetCursor(cursor, new Vector2(0, 0), 0);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾ �ݶ��̴��� ���� ��
        if (collision.CompareTag("Player")) // �÷��̾� �±װ� �ʿ�
        {
            isPlayerInRange = true;
            interactionKeyImage.gameObject.SetActive(true); // F Ű �̹��� ǥ��
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // �÷��̾ �ݶ��̴����� ���� ��
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionKeyImage.gameObject.SetActive(false); // F Ű �̹��� �����
        }
    }
}
