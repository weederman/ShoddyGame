using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sleeping_Bag : MonoBehaviour
{
    // ������Ʈ Ʈ����
    public Transform targetObject;
    public RawImage f_Img;
    public float radius;

    // �ܰ���
    public Material objectMaterial;
    public string outlineProperty = "_OutlineEnabled";

    // UI ����
    public GameObject textPanel;  // "���� �ڰڽ��ϱ�?" �г�
    public Button yesButton;      // "��" ��ư
    public Button noButton;       // "�ƴϿ�" ��ư
    public Image fadeScreen;      // ȭ�� ���̵� ȿ�� �̹��� (���� �̹���)
    public TextMeshProUGUI questionText;     // "���� �ڰڽ��ϱ�?" �ؽ�Ʈ
    public TextMeshProUGUI daysText;         // ��ĥ�� �������� ǥ���ϴ� �ؽ�Ʈ

    private string question = "Want to Sleep?";  // Ÿ���ε� �ؽ�Ʈ ����

    void Start()
    {
        f_Img.gameObject.SetActive(false);
        objectMaterial.SetFloat(outlineProperty, 0f);
        daysText.text = "DAY " + GameManager.Instance.survivalDays; // ������ �ؽ�Ʈ �ʱ�ȭ
        textPanel.SetActive(false);  // ó���� �г� ��Ȱ��ȭ
        fadeScreen.gameObject.SetActive(false);  // ���̵� �̹��� ��Ȱ��ȭ
        daysText.gameObject.SetActive(false);    // ��¥ �ؽ�Ʈ ��Ȱ��ȭ

        // ��ư �̺�Ʈ ����
        yesButton.onClick.AddListener(OnYesClicked);
        noButton.onClick.AddListener(OnNoClicked);

        yesButton.gameObject.SetActive(false);  // ��ư ��Ȱ��ȭ
        noButton.gameObject.SetActive(false);   // ��ư ��Ȱ��ȭ
    }

    void Update()
    {
        insideBox();
    }

    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.position);

        // ������Ʈ�� �ݰ� �ȿ� ���Դ��� Ȯ��
        if (distance <= radius)
        {
            // f_Img�� Ȱ��ȭ
            f_Img.gameObject.SetActive(true);
            objectMaterial.SetFloat(outlineProperty, 1f);

            // F Ű�� ������ �� �̺�Ʈ �߻�
            if (Input.GetKeyDown(KeyCode.F))
            {
                OpenTextPanel();
            }
        }
        else
        {
            // f_Img�� ��Ȱ��ȭ
            f_Img.gameObject.SetActive(false);
            objectMaterial.SetFloat(outlineProperty, 0f);

            if (f_Img.gameObject.activeSelf)
            {
                textPanel.SetActive(false);
                f_Img.gameObject.SetActive(false);
            }
        }

    }

    // �ؽ�Ʈ �г� ���� �� Ÿ���� ����
    private void OpenTextPanel()
    {
        textPanel.SetActive(true);  // �г� Ȱ��ȭ
        questionText.text = "";     // �ؽ�Ʈ �ʱ�ȭ
        StartCoroutine(TypeText());  // Ÿ���� ȿ�� ����
    }

    // Ÿ���� ȿ�� �ڷ�ƾ
    private IEnumerator TypeText()
    {
        foreach (char letter in question.ToCharArray())
        {
            questionText.text += letter;
            yield return new WaitForSeconds(0.1f);  // �� ���ڸ��� 0.1�� ���
        }

        // Ÿ������ ���� �� ��ư Ȱ��ȭ
        yesButton.gameObject.SetActive(true);
        noButton.gameObject.SetActive(true);
    }

    // "��" ��ư Ŭ�� �� ����
    private void OnYesClicked()
    {
        textPanel.SetActive(false);  // �г� �ݱ�
        GameManager.Instance.survivalDays++;  // GameManager���� ������ �� ����
        daysText.text = "DAY " + GameManager.Instance.survivalDays; // ������ �ؽ�Ʈ ������Ʈ
        StartCoroutine(FadeAndShowDays());  // ȭ�� ���̵� �� ��¥ ǥ��
    }
    // "�ƴϿ�" ��ư Ŭ�� �� ����
    private void OnNoClicked()
    {
        textPanel.SetActive(false);  // �г� �ݱ�
    }

    // ȭ�� ���̵� ��/�ƿ� �� ��¥ ǥ�� �ڷ�ƾ
    private IEnumerator FadeAndShowDays()
    {
        fadeScreen.gameObject.SetActive(true);  // ���̵� �̹��� Ȱ��ȭ
        Color fadeColor = fadeScreen.color;
        fadeColor.a = 0f;
        fadeScreen.color = fadeColor;

        // ȭ���� ������ ��Ӱ� (���̵� ��)
        while (fadeColor.a < 1f)
        {
            fadeColor.a += Time.deltaTime / 2f;  // 2�� ���� ��ο���
            fadeScreen.color = fadeColor;
            yield return null;
        }

        // ��ĥ�� �������� �ؽ�Ʈ Ȱ��ȭ
        daysText.gameObject.SetActive(true);

        Color textColor = daysText.color;
        textColor.a = 0f;
        daysText.color = textColor;

        // ��¥ �ؽ�Ʈ ������ ��Ÿ����
        while (textColor.a < 1f)
        {
            textColor.a += Time.deltaTime / 2f;  // 2�� ���� �ؽ�Ʈ ��Ÿ��
            daysText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(2f);  // 2�ʰ� ���

        // ȭ���� ������ ��� (���̵� �ƿ�)
        while (fadeColor.a > 0f)
        {
            fadeColor.a -= Time.deltaTime / 2f;  // 2�� ���� �����
            fadeScreen.color = fadeColor;
            yield return null;
        }

        fadeScreen.gameObject.SetActive(false);  // ���̵� �̹��� ��Ȱ��ȭ

        while (textColor.a > 0f)
        {
            textColor.a -= Time.deltaTime / 2f;  // 2�� ���� �ؽ�Ʈ �����
            daysText.color = textColor;
            yield return null;
        }
        daysText.gameObject.SetActive(false);    // �ؽ�Ʈ ��Ȱ��ȭ
    }
}
