using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_RandomChat : MonoBehaviour
{
    public DialogueManager logManager;
    public GameObject targetObject;
    public GameObject f_Img;
    public float radius;
    public bool event_issued = false;
    int tmp_surv_day;
    public cvsReader reader;  // CSVReader�� ���� �����͸� �ҷ���
    int count;
    public List<Dictionary<string, object>> rand_chat { get; private set; }

    // num �ʵ� ����
    private List<int> numList = new List<int>(); // num �� ����

    void Start()
    {
        targetObject = GameObject.Find("Player");
        tmp_surv_day = GameManager.Instance.survivalDays;

        // �ʱ� ��ȭ ���� ����
        count = logManager.count;
        rand_chat = reader.data_RandomChat;

        // num ����Ʈ ������Ʈ
        UpdateNumList();

        // ó�� �����͸� CSV���� �ҷ��� log�� �ݿ�
        UpdateDialogueFromChat();
    }

    void Update()
    {
        // ���� ��縦 ������Ʈ�ϵ��� üũ
        if (tmp_surv_day != GameManager.Instance.survivalDays)
        {
            event_issued = false;
            tmp_surv_day = GameManager.Instance.survivalDays;
            UpdateDialogueFromChat(); // ���ο� ���� ��ȭ ������ ������Ʈ
        }

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
                logManager.ShowDialogue(this.gameObject.name);
            }
        }
        else
        {
            f_Img.gameObject.SetActive(false);
        }
    }

    // CSV���� �����͸� ������ DialogueManager�� log�� ������Ʈ
    private void UpdateDialogueFromChat()
    {
        if (rand_chat == null || rand_chat.Count == 0)
        {
            Debug.LogError("���� ��ȭ ������ ����");
            return;
        }

        // �����ϰ� num ���� ����
        int randomNum = Random.Range(0, numList.Count);
        int selectedNum = numList[randomNum];

        // ���õ� num ���� ��ġ�ϴ� ��ȭ ������ ������
        List<Dictionary<string, object>> selectedDialogueGroup = GetDialogueGroupByNum(selectedNum);

        // logManager�� log ũ�⸦ CSV ������ ũ�⿡ ���� �������� ����
        logManager.SetLogLength(selectedDialogueGroup.Count);

        // DialogueManager�� log�� isSelection�� ������Ʈ
        for (int i = 0; i < selectedDialogueGroup.Count; i++)
        {
            logManager.log[i].title = (string)selectedDialogueGroup[i]["name"];         // CSV�� "name" �ʵ�
            logManager.log[i].dialogue = (string)selectedDialogueGroup[i]["dialogue"];   // CSV�� "dialogue" �ʵ�
            logManager.log[i].selection1Text = (string)selectedDialogueGroup[i]["selection_dialouge"]; // CSV�� ������1 �ʵ�

            // CSV�� is_selection ���� DialogueManager�� isSelection�� �ݿ�
            if (selectedDialogueGroup[i].ContainsKey("is_selection"))
            {
                string isSelectionStr = selectedDialogueGroup[i]["is_selection"].ToString();

                // bool ������ �����ϰ� ��ȯ
                if (bool.TryParse(isSelectionStr, out bool isSelection))
                {
                    logManager.isSelection = isSelection;
                }
                else
                {
                    Debug.LogError($"'is_selection' �ʵ� �� '{isSelectionStr}'�� ��ȿ�� bool ���� �ƴմϴ�.");
                }
            }

            if (i + 1 < selectedDialogueGroup.Count)
            {
                logManager.log[i].selection2Text = (string)selectedDialogueGroup[i + 1]["selection_dialouge"]; // CSV�� ������2 �ʵ�
            }
        }

        Debug.Log("��ȭ ���� ������Ʈ �Ϸ�");
    }


    // num �ʵ� ���� ����Ʈ�� ������Ʈ�ϴ� �Լ�
    private void UpdateNumList()
    {
        numList.Clear(); // �ʱ�ȭ
        foreach (var chat in rand_chat)
        {
            if (chat.ContainsKey("num"))
            {
                if (int.TryParse(chat["num"].ToString(), out int num)) // �����ϰ� num �ʵ带 ������ ��ȯ
                {
                    if (!numList.Contains(num))
                    {
                        numList.Add(num); // �ߺ����� �ʰ� num ����Ʈ�� �߰�
                    }
                }
                else
                {
                    Debug.LogError($"'{chat["num"]}'�� ��ȿ�� ���ڰ� �ƴմϴ�.");
                }
            }
        }
    }

    // �־��� num ���� �ش��ϴ� ��ȭ ���� ��ȯ
    private List<Dictionary<string, object>> GetDialogueGroupByNum(int num)
    {
        List<Dictionary<string, object>> selectedGroup = new List<Dictionary<string, object>>();

        foreach (var chat in rand_chat)
        {
            if (chat.ContainsKey("num"))
            {
                if (int.TryParse(chat["num"].ToString(), out int chatNum) && chatNum == num)
                {
                    selectedGroup.Add(chat); // num ���� ��ġ�ϴ� ��ȭ���� �׷쿡 �߰�
                }
            }
        }

        return selectedGroup;
    }
}
