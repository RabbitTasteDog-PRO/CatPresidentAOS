using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

enum eClapEvent
{
    NONE,
    FIRST,
    SECOND,
    THIRD,
    FOURTH,
    COUNT,
}

public class ActionScedule : MonoBehaviour
{
    public static event Action<bool> ACTION_STORY_EVENT; // 중간중간 스토리 이벤트 
    public static event Action ACTION_ACT;
    public static event Action<string[], int[], string[], int[]> ACTION_STATEUP; /// 일반 스텟가감
    public static event Action<string, int, string[], int[]> ACTION_REST; // 휴식 스텟 가감
    public static event Action<List<string>, List<int>> ACTION_CLAP_EVENT; // 대선토론 박수 이벤트 스텟 가감 


    [Header("액션 스프라이트")]
    public UI2DSprite Sprite_Act;
    public UI2DSpriteAnimation spriteAnimCat;
    [Header("박수 오브젝트")]
    public GameObject Object_Clap;
    public GameObject objClap_0;
    public GameObject objClap_1;
    public GameObject objClap_2;
    [Header("타이머 오브젝트")]
    public GameObject Object_Timer;
    public UILabel Label_Timer;
    [Header("종료버튼")]
    public UIButton btnEnd;
    [Header("대화 오브젝트들")]
    public GameObject mBtn_Talk;
    public UIButton btnTalk;
    public UILabel Label_Talk;

    [Header("중간에 나오는 이벤트")]
    List<xmlData> listClapEventStarMnet; /// 대선토론 이벤트

    string[] TALK_MENT = new string[2]; // 대화맨트
    string[] STATE_UP_KEY = new string[2]; /// 스텟 업 키값
    int[] STATE_UP_POINT = new int[2]; /// 스텟 업 포인트값
    string[] STATE_DOWN_KEY = new string[2]; // 스텟 다운 키값
    int[] STATE_DOWN_POINT = new int[2]; // 스텟 다운 포인트 값

    string[] CAT_ANIM_IMG = new string[2]; // 애니메이션 스프라이트 이미지 


    eScedule _eScedule;

    /// act : 유세 , kind : 유세종류
    public void SetActionScedule(eActive _active, eScedule _scedule)
    {
        int random_ = UnityEngine.Random.Range(0, 1);
        SceneBase.Instance.PLAY_BGM(string.Format("ACTIVITY_{0}", random_));

        _eScedule = _scedule;
        Object_Timer.SetActive(false);
        btnEnd.onClick.Clear();
        gameObject.SetActive(true);

        btnEnd.gameObject.SetActive(false);
        Object_Clap.SetActive(false);
        mTalk_Index = 0;
        Label_Timer.text = string.Format("{0:00}", Timer);

        btnTalk.onClick.Clear();
        btnTalk.onClick.Add(new EventDelegate(onClickTalk));
        /// 대화버튼 활성화
        mBtn_Talk.GetComponent<BoxCollider2D>().enabled = true;

        Dictionary<eScedule, STAticveSceduleData> dic = SceneBase.Instance.dataManager.GetDicSceduleData(_active);
        STAticveSceduleData _data = dic[_scedule];

        TALK_MENT[0] = _data.ment_1;
        TALK_MENT[1] = _data.ment_2;

        STATE_UP_KEY[0] = _data.upKey_0;
        STATE_UP_KEY[1] = _data.upKey_1;

        STATE_UP_POINT[0] = _data.upPoint_0;
        STATE_UP_POINT[1] = _data.upPoint_1;

        STATE_DOWN_KEY[0] = _data.downKey_0;
        STATE_DOWN_KEY[1] = _data.downKey_1;

        STATE_DOWN_POINT[0] = _data.downPoint_0;
        STATE_DOWN_POINT[1] = _data.downPoint_1;

        CAT_ANIM_IMG[0] = string.Format("Image/Act_Cat/{0}/{1}", _active, _data.anim_img_1);
        CAT_ANIM_IMG[1] = string.Format("Image/Act_Cat/{0}/{1}", _active, _data.anim_img_2);

        spriteAnimCat.frames[0] = Resources.Load<Sprite>(CAT_ANIM_IMG[0]);
        spriteAnimCat.frames[1] = Resources.Load<Sprite>(CAT_ANIM_IMG[1]);

        btnEnd.onClick.Add(new EventDelegate(onClickCatActEnd));

        spriteAnimCat.enabled = false;

        Label_Talk.text = "TOUCH!!";

        /****************************************************************************************************************************/
        /// 안쓰는 제이슨 데이터 
        /// 나중에 참고용으로 삭제 하지 않음 

        // TextAsset targetFile = Resources.Load<TextAsset>("Data/Json/info_cat");
        // /// 패스 설정 
        // string jsonFile = targetFile.text;
        // JSONObject json_ = new JSONObject(jsonFile);
        // JSONObject info_ = json_[mActive];
        // Debug.LogError("info_[name] : " + info_["event"]);
        // JSONObject mJson_ItemInfo = info_["items"];

        // Debug.LogError("mJson_ItemInfo[name] : " + mJson_ItemInfo[mScedule]["name"]);

        // TALK_MENT[0] = mJson_ItemInfo[mScedule]["ment_1"].str;
        // TALK_MENT[1] = mJson_ItemInfo[mScedule]["ment_2"].str;

        // STATE_UP_KEY[0] = mJson_ItemInfo[mScedule]["up_state_key_1"].str;
        // STATE_UP_KEY[1] = mJson_ItemInfo[mScedule]["up_state_key_2"].str;

        // STATE_UP_POINT[0] = int.Parse(mJson_ItemInfo[mScedule]["up_state_point_1"].str);
        // STATE_UP_POINT[1] = int.Parse(mJson_ItemInfo[mScedule]["up_state_point_2"].str);

        // STATE_DOWN_KEY[0] = mJson_ItemInfo[mScedule]["down_state_key_1"].str;
        // STATE_DOWN_KEY[1] = mJson_ItemInfo[mScedule]["down_state_key_2"].str;

        // STATE_DOWN_POINT[0] = int.Parse(mJson_ItemInfo[mScedule]["down_state_point_1"].str);
        // STATE_DOWN_POINT[1] = int.Parse(mJson_ItemInfo[mScedule]["down_state_point_1"].str);

        // CAT_ANIM_IMG[0] = mJson_ItemInfo[mScedule]["anim_img_1"].str;
        // CAT_ANIM_IMG[1] = mJson_ItemInfo[mScedule]["anim_img_2"].str;

        // Label_Talk.text = "";

        /****************************************************************************************************************************/

    }

