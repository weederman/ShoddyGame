using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ExitScript : MonoBehaviour
{
    public GameObject interactionKeyImage; // F Ű �̹����� ���� UI Image
    private bool isPlayerInRange = false; // �÷��̾ ���� ���� �ִ��� Ȯ��
    public GameObject ExitPanel;
    void Start()
    {
        interactionKeyImage.gameObject.SetActive(false); // F Ű �̹����� ��Ȱ��ȭ
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
            ExitPanel.SetActive(false);
            interactionKeyImage.gameObject.SetActive(false); // F Ű �̹��� �����
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
