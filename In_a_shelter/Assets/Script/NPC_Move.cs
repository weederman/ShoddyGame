using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements.Experimental;

public class NPC_Move : MonoBehaviour
{
    private GameObject player; // �÷��̾� ��ü
    private NavMeshAgent agent; // NavMeshAgent ����

    public float roamRadius = 5f; // ��ȸ �ݰ�
    public float moveInterval = 2f; // �̵� ���� (��)
    private Vector3 roamTarget; // ��ȸ�� ��ǥ ��ġ
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.FindWithTag("Player"); // �±׷� �÷��̾� ã��

        StartCoroutine(RoamCoroutine());
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
