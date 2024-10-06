using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int survivalDays = 0;  // 생존일 수
    public int Food = 0;
    public int Material = 0;
    public bool isTyping = false;
    public UnityEngine.UI.Image fadeScreen; //화면 전환시 잠시 어두워지는 이미지
    public TextMeshProUGUI daysText;//화면 전환시 날짜 혹은 로딩중을 표시하는 텍스트
    private void Awake()
    {
        // Instance가 이미 존재하면 이 객체를 파괴
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
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
            Debug.LogWarning("FadePanel이 씬에서 찾을 수 없습니다!");
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
            fadeColor.a += Time.deltaTime / 2f;  // 2초 동안 어두워짐
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
            textColor.a += Time.deltaTime / 1f;  // 1초 동안 텍스트 나타남
            daysText.color = textColor;
            yield return null;
        }
        StartCoroutine(TypeText("DAY "+ survivalDays.ToString()));
        yield return new WaitForSeconds(2f);  // 2초간 대기
        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime / 2f;  // 2초 동안 텍스트 사라짐
            daysText.color = textColor;
            yield return null;
        }
        daysText.gameObject.SetActive(false);    // 텍스트 비활성화
        DateManager.Instance.SaveGameData();
        SceneManager.LoadScene("Scenes/Tutorial/Shelter");
        // 화면을 서서히 밝게 (페이드 아웃)
        /*while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime / 2f;  // 2초 동안 밝아짐
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeScreen.gameObject.SetActive(false);  // 페이드 이미지 비활성화
        */

        
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

        for (float t = 0.0f; t < 1f; t += Time.deltaTime)
        {
            fadeColor.a = Mathf.Lerp(0f, 1f, t / 1f);
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1f;
        fadeScreen.color = fadeColor;
    }

    // 페이드 아웃 (검은 화면이 점점 사라짐)
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
        fadeScreen.gameObject.SetActive(false);  // 페이드 아웃 후 화면 숨김
    }
    //1.. 일단 씬 재실행 개오래 걸림
    //2. 텍스트 ui 안사라짐
    //3. 페이드 함수 실행 안됨
    //4.. 그럼 난 어디에 저장 기능 만들어
}
