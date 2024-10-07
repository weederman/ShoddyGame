using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    //오브젝트 트리거
    public Transform targetObject;
    public RawImage f_Img;
    public float radius;

    //외각선
    public Material objectMaterial;
    public string outlineProperty = "_OutlineEnabled";

    void Start()
    {
        f_Img.gameObject.SetActive(false);
        objectMaterial.SetFloat(outlineProperty, 0f);
    }

    void Update()
    {
        insideBox();
    }

    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.position);

        // 오브젝트가 반경 안에 들어왔는지 확인
        if (distance <= radius)
        {
            // f_Img를 활성화
            f_Img.gameObject.SetActive(true);
            objectMaterial.SetFloat(outlineProperty, 1f);

            //f를 누르면 인벤트 발생
        }
        else
        {
            // f_Img를 비활성화
            f_Img.gameObject.SetActive(false);
            objectMaterial.SetFloat(outlineProperty, 0f);
        }
    }// 박스 사거리 안에 들어가면이미지랑 외각선 표시하는 함수
}
