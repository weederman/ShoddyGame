using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    private GameObject player; // �÷��̾� ��ü
    private NavMeshAgent agent; // NavMeshAgent ����
    private CircleCollider2D circleCollider; // ���� �ݶ��̴� ����

    private bool chasing; // �÷��̾ �Ѱ� �ִ��� ����
    public float roamRadius = 5f; // ��ȸ �ݰ�
    public float moveInterval = 2f; // �̵� ���� (��)
    private Vector3 roamTarget; // ��ȸ�� ��ǥ ��ġ

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        circleCollider = GetComponentInChildren<CircleCollider2D>(); // ���� �ݶ��̴� ��������
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        player = GameObject.FindWithTag("Player"); // �±׷� �÷��̾� ã��
        chasing = false; // �ʱⰪ false�� ����
        SetRoamTarget(); // ��ȸ ��ǥ ����
        ZombieManager.Instance.RegisterZombie(this); // ���� �Ŵ����� ���

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
        chasing = value; // chasing ���� ����
        if (value)
        {
            StartCoroutine(StopChasingAfterDelay(duration)); // ���� �� chasing ����
        }
    }

    private IEnumerator StopChasingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // ���� ���
        chasing = false; // chasing ���� ����
        agent.ResetPath(); // ���� ��� �ʱ�ȭ
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            SetChasing(true, 5f); // 2�� ���� chasing
            ZombieManager.Instance.TriggerChaseAll(5f); // ��� ���� chasing ���·� ��ȯ
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player) // �÷��̾ Ʈ���� �ȿ� �ִ� ����
        {
            // �÷��̾��� ��ġ�� ��ǥ�� ��� ����
            agent.SetDestination(player.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player) // �÷��̾ Ʈ���ſ��� ���� ���
        {
            chasing = false; // �ѱ� ����
            agent.ResetPath(); // ���� ��θ� �ʱ�ȭ�Ͽ� ����
            StartCoroutine(ResumeRoamingAfterDelay(1f)); // 1�� �Ŀ� ��ȸ �簳
        }
    }

    IEnumerator RoamCoroutine()
    {
        while (true)
        {
            if (!chasing)
            {
                // ��ȸ ��ǥ ���� �� �̵�
                yield return new WaitForSeconds(Random.Range(1f,moveInterval)); // �̵� ���ݸ�ŭ ���
                agent.SetDestination(roamTarget);
                
                SetRoamTarget(); // ���ο� ��ǥ ����
            }
            else
            {
                yield return null; // �Ѱ� �ִ� ��� ������� ����
            }
        }
    }

    IEnumerator ResumeRoamingAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // 1�� ���
        SetRoamTarget(); // ���ο� ��ȸ ��ǥ ����
        StartCoroutine(RoamCoroutine()); // ��ȸ �ڷ�ƾ �����
    }

    void SetRoamTarget()
    {
        // ��ȸ�� ������ ��ǥ ����
        float randomX = Random.Range(-roamRadius, roamRadius);
        float randomY = Random.Range(-roamRadius, roamRadius);
        roamTarget = new Vector3(transform.position.x + randomX, transform.position.y + randomY, transform.position.z);
    }
}




