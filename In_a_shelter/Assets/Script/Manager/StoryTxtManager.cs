using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryTxtManager : MonoBehaviour
{
    [SerializeField] private Image Sp_Character;
    [SerializeField] private GameObject Sp_txtBox;
    [SerializeField] private Image Sp_BG;
    [SerializeField] private Text T_title;
    [SerializeField] private Text T_txt;

    public Sprite[] CharacterImg;
    public Sprite[] BGImg;
    public cvsReader reader;
    public static StoryTxtManager instance;
    public List<Dictionary<string, object>> chat { get; private set; }

    private bool isDialogue = false;
    private int count = 0;
    private bool isTyping = false;
    private bool canSkip = false;
    private bool isInputBlocked = false; // �Է� ���� ���¸� �����ϴ� ����

    private void Start()
    {
        reader = gameObject.GetComponent<cvsReader>();
        instance = this;
    }

    public void ASDF(List<Dictionary<string, object>> a)
    {
        chat = a;
        if (a.Count >= 0)
        {
            foreach (var kvp in a[0])
            {
                Debug.Log(kvp.Key);
            }
        }
        ShowDialogue();
    }

    public void ShowDialogue()
    {
        ONOFF(true);
        count = 0;
        NextDialogue();
    }

    private void ONOFF(bool _flag)
    {
        Sp_txtBox.gameObject.SetActive(_flag);
        Sp_Character.gameObject.SetActive(_flag);
        T_txt.gameObject.SetActive(_flag);
        isDialogue = _flag;
    }

    private void NextDialogue()
    {
        if (count >= chat.Count) return; // ��ȭ�� ������ �� �߰����� ó���� ����

        T_title.text = chat[count]["name"].ToString();

        int.TryParse(chat[count]["CharacterID"].ToString(), out int characterID);
        int.TryParse(chat[count]["BGID"].ToString(), out int bgID);
        Debug.Log($"{count} �� ĳ����ID: {characterID}, ���ID: {bgID}");

        Sp_Character.sprite = CharacterImg[characterID];
        Sp_BG.sprite = BGImg[bgID];

        StartCoroutine(TypeText(chat[count]["dialogue"].ToString()));
        count++;
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;
        T_txt.text = "";

        foreach (char letter in text.ToCharArray())
        {
            T_txt.text += letter;
            yield return new WaitForSeconds(0.05f);

            if (canSkip)  // �����̽��ٰ� ������ �ؽ�Ʈ ��� �Ϸ�
            {
                T_txt.text = text;
                break;
            }
        }

        isTyping = false;
        canSkip = false;
    }

    void Update()
    {
        if (isDialogue && !isInputBlocked) // �Է��� ���ܵ��� ���� ��츸 ó��
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTyping) // Ÿ���� ���� �� �����̽��ٰ� ������ �ؽ�Ʈ�� �Ϸ�
                {
                    canSkip = true;
                    return;
                }

                if (count < chat.Count)
                {
                    StartCoroutine(BlockInputForSeconds(0.2f)); // 0.2�� ���� �Է� ����
                    NextDialogue();
                }
                else
                {
                    ONOFF(false);
                }
            }
        }
    }

    private IEnumerator BlockInputForSeconds(float seconds)
    {
        isInputBlocked = true; // �Է� ����
        yield return new WaitForSeconds(seconds); // ���
        isInputBlocked = false; // �Է� ���
    }
}
