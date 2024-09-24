using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float playerMoveSpeed; // �̵� �ӵ��� ������ �� �ִ� ����

    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        MovePlayerToMouse();
    }

    private void MovePlayerToMouse()
    {
        // ���콺 ������ ��ư�� Ŭ������ ��
        if (Input.GetMouseButton(1))
        {
            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            isMoving = true;
        }

        // ������Ʈ�� ��ǥ �������� �̵� ���� ��
        if (isMoving)
        {
            // ���� ��ġ���� ��ǥ ���������� �Ÿ� ���
            float distance = Vector3.Distance(transform.position, targetPosition);

            // ��ǥ ������ �����ϸ� �̵� ����
            if (distance <= 0.1f)
            {
                isMoving = false;
            }
            else
            {
                // ��ǥ �������� �̵�
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, playerMoveSpeed * Time.deltaTime);
            }
        }
    }// ���콺 ��Ŭ������ �÷��̾ �����̴� �Լ�
}
