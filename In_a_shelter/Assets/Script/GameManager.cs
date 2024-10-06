using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int survivalDays = 0;  // ������ ��
    public int Food = 0;
    public int Material = 0;
    public bool isTyping = false;
    public UnityEngine.UI.Image fadeScreen; //ȭ�� ��ȯ�� ��� ��ο����� �̹���
    public TextMeshProUGUI daysText;//ȭ�� ��ȯ�� ��¥ Ȥ�� �ε����� ǥ���ϴ� �ؽ�Ʈ
    private void Awake()
    {
        // Instance�� �̹� �����ϸ� �� ��ü�� �ı�
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
    }
    public void Start()
    {
        GameObject fadePanel = GameObject.Find("FadePanel");
        if (fadePanel != null)
        {
            fadeScreen = fadePanel.GetComponent<UnityEngine.UI.Image>();
            daysText = fadePanel.transform.Find("Text").GetComponent<TextMeshProUGUI>();
            
        }
        else
        {
            Debug.LogWarning("FadePanel�� ������ ã�� �� �����ϴ�!");
        }
        StartCoroutine(FadeOut());
    }
    public void NextDay()
    {
        survivalDays++;
        daysText.text = "";
        StartCoroutine(FadeAndShow());
    }
    public void AdventureStart()
    {
        SceneManager.LoadScene("Adventure");
    }

    private IEnumerator FadeAndShow()
    {
        /*fadeScreen.gameObject.SetActive(true);
        Color fadeColor = fadeScreen.color;
        fadeScreen.color = fadeColor;

        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime / 2f;  // 2�� ���� ��ο���
            fadeScreen.color = fadeColor;
            yield return null;
        }*/
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1f);
        daysText.gameObject.SetActive(true);
        Color textColor = daysText.color;
        textColor.a = 0f;
        daysText.color = textColor;
        while (textColor.a < 1f)
        {
            textColor.a += Time.deltaTime / 1f;  // 1�� ���� �ؽ�Ʈ ��Ÿ��
            daysText.color = textColor;
            yield return null;
        }
        StartCoroutine(TypeText("DAY "+ survivalDays.ToString()));
        yield return new WaitForSeconds(2f);  // 2�ʰ� ���
        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime / 2f;  // 2�� ���� �ؽ�Ʈ �����
            daysText.color = textColor;
            yield return null;
        }
        daysText.gameObject.SetActive(false);    // �ؽ�Ʈ ��Ȱ��ȭ
        DateManager.Instance.SaveGameData();
        SceneManager.LoadScene("Scenes/Tutorial/Shelter");
        // ȭ���� ������ ��� (���̵� �ƿ�)
        /*while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime / 2f;  // 2�� ���� �����
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeScreen.gameObject.SetActive(false);  // ���̵� �̹��� ��Ȱ��ȭ
        */

        
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

        for (float t = 0.0f; t < 1f; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(0f, 1f, t / 1f);
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1f;
        fadeScreen.color = fadeColor;
    }

    // ���̵� �ƿ� (���� ȭ���� ���� �����)
    public IEnumerator FadeOut()
    {
        Color fadeColor = fadeScreen.color;
        fadeColor.a = 1f;

        for (float t = 0.0f; t < 1f; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(1f, 0f, t / 1f);
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0f;
        fadeScreen.color = fadeColor;
        fadeScreen.gameObject.SetActive(false);  // ���̵� �ƿ� �� ȭ�� ����
    }
    //1.. �ϴ� �� ����� ������ �ɸ�
    //2. �ؽ�Ʈ ui �Ȼ����
    //3. ���̵� �Լ� ���� �ȵ�
    //4.. �׷� �� ��� ���� ��� �����
}
