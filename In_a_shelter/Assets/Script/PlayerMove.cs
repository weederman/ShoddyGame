using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerMoveSpeed; // 이동 속도를 조정할 수 있는 변수

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        MovePlayerToMouse();
    }

    private void MovePlayerToMouse()
    {
        // 마우스 오른쪽 버튼을 클릭했을 때
        if (Input.GetMouseButton(1))
        {
            // 마우스 위치를 월드 좌표로 변환
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            isMoving = true;
        }

        // 오브젝트가 목표 지점으로 이동 중일 때
        if (isMoving)
        {
            // 현재 위치에서 목표 지점까지의 거리 계산
            float distance = Vector3.Distance(transform.position, targetPosition);

            // 목표 지점에 도달하면 이동 중지
            if (distance <= 0.1f)
            {
                isMoving = false;
            }
            else
            {
                // 목표 지점으로 이동
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerMoveSpeed * Time.deltaTime);
            }
        }
    }// 마우스 우클릭으로 플레이어를 움직이는 함수
}
