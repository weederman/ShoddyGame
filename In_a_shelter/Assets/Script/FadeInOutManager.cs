using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FadeInOutManager : MonoBehaviour
{
    public Image fadeScreen; // ���̵� ��ũ��
    public TextMeshProUGUI daysText; // ��¥ ǥ�� �ؽ�Ʈ

    private void Start()
    {
        fadeScreen.gameObject.SetActive(true); // ó���� ȭ���� ���������� ����
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
            textColor.a += Time.deltaTime; // 1�� ���� �ؽ�Ʈ ��Ÿ��
            daysText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f); // 2�� ���

        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime; // 1�� ���� �ؽ�Ʈ �����
            daysText.color = textColor;
            yield return null;
        }

        daysText.gameObject.SetActive(false); // �ؽ�Ʈ ��Ȱ��ȭ
    }
}
