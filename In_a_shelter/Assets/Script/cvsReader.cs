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
        public string cvsFileName; // Inspector���� �Ҵ��� CSV ���� �̸�
        public Dictionary<int, log> randomChat = new Dictionary<int, log>(); // �� ��ȣ�� �����ϴ� Dictionary

        private int rowCount = 0; // CSV ������ �� �� ��

        private void Start()
        {
            ReadCSV();
        }

        // CSV ������ �о���� �޼���
        private void ReadCSV()
        {
            string path = cvsFileName;
            StreamReader reader = new StreamReader(Application.dataPath + "/" + path);
            bool isFinish = false;

            // ù ��(���)�� �����ϴ� �ڵ� (���û���)
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

                // splitData�� ���̰� �ּ��� 6 �̻����� Ȯ�� (num, name, dialogue, is_selection, selection_dialouge, happiness)
                if (splitData.Length < 6)
                {
                    Debug.LogError("Invalid data format. Expected 6 columns but got fewer.");
                    continue; // �� ���� �ǳʶݴϴ�.
                }

                // log �ν��Ͻ� ����
                log logEntry = new log
                {
                    num = splitData[0],
                    name = splitData[1],
                    dialouge = splitData[2],
                    is_selection = splitData[3],
                    selection_dialouge = splitData[4],
                    happiness = splitData[5]
                };

                // randomChat�� �߰�
                randomChat.Add(rowCount, logEntry);
                rowCount++;
            }
        }
    }
