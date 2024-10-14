using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static UnityEditorInternal.ReorderableList;

public class Trash_Move : MonoBehaviour,IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public static Vector2 DefaultPos;
    //private bool isDragging = false; // 드래그 중인지 여부를 나타내는 플래그
    private bool isClickAllowed = true; // 클릭이 허용되는지 여부를 나타내는 플래그
    private float clickCooldown = 0.1f; // 드래그 후 클릭을 무시할 시간 (초 단위)
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그가 시작될 때 실행될 코드
        Debug.Log("드래그가 시작되었습니다.");
        DefaultPos = this.transform.position;
        //isDragging = true; // 드래그 시작
    }
    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중일 때 실행될 코드
        Debug.Log("드래그 중입니다.");
        Vector2 currentPos = eventData.position;
        this.transform.position = currentPos;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // 드래그가 끝났을 때 실행될 코드
        Debug.Log("드래그가 끝났습니다.");
        Vector2 mousePos = Input.mousePosition;

        this.transform.position = mousePos;
        //isDragging = false; // 드래그 종료
        StartCoroutine(ResetClickCooldown()); // 클릭 쿨다운 시작
    }
    private IEnumerator ResetClickCooldown()
    {
        isClickAllowed = false;
        yield return new WaitForSeconds(clickCooldown);
        isClickAllowed = true;
    }
}
