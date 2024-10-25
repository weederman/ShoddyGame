using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class cvsReader : MonoBehaviour
{
    public List<Dictionary<string, object>> data_Chat = new List<Dictionary<string, object>>();

    // ������ �о���� �޼���
    public List<Dictionary<string, object>> ReadCSV(string cvsFileName)
    {
        string path = Application.dataPath + "/" + cvsFileName;

        // StreamReader�� ���� �б�
        Debug.Log("CSV ���� ���: " + path);
        StreamReader reader = new StreamReader(path);

        // ù ��° ��(���) �б�
        string headerLine = reader.ReadLine();
        if (string.IsNullOrEmpty(headerLine))
        {
            Debug.LogError("����� ��� �ֽ��ϴ�.");
        }

        // ��� �и�
        var headers = headerLine.Split(',');

        // �� ���� �о� Dictionary�� ����
        bool isFinish = false;

        while (!isFinish)
        {
            string dataLine = reader.ReadLine(); // �� �� �б�

            if (dataLine == null)
            {
                // ������ ���̸� �ݺ��� Ż��
                isFinish = true;
                break;
            }

            
            var splitData = dataLine.Split(','); // ������ �Ľ�
            

            // ���ο� Dictionary ���� �� ������ �߰�
            var entry = new Dictionary<string, object>();
            for (int i = 0; i < headers.Length; i++)
            {
                entry[headers[i]] = splitData[i].Replace("%", ",").Trim(); // ����� Ű�� ���
            }

            // List�� Dictionary �߰�
            data_Chat.Add(entry);
        }

        reader.Close();
        return data_Chat;
    }
}
