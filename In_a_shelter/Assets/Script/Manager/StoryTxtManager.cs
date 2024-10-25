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
    public string cvsFileName; // Inspector에서 CSV 파일 이름을 할당

    private bool isDialogue = false;
    private int count = 0;
    private bool isTyping = false;
    private bool canSkip = false;
    private bool isInputBlocked = false; // 입력 차단 상태를 관리하는 변수

    private void Start()
    {

        chat = reader.ReadCSV(cvsFileName);
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
        if (count >= chat.Count) return; // 대화가 끝났을 때 추가적인 처리를 방지

        T_title.text = chat[count]["name"].ToString();

        int.TryParse(chat[count]["CharacterID"].ToString(), out int characterID);
        int.TryParse(chat[count]["BGID"].ToString(), out int bgID);
        Debug.Log($"{count} 줄 캐릭터ID: {characterID}, 배경ID: {bgID}");

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

            if (canSkip)  // 스페이스바가 눌리면 텍스트 즉시 완료
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
        if (isDialogue && !isInputBlocked) // 입력이 차단되지 않은 경우만 처리
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTyping) // 타이핑 중일 때 스페이스바가 눌리면 텍스트를 완료
                {
                    canSkip = true;
                    return;
                }

                if (count < chat.Count)
                {
                    StartCoroutine(BlockInputForSeconds(0.2f)); // 0.2초 동안 입력 차단
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
        isInputBlocked = true; // 입력 차단
        yield return new WaitForSeconds(seconds); // 대기
        isInputBlocked = false; // 입력 허용
    }
}
