using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_RandomChat : MonoBehaviour
{
    public DialogueManager logManager;
    public GameObject targetObject;
    public GameObject f_Img;
    public float radius;
    public bool event_issued = false;
    int tmp_surv_day;
    public cvsReader reader;  // CSVReader를 통해 데이터를 불러옴
    int count;
    public List<Dictionary<string, object>> rand_chat { get; private set; }

    // num 필드 추적
    private List<int> numList = new List<int>(); // num 값 저장

    void Start()
    {
        targetObject = GameObject.Find("Player");
        tmp_surv_day = GameManager.Instance.survivalDays;

        // 초기 대화 내용 설정
        count = logManager.count;
        rand_chat = reader.data_RandomChat;

        // num 리스트 업데이트
        UpdateNumList();

        // 처음 데이터를 CSV에서 불러와 log에 반영
        UpdateDialogueFromChat();
    }

    void Update()
    {
        // 매일 대사를 업데이트하도록 체크
        if (tmp_surv_day != GameManager.Instance.survivalDays)
        {
            event_issued = false;
            tmp_surv_day = GameManager.Instance.survivalDays;
            UpdateDialogueFromChat(); // 새로운 날에 대화 내용을 업데이트
        }

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
                logManager.ShowDialogue(this.gameObject.name);
            }
        }
        else
        {
            f_Img.gameObject.SetActive(false);
        }
    }

    // CSV에서 데이터를 가져와 DialogueManager의 log를 업데이트
    private void UpdateDialogueFromChat()
    {
        if (rand_chat == null || rand_chat.Count == 0)
        {
            Debug.LogError("랜덤 대화 데이터 없음");
            return;
        }

        // 랜덤하게 num 값을 선택
        int randomNum = Random.Range(0, numList.Count);
        int selectedNum = numList[randomNum];

        // 선택된 num 값과 일치하는 대화 묶음을 가져옴
        List<Dictionary<string, object>> selectedDialogueGroup = GetDialogueGroupByNum(selectedNum);

        // logManager의 log 크기를 CSV 데이터 크기에 맞춰 동적으로 변경
        logManager.SetLogLength(selectedDialogueGroup.Count);

        // DialogueManager의 log와 isSelection을 업데이트
        for (int i = 0; i < selectedDialogueGroup.Count; i++)
        {
            logManager.log[i].title = (string)selectedDialogueGroup[i]["name"];         // CSV의 "name" 필드
            logManager.log[i].dialogue = (string)selectedDialogueGroup[i]["dialogue"];   // CSV의 "dialogue" 필드
            logManager.log[i].selection1Text = (string)selectedDialogueGroup[i]["selection_dialouge"]; // CSV의 선택지1 필드

            // CSV의 is_selection 값을 DialogueManager의 isSelection에 반영
            if (selectedDialogueGroup[i].ContainsKey("is_selection"))
            {
                string isSelectionStr = selectedDialogueGroup[i]["is_selection"].ToString();

                // bool 값으로 안전하게 변환
                if (bool.TryParse(isSelectionStr, out bool isSelection))
                {
                    logManager.isSelection = isSelection;
                }
                else
                {
                    Debug.LogError($"'is_selection' 필드 값 '{isSelectionStr}'는 유효한 bool 값이 아닙니다.");
                }
            }

            if (i + 1 < selectedDialogueGroup.Count)
            {
                logManager.log[i].selection2Text = (string)selectedDialogueGroup[i + 1]["selection_dialouge"]; // CSV의 선택지2 필드
            }
        }

        Debug.Log("대화 내용 업데이트 완료");
    }


    // num 필드 값을 리스트로 업데이트하는 함수
    private void UpdateNumList()
    {
        numList.Clear(); // 초기화
        foreach (var chat in rand_chat)
        {
            if (chat.ContainsKey("num"))
            {
                if (int.TryParse(chat["num"].ToString(), out int num)) // 안전하게 num 필드를 정수로 변환
                {
                    if (!numList.Contains(num))
                    {
                        numList.Add(num); // 중복되지 않게 num 리스트에 추가
                    }
                }
                else
                {
                    Debug.LogError($"'{chat["num"]}'는 유효한 숫자가 아닙니다.");
                }
            }
        }
    }

    // 주어진 num 값에 해당하는 대화 묶음 반환
    private List<Dictionary<string, object>> GetDialogueGroupByNum(int num)
    {
        List<Dictionary<string, object>> selectedGroup = new List<Dictionary<string, object>>();

        foreach (var chat in rand_chat)
        {
            if (chat.ContainsKey("num"))
            {
                if (int.TryParse(chat["num"].ToString(), out int chatNum) && chatNum == num)
                {
                    selectedGroup.Add(chat); // num 값이 일치하는 대화들을 그룹에 추가
                }
            }
        }

        return selectedGroup;
    }
}