    ///<summary>
    /// 휴식할 경우에만 사용됨 
    ///</summary>
    public void SetActionRest(eActive _active, eScedule _scedule)
    {
        SceneBase.Instance.PLAY_BGM("ACTIVITY_REST");

        _eScedule = _scedule;
        Object_Timer.SetActive(false);
        gameObject.SetActive(true);

        btnEnd.onClick.Clear();
        btnEnd.onClick.Add(new EventDelegate(OnClickRestEnd));

        btnEnd.gameObject.SetActive(false);
        Object_Clap.SetActive(false);
        mTalk_Index = 0;
        Label_Timer.text = string.Format("{0:00}", Timer);

        btnTalk.onClick.Clear();
        btnTalk.onClick.Add(new EventDelegate(onClickTalk));
        
        /// 대화버튼 활성화
        mBtn_Talk.GetComponent<BoxCollider2D>().enabled = true;

        Dictionary<eScedule, STAticveSceduleData> dic = SceneBase.Instance.dataManager.GetDicSceduleData(_active);
        STAticveSceduleData _data = dic[_scedule];

        TALK_MENT[0] = _data.ment_1;
        TALK_MENT[1] = _data.ment_2;

        string path_1 = string.Format("Image/Act_Cat/{0}/{1}", eActive.REST, _data.anim_img_1);
        string path_2 = string.Format("Image/Act_Cat/{0}/{1}", eActive.REST, _data.anim_img_2);

        spriteAnimCat.frames[0] = Resources.Load<Sprite>(path_1);
        spriteAnimCat.frames[1] = Resources.Load<Sprite>(path_2);

        spriteAnimCat.enabled = false;

        Label_Talk.text = "TOUCH!!";

    }

