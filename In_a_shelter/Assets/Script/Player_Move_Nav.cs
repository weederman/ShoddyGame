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
        if (Input.GetMouseButtonDown(1)) // 마우스 우클릭 체크
        {
            // 마우스 위치를 월드 좌표로 변환
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // 플레이어를 목표 위치로 이동
        agent.SetDestination(targetPosition);
    }
}
