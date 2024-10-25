using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class NPC_Move : MonoBehaviour
{
    private GameObject player; // �÷��̾� ��ü
    private NavMeshAgent agent; // NavMeshAgent ����
    private Animator animator;
    public float roamRadius = 5f; // ��ȸ �ݰ�
    public float moveInterval = 5f; // �̵� ���� (��)
    private Vector3 roamTarget; // ��ȸ�� ��ǥ ��ġ
    bool walk = false;
    SpriteRenderer playerRenderer;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player"); // �±׷� �÷��̾� ã��
        playerRenderer = this.GetComponent<SpriteRenderer>();
        StartCoroutine(RoamCoroutine());
    }
    private void Update()
    {
        if (agent.velocity.x > 0) // ���������� �̵� ��
        {
            playerRenderer.flipX = true;
        }
        else if (agent.velocity.x < 0) // �������� �̵� ��
        {
            playerRenderer.flipX = false;
        }
        // �ִϸ��̼� ó��
        if (agent.velocity.sqrMagnitude > 0.01f && !walk)
        {
            walk = true;
            animator.SetBool("Walk", true);  // �ȴ� �ִϸ��̼� Ʈ����
        }
        else if (agent.velocity.sqrMagnitude <= 0.01f && walk)
        {
            walk = false;
            animator.SetBool("Walk", false);  // ���� �ִϸ��̼� Ʈ����
        }
    }
    IEnumerator RoamCoroutine()
    {
        while (true)
        {
            // ��ȸ ��ǥ ���� �� �̵�
            yield return new WaitForSeconds(Random.Range(1f, moveInterval)); // �̵� ���ݸ�ŭ ���
            agent.SetDestination(roamTarget);

            SetRoamTarget(); // ���ο� ��ǥ ����
        }
    }
    void SetRoamTarget()
    {
        // ��ȸ�� ������ ��ǥ ����
        float randomX = Random.Range(-roamRadius, roamRadius);
        float randomY = Random.Range(-roamRadius, roamRadius);
        roamTarget = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
    }
}
