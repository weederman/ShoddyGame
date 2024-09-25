using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sleeping_Bag : MonoBehaviour
{
    // 오브젝트 트리거
    public Transform targetObject;
    public RawImage f_Img;
    public float radius;

    // 외곽선
    public Material objectMaterial;
    public string outlineProperty = "_OutlineEnabled";

    // UI 관련
    public GameObject textPanel;  // "잠을 자겠습니까?" 패널
    public Button yesButton;      // "예" 버튼
    public Button noButton;       // "아니오" 버튼
    public Image fadeScreen;      // 화면 페이드 효과 이미지 (검은 이미지)
    public TextMeshProUGUI questionText;     // "잠을 자겠습니까?" 텍스트
    public TextMeshProUGUI daysText;         // 며칠이 지났는지 표시하는 텍스트

    private string question = "Want to Sleep?";  // 타이핑될 텍스트 내용

    void Start()
    {
        f_Img.gameObject.SetActive(false);
        objectMaterial.SetFloat(outlineProperty, 0f);
        daysText.text = "DAY " + GameManager.Instance.survivalDays; // 생존일 텍스트 초기화
        textPanel.SetActive(false);  // 처음엔 패널 비활성화
        fadeScreen.gameObject.SetActive(false);  // 페이드 이미지 비활성화
        daysText.gameObject.SetActive(false);    // 날짜 텍스트 비활성화

        // 버튼 이벤트 설정
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);

        yesButton.gameObject.SetActive(false);  // 버튼 비활성화
        noButton.gameObject.SetActive(false);   // 버튼 비활성화
    }

    void Update()
    {
        insideBox();
    }

    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.position);

        // 오브젝트가 반경 안에 들어왔는지 확인
        if (distance <= radius)
        {
            // f_Img를 활성화
            f_Img.gameObject.SetActive(true);
            objectMaterial.SetFloat(outlineProperty, 1f);

            // F 키를 눌렀을 때 이벤트 발생
            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenTextPanel();
            }
        }
        else
        {
            // f_Img를 비활성화
            f_Img.gameObject.SetActive(false);
            objectMaterial.SetFloat(outlineProperty, 0f);

            if (f_Img.gameObject.activeSelf)
            {
                textPanel.SetActive(false);
                f_Img.gameObject.SetActive(false);
            }
        }

    }

    // 텍스트 패널 열기 및 타이핑 시작
    private void OpenTextPanel()
    {
        textPanel.SetActive(true);  // 패널 활성화
        questionText.text = "";     // 텍스트 초기화
        StartCoroutine(TypeText());  // 타이핑 효과 시작
    }

    // 타이핑 효과 코루틴
    private IEnumerator TypeText()
    {
        foreach (char letter in question.ToCharArray())
        {
            questionText.text += letter;
            yield return new WaitForSeconds(0.1f);  // 각 글자마다 0.1초 대기
        }

        // 타이핑이 끝난 후 버튼 활성화
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    // "예" 버튼 클릭 시 실행
    private void OnYesClicked()
    {
        textPanel.SetActive(false);  // 패널 닫기
        GameManager.Instance.survivalDays++;  // GameManager에서 생존일 수 증가
        daysText.text = "DAY " + GameManager.Instance.survivalDays; // 생존일 텍스트 업데이트
        StartCoroutine(FadeAndShowDays());  // 화면 페이드 및 날짜 표시
    }
    // "아니오" 버튼 클릭 시 실행
    private void OnNoClicked()
    {
        textPanel.SetActive(false);  // 패널 닫기
    }

    // 화면 페이드 인/아웃 및 날짜 표시 코루틴
    private IEnumerator FadeAndShowDays()
    {
        fadeScreen.gameObject.SetActive(true);  // 페이드 이미지 활성화
        Color fadeColor = fadeScreen.color;
        fadeColor.a = 0f;
        fadeScreen.color = fadeColor;

        // 화면을 서서히 어둡게 (페이드 인)
        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime / 2f;  // 2초 동안 어두워짐
            fadeScreen.color = fadeColor;
            yield return null;
        }

        // 며칠이 지났는지 텍스트 활성화
        daysText.gameObject.SetActive(true);

        Color textColor = daysText.color;
        textColor.a = 0f;
        daysText.color = textColor;

        // 날짜 텍스트 서서히 나타나기
        while (textColor.a < 1f)
        {
            textColor.a += Time.deltaTime / 2f;  // 2초 동안 텍스트 나타남
            daysText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f);  // 2초간 대기

        // 화면을 서서히 밝게 (페이드 아웃)
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime / 2f;  // 2초 동안 밝아짐
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeScreen.gameObject.SetActive(false);  // 페이드 이미지 비활성화

        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime / 2f;  // 2초 동안 텍스트 사라짐
            daysText.color = textColor;
            yield return null;
        }
        daysText.gameObject.SetActive(false);    // 텍스트 비활성화
    }
}
