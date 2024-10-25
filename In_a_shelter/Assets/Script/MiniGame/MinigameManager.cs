using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public List<GameObject> spawnablePrefabs; // ���� ������ ������ ����Ʈ
    public List<GameObject> miniGamePanelPrefabs; // �̴ϰ��� �г� ������ ����Ʈ
    public int numberOfEventBoxes = 5; // ������ ������Ʈ ����
    public List<Vector2> spawnPositions; // �̸� ������ ���� ��ġ ����Ʈ
    private Canvas mainCanvas; // UI ĵ����
    public Transform parentObject; // ������ �θ� ������Ʈ

    void Start()
    {
        mainCanvas = FindObjectOfType<Canvas>();
        //parentObject = GameObject.Find("Object").transform; // "object" ������Ʈ�� �θ�� ����
        SpawnEventBoxes();
    }

    void SpawnEventBoxes()
    {
        for (int i = 0; i < numberOfEventBoxes; i++)
        {
            Vector2 selectedLocalPosition = spawnPositions[i];
            Vector3 selectedWorldPosition = parentObject.TransformPoint(selectedLocalPosition); // ���� ��ǥ�� ���� ��ǥ�� ��ȯ

            GameObject selectedPrefab = spawnablePrefabs[Random.Range(0, spawnablePrefabs.Count)];
            GameObject newSpawnedObject = Instantiate(selectedPrefab, selectedWorldPosition, Quaternion.identity, parentObject);

            // Y��ǥ�� ���� Order in Layer ���� (���� ��ǥ�� ��)
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
            float yPosition = localPosition.y; // ���� Y��ǥ ���
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
                spriteRenderer.sortingOrder = 0; // �⺻��
            }
        }
    }
}
