using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class NPC_Move : MonoBehaviour
{
    private GameObject player; // 플레이어 객체
    private NavMeshAgent agent; // NavMeshAgent 참조
    private Animator animator;
    public float roamRadius = 5f; // 배회 반경
    public float moveInterval = 5f; // 이동 간격 (초)
    private Vector3 roamTarget; // 배회할 목표 위치
    bool walk = false;
    SpriteRenderer playerRenderer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player"); // 태그로 플레이어 찾기
        playerRenderer = this.GetComponent<SpriteRenderer>();
        StartCoroutine(RoamCoroutine());
    }
    private void Update()
    {
        if (agent.velocity.x > 0) // 오른쪽으로 이동 중
        {
            playerRenderer.flipX = true;
        }
        else if (agent.velocity.x < 0) // 왼쪽으로 이동 중
        {
            playerRenderer.flipX = false;
        }
        // 애니메이션 처리
        if (agent.velocity.sqrMagnitude > 0.01f && !walk)
        {
            walk = true;
            animator.SetBool("Walk", true);  // 걷는 애니메이션 트리거
        }
        else if (agent.velocity.sqrMagnitude <= 0.01f && walk)
        {
            walk = false;
            animator.SetBool("Walk", false);  // 정지 애니메이션 트리거
        }
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
