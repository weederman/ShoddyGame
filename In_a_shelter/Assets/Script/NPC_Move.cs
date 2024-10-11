using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Move : MonoBehaviour
{
    public float moveSpeed = 2f;   // NPC 이동 속도
    public float moveTime;         // NPC가 한 방향으로 이동하는 시간
    public float waitTime;         // NPC가 대기하는 시간

    private Vector2 movementDirection;
    private bool isMoving = false;

    // 좌측 상단과 우측 하단 오브젝트의 Transform
    public Transform topLeftBoundary;   // 좌측 상단의 위치
    public Transform bottomRightBoundary; // 우측 하단의 위치

    private Vector2 centerPosition;  // 범위 중앙 위치

    void Start()
    {
        // 범위 중앙 좌표 계산
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

            // NPC가 범위를 벗어났는지 체크
            if (!IsWithinBounds(transform.position))
            {
                // 범위를 벗어나면 중앙 근처로 이동
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
                // NPC가 랜덤한 방향으로 이동
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

    // 범위 안에 있는지 확인하는 함수
    bool IsWithinBounds(Vector2 position)
    {
        return position.x >= topLeftBoundary.position.x && position.x <= bottomRightBoundary.position.x &&
               position.y <= topLeftBoundary.position.y && position.y >= bottomRightBoundary.position.y;
    }

    // NPC가 범위를 벗어났을 때 중앙으로 이동하는 코루틴
    IEnumerator ReturnToCenter()
    {
        isMoving = false;  // 이동을 중단
        Vector2 directionToCenter = (centerPosition - (Vector2)transform.position).normalized;

        // 중앙으로 일정 시간 동안 이동
        float returnTime = 1f;  // 중앙으로 이동하는 데 걸리는 시간
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
