using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Move : MonoBehaviour
{
    public Transform player; // �÷��̾� ��ġ
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ��� �ε巴�� ����� ���� ������ �ӵ�
    public Vector3 offset; // ī�޶� ������

    void LateUpdate()
    {
        
        if (player.position.x > -9.1f && player.position.x< 9.1f)
        {
            //Debug.Log(player.position.y);
            // ī�޶� ���� ��ġ ���
            Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z) + offset;

            // �ε巯�� �̵��� ���� Lerp ���
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
