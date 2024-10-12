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

        // 오브젝트가 반경 안에 들어왔는지 확인
        if (distance <= radius)
        {
            // f_Img를 활성화
            f_Img.gameObject.SetActive(true);

            //f를 누르면 인벤트 발생
            if (Input.GetKeyDown(KeyCode.F) && !logManager.isDialogue)
            {
                logManager.ShowDialogue(randomDialogue());
            }
        }
        else
        {
            // f_Img를 비활성화
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
