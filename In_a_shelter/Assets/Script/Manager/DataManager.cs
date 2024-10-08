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
            Debug.Log("DataManager �ν��Ͻ��� �����Ǿ���, sceneLoaded �̺�Ʈ�� �����Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("�̹� �����ϴ� DataManager �ν��Ͻ��� �־ ���ο� �ν��Ͻ��� �ı��մϴ�.");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"OnSceneLoaded ȣ���: {scene.name}");
        if (scene.name == "Shelter")
        {
            LoadGameData();
            Debug.Log("���� �ε� �Ϸ�");
        }
    }

    public void LoadGameData()
    {
        string filePath = Path.Combine(Application.dataPath, GameDataFileName); // ���� ��ġ

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath); // ���� �ҷ�����
            data = JsonUtility.FromJson<Data>(FromJsonData);
            Debug.Log($"������ �ҷ����� �Ϸ�: {filePath}");

            // GameManager�� ���� ����
            if (GameManager.Instance != null)
            {
                GameManager.Instance.survivalDays = data.survivalDays;
                GameManager.Instance.Food = data.Food;
                GameManager.Instance.Material = data.Material;
                GameManager.Instance.Medical = data.Medical;
                Debug.Log("GameManager �������� ���������� ������Ʈ�Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogError("GameManager �ν��Ͻ��� �������� �ʽ��ϴ�.");
            }
        }
        else
        {
            Debug.LogWarning("����� ���� �����Ͱ� �����ϴ�. �⺻���� ����մϴ�.");
            // �� ������ ������ �� �ʱ�ȭ�� �ʿ��ϸ� ���⼭ ����
        }
    }

    public void SaveGameData()
    {
        // GameManager�� ���� ���� Data�� ����
        data.survivalDays = GameManager.Instance.survivalDays;
        data.Food = GameManager.Instance.Food;
        data.Material = GameManager.Instance.Material;
        data.Medical = GameManager.Instance.Medical;

        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Path.Combine(Application.dataPath, GameDataFileName); // ���� ���丮�� ����

        File.WriteAllText(filePath, ToJsonData);
        Debug.Log($"������ ���� �Ϸ�: {filePath}");
    }

    public void InitializeNewGame()
    {
        data.survivalDays = 0;
        data.Food = 0;
        data.Material = 0;
        data.Medical = 0;

        // GameManager�� ������ �ʱ�ȭ
        if (GameManager.Instance != null)
        {
            GameManager.Instance.survivalDays = data.survivalDays;
            GameManager.Instance.Food = data.Food;
            GameManager.Instance.Material = data.Material;
            GameManager.Instance.Medical = data.Medical;
        }

        SaveGameData(); // �ʱ� ���¸� ����
    }
}
