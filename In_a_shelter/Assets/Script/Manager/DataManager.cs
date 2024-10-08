using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;


public class DataManager : MonoBehaviour
{
    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                GameObject container = new GameObject("DataManager");
                instance = container.AddComponent<DataManager>();
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    string GameDataFileName = "GameData.json";
    public Data data = new Data();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("DataManager 인스턴스가 생성되었고, sceneLoaded 이벤트에 구독되었습니다.");
        }
        else
        {
            Debug.Log("이미 존재하는 DataManager 인스턴스가 있어서 새로운 인스턴스를 파괴합니다.");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded 호출됨: {scene.name}");
        if (scene.name == "Shelter")
        {
            LoadGameData();
            Debug.Log("게임 로드 완료");
        }
    }

    public void LoadGameData()
    {
        string filePath = Path.Combine(Application.dataPath, GameDataFileName); // 파일 위치

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath); // 파일 불러오기
            data = JsonUtility.FromJson<Data>(FromJsonData);
            Debug.Log($"데이터 불러오기 완료: {filePath}");

            // GameManager의 변수 설정
            if (GameManager.Instance != null)
            {
                GameManager.Instance.survivalDays = data.survivalDays;
                GameManager.Instance.Food = data.Food;
                GameManager.Instance.Material = data.Material;
                GameManager.Instance.Medical = data.Medical;
                Debug.Log("GameManager 변수들이 성공적으로 업데이트되었습니다.");
            }
            else
            {
                Debug.LogError("GameManager 인스턴스가 존재하지 않습니다.");
            }
        }
        else
        {
            Debug.LogWarning("저장된 게임 데이터가 없습니다. 기본값을 사용합니다.");
            // 새 게임을 시작할 때 초기화가 필요하면 여기서 설정
        }
    }

    public void SaveGameData()
    {
        // GameManager의 변수 값을 Data에 저장
        data.survivalDays = GameManager.Instance.survivalDays;
        data.Food = GameManager.Instance.Food;
        data.Material = GameManager.Instance.Material;
        data.Medical = GameManager.Instance.Medical;

        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.dataPath, GameDataFileName); // 하위 디렉토리에 저장

        File.WriteAllText(filePath, ToJsonData);
        Debug.Log($"데이터 저장 완료: {filePath}");
    }

    public void InitializeNewGame()
    {
        data.survivalDays = 0;
        data.Food = 0;
        data.Material = 0;
        data.Medical = 0;

        // GameManager의 변수도 초기화
        if (GameManager.Instance != null)
        {
            GameManager.Instance.survivalDays = data.survivalDays;
            GameManager.Instance.Food = data.Food;
            GameManager.Instance.Material = data.Material;
            GameManager.Instance.Medical = data.Medical;
        }

        SaveGameData(); // 초기 상태를 저장
    }
}
