using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class FadeInOutManager : MonoBehaviour
{
    public Image fadeScreen; // 페이드 스크린
    public TextMeshProUGUI daysText; // 날짜 표시 텍스트
    public bool isTyping = false;
    DialogueManager logManager;
    private void Start()
    {
        logManager = FindObjectOfType<DialogueManager>();
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
            yield return new WaitForSeconds(0.05f);  // 각 글자마다 0.1초 대기
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
            textColor.a += Time.deltaTime; // 1초 동안 텍스트 나타남
            daysText.color = textColor;
            yield return null;
        }
        StartCoroutine(TypeText("DAY " + GameManager.Instance.survivalDays.ToString()));
        yield return new WaitForSeconds(2f); // 2초 대기

        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime; // 1초 동안 텍스트 사라짐
            daysText.color = textColor;
            yield return null;
        }

        DataManager.Instance.SaveGameData();
        daysText.gameObject.SetActive(false); // 텍스트 비활성화
    }
    public void NextDay()
    {
        logManager.OFFdialogue();
        GameManager.Instance.survivalDays++;
        daysText.text = "";
        StartCoroutine(NextDayCoroutine());
    }
    private IEnumerator NextDayCoroutine()
    {
        yield return StartCoroutine(FadeIn());
        yield return StartCoroutine(FadeInDaysText());
        yield return StartCoroutine(FadeOut());
    }
    public void AdventureStart()
    {
        SceneManager.LoadScene("Adventure");
    }

}
