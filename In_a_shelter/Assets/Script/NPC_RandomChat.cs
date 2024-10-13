using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_RandomChat : MonoBehaviour
{
    public DialogueManager logManager;
    public GameObject targetObject;
    public GameObject f_Img;
    public float radius;
    public bool event_issued = false;
    int tmp_surv_day;
    public cvsReader reader;  // csvReader�� ���� �����͸� �ҷ���
    string[,] chat;
    string dialogueText;
    string titleText;
    string selection1;
    string selection2;
    int count;
    bool is_selection;

    void Start()
    {
        tmp_surv_day = GameManager.Instance.survivalDays;

        // �ʱ� ��ȭ ���� ����
        count = logManager.count;
        UpdateDialogueFromChat();
    }

    // Update�� �� ������ ȣ��
    void Update()
    {
        if (tmp_surv_day != GameManager.Instance.survivalDays) event_issued = false;
        insideBox();  // �ݰ� üũ
    }

    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.transform.position);

        // ������Ʈ�� �ݰ� �ȿ� ���Դ��� Ȯ��
        if (distance <= radius)
        {
            f_Img.gameObject.SetActive(true);

            // F�� ������ �̺�Ʈ �߻�
            if (Input.GetKeyDown(KeyCode.F) && !logManager.isDialogue)
            {
                logManager.ShowDialogue(randomDialogue());
            }
        }
        else
        {
            f_Img.gameObject.SetActive(false);
        }
    }

    // ���� ��ȭ ����
    public string randomDialogue()
    {
        // �������� num ���� ���� (num ���� 0~3 ����)
        int randomNum = Random.Range(0, 4);
        string dialogue = "";
        int startIndex = -1;  // ���õ� num�� ���� �ε���
        int endIndex = -1;    // ���õ� num�� ������ �ε���

        // num�� ���� ���� �ε����� ������ �ε��� ����
        switch (randomNum)
        {
            case 0:
                startIndex = 0;
                endIndex = 3; // num�� 1�� �ٲ�� ������ (0~3)
                break;

            case 1:
                startIndex = 4;
                endIndex = 7; // num�� 2�� �ٲ�� ������ (4~7)
                break;

            case 2:
                startIndex = 8;
                endIndex = 11; // num�� 3���� �ٲ�� ������ (8~11)
                break;

            case 3:
                startIndex = 12;
                endIndex = chat.GetLength(0) - 1; // num�� ������ ��µǵ��� (12~��)
                break;
        }

        // ���õ� �ε��� ������ ��縦 ���������� ���
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (string.IsNullOrEmpty(chat[i, 0])) continue;  // num�� ���� �� ������ �ǳʶ�
            dialogue += chat[i, 2] + "\n";  // ��縦 �����ؼ� �߰�
        }

        return dialogue;  // ���õ� ��� ��ȯ
    }

    // chat �迭���� �����͸� ������ ��ȭ ������ ������Ʈ
    private void UpdateDialogueFromChat()
    {
        // chat �迭���� ���� �ε��Ͽ� logManager�� ����
        titleText = chat[count, 1];
        dialogueText = chat[count, 2];

        // is_selection ���� 0 �Ǵ� 1�� ó��
        is_selection = chat[count, 3] == "1";  // 1�̸� true, 0�̸� false�� ó��
        selection1 = chat[count, 4];
        selection2 = chat[count + 1, 4];

        // logManager�� ���� �ݿ�
        logManager.txt_title.text = titleText;
        logManager.txt_dialogue.text = dialogueText;
        logManager.txt_selection1.text = selection1;
        logManager.txt_selection2.text = selection2;
    }
}
