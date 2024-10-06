using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DateManager : MonoBehaviour
{
    static GameObject container; //데이터 담을 오브젝트
    static DateManager instance;
    public static DateManager Instance //싱글톤 생성
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataManager";

                instance = container.AddComponent(typeof(DateManager)) as DateManager;
                DontDestroyOnLoad(container); //데이터 사라지면 안돼
            }
            return instance;
        }
    }

    string GameDataFileName = "GameData.json";
    public Data data = new Data();

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName; //파일위치

        string FromJsonData = File.ReadAllText(filePath); //파일 불러와서 
        data = JsonUtility.FromJson<Data>(FromJsonData);
        Debug.Log(filePath);
        Debug.Log("불러오기 완료");
    }

    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName; //하위 디렉토리에 저장

        File.WriteAllText(filePath, ToJsonData);
        Debug.Log(filePath);
        Debug.Log("저장 완료");
    }
}