using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Move : MonoBehaviour
{
    public float moveSpeed = 2f;   // NPC �̵� �ӵ�
    public float moveTime;         // NPC�� �� �������� �̵��ϴ� �ð�
    public float waitTime;         // NPC�� ����ϴ� �ð�

    private Vector2 movementDirection;
    private bool isMoving = false;

    // ���� ��ܰ� ���� �ϴ� ������Ʈ�� Transform
    public Transform topLeftBoundary;   // ���� ����� ��ġ
    public Transform bottomRightBoundary; // ���� �ϴ��� ��ġ

    private Vector2 centerPosition;  // ���� �߾� ��ġ

    void Start()
    {
        // ���� �߾� ��ǥ ���
        topLeftBoundary = GameObject.Find("TopLeft").transform;
        bottomRightBoundary = GameObject.Find("BottomRight").transform;
        centerPosition = new Vector2(
            (topLeftBoundary.position.x + bottomRightBoundary.position.x) / 2,
            (topLeftBoundary.position.y + bottomRightBoundary.position.y) / 2
        );

        StartCoroutine(MoveNPC());
    }

    void Update()
    {
        if (isMoving)
        {
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime);

            // NPC�� ������ ������� üũ
            if (!IsWithinBounds(transform.position))
            {
                // ������ ����� �߾� ��ó�� �̵�
                StartCoroutine(ReturnToCenter());
            }
        }
    }

    IEnumerator MoveNPC()
    {
        while (true)
        {
            if (!isMoving)
            {
                // NPC�� ������ �������� �̵�
                movementDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                isMoving = true;
                moveTime = Random.Range(0, 5f);

                yield return new WaitForSeconds(moveTime);
                isMoving = false;

                waitTime = Random.Range(0, 5f);
                yield return new WaitForSeconds(waitTime);
            }
        }
    }

    // ���� �ȿ� �ִ��� Ȯ���ϴ� �Լ�
    bool IsWithinBounds(Vector2 position)
    {
        return position.x >= topLeftBoundary.position.x && position.x <= bottomRightBoundary.position.x &&
               position.y <= topLeftBoundary.position.y && position.y >= bottomRightBoundary.position.y;
    }

    // NPC�� ������ ����� �� �߾����� �̵��ϴ� �ڷ�ƾ
    IEnumerator ReturnToCenter()
    {
        isMoving = false;  // �̵��� �ߴ�
        Vector2 directionToCenter = (centerPosition - (Vector2)transform.position).normalized;

        // �߾����� ���� �ð� ���� �̵�
        float returnTime = 1f;  // �߾����� �̵��ϴ� �� �ɸ��� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < returnTime)
        {
            transform.Translate(directionToCenter * moveSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
    }
}
