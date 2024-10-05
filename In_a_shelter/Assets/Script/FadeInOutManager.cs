using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FadeInOutManager : MonoBehaviour
{
    public Image fadeScreen; // 페이드 스크린
    public TextMeshProUGUI daysText; // 날짜 표시 텍스트

    private void Start()
    {
        fadeScreen.gameObject.SetActive(true); // 처음에 화면이 검은색으로 시작
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
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

    public IEnumerator FadeOut()
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
        Color textColor = daysText.color;
        textColor.a = 0f;
        daysText.color = textColor;

        while (textColor.a < 1f)
        {
            textColor.a += Time.deltaTime; // 1초 동안 텍스트 나타남
            daysText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f); // 2초 대기

        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime; // 1초 동안 텍스트 사라짐
            daysText.color = textColor;
            yield return null;
        }

        daysText.gameObject.SetActive(false); // 텍스트 비활성화
    }
}
