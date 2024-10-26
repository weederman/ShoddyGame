using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

[System.Serializable]
public class Dialogue
{
    [TextArea]
    public string dialogue;
    public string title;
    public string selection1Text;
    public string selection2Text;

    public bool isSelection;

}

public class DialogueManager : MonoBehaviour
{
    public Button Selection1;
    public Button Selection2;
    [SerializeField] private GameObject sprite_DialogueBox;
    public Text txt_dialogue;
    public Text txt_title;
    public Text txt_selection1;
    public Text txt_selection2;
    FadeInOutManager FadeManager;
    public bool isDialogue = false; // 대화가 진행중인지
    public int count = 0; //대사가 얼마나 진행되었는지
    public Dialogue[] log;

    private void Start()
    {
        sprite_DialogueBox = GameObject.Find("Canvas").transform.Find("TextPanel").gameObject;
        Debug.Log(sprite_DialogueBox.gameObject);
        FadeManager = FindObjectOfType<FadeInOutManager>();
        Text[] Panel_Text=sprite_DialogueBox.GetComponentsInChildren<Text>();
        Debug.Log(Panel_Text.Length);
        txt_title = Panel_Text[0];
        txt_dialogue = Panel_Text[1];
        Button[] Panel_Button=sprite_DialogueBox.GetComponentsInChildren<Button>();
        Debug.Log(Panel_Button.Length);
        Selection1 = Panel_Button[0];
        Selection2 = Panel_Button[1];
        txt_selection1=Selection1.GetComponentInChildren<Text>();
        txt_selection2=Selection2.GetComponentInChildren<Text>();
    }

    public void ShowDialogue(string context)
    {
        Time.timeScale = 0f;
        switch (context)
        {
            case "침낭":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();
                Selection1.onClick.AddListener(() => FadeManager.NextDay());
                Selection2.onClick.AddListener(() => OFFdialogue());
                break;
            case "문":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();
                Selection1.onClick.AddListener(() => FadeManager.AdventureStart());
                Selection2.onClick.AddListener(() => OFFdialogue());
                break;
            case "NPC":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();
                Selection1.onClick.AddListener(() => FadeManager.Remove_Resource());
                Selection2.onClick.AddListener(() => OFFdialogue());
                break;
            case "EventBox":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();
                Selection1.onClick.AddListener(() => FadeManager.Get_Resource());
                Selection2.onClick.AddListener(()=>OFFdialogue());
                break;
                
        }
        txt_title.text = log[count].title;
        ONOFF(true);
        txt_dialogue.text = log[count].dialogue;
        count = 0;
        NextDialogue();
        Debug.Log("쇼실행");
    }
    public void ONOFF(bool _flag)
    {
        sprite_DialogueBox.gameObject.SetActive(_flag);
        txt_dialogue.gameObject.SetActive(_flag);
        txt_title.gameObject.SetActive(_flag);
        isDialogue = _flag;
        if (!_flag)
        {
            Time.timeScale = 1f;
            count = 0;
        }
    }
    public void OFFdialogue()//버튼에서 함수가 안떠서 따로 만듦 ㅡㅅㅡ
    {
        bool _flag = false;
        sprite_DialogueBox.gameObject.SetActive(_flag);
        txt_dialogue.gameObject.SetActive(_flag);
        txt_title.gameObject.SetActive(_flag);
        isDialogue = _flag;
        Selection1.gameObject.SetActive(_flag);
        Selection2.gameObject.SetActive(_flag);
        Time.timeScale = 1f;
        count = 0;
    }

    // log 배열의 크기를 동적으로 변경하는 함수
    public void SetLogLength(int length)
    {
        log = new Dialogue[length];
        for (int i = 0; i < length; i++)
        {
            log[i] = new Dialogue();
        }
    }

    private void NextDialogue()
    {
        txt_dialogue.text = log[count].dialogue;
        count++;
    }
    void Update()
    {
        if (isDialogue)
        {
            if (log[count].isSelection && !isDialogue)//선택지가 존재하는 대화창이면 선택버튼 표시
            {
                Text Selection1Text = Selection1.GetComponentInChildren<Text>();
                Text Selection2Text = Selection2.GetComponentInChildren<Text>();
                Selection1Text.text = log[count - 1].selection1Text;
                Selection2Text.text = log[count - 1].selection2Text;
                Selection1.gameObject.SetActive(true);
                Debug.Log("버튼 하나 생성");
                Selection2.gameObject.SetActive(true);
            }
            else {//선택지가 없는 대화창일 경우
                Selection1.gameObject.SetActive(false);
                Selection2.gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space))//만약 스페이스바가 눌렸을 떄
                {
                    if (count < log.Length) NextDialogue();//대화가 더 있으면 다음 대화 출력
                    else
                    {

                        ONOFF(false);//끝났으면 대화 종료
                    }
                }
            }
        }
    }
}
