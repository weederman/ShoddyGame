using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_RandomChat : MonoBehaviour
{
    public DialogueManager logManager;
    public GameObject targetObject;
    public GameObject f_Img;
    public float radius;
    public bool event_issued = false;
    int tmp_surv_day;
    public cvsReader reader;  // csvReader를 통해 데이터를 불러옴
    string[,] chat;
    string dialogueText;
    string titleText;
    string selection1;
    string selection2;
    int count;
    bool is_selection;

    void Start()
    {
        tmp_surv_day = GameManager.Instance.survivalDays;

        // 초기 대화 내용 설정
        count = logManager.count;
        UpdateDialogueFromChat();
    }

    // Update는 매 프레임 호출
    void Update()
    {
        if (tmp_surv_day != GameManager.Instance.survivalDays) event_issued = false;
        insideBox();  // 반경 체크
    }

    private void insideBox()
    {
        float distance = Vector2.Distance(transform.position, targetObject.transform.position);

        // 오브젝트가 반경 안에 들어왔는지 확인
        if (distance <= radius)
        {
            f_Img.gameObject.SetActive(true);

            // F를 누르면 이벤트 발생
            if (Input.GetKeyDown(KeyCode.F) && !logManager.isDialogue)
            {
                logManager.ShowDialogue(randomDialogue());
            }
        }
        else
        {
            f_Img.gameObject.SetActive(false);
        }
    }

    // 랜덤 대화 선택
    public string randomDialogue()
    {
        // 랜덤으로 num 값을 선택 (num 값은 0~3 사이)
        int randomNum = Random.Range(0, 4);
        string dialogue = "";
        int startIndex = -1;  // 선택된 num의 시작 인덱스
        int endIndex = -1;    // 선택된 num이 끝나는 인덱스

        // num에 따른 시작 인덱스와 끝나는 인덱스 설정
        switch (randomNum)
        {
            case 0:
                startIndex = 0;
                endIndex = 3; // num이 1로 바뀌기 전까지 (0~3)
                break;

            case 1:
                startIndex = 4;
                endIndex = 7; // num이 2로 바뀌기 전까지 (4~7)
                break;

            case 2:
                startIndex = 8;
                endIndex = 11; // num이 3으로 바뀌기 전까지 (8~11)
                break;

            case 3:
                startIndex = 12;
                endIndex = chat.GetLength(0) - 1; // num이 끝까지 출력되도록 (12~끝)
                break;
        }

        // 선택된 인덱스 범위의 대사를 순차적으로 출력
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (string.IsNullOrEmpty(chat[i, 0])) continue;  // num이 없는 빈 라인은 건너뜀
            dialogue += chat[i, 2] + "\n";  // 대사를 누적해서 추가
        }

        return dialogue;  // 선택된 대사 반환
    }

    // chat 배열에서 데이터를 가져와 대화 내용을 업데이트
    private void UpdateDialogueFromChat()
    {
        // chat 배열에서 값을 로드하여 logManager에 적용
        titleText = chat[count, 1];
        dialogueText = chat[count, 2];

        // is_selection 값을 0 또는 1로 처리
        is_selection = chat[count, 3] == "1";  // 1이면 true, 0이면 false로 처리
        selection1 = chat[count, 4];
        selection2 = chat[count + 1, 4];

        // logManager에 값을 반영
        logManager.txt_title.text = titleText;
        logManager.txt_dialogue.text = dialogueText;
        logManager.txt_selection1.text = selection1;
        logManager.txt_selection2.text = selection2;
    }
}
