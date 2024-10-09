using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    public List<GameObject> NpcPrefabs = new List<GameObject>(); // NPC 프리팹 리스트
    public List<GameObject> inGame_npc = new List<GameObject>(); // 스폰된 NPC 리스트
    public Transform spawnLocation; // NPC가 스폰될 위치
    private int lastSurvivalDay = 0;

    void Start()
    {
        LoadNpcPrefabs(); // 시작할 때 프리팹 불러오기
        lastSurvivalDay = GameManager.Instance.survivalDays;
    }

    void Update()
    {

        if (GameManager.Instance.survivalDays > lastSurvivalDay)
        {
            SpawnNpc();
            lastSurvivalDay = GameManager.Instance.survivalDays;
        }
    }


    //NPC 프리랩 불러오기
    void LoadNpcPrefabs()
    {
        Object[] npcs = Resources.LoadAll("NPC", typeof(GameObject)); // NPCs 폴더에서 프리팹 불러오기
        Debug.Log($"불러와진 npc: {npcs.Length}");

        foreach (Object npc in npcs)
        {
            NpcPrefabs.Add(npc as GameObject); // 리스트에 추가
        }
    }

    // NPC를 스폰
    public void SpawnNpc()
    {
        if (NpcPrefabs.Count == 0)
        {
            Debug.LogError("NPC 프리팹 없음");
            return;
        }

        int index = Random.Range(0, NpcPrefabs.Count); // 랜덤으로 NPC 선택
        GameObject npc = Instantiate(NpcPrefabs[index], spawnLocation.position, Quaternion.identity); // NPC 스폰
        inGame_npc.Add(npc);
        Debug.Log("NPC 스폰: " + npc.name);
    }
}
