using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_Move_Nav : MonoBehaviour
{
    private NavMeshAgent agent;
    //public Camera cam;
    private Vector2 targetPosition;
    SpriteRenderer playerRenderer;
    GameObject[] objects;
    private Animator animator;
    bool walk = false;

    void Start()
    {
  

        objects = GameObject.FindGameObjectsWithTag("Obstacle");
        playerRenderer = this.GetComponent<SpriteRenderer>();
        //Debug.Log(objects.Length);
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (agent.velocity.x > 0) // ���������� �̵� ��
        {
            playerRenderer.flipX = true;
        }
        else if (agent.velocity.x < 0) // �������� �̵� ��
        {
            playerRenderer.flipX = false;
        }
        if (Input.GetMouseButtonDown(1)) // ���콺 ��Ŭ�� üũ
        {
            // ���콺 ��ġ�� ���� ��ǥ�� ��ȯ
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        // �÷��̾ ��ǥ ��ġ�� �̵�
        agent.SetDestination(targetPosition);
        // �ִϸ��̼� ó��
        if (agent.velocity.sqrMagnitude > 0.4f && !walk)
        {
            walk = true;
            Debug.Log("�ȱ� ����");
            animator.SetBool("Walk", true);  // �ȴ� �ִϸ��̼� Ʈ����
        }
        else if (agent.velocity.sqrMagnitude <= 0.4f && walk)
        {
            walk = false;
            Debug.Log("�ȱ� ����");
            animator.SetBool("Walk", false);  // ���� �ִϸ��̼� Ʈ����
        }
        foreach (GameObject obj in objects)
        {
            SpriteRenderer objectRenderer = obj.GetComponent<SpriteRenderer>();

            if (this.transform.position.y > obj.transform.position.y)
            {
                playerRenderer.sortingOrder = objectRenderer.sortingOrder - 1;
                //Debug.Log(obj.name);
            }
            else
            {
                //playerRenderer.sortingOrder = objectRenderer.sortingOrder + 1;
            }
        }
    }
}
