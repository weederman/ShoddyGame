using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int survivalDays = 0;  // 생존일 수
    public int Food = 0;
    public int Material = 0;
    private void Awake()
    {
        // Instance가 이미 존재하면 이 객체를 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
    }
}
