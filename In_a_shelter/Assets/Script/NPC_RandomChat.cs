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
    void Start()
    {
        tmp_surv_day = GameManager.Instance.survivalDays;
    }

    // Update is called once per frame
    void Update()
    {
        if (tmp_surv_day != GameManager.Instance.survivalDays) event_issued = false;
    }
    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.transform.position);

        // ������Ʈ�� �ݰ� �ȿ� ���Դ��� Ȯ��
        if (distance <= radius)
        {
            // f_Img�� Ȱ��ȭ
            f_Img.gameObject.SetActive(true);

            //f�� ������ �κ�Ʈ �߻�
            if (Input.GetKeyDown(KeyCode.F) && !logManager.isDialogue)
            {
                logManager.ShowDialogue(randomDialogue());
            }
        }
        else
        {
            // f_Img�� ��Ȱ��ȭ
            f_Img.gameObject.SetActive(false);
        }
    }

    public string randomDialogue()
    {
        int random=Random.Range(0, 4);
        switch (random)
        {
            case 0:
                event_issued = true;
                return "NPC_Event1";
            case 1:
                return "NPC_Event2";
            case 2:
                return "NPC_Event3";
            case 3:
                return "";
            default:
                break;
        }
        return "NPC";
    }
}