    List<xmlStandTalkBox> listClapEventEndStory;
    ///<summary>
    /// 대선토론 이벤트
    ///</summary>
    public void SetActionEvent(eActive _active, eScedule _scedule)
    {
        SceneBase.Instance.PLAY_BGM("EVENT");

        listClapEventStarMnet = XmlParser.Read<xmlData>("Data/Xml/CAT_EVENT_MENT");
        listClapEventEndStory = XmlParser.Read<xmlStandTalkBox>("Data/Xml/CAT_EVENT_RESULT_MENT");

        _eScedule = _scedule;
        gameObject.SetActive(true);

        btnEnd.onClick.Clear();
        btnEnd.onClick.Add(new EventDelegate(OnClickEventEnd));

        Object_Timer.SetActive(true);
        Object_Clap.SetActive(true);
        Object_Clap.GetComponent<BoxCollider2D>().enabled = false;
        objClap[0] = objClap_0;
        objClap[1] = objClap_1;
        objClap[2] = objClap_2;
        Label_Timer.text = string.Format("{0:00}", Timer);


        btnEnd.gameObject.SetActive(false);
        mTalk_Index = 0;

        btnTalk.onClick.Clear();
        btnTalk.onClick.Add(new EventDelegate(onClickTalk));
        /// 대화버튼 활성화
        mBtn_Talk.GetComponent<BoxCollider2D>().enabled = true;

        Dictionary<eScedule, STAticveSceduleData> dic = SceneBase.Instance.dataManager.GetDicSceduleData(_active);
        STAticveSceduleData _data = dic[_scedule];

        TALK_MENT[0] = _data.ment_1;
        TALK_MENT[1] = _data.ment_2;

        string path_1 = string.Format("Image/Act_Cat/{0}/{1}", eActive.EVENT, _data.anim_img_1);
        string path_2 = string.Format("Image/Act_Cat/{0}/{1}", eActive.EVENT, _data.anim_img_1);

        spriteAnimCat.frames[0] = Resources.Load<Sprite>(path_1);
        spriteAnimCat.frames[1] = Resources.Load<Sprite>(path_2);

        spriteAnimCat.enabled = false;

        Label_Talk.text = "TOUCH!!";

    }


    /****************************************************************************************************************/
    private int mTalk_Index = 0;
    bool isActionTalk = false;
    public void onClickTalk()
    {
        if (isActionTalk == true)
            return;

        switch (_eScedule)
        {
            case eScedule.eEvent:
                {
                    /// 이벤트 부분 대사 나와서 로직 수정 해야함 
                    if (isClapEventEndMent == true)
                    {
                        try
                        {
                            isActionTalk = true;
                            Invoke("isActionTalk_", 0.2f);

                            LoadClapEventXmlStoryRead();
                            clapEventIndex++;
                        }
                        catch (Exception e)
                        {
                            Debug.LogError("########### isActionEventMent Exception : " + e.ToString());
                        }
                    }
                    else
                    {

                        /// 대사 인덱스 넘어가면 넥스트 버튼 가리고 
                        /// 스타트
                        if (mTalk_Index > (listClapEventStarMnet.Count - 1))
                        {
                            mTalk_Index = 0;
                            mBtn_Talk.GetComponent<BoxCollider2D>().enabled = false;
                            spriteAnimCat.enabled = true;

                            StartCoroutine(StartTimer());
                        }
                        else
                        /// 대사 뿌림 
                        {
                            isActionTalk = true;
                            Invoke("isActionTalk_", 0.2f);
                            Label_Talk.text = listClapEventStarMnet[mTalk_Index].ment_1;
                            mTalk_Index++;
                        }
                    }

                    break;
                }

            default:
                {
                    /// 대사 인덱스 넘어가면 넥스트 버튼 가리고 
                    /// 스타트
                    if (mTalk_Index > 1)
                    {
                        mTalk_Index = 0;
                        mBtn_Talk.GetComponent<BoxCollider2D>().enabled = false;
                        btnEnd.gameObject.SetActive(true);

                    }
                    else
                    /// 대사 뿌림 
                    {
                        isActionTalk = true;
                        Invoke("isActionTalk_", 0.2f);
                        Label_Talk.text = TALK_MENT[mTalk_Index];
                        mTalk_Index++;
                    }

                    break;
                }
        }

    }

    void isActionTalk_()
    {
        isActionTalk = false;
    }


