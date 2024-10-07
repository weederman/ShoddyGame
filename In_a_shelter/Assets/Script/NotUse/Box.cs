using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Box : MonoBehaviour
{
    //������Ʈ Ʈ����
    public Transform targetObject;
    public RawImage f_Img;
    public float radius;

    //�ܰ���
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

        // ������Ʈ�� �ݰ� �ȿ� ���Դ��� Ȯ��
        if (distance <= radius)
        {
            // f_Img�� Ȱ��ȭ
            f_Img.gameObject.SetActive(true);
            objectMaterial.SetFloat(outlineProperty, 1f);

            //f�� ������ �κ�Ʈ �߻�
        }
        else
        {
            // f_Img�� ��Ȱ��ȭ
            f_Img.gameObject.SetActive(false);
            objectMaterial.SetFloat(outlineProperty, 0f);
        }
    }// �ڽ� ��Ÿ� �ȿ� �����̹����� �ܰ��� ǥ���ϴ� �Լ�
}
