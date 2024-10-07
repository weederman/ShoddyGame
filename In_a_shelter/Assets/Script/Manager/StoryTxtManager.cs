using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StoryTxt
{
    [TextArea]
    public string Text;
    public Sprite CharacterImg;
    public Sprite BGImg;
}

public class StoryTxtManager : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer Sp_Character;
    [SerializeField]
    private SpriteRenderer Sp_txtBox;
    [SerializeField]
    private SpriteRenderer Sp_BG;
    [SerializeField]
    private Text T_txt;

    private bool isDialogue = false; //대화가 진행중인지 알려줄 변수
    private int count = 0; //대사가 얼마나 진행됐는지 알려줄 변수

    [SerializeField] private StoryTxt[] storyTxt;





    //이제 추가로 아래의 ShowDialogue를 사용하는 함수나 update부분에서 사용
    public void ShowDialogue()
    {
        ONOFF(true); //대화가 시작됨
        count = 0;
        NextDialogue(); //호출되자마자 대사가 진행될 수 있도록 
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
        //첫번째 대사와 첫번째 CharacterImg부터 계속 다음 CharacterImg로 진행되면서 화면에 보이게 된다. 
        T_txt.text = storyTxt[count].Text;
        Sp_Character.sprite = storyTxt[count].CharacterImg;
        Sp_BG.sprite = storyTxt[count].BGImg;
        count++; //다음 대사와 CharacterImg가 나오도록 

    }

    void Update()
    {
        //spacebar 누를 때마다 대사가 진행되도록. 
        if (isDialogue) //활성화가 되었을 때만 대사가 진행되도록
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //대화의 끝을 알아야함.
                if (count < storyTxt.Length) NextDialogue(); //다음 대사가 진행됨
                else ONOFF(false); //대사가 끝남
            }
        }
    }
}
