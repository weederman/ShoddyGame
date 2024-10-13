using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditorInternal.ReorderableList;

public class Trash_Move : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public static Vector2 DefaultPos;
    //private bool isDragging = false; // �巡�� ������ ���θ� ��Ÿ���� �÷���
    private bool isClickAllowed = true; // Ŭ���� ���Ǵ��� ���θ� ��Ÿ���� �÷���
    private float clickCooldown = 0.1f; // �巡�� �� Ŭ���� ������ �ð� (�� ����)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�װ� ���۵� �� ����� �ڵ�
        Debug.Log("�巡�װ� ���۵Ǿ����ϴ�.");
        DefaultPos = this.transform.position;
        //isDragging = true; // �巡�� ����
    }
    public void OnDrag(PointerEventData eventData)
    {
        // �巡�� ���� �� ����� �ڵ�
        Debug.Log("�巡�� ���Դϴ�.");
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // �巡�װ� ������ �� ����� �ڵ�
        Debug.Log("�巡�װ� �������ϴ�.");
        Vector2 mousePos = Input.mousePosition;

        this.transform.position = mousePos;
        //isDragging = false; // �巡�� ����
        StartCoroutine(ResetClickCooldown()); // Ŭ�� ��ٿ� ����
    }
    private IEnumerator ResetClickCooldown()
    {
        isClickAllowed = false;
        yield return new WaitForSeconds(clickCooldown);
        isClickAllowed = true;
    }
}
