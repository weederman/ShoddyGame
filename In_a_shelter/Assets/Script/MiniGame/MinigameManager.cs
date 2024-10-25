using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject eventBoxPrefab; // Event Box 프리팹
    public List<GameObject> miniGamePanelPrefabs; // 미니게임 패널 프리팹 리스트
    public int numberOfEventBoxes = 5; // 생성할 Event Box 개수
    public Vector2 spawnAreaMin; // 스폰 영역 최소 좌표
    public Vector2 spawnAreaMax; // 스폰 영역 최대 좌표
    private Canvas mainCanvas; // UI 캔버스

    void Start()
    {
        // Scene에 있는 Canvas를 찾습니다.
        mainCanvas = FindObjectOfType<Canvas>();
        SpawnEventBoxes();
    }

    void SpawnEventBoxes()
    {
        for (int i = 0; i < numberOfEventBoxes; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );

            GameObject newEventBox = Instantiate(eventBoxPrefab, randomPosition, Quaternion.identity);

            // 랜덤한 미니게임 패널을 Canvas의 자식으로 인스턴스화
            GameObject selectedMiniGamePanelPrefab = miniGamePanelPrefabs[Random.Range(0, miniGamePanelPrefabs.Count)];
            GameObject miniGamePanelInstance = Instantiate(selectedMiniGamePanelPrefab, mainCanvas.transform);

            // 미니게임 패널 비활성화 및 초기 위치 조정
            miniGamePanelInstance.SetActive(false);
            miniGamePanelInstance.transform.localPosition = Vector3.zero; // 화면 중앙에 위치
            miniGamePanelInstance.transform.localScale = Vector3.one;

            // EventBox에 미니게임 패널 참조 설정
            EventBox eventBoxScript = newEventBox.GetComponent<EventBox>();
            eventBoxScript.miniGamePanel = miniGamePanelInstance;
        }
    }
}
