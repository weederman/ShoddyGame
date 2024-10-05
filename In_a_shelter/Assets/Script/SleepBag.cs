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
    }

    // Update is called once per frame
    void Update()
    {
        insideBox();
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
            if(Input.GetKeyDown(KeyCode.F))
            {
                logManager.ShowDialogue(this.gameObject.name);
            }
        }
        else
        {
            // f_Img를 비활성화
            f_Img.gameObject.SetActive(false);
        }
    }
}
