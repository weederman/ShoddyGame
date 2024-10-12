using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private GameObject player; // 플레이어 객체
    private NavMeshAgent agent; // NavMeshAgent 참조
    private CircleCollider2D circleCollider; // 원의 콜라이더 참조

    private bool chasing; // 플레이어를 쫓고 있는지 여부
    public float roamRadius = 5f; // 배회 반경
    public float moveInterval = 2f; // 이동 간격 (초)
    private Vector3 roamTarget; // 배회할 목표 위치

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        circleCollider = GetComponentInChildren<CircleCollider2D>(); // 하위 콜라이더 가져오기
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.FindWithTag("Player"); // 태그로 플레이어 찾기
        chasing = false; // 초기값 false로 설정
        SetRoamTarget(); // 배회 목표 설정
        ZombieManager.Instance.RegisterZombie(this); // 좀비 매니저에 등록

        StartCoroutine(RoamCoroutine());
    }

    void Update()
    {
        if (chasing)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    public void SetChasing(bool value, float duration)
    {
        chasing = value; // chasing 상태 설정
        if (value)
        {
            StartCoroutine(StopChasingAfterDelay(duration)); // 지연 후 chasing 중지
        }
    }

    private IEnumerator StopChasingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 지연 대기
        chasing = false; // chasing 상태 해제
        agent.ResetPath(); // 현재 경로 초기화
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            SetChasing(true, 5f); // 2초 동안 chasing
            ZombieManager.Instance.TriggerChaseAll(5f); // 모든 좀비를 chasing 상태로 전환
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player) // 플레이어가 트리거 안에 있는 동안
        {
            // 플레이어의 위치를 목표로 계속 설정
            agent.SetDestination(player.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player) // 플레이어가 트리거에서 나간 경우
        {
            chasing = false; // 쫓기 중지
            agent.ResetPath(); // 현재 경로를 초기화하여 정지
            StartCoroutine(ResumeRoamingAfterDelay(1f)); // 1초 후에 배회 재개
        }
    }

    IEnumerator RoamCoroutine()
    {
        while (true)
        {
            if (!chasing)
            {
                // 배회 목표 설정 및 이동
                yield return new WaitForSeconds(Random.Range(1f,moveInterval)); // 이동 간격만큼 대기
                agent.SetDestination(roamTarget);
                
                SetRoamTarget(); // 새로운 목표 설정
            }
            else
            {
                yield return null; // 쫓고 있는 경우 대기하지 않음
            }
        }
    }

    IEnumerator ResumeRoamingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 1초 대기
        SetRoamTarget(); // 새로운 배회 목표 설정
        StartCoroutine(RoamCoroutine()); // 배회 코루틴 재시작
    }

    void SetRoamTarget()
    {
        // 배회할 무작위 목표 설정
        float randomX = Random.Range(-roamRadius, roamRadius);
        float randomY = Random.Range(-roamRadius, roamRadius);
        roamTarget = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
    }
}




