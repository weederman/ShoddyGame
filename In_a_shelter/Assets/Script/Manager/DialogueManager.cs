using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    
}

public class DialogueManager : MonoBehaviour
{
    public bool isSelection = false;
    public Button Selection1;
    public Button Selection2;
    [SerializeField] private GameObject sprite_DialogueBox;
    [SerializeField] private Text txt_dialogue;
    [SerializeField] private Text txt_title;
    [SerializeField] private Text txt_selection1;
    [SerializeField] private Text txt_selection2;
    FadeInOutManager FadeManager;
    public bool isDialogue = false; // ��ȭ�� ����������
    public int count = 0; //��簡 �󸶳� ����Ǿ�����
    [SerializeField] private Dialogue[] log;
    private void Start()
    {
        sprite_DialogueBox = GameObject.Find("Canvas").transform.Find("TextPanel").gameObject;
        FadeManager = FindObjectOfType<FadeInOutManager>();
        Text[] Panel_Text=sprite_DialogueBox.GetComponentsInChildren<Text>();
        Debug.Log(Panel_Text.Length);
        txt_title = Panel_Text[0];
        txt_dialogue = Panel_Text[1];
        Button[] Panel_Button=sprite_DialogueBox.GetComponentsInChildren<Button>();
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
            case "ħ��":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();
                Selection1.onClick.AddListener(() => FadeManager.NextDay());
                Selection2.onClick.AddListener(() => OFFdialogue());
                break;
            case "��":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();
                Selection1.onClick.AddListener(() => FadeManager.AdventureStart());
                Selection2.onClick.AddListener(() => OFFdialogue());
                break;
            case "NPC":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();

                break;
            case "EventBox":
                Selection1.onClick.RemoveAllListeners();
                Selection2.onClick.RemoveAllListeners();
                Selection1.onClick.AddListener(() => FadeManager.Get_Rresource());
                Selection2.onClick.AddListener(()=>OFFdialogue());
                break;
                
        }
        txt_title.text = this.gameObject.name;
        ONOFF(true);
        txt_dialogue.text = log[count].dialogue;
        count = 0;
        NextDialogue();
        Debug.Log("�����");
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
    public void OFFdialogue()//��ư���� �Լ��� �ȶ��� ���� ���� �Ѥ���
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

    private void NextDialogue()
    {
        txt_dialogue.text = log[count].dialogue;
        count++;
    }
    void Update()
    {
        if (isDialogue)
        {
            if (isSelection && !(count < log.Length))//�������� �����ϴ� ��ȭâ�̰�, ��ȭ�� ������ ��� ���ù�ư ǥ��
            {
                Text Selection1Text = Selection1.GetComponentInChildren<Text>();
                Text Selection2Text = Selection2.GetComponentInChildren<Text>();
                Selection1Text.text = log[count - 1].selection1Text;
                Selection2Text.text = log[count - 1].selection2Text;
                Selection1.gameObject.SetActive(true);
                Selection2.gameObject.SetActive(true);
            }
            else {//�������� ���� ��ȭâ�� ���
                Selection1.gameObject.SetActive(false);
                Selection2.gameObject.SetActive(false);
                if (Input.GetKeyDown(KeyCode.Space))//���� �����̽��ٰ� ������ ��
                {
                    if (count < log.Length) NextDialogue();//��ȭ�� �� ������ ���� ��ȭ ���
                    else
                    {

                        ONOFF(false);//�������� ��ȭ ����
                    }
                }
            }
        }
    }
}
