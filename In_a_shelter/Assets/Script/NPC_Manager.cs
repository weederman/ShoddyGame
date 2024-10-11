using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Manager : MonoBehaviour
{
    /*
    public List<GameObject> NpcPrefabs = new List<GameObject>(); // NPC ������ ����Ʈ
    public List<GameObject> inGame_npc = new List<GameObject>(); // ������ NPC ����Ʈ
    */
    public Transform spawnLocation; // NPC�� ������ ��ġ
    public GameObject[] npcs;
    private int lastSurvivalDay = 0;

    void Start()
    {
        //LoadNpcPrefabs(); // ������ �� ������ �ҷ�����
        lastSurvivalDay = GameManager.Instance.survivalDays;
        SpawnNpc();
    }

    void Update()
    {

        if (GameManager.Instance.survivalDays > lastSurvivalDay)
        {
            
            lastSurvivalDay = GameManager.Instance.survivalDays;
        }
    }

    /*
    //NPC ������ �ҷ�����
    void LoadNpcPrefabs()
    {
        Object[] npcs = Resources.LoadAll("NPC", typeof(GameObject)); // NPCs �������� ������ �ҷ�����
        Debug.Log($"�ҷ����� npc: {npcs.Length}");

        foreach (Object npc in npcs)
        {
            NpcPrefabs.Add(npc as GameObject); // ����Ʈ�� �߰�
        }
    }
    */
    void SpawnNpc()
    {
        for(int i = 0; i < 5; i++)
        {
            if (GameManager.Instance.NPC[i])
            {
                Instantiate(npcs[i], spawnLocation.position,Quaternion.identity);
            }
        }
    }

    // NPC�� ����
    /*public void SpawnNpc()
    {
        if (NpcPrefabs.Count == 0)
        {
            Debug.LogError("NPC ������ ����");
            return;
        }

        int index = Random.Range(0, NpcPrefabs.Count); // �������� NPC ����
        GameObject npc = Instantiate(NpcPrefabs[index], spawnLocation.position, Quaternion.identity); // NPC ����
        inGame_npc.Add(npc);
        Debug.Log("NPC ����: " + npc.name);
    }*/
}