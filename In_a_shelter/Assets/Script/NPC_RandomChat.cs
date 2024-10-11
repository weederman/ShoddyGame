using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_RandomChat : MonoBehaviour
{
    public DialogueManager logManager;
    public GameObject targetObject;
    public GameObject f_Img;
    public float radius;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                logManager.ShowDialogue(this.gameObject.name);
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
        
        return "NPC";
    }
}
