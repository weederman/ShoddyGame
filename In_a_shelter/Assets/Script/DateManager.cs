using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DateManager : MonoBehaviour
{
    static GameObject container; //������ ���� ������Ʈ
    static DateManager instance;
    public static DateManager Instance //�̱��� ����
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";

                instance = container.AddComponent(typeof(DateManager)) as DateManager;
                DontDestroyOnLoad(container); //������ ������� �ȵ�
            }
            return instance;
        }
    }

    string GameDataFileName = "GameData.json";
    public Data data = new Data();

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName; //������ġ

        string FromJsonData = File.ReadAllText(filePath); //���� �ҷ��ͼ� 
        data = JsonUtility.FromJson<Data>(FromJsonData);
        Debug.Log(filePath);
        Debug.Log("�ҷ����� �Ϸ�");
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName; //���� ���丮�� ����

        File.WriteAllText(filePath, ToJsonData);
        Debug.Log(filePath);
        Debug.Log("���� �Ϸ�");
    }
}