    /****************************************************************************************************************/
    float Timer = 10;
    /// 타이머 이미지 나오기 
    IEnumerator StartTimer()
    {
        Object_Clap.GetComponent<BoxCollider2D>().enabled = true;
        while (true)
        {
            if (Timer < 0)
            {
                SetEventAction();
                isStopEvent = true;
                break;
            }
            Label_Timer.text = string.Format("{0:00}", Timer);
            yield return YieldHelper.waitForSeconds(1000);
            Timer -= 1;
        }
    }
    /****************************************************************************************************************/
    bool isStopEvent = false;
    bool isActionClap = false;
    private GameObject tempClapObj_0;
    private GameObject tempClapObj_1;
    private GameObject[] objClap = new GameObject[3];
    private int clapAmount = 0;
    /// 박수 애니메이션
    public void onClickTouch()
    {
        if (isActionClap == true) { return; }
        if (isStopEvent == true) { return; }
        if (_eScedule == eScedule.eEvent)
        {

            SceneBase.Instance.PLAY_SE("BTN_CLAP");

            isActionClap = true;
            int random_ = UnityEngine.Random.Range(0, 3);
            tempClapObj_0 = objClap[random_].transform.Find("Clap_1").gameObject;
            tempClapObj_1 = objClap[random_].transform.Find("Clap_2").gameObject;

            tempClapObj_0.SetActive(false);
            tempClapObj_1.SetActive(true);


            Invoke("isActionClap_", 0.1f);

            clapAmount++;
            // Debug.LogError("clapAmount : " + clapAmount);
        }

    }

    void isActionClap_()
    {
        isActionClap = false;

        tempClapObj_0.SetActive(true);
        tempClapObj_1.SetActive(false);

        tempClapObj_0 = null;
        tempClapObj_1 = null;

        Label_Talk.text = "TOUCH!!";
    }
    /// 기본 가감값 설정 
    void SetCatState(string[] up_key, int[] up_point, string[] down_key, int[] down_point)
    {

        /// 가감값 수정 해야함 
        int beforeUp_1 = UserInfoManager.Instance.getSaveState(up_key[0]);
        int beforeUp_2 = UserInfoManager.Instance.getSaveState(up_key[1]);
        /// 업세텟
        UserInfoManager.Instance.setSaveState(up_key[0], Mathf.Min(100, (beforeUp_1 + up_point[0])));
        UserInfoManager.Instance.setSaveState(up_key[1], Mathf.Min(100, (beforeUp_2 + up_point[1])));

        int beforeDown_1 = UserInfoManager.Instance.getSaveState(down_key[0]);
        int beforeDown_2 = UserInfoManager.Instance.getSaveState(down_key[1]);
        /// 다운스텟
        UserInfoManager.Instance.setSaveState(down_key[0], Mathf.Max(0, (down_point[0] * -1) + beforeDown_1));
        UserInfoManager.Instance.setSaveState(down_key[1], Mathf.Max(0,  (down_point[1] * -1) + beforeDown_2 ));

        //HP설정 ㄴ
        int beforeHP = UserInfoManager.Instance.getSaveHP();

        UserInfoManager.Instance.setSaveHP(Mathf.Max(0, Mathf.Min(Integers.CAT_HP, beforeHP - Integers.CAT_HP_MINUS)));
        /// 날짜 설정 
        int beforDay = UserInfoManager.Instance.getSaveDay();
        UserInfoManager.Instance.setSaveDay(beforDay + 1);

        // Debug.LogError("########## beforDay : " + beforDay + "// crrDay : " + UserInfoManager.Instance.getSaveDay());
    }


    /******************************************************************************************************** */
    int CAT_HP_PLUS = 5;

    string[] STATE_KEY = new string[]
    {
        Strings.TYPE_CHAR, Strings.TYPE_AWAR, Strings.TYPE_NATU, Strings.TYPE_CHARM,
        Strings.TYPE_TALK, Strings.TYPE_DIPLO, Strings.TYPE_ECO, Strings.TYPE_CUR
    };

    int[] DOWNPOINT = new int[]
    {
        -1,-1,-1,-1,-1,-1,-1,-1,
    };

    /// 휴식시 가감값 
    void SetRestAction()
    {
        /// 체력은 +5가 오르고 나머지 스텟들이 -1씩 떨어짐
        // 일단 이건 확인해봐야할듯 

        for (int i = 0; i < STATE_KEY.Length; i++)
        {
            // UserInfoManager.instance.set
            int minusState = Mathf.Max(0,UserInfoManager.Instance.getSaveState(STATE_KEY[i]) - 1) ;
            UserInfoManager.Instance.setSaveState(STATE_KEY[i], minusState);
        }
        int beforDay = UserInfoManager.Instance.getSaveDay();
        UserInfoManager.Instance.setSaveDay(beforDay + 1);

        // Debug.LogError("########## beforDay : " + beforDay + "// crrDay : " + UserInfoManager.Instance.getSaveDay());

        int plusHP = UserInfoManager.Instance.getSaveHP() + CAT_HP_PLUS;
        UserInfoManager.Instance.setSaveHP(Mathf.Max(0, Mathf.Min(Integers.CAT_HP, plusHP)));

    }

