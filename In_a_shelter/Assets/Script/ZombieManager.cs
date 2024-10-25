using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    private static ZombieManager instance; // �̱��� �ν��Ͻ�
    private List<Zombie> zombies = new List<Zombie>(); // ��� ���� �����ϴ� ����Ʈ

    public static ZombieManager Instance
    {
        get
        {
            if (instance == null)
            {
                // ������ ZombieManager ��ü�� ã�� ������ �� ��� �޽���
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
            // DontDestroyOnLoad(gameObject); // �����Ͽ� �� ��ȯ �� ��ü �������� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �ν��Ͻ��� �����ϸ� ���� ������ ��ü �ı�
        }
    }

    public void RegisterZombie(Zombie zombie)
    {
        if (!zombies.Contains(zombie))
        {
            zombies.Add(zombie); // ���� ���
        }
    }

    public void TriggerChaseAll(float duration)
    {
        foreach (var zombie in zombies)
        {
            zombie.SetChasing(true, duration); // ��� ���� �ѵ��� ����
        }
    }
}
