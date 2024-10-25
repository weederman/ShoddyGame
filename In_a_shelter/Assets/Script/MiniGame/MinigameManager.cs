using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameManager : MonoBehaviour
{
    public GameObject eventBoxPrefab; // Event Box ������
    public List<GameObject> miniGamePanelPrefabs; // �̴ϰ��� �г� ������ ����Ʈ
    public int numberOfEventBoxes = 5; // ������ Event Box ����
    public Vector2 spawnAreaMin; // ���� ���� �ּ� ��ǥ
    public Vector2 spawnAreaMax; // ���� ���� �ִ� ��ǥ
    private Canvas mainCanvas; // UI ĵ����

    void Start()
    {
        // Scene�� �ִ� Canvas�� ã���ϴ�.
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

            // ������ �̴ϰ��� �г��� Canvas�� �ڽ����� �ν��Ͻ�ȭ
            GameObject selectedMiniGamePanelPrefab = miniGamePanelPrefabs[Random.Range(0, miniGamePanelPrefabs.Count)];
            GameObject miniGamePanelInstance = Instantiate(selectedMiniGamePanelPrefab, mainCanvas.transform);

            // �̴ϰ��� �г� ��Ȱ��ȭ �� �ʱ� ��ġ ����
            miniGamePanelInstance.SetActive(false);
            miniGamePanelInstance.transform.localPosition = Vector3.zero; // ȭ�� �߾ӿ� ��ġ
            miniGamePanelInstance.transform.localScale = Vector3.one;

            // EventBox�� �̴ϰ��� �г� ���� ����
            EventBox eventBoxScript = newEventBox.GetComponent<EventBox>();
            eventBoxScript.miniGamePanel = miniGamePanelInstance;
        }
    }
}
