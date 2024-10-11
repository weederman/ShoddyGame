using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text Food;
    public Text Material;
    public Text Medical;//¤·¤µ¤·?
    public Text SurviveDays;
    private void Update()
    {
        Food.text=GameManager.Instance.Food.ToString(); 
        Material.text=GameManager.Instance.Material.ToString();
        Medical.text=GameManager.Instance.Medical.ToString();
        SurviveDays.text="DAY "+GameManager.Instance.survivalDays.ToString();
    }
}