    List<string> listState = new List<string>();
    List<int> listPoint = new List<int>();
    bool isClapEventEndMent = false;
    int clapEventIndex = 0;
    string strClapEventCehck = "";


    /// 박수이벤트 종료 후 로직 
    void SetEventAction()
    {
        Object_Clap.SetActive(false);
        mBtn_Talk.GetComponent<BoxCollider2D>().enabled = true;

        int beforDay = UserInfoManager.Instance.getSaveDay();
        UserInfoManager.Instance.setSaveDay(beforDay + 1);

        isClapEventEndMent = true;

        // 예: 터치 횟수 100회 이상 = 카리스마 + 5, 체력 +5
        //    70회 이상 = 체력 + 3
        //    50회 이상 = 인품 + 2
        //    11회~49회 = 아무 변화 없음
        //    10회 미만 = 체력 - 5
        if (clapAmount < 10)
        {
            int minusHP = UserInfoManager.Instance.getSaveHP() - 5;
            UserInfoManager.Instance.setSaveHP(minusHP);


            listState.Add(Strings.SAVE_CAT_HP);
            listPoint.Add(-5);

            strClapEventCehck = "ZERO";

        }
        else if (clapAmount > 10 && clapAmount < 49)
        {
            if (listState != null)
            {
                listState.Clear();
            }

            if (listPoint != null)
            {
                listPoint.Clear();
            }
            strClapEventCehck = "FIRST";
        }
        else if (clapAmount > 49 && clapAmount < 70)
        {
            /// 인품 +2
            // TYPE_NATU
            int getState = Mathf.Min(100, UserInfoManager.Instance.getSaveState(Strings.TYPE_NATU) + 2) ;
            UserInfoManager.Instance.setSaveState(Strings.TYPE_NATU, getState);

            listState.Add(Strings.TYPE_NATU);
            listPoint.Add(2);

            strClapEventCehck = "SECOND";
        }
        else if (clapAmount > 70 && clapAmount < 100)
        {
            /// 체력 +3
            int plusHP = UserInfoManager.Instance.getSaveHP() + 3;
            UserInfoManager.Instance.setSaveHP(plusHP);

            listState.Add(Strings.SAVE_CAT_HP);
            listPoint.Add(3);

            strClapEventCehck = "THIRD";

        }
        else
        {
            // TYPE_CHAR
            /// 카리스마 +5
            /// 체력 + 5
            int plusHP = UserInfoManager.Instance.getSaveHP() + 5;
            UserInfoManager.Instance.setSaveHP(plusHP);

            int getState = Mathf.Min(100, UserInfoManager.Instance.getSaveState(Strings.TYPE_CHAR) + 5)  ;
            UserInfoManager.Instance.setSaveState(Strings.TYPE_CHAR, getState);

            listState.Add(Strings.SAVE_CAT_HP);
            listPoint.Add(5);

            listState.Add(Strings.TYPE_CHAR);
            listPoint.Add(5);

            strClapEventCehck = "FOURTH";

        }

        while (true)
        {
            if (strClapEventCehck.Equals(listClapEventEndStory[clapEventIndex].type))
            {
                break;
            }
            clapEventIndex++;
        }
        LoadClapEventXmlStoryRead();
    }


    void LoadClapEventXmlStoryRead()
    {
        if (listClapEventEndStory != null)
        {
            NextClapEventXmlStory(listClapEventEndStory[clapEventIndex]);
        }
    }

    void NextClapEventXmlStory(xmlStandTalkBox Data_)
    {

        switch (Data_.type)
        {
            case "ZERO":
            case "FIRST":
            case "SECOND":
            case "THIRD":
            case "FOURTH":
                {
                    Label_Talk.text = Data_.standTalk;
                    break;
                }
            case "END":
                {
                    isClapEventEndMent = false;
                    btnEnd.gameObject.SetActive(true);
                    mBtn_Talk.GetComponent<BoxCollider2D>().enabled = false;
                    break;
                }
        }
    }




    ///<summary>
    /// 스토리 이벤트 체크
    ///</summary>
    bool SetStoryEventCheck(int day)
    {

        // 3, 8, 15, 20, 27, 32, 38, 45, 48
        if (day == 3 || day == 8 || day == 15 || day == 20 || day == 27 || day == 32
            || day == 38 || day == 45 || day == 48)
        {
            // isCheckStoryEvent = true;
            return true;
        }
        return false;
    }

