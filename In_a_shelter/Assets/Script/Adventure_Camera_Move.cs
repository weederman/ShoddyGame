using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventure_Camera_Move : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    public float smoothSpeed = 0.125f; // 카메라 이동 속도를 부드럽게 만들기 위한 스무스 속도
    public Vector3 offset; // 카메라 오프셋

    public Vector2 TopLeft; // 범위의 좌측 상단 좌표
    public Vector2 BottomRight; // 범위의 우측 하단 좌표

    void LateUpdate()
    {
        Vector3 targetPosition = transform.position; // 현재 카메라 위치를 기본으로 설정

        // 플레이어의 X 좌표가 범위 내에 있는지 확인
        if (player.position.x >= TopLeft.x && player.position.x <= BottomRight.x)
        {
            targetPosition.x = player.position.x + offset.x; // X 좌표는 따라가기
        }
        else
        {
            // 범위를 벗어나면 TopLeft.x와 BottomRight.x 사이의 경계값 고정
            targetPosition.x = Mathf.Clamp(targetPosition.x, TopLeft.x + offset.x, BottomRight.x + offset.x);
        }

        // 플레이어의 Y 좌표가 범위 내에 있는지 확인
        if (player.position.y <= TopLeft.y && player.position.y >= BottomRight.y)
        {
            targetPosition.y = player.position.y + offset.y; // Y 좌표는 따라가기
        }
        else
        {
            // 범위를 벗어나면 TopLeft.y와 BottomRight.y 사이의 경계값 고정
            targetPosition.y = Mathf.Clamp(targetPosition.y, BottomRight.y + offset.y, TopLeft.y + offset.y);
        }

        // 부드러운 이동을 위해 Lerp 사용
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

