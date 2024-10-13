using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class log
{
    public string num;
    public string name;
    public string dialouge;
    public string is_selection;
    public string selection_dialouge;
    public string happiness;
}

    public class cvsReader : MonoBehaviour
    {
        public string cvsFileName; // Inspector에서 할당할 CSV 파일 이름
        public Dictionary<int, log> randomChat = new Dictionary<int, log>(); // 행 번호로 관리하는 Dictionary

        private int rowCount = 0; // CSV 파일의 총 행 수

        private void Start()
        {
            ReadCSV();
        }

        // CSV 파일을 읽어오는 메서드
        private void ReadCSV()
        {
            string path = cvsFileName;
            StreamReader reader = new StreamReader(Application.dataPath + "/" + path);
            bool isFinish = false;

            // 첫 줄(헤더)을 무시하는 코드 (선택사항)
            reader.ReadLine();

            while (!isFinish)
            {
                string data = reader.ReadLine();
                if (data == null)
                {
                    isFinish = true;
                    break;
                }

                var splitData = data.Split(',');

                // splitData의 길이가 최소한 6 이상인지 확인 (num, name, dialogue, is_selection, selection_dialouge, happiness)
                if (splitData.Length < 6)
                {
                    Debug.LogError("Invalid data format. Expected 6 columns but got fewer.");
                    continue; // 이 줄을 건너뜁니다.
                }

                // log 인스턴스 생성
                log logEntry = new log
                {
                    num = splitData[0],
                    name = splitData[1],
                    dialouge = splitData[2],
                    is_selection = splitData[3],
                    selection_dialouge = splitData[4],
                    happiness = splitData[5]
                };

                // randomChat에 추가
                randomChat.Add(rowCount, logEntry);
                rowCount++;
            }
        }
    }