    // 일반 액션 종료 
    void onClickCatActEnd()
    {

        SceneBase.Instance.PLAY_SE("BTN_CLOSED");

        SetCatState(STATE_UP_KEY, STATE_UP_POINT, STATE_DOWN_KEY, STATE_DOWN_POINT);

        if (ACTION_ACT != null && ACTION_ACT.GetInvocationList().Length > 0)
        {
            ACTION_ACT();
        }

        if (ACTION_STATEUP != null && ACTION_STATEUP.GetInvocationList().Length > 0)
        {
            ACTION_STATEUP(STATE_UP_KEY, STATE_UP_POINT, STATE_DOWN_KEY, STATE_DOWN_POINT);
        }

        /// 스토리 이벤트 체크 
        if (ACTION_STORY_EVENT != null && ACTION_STORY_EVENT.GetInvocationList().Length > 0)
        {
            ACTION_STORY_EVENT(SetStoryEventCheck(Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
            /// 스토리 이벤트 인덱스 
        }
        initActionScedule();
    }

    /// 휴식 행동 종료 
    void OnClickRestEnd()
    {
        SceneBase.Instance.PLAY_SE("BTN_CLOSED");

        SetRestAction();

        if (ACTION_REST != null && ACTION_REST.GetInvocationList().Length > 0)
        {
            ACTION_REST(Strings.TYPE_HP, CAT_HP_PLUS, STATE_KEY, DOWNPOINT);
        }
        /// 스토리 이벤트 체크 
        if (ACTION_STORY_EVENT != null && ACTION_STORY_EVENT.GetInvocationList().Length > 0)
        {
            ACTION_STORY_EVENT(SetStoryEventCheck(Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
            /// 스토리 이벤트 인덱스 
        }
        initActionScedule();
    }

    //// 박수 이벤트 종료 
    void OnClickEventEnd()
    {
        // SetEventAction();
        SceneBase.Instance.PLAY_SE("BTN_CLOSED");

        if (ACTION_CLAP_EVENT != null && ACTION_CLAP_EVENT.GetInvocationList().Length > 0)
        {
            ACTION_CLAP_EVENT(listState, listPoint);
        }

        /// 스토리 이벤트 체크 
        if (ACTION_STORY_EVENT != null && ACTION_STORY_EVENT.GetInvocationList().Length > 0)
        {
            ACTION_STORY_EVENT(SetStoryEventCheck(Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
            /// 스토리 이벤트 인덱스 
        }
        initActionScedule();
    }



    public void initActionScedule()
    {
        //// 박수 애니메이션 
        isActionClap = false;
        clapAmount = 0;
        Timer = 10;
        /// 대화 애니메이션 
        isActionTalk = false;
        mTalk_Index = 0;

        mBtn_Talk.GetComponent<BoxCollider2D>().enabled 
            = SetStoryEventCheck(Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()); // false;
        Object_Clap.GetComponent<BoxCollider2D>().enabled = false;


        /// 대선토론 이벤트 bool값 초기ㅘ
        isStopEvent = false;
        clapEventIndex = 0;
        strClapEventCehck = "";
        if (listClapEventEndStory != null)
        {
            listClapEventEndStory.Clear();
        }

        if (listClapEventStarMnet != null)
        {
            listClapEventStarMnet.Clear();
        }


        /// 스케줄 쵸기화 
        _eScedule = eScedule.NONE;

        /// 배열 초기화
        Array.Clear(TALK_MENT, 0, TALK_MENT.Length);
        Array.Clear(STATE_UP_KEY, 0, STATE_UP_KEY.Length);
        Array.Clear(STATE_UP_POINT, 0, STATE_UP_POINT.Length);
        Array.Clear(STATE_DOWN_KEY, 0, STATE_DOWN_KEY.Length);
        Array.Clear(STATE_DOWN_POINT, 0, STATE_DOWN_POINT.Length);
        Array.Clear(CAT_ANIM_IMG, 0, CAT_ANIM_IMG.Length);
        
        // TALK_MENT.Initialize();
        // STATE_UP_KEY.Initialize();
        // STATE_UP_POINT.Initialize();
        // STATE_DOWN_KEY.Initialize();
        // STATE_DOWN_POINT.Initialize();
        // CAT_ANIM_IMG.Initialize();
        if (listState != null)
        {
            listState.Clear();
        }
        if (listPoint != null)
        {
            listPoint.Clear();
        }

        // Label_Talk.text = "오늘의 일정을 선택하시라옹!";
    }

}
