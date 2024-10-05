using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map_Object : MonoBehaviour
{
    // ������Ʈ Ʈ����
    public Transform targetObject;
    public GameObject f_Img;  // FŰ�� ���� ��ȣ�ۿ� ������ �̹���
    public GameObject f_map_out;
    public float radius;
    public UIManager UImanager;
    // ���� �̹��� UI (ū ����)
    public GameObject mapImage;  // ������ ǥ���� UI

    // �ܰ���
    public Material objectMaterial;
    public string outlineProperty = "_OutlineEnabled";

    private bool isPlayerNearby = false;  // �÷��̾ �ݰ� �ȿ� �ִ��� ����

    void Start()
    {
        // FŰ �̹��� �� ���� ��Ȱ��ȭ
        f_Img.gameObject.SetActive(false);
        f_map_out.gameObject.SetActive(false);
        mapImage.gameObject.SetActive(false);
        objectMaterial.SetFloat(outlineProperty, 0f);
    }

    void Update()
    {
        insideBox();

        // �÷��̾ ���� �ȿ� �ְ� F Ű�� ������ �� ������ ǥ��/��Ȱ��ȭ
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.F))
        {
            ToggleMap();
        }
    }

    // �÷��̾ �ݰ� �ȿ� �ִ��� Ȯ���ϴ� �Լ�
    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.position);

        // ������Ʈ�� �ݰ� �ȿ� ���Դ��� Ȯ��
        if (distance <= radius)
        {
            // f_Img�� Ȱ��ȭ
            f_Img.gameObject.SetActive(true);
            objectMaterial.SetFloat(outlineProperty, 1f);
            isPlayerNearby = true;
        }
        else
        {
            // f_Img�� ��Ȱ��ȭ
            f_Img.gameObject.SetActive(false);
            objectMaterial.SetFloat(outlineProperty, 0f);
            isPlayerNearby = false;

            // �ݰ��� ����� ������ �ݱ�
            if (mapImage.gameObject.activeSelf)
            {
                mapImage.gameObject.SetActive(false);
                f_map_out.gameObject.SetActive(false);
            }
        }
    }

    // ������ ����ϴ� �Լ� (�ѱ�/����)
    private void ToggleMap()
    {
        bool isActive = mapImage.gameObject.activeSelf;
        mapImage.gameObject.SetActive(!isActive);
        f_map_out.gameObject.SetActive(!isActive);
    }
}
