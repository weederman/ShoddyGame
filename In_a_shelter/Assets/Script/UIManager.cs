using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject textPanel;
    public Text talkText;
    public GameObject scanObject;

    public void Action(GameObject name)
    {
        scanObject = name;
        talkText.text = "�̰��� �̸��� "+scanObject.name+"�̴�";
    }

    public void Map()
    {
        
    }
}
