using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class NPC_Move : MonoBehaviour
{
    private GameObject player; // 플레이어 객체
    private NavMeshAgent agent; // NavMeshAgent 참조

    public float roamRadius = 5f; // 배회 반경
    public float moveInterval = 2f; // 이동 간격 (초)
    private Vector3 roamTarget; // 배회할 목표 위치
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.FindWithTag("Player"); // 태그로 플레이어 찾기

        StartCoroutine(RoamCoroutine());
    }
    IEnumerator RoamCoroutine()
    {
        while (true)
        {
            // 배회 목표 설정 및 이동
            yield return new WaitForSeconds(Random.Range(1f, moveInterval)); // 이동 간격만큼 대기
            agent.SetDestination(roamTarget);

            SetRoamTarget(); // 새로운 목표 설정
        }
    }
    void SetRoamTarget()
    {
        // 배회할 무작위 목표 설정
        float randomX = Random.Range(-roamRadius, roamRadius);
        float randomY = Random.Range(-roamRadius, roamRadius);
        roamTarget = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
    }
}
