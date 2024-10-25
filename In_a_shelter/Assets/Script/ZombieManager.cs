using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private static ZombieManager instance; // 싱글톤 인스턴스
    private List<Zombie> zombies = new List<Zombie>(); // 모든 좀비를 저장하는 리스트

    public static ZombieManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 씬에서 ZombieManager 객체를 찾지 못했을 때 경고 메시지
                Debug.LogWarning("ZombieManager instance is null. Please ensure there is a ZombieManager in the scene.");
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject); // 삭제하여 씬 전환 시 객체 유지하지 않음
        }
        else
        {
            Destroy(gameObject); // 이미 인스턴스가 존재하면 새로 생성한 객체 파괴
        }
    }

    public void RegisterZombie(Zombie zombie)
    {
        if (!zombies.Contains(zombie))
        {
            zombies.Add(zombie); // 좀비 등록
        }
    }

    public void TriggerChaseAll(float duration)
    {
        foreach (var zombie in zombies)
        {
            zombie.SetChasing(true, duration); // 모든 좀비를 쫓도록 설정
        }
    }
}
