using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StoryTxt
{
    [TextArea]
    public string Text;
    public string Title;
    public Sprite CharacterImg;
    public Sprite BGImg;
}

public class StoryTxtManager : MonoBehaviour
{
    [SerializeField] private Image Sp_Character;
    [SerializeField] private GameObject Sp_txtBox;
    [SerializeField] private Image Sp_BG;
    [SerializeField] private Text T_title;
    [SerializeField] private Text T_txt;

    private bool isDialogue = false;
    private int count = 0;
    private bool isTyping = false;
    private bool canSkip = false;

    [SerializeField] private StoryTxt[] storyTxt;

    private void Start()
    {
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
        T_title.text = storyTxt[count].Title;
        Sp_Character.sprite = storyTxt[count].CharacterImg;
        Sp_BG.sprite = storyTxt[count].BGImg;
        StartCoroutine(TypeText(storyTxt[count].Text));
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
        if (isDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isTyping) // 타이핑 중일 때 스페이스바가 눌리면 텍스트를 완료
                {
                    canSkip = true;
                    return;
                }

                if (count < storyTxt.Length)
                {
                    StartCoroutine(WaitAndNextDialogue(0.2f)); // 0.2초 후에 다음 대사로
                }
                else
                {
                    ONOFF(false);
                }
            }
        }
    }

    private IEnumerator WaitAndNextDialogue(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextDialogue();
    }
}