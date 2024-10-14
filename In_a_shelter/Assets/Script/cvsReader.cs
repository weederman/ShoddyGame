using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class cvsReader : MonoBehaviour
{
    public string cvsFileName; // Inspector에서 CSV 파일 이름을 할당
    public List<Dictionary<string, object>> data_RandomChat = new List<Dictionary<string, object>>();

    private void Start()
    {
        ReadCSV();
    }

    // 파일을 읽어오는 메서드
    private void ReadCSV()
    {
        string path = Application.dataPath + "/" + cvsFileName;

        // StreamReader로 파일 읽기
        Debug.Log("CSV 파일 경로: " + path);
        StreamReader reader = new StreamReader(path);

        // 첫 번째 줄(헤더) 읽기
        string headerLine = reader.ReadLine();
        if (string.IsNullOrEmpty(headerLine))
        {
            Debug.LogError("헤더가 비어 있습니다.");
            return;
        }

        // 헤더 분리
        var headers = headerLine.Split(',');

        // 각 줄을 읽어 Dictionary에 저장
        bool isFinish = false;

        while (!isFinish)
        {
            string dataLine = reader.ReadLine(); // 한 줄 읽기

            if (dataLine == null)
            {
                // 마지막 줄이면 반복문 탈출
                isFinish = true;
                break;
            }

            var splitData = dataLine.Split(','); // 데이터 파싱

            // 새로운 Dictionary 생성 및 데이터 추가
            var entry = new Dictionary<string, object>();
            for (int i = 0; i < headers.Length; i++)
            {
                entry[headers[i]] = splitData[i].Trim(); // 헤더를 키로 사용
            }

            // List에 Dictionary 추가
            data_RandomChat.Add(entry);
        }

        reader.Close();
    }
}
