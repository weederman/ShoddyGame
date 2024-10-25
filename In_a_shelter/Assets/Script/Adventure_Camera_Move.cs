using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventure_Camera_Move : MonoBehaviour
{
    public Transform player; // �÷��̾� ��ġ
    public float smoothSpeed = 0.125f; // ī�޶� �̵� �ӵ��� �ε巴�� ����� ���� ������ �ӵ�
    public Vector3 offset; // ī�޶� ������

    public Vector2 TopLeft; // ������ ���� ��� ��ǥ
    public Vector2 BottomRight; // ������ ���� �ϴ� ��ǥ

    void LateUpdate()
    {
        Vector3 targetPosition = transform.position; // ���� ī�޶� ��ġ�� �⺻���� ����

        // �÷��̾��� X ��ǥ�� ���� ���� �ִ��� Ȯ��
        if (player.position.x >= TopLeft.x && player.position.x <= BottomRight.x)
        {
            targetPosition.x = player.position.x + offset.x; // X ��ǥ�� ���󰡱�
        }
        else
        {
            // ������ ����� TopLeft.x�� BottomRight.x ������ ��谪 ����
            targetPosition.x = Mathf.Clamp(targetPosition.x, TopLeft.x + offset.x, BottomRight.x + offset.x);
        }

        // �÷��̾��� Y ��ǥ�� ���� ���� �ִ��� Ȯ��
        if (player.position.y <= TopLeft.y && player.position.y >= BottomRight.y)
        {
            targetPosition.y = player.position.y + offset.y; // Y ��ǥ�� ���󰡱�
        }
        else
        {
            // ������ ����� TopLeft.y�� BottomRight.y ������ ��谪 ����
            targetPosition.y = Mathf.Clamp(targetPosition.y, BottomRight.y + offset.y, TopLeft.y + offset.y);
        }

        // �ε巯�� �̵��� ���� Lerp ���
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

