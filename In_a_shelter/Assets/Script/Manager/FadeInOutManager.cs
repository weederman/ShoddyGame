using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeInOutManager : MonoBehaviour
{
    public Image fadeScreen; // ���̵� ��ũ��
    public TextMeshProUGUI daysText; // ��¥ ǥ�� �ؽ�Ʈ
    private GameManager gameManager;
    public bool isTyping = false;
    DialogueManager logManager;
    NPC_RandomChat npcManager;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        logManager = FindObjectOfType<DialogueManager>();
        npcManager = FindObjectOfType<NPC_RandomChat>();
        fadeScreen.gameObject.SetActive(true); 
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        fadeScreen.gameObject.SetActive(true);
        Color fadeColor = fadeScreen.color;
        fadeColor.a = 1f;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(1f, 0f, t);
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0f;
        fadeScreen.color = fadeColor;
        fadeScreen.gameObject.SetActive(false);
    }
    private IEnumerator TypeText(string log)
    {
        isTyping = true;
        foreach (char letter in log.ToCharArray())
        {
            daysText.text += letter;
            yield return new WaitForSeconds(0.05f);  // �� ���ڸ��� 0.1�� ���
        }
        isTyping = false;
    }
    public IEnumerator FadeIn()
    {
        fadeScreen.gameObject.SetActive(true);
        Color fadeColor = fadeScreen.color;
        fadeColor.a = 0f;

        for (float t = 0; t < 1; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(0f, 1f, t);
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1f;
        fadeScreen.color = fadeColor;
    }

    public void ShowDaysText(int day)
    {
        daysText.gameObject.SetActive(true);
        daysText.text = "DAY " + day;
        StartCoroutine(FadeInDaysText());
    }

    private IEnumerator FadeInDaysText()
    {
        daysText.gameObject.SetActive(true);
        Color textColor = daysText.color;
        textColor.a = 0f;
        daysText.color = textColor;

        while (textColor.a < 1f)
        {
            textColor.a += Time.deltaTime; // 1�� ���� �ؽ�Ʈ ��Ÿ��
            daysText.color = textColor;
            yield return null;
        }
        StartCoroutine(TypeText("DAY " + GameManager.Instance.survivalDays.ToString()));
        yield return new WaitForSeconds(2f); // 2�� ���

        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime; // 1�� ���� �ؽ�Ʈ �����
            daysText.color = textColor;
            yield return null;
        }

        DataManager.Instance.SaveGameData();
        daysText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
    }
    public void NextDay()
    {
        logManager.OFFdialogue();
        GameManager.Instance.survivalDays++;
        daysText.text = "";
        StartCoroutine(NextDayCoroutine());
        gameManager.ResetTimeToMorning();
    }
    private IEnumerator NextDayCoroutine()
    {
        yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(FadeInDaysText());
        yield return StartCoroutine(FadeOut());
    }
    public void AdventureStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Adventure2");
    }
    public void Get_Resource()
    {
        GameManager.Instance.Food += 10;
        GameManager.Instance.Material += 10;
        GameManager.Instance.Medical += 10;
        logManager.OFFdialogue();
    }
    public void Remove_Resource()
    {
        GameManager.Instance.Food -= (int)npcManager.rand_chat[logManager.count]["using_food"];
        GameManager.Instance.Material -= (int)npcManager.rand_chat[logManager.count]["using_metarial"];
        GameManager.Instance.Medical -= (int)npcManager.rand_chat[logManager.count]["using_metarial"];
    }
}
