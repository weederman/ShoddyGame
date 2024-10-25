using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public List<GameObject> spawnablePrefabs; // 스폰 가능한 프리팹 리스트
    public List<GameObject> miniGamePanelPrefabs; // 미니게임 패널 프리팹 리스트
    public int numberOfEventBoxes = 5; // 생성할 오브젝트 개수
    public List<Vector2> spawnPositions; // 미리 지정된 스폰 위치 리스트
    private Canvas mainCanvas; // UI 캔버스
    public Transform parentObject; // 스폰될 부모 오브젝트

    void Start()
    {
        mainCanvas = FindObjectOfType<Canvas>();
        //parentObject = GameObject.Find("Object").transform; // "object" 오브젝트를 부모로 설정
        SpawnEventBoxes();
    }

    void SpawnEventBoxes()
    {
        for (int i = 0; i < numberOfEventBoxes; i++)
        {
            Vector2 selectedLocalPosition = spawnPositions[i];
            Vector3 selectedWorldPosition = parentObject.TransformPoint(selectedLocalPosition); // 로컬 좌표를 월드 좌표로 변환

            GameObject selectedPrefab = spawnablePrefabs[Random.Range(0, spawnablePrefabs.Count)];
            GameObject newSpawnedObject = Instantiate(selectedPrefab, selectedWorldPosition, Quaternion.identity, parentObject);

            // Y좌표에 따라 Order in Layer 조정 (로컬 좌표로 비교)
            SetOrderInLayerBasedOnLocalYPosition(newSpawnedObject, selectedLocalPosition);

            GameObject selectedMiniGamePanelPrefab = miniGamePanelPrefabs[Random.Range(0, miniGamePanelPrefabs.Count)];
            GameObject miniGamePanelInstance = Instantiate(selectedMiniGamePanelPrefab, mainCanvas.transform);

            miniGamePanelInstance.SetActive(false);
            miniGamePanelInstance.transform.localPosition = Vector3.zero;
            miniGamePanelInstance.transform.localScale = Vector3.one;

            EventBox eventBoxScript = newSpawnedObject.GetComponent<EventBox>();
            if (eventBoxScript != null)
            {
                eventBoxScript.miniGamePanel = miniGamePanelInstance;
            }
        }
    }

    void SetOrderInLayerBasedOnLocalYPosition(GameObject obj, Vector2 localPosition)
    {
        SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            float yPosition = localPosition.y; // 로컬 Y좌표 사용
            if (yPosition >= 13.5f)
            {
                spriteRenderer.sortingOrder = 3;
            }
            else if (yPosition >= 9f)
            {
                spriteRenderer.sortingOrder = 6;
            }
            else if (yPosition >= 3f)
            {
                spriteRenderer.sortingOrder = 8;
            }
            else
            {
                spriteRenderer.sortingOrder = 0; // 기본값
            }
        }
    }
}
