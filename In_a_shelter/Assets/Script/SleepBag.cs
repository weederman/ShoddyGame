using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepBag : MonoBehaviour
{
    public DialogueManager logManager;
    public GameObject targetObject;
    public GameObject f_Img;
    public float radius;
    // Start is called before the first frame update
    void Start()
    {
        targetObject = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        insideBox();
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
            if(Input.GetKeyDown(KeyCode.F)&&!logManager.isDialogue)
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
    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            f_Img.gameObject.SetActive(true);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("�ȿ� �̾�");
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                logManager.ShowDialogue(this.gameObject.name);
                Debug.Log("IF�� ����");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            f_Img.gameObject.SetActive(false);
    }*/
}
