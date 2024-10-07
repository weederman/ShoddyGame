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

    private bool isDialogue = false; //��ȭ�� ���������� �˷��� ����
    private int count = 0; //��簡 �󸶳� ����ƴ��� �˷��� ����

    [SerializeField] private StoryTxt[] storyTxt;





    //���� �߰��� �Ʒ��� ShowDialogue�� ����ϴ� �Լ��� update�κп��� ���
    public void ShowDialogue()
    {
        ONOFF(true); //��ȭ�� ���۵�
        count = 0;
        NextDialogue(); //ȣ����ڸ��� ��簡 ����� �� �ֵ��� 
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
        //ù��° ���� ù��° CharacterImg���� ��� ���� CharacterImg�� ����Ǹ鼭 ȭ�鿡 ���̰� �ȴ�. 
        T_txt.text = storyTxt[count].Text;
        Sp_Character.sprite = storyTxt[count].CharacterImg;
        Sp_BG.sprite = storyTxt[count].BGImg;
        count++; //���� ���� CharacterImg�� �������� 

    }

    void Update()
    {
        //spacebar ���� ������ ��簡 ����ǵ���. 
        if (isDialogue) //Ȱ��ȭ�� �Ǿ��� ���� ��簡 ����ǵ���
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //��ȭ�� ���� �˾ƾ���.
                if (count < storyTxt.Length) NextDialogue(); //���� ��簡 �����
                else ONOFF(false); //��簡 ����
            }
        }
    }
}
