using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int survivalDays = 0;  // ������ ��
    public int Food = 0;
    public int Material = 0;
    public int Medical = 0;
    public bool[] NPC;
    private void Awake()
    {
        // Instance�� �̹� �����ϸ� �� ��ü�� �ı�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        Debug.Log("���ӿ�����Ʈ����");
    } 
}
