using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Move_Nav : MonoBehaviour
{
    private NavMeshAgent agent;
    public Camera cam;
    private Vector2 targetPosition;

    void Start()
    {
        agent=GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ�� üũ
        {
            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // �÷��̾ ��ǥ ��ġ�� �̵�
        agent.SetDestination(targetPosition);
    }
}
