using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public Transform player; // 플레이어 위치
    public float smoothSpeed = 0.125f; // 카메라 이동 속도를 부드럽게 만들기 위한 스무스 속도
    public Vector3 offset; // 카메라 오프셋

    void LateUpdate()
    {
        
        if (player.position.x > -9.1f && player.position.x< 9.1f)
        {
            //Debug.Log(player.position.y);
            // 카메라가 따라갈 위치 계산
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z) + offset;

            // 부드러운 이동을 위해 Lerp 사용
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
