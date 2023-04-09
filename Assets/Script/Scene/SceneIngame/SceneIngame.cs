using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;
using System.Linq;
using System;



public class SceneIngame : SceneBase
{
    /// 버튼 스트링 
    string[,] button_name = new string[,]{
        {"select_0_0","select_0_1","select_0_2","select_0_3"},
        {"select_1_0","select_1_1","select_1_2","select_1_3"},
        {"select_2_0","select_2_1","select_2_2","select_2_3"},
        {"select_3_0","select_3_1","select_3_2","select_3_3"},
        {"","","",""},
        {"select_4_0","select_4_1","select_4_2","select_4_3"},
        {"select_5_0","select_5_1","select_5_2","select_5_3"},
        {"","","",""}
    };

    public GameObject objRoot;
    public GameObject objPopupRootView;

    [Header("대기화면 토크박스")]
    public GameObject objBubbleTalkBox;
    public GameObject objStandTalkBox;
    public UIButton btnSatanTalkBox;
    public UILabel labelStateTalkBox;
    [Header("대기화면 4가지 스케쥴")]
    public GameObject objSchedule;
    [Header("좌우 유세하기")]
    public GameObject objLeftButton;
    public GameObject objRightButton;
    public UIButton[] btnActive;
    [Header("하단 스케쥴")]
    public UIButton[] btnSchedule; /// 유세하기 누르면 나오는 네가지 스케쥴
    [Header("휴식 오브젝트")]
    public GameObject objRest;
    public UIButton btnRest;
    [Header("대기화면 오브젝트")]
    public GameObject objStand;
    [Header("액션 오브젝트")]
    public GameObject objAction;
    [Header("인게임 대기화면 케릭터")]
    public UI2DSpriteAnimation SpriteAnimCatStand;

    [Header("액션 스크립트")]
    public ActionScedule CatActionScedule;
    [Header("인게임 고양이 액션")]
    public UI2DSpriteAnimation SpriteAnimActionCat;

    [Header("액션종료")]
    public UIButton btnActionEnd;

    [Header("체럭 벨류값")]
    public UIProgressBar HPProgress;
    public UILabel labelHP;

    [Header("상단 고양이대화")]
    public UILabel labelStandCatTalk;

    [Header("날짜 카운트")]
    public UILabel labelDayCnt;

    [Header("대선토론")]
    public UIButton btnEventClap;

    private int activeIndex = -1; // 어떤 유세눌렀는지 
    private int scheduleIndex = 0; // 그 유세에서 어떤거 선택 했는지 
    private bool isActionEnding = false; /// 엔딩 체크

    [Header("엔딩")]
    public GameObject objEnding;
    public UILabel labelEnding;
    public UIButton btnEnding;

    public UILabel labelTestCoin;


    [Header("이벤트스토리")]
    List<xmlAlice> listStoryEvent;
    public GameObject objStoryEvent;
    public UIButton btnStoryLeft;
    public UILabel labelStoryLeft;
    public UIButton btnStoryRight;
    public UILabel labelStoryRight;

    [Header("추가")]
    public Animation animCatStand;
    public UIButton btnCatStanAnim;
    private bool isActionCatStanAnima = false;

    /****************************************************************************************************/

    public GameObject objVideoUntiyHp;
    public UIButton btnVideoUnityHp;

    public GameObject objVideoUnityCoin;
    public UIButton btnVideoUnityCoin;


    void OnEnable()
    {
        ActionScedule.ACTION_ACT += SetCatHPSetting;
        ActionScedule.ACTION_STATEUP += SetPopupStateUp;
        ActionScedule.ACTION_REST += SetRestPopupStateUp;
        ActionScedule.ACTION_CLAP_EVENT += SetClapEventPopupStateUp;

        ActionScedule.ACTION_STORY_EVENT += SetStoryEventCheck;

        SceneBase.RefreshNotification += CoinCheck;

        Popup_Store.ACTION_REFRASH += CoinCheck;
        Popup_Store.ACTION_REFRASH += SetCatImgSetting;

        JHAdsManager.ACTION_REWARD_UNITY += CoinCheck;


    }

    void OnDisable()
    {

        ActionScedule.ACTION_ACT -= SetCatHPSetting;
        ActionScedule.ACTION_STATEUP -= SetPopupStateUp;
        ActionScedule.ACTION_REST -= SetRestPopupStateUp;
        ActionScedule.ACTION_CLAP_EVENT -= SetClapEventPopupStateUp;

        ActionScedule.ACTION_STORY_EVENT -= SetStoryEventCheck;

        SceneBase.RefreshNotification -= CoinCheck;

        SceneBase.RefreshNotification -= CoinCheck;
        Popup_Store.ACTION_REFRASH -= SetCatImgSetting;

        JHAdsManager.ACTION_REWARD_UNITY += CoinCheck;


    }

    void CoinCheck()
    {
        labelTestCoin.text = string.Format("내 쮸르 : {0:N}개", UserInfoManager.Instance.GetCoin());
    }



    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        for (int i = 0; i < btnActive.Length; i++)
        {
            EventDelegate del = new EventDelegate(this, "onClickAct");
            EventDelegate.Parameter parm = new EventDelegate.Parameter();
            parm.value = (Enums.eActive)i;
            parm.expectedType = typeof(Enums.eActive);
            del.parameters[0] = parm;
            EventDelegate.Add(btnActive[i].onClick, del);

        }

        for (int i = 0; i < btnSchedule.Length; i++)
        {
            EventDelegate del = new EventDelegate(this, "onClickActveScadule");
            EventDelegate.Parameter parm = new EventDelegate.Parameter();
            parm.value = i;
            parm.expectedType = typeof(int);
            del.parameters[0] = parm;
            EventDelegate.Add(btnSchedule[i].onClick, del);

        }

        btnRest.onClick.Add(new EventDelegate(OnClickRest));
        btnActionEnd.onClick.Add(new EventDelegate(onClickCatActEnd));
        btnEventClap.onClick.Add(new EventDelegate(OnClickEventClap));

        btnEnding.onClick.Add(new EventDelegate(OnClickEnding));

        btnVideoUnityHp.onClick.Add(new EventDelegate(onClickVideoUntiyHp));
        btnVideoUnityCoin.onClick.Add(new EventDelegate(onClickVideoUntiyCoin));

        btnCatStanAnim.onClick.Add(new EventDelegate(onClickCatStanAnim));


    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
    }


    IEnumerator Start()
    {
        CoinCheck();

        SetCatImgSetting();
        SetCatHPSetting();
        SetActiveEventClap((Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));

        yield return new WaitForEndOfFrame();
        if (UserInfoManager.Instance.getSaveTutorial() == false)
        {
            Popup_Tutorial tutorial = Instantiate<Popup_Tutorial>(Resources.Load<Popup_Tutorial>("Prefab/Popup/Popup_Tutorial"));
            tutorial.transform.SetParent(objPopupRootView.transform);
            tutorial.transform.localPosition = Vector2.zero;
            tutorial.transform.localScale = Vector3.one;
        }

    }


    /****************************************************************************************************/
    ///<summery>
    /// 행동버튼 눌렀을때 아래 엑티브 UI 변경 
    ///</summery>
    public void onClickAct(eActive btnActive)
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (isActionEnding == true)
            return;

        activeIndex = (int)btnActive;

        switch (btnActive)
        {
            /// 휴식일 경유
            case Enums.eActive.REST:
                {
                    objStandTalkBox.SetActive(false);
                    objSchedule.SetActive(false);
                    objRest.SetActive(true);
                    break;
                }
            /// 팝업으로 스텟보기
            case Enums.eActive.STATE:
                {
                    Popup_State popup = Instantiate<Popup_State>(Resources.Load<Popup_State>("Prefab/Popup/Popup_State"), objPopupRootView.transform) as Popup_State;
                    popup.transform.localPosition = Vector2.zero;
                    popup.transform.localScale = Vector3.one;
                    popup.SetPopup_State();
                    break;
                }

            default:
                {
                    objStandTalkBox.SetActive(false);
                    objSchedule.SetActive(true);
                    objRest.SetActive(false);

                    SetActSelectUI(activeIndex);
                    break;
                }
        }
    }

    /****************************************************************************************************/
    ///<summery>
    /// 행동버튼 눌렀을때 아래 내가 할 유새선택  
    ///</summery>
    public void onClickActveScadule(int _scheduleIndex)
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (isActionEnding == true)
            return;

        eActive _active = (eActive)activeIndex;
        int _schedule = (activeIndex * 4) + _scheduleIndex;
        Debug.LogError("###################### _active : " + _active + " // clac : " + _schedule + " // eScedule : " + (eScedule)_schedule);

        objStand.SetActive(false);
        objStandTalkBox.SetActive(false);
        objRest.SetActive(false);
        objSchedule.SetActive(false);


        CatActionScedule.SetActionScedule(_active, (eScedule)_schedule);

        if (SpriteAnimActionCat.enabled == false)
        {
            SpriteAnimActionCat.enabled = true;
        }
    }
    /****************************************************************************************************/
    // 액션 종료 
    public void onClickCatActEnd()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (isActionEnding == true)
            return;

        SetCatHPSetting();
        SetCatImgSetting();
    }
    /****************************************************************************************************/
    void SetCatHPSetting()
    {

        objAction.SetActive(false);
        objStand.SetActive(true);


        objStandTalkBox.SetActive(true);
        objSchedule.SetActive(false);
        objRest.SetActive(false);

        // Debug.LogError("################## 남은날짜 : " + (Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
        labelDayCnt.text = string.Format("Day - {0}", (Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
        labelHP.text = string.Format("{0} / {1}", UserInfoManager.Instance.getSaveHP(), Integers.CAT_HP);
        // Debug.LogError("###################### UserInfoManager.Instance.getSaveHP() : " + UserInfoManager.Instance.getSaveHP());
        objVideoUntiyHp.SetActive(UserInfoManager.Instance.getSaveHP() < Integers.CAT_HP);

        float hp_value = (Integers.CAT_HP - UserInfoManager.Instance.getSaveHP()) * 0.01f;
        HPProgress.GetComponent<UIProgressBar>().value = hp_value;

        SetActiveEventClap((Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
        SetUnityCoinCheck((Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
        SetEndingCheck();

    }

    ///<summary>
    /// 오른쪽 유니티 비디오 광고 
    /// Visible Check
    ///</summary>
    void SetUnityCoinCheck(int count)
    {
        objVideoUnityCoin.SetActive(count % 5 == 0);
    }


    void SetPopupStateUp(string[] up_key_, int[] up_point_, string[] down_key_, int[] down_pont_)
    {
        Popup_StateUp popup = Instantiate<Popup_StateUp>(Resources.Load<Popup_StateUp>("Prefab/Popup/Popup_StateUp"), objPopupRootView.transform) as Popup_StateUp;
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        popup.SetPopupActiveStateUp(up_key_, up_point_, down_key_, down_pont_);
        popup.gameObject.SetActive(true);

        SetCatHPSetting();
        SetCatImgSetting();
        SetActiveEventClap((Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));

    }
    /****************************************************************************************************/

    ///<summary>
    /// 휴식 버튼
    ///</summary>
    void OnClickRest()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (isActionEnding == true)
            return;

        eActive _active = (eActive)activeIndex;

        objStand.SetActive(false);
        objStandTalkBox.SetActive(false);
        objRest.SetActive(false);
        objSchedule.SetActive(false);


        //// 이부분부터 수정 할것
        CatActionScedule.SetActionRest(eActive.REST, eScedule.eRest);

        if (SpriteAnimActionCat.enabled == false)
        {
            SpriteAnimActionCat.enabled = true;
        }
    }

    ///<summary>
    /// 휴식 후 보여줄 상태 팝업
    ///</summary>
    void SetRestPopupStateUp(string up_key_, int up_point_, string[] down_key_, int[] down_pont_)
    {
        Popup_StateUp popup = Instantiate<Popup_StateUp>(Resources.Load<Popup_StateUp>("Prefab/Popup/Popup_StateUp"), objPopupRootView.transform) as Popup_StateUp;
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        popup.SetPopupRestStateUp(up_key_, up_point_, down_key_, down_pont_);
        popup.gameObject.SetActive(true);

        SetCatHPSetting();
        SetCatImgSetting();
        SetActiveEventClap((Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
    }

    void SetClapEventPopupStateUp(List<string> _state, List<int> _point)
    {
        Popup_StateUp popup = Instantiate<Popup_StateUp>(Resources.Load<Popup_StateUp>("Prefab/Popup/Popup_StateUp"), objPopupRootView.transform) as Popup_StateUp;
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        popup.SetPopupEventState(_state, _point);
        popup.gameObject.SetActive(true);

        SetCatHPSetting();
        SetCatImgSetting();
        SetActiveEventClap((Integers.CAT_DAY - UserInfoManager.Instance.getSaveDay()));
    }

    /****************************************************************************************************/


    ///<summery>
    /// 행동 버튼 이미지 변경 
    ///</summery>
    void SetActSelectUI(int act_Index)
    {
        for (int i = 0; i < btnSchedule.Length; i++)
        {
            btnSchedule[i].normalSprite = button_name[act_Index, i];
        }
    }

    /// <summary>
    /// 대선 토론 이벤트
    /// </summary>
    void SetActiveEventClap(int day)
    {
        bool dayCheck = (day == 40 || day == 25 || day == 10);
        // bool dayCheck = true;

        for (int i = 0; i < btnActive.Length; i++)
        {
            btnActive[i].gameObject.SetActive(!dayCheck);
        }
        btnEventClap.gameObject.SetActive(dayCheck);
    }

    /****************************************************************************************************/


    ///<summary>
    /// 대선토론 이벤트
    ///</summary>
    void OnClickEventClap()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (isActionEnding == true)
            return;

        objStand.SetActive(false);
        objStandTalkBox.SetActive(false);
        objRest.SetActive(false);
        objSchedule.SetActive(false);


        CatActionScedule.SetActionEvent(eActive.EVENT, eScedule.eEvent);

        if (SpriteAnimActionCat.enabled == false)
        {
            SpriteAnimActionCat.enabled = true;
        }
    }

    /****************************************************************************************************/
    ///<summery>
    /// 키 종류
    /// 카리스마 / CHAR	/ charisma_img
    /// 인지도 / AWAR / popular_img
    /// 성품 / NATU / kind_img
    /// 매력 / CHARM / cute_img
    /// 화술  / TALK	/ talk_img
    /// 외교 / DIPLO	/ abcd_img
    /// 경제 / ECO	/ economy_img
    /// 문화 / CUR / culture_img
    ///</summery>
    string[,] CAT_ANIM_IMG = new string[,]{
        {"ch_defalut_1", "ch_defalut_2"},
        {"ch_room_charisma1","ch_room_charisma2"},  // 카리스마
        {"ch_room_popular1","ch_room_popular2"}, // 인지도
        {"ch_room_kind1","ch_room_kind2"}, // 성품
        {"ch_room_cute1","ch_room_cute2"}, // 매력
        {"ch_room_talk1","ch_room_talk2"}, // 화술
        {"ch_room_abcd1","ch_room_abcd2"}, // 외교
        {"ch_room_economy1","ch_room_economy2"}, // 경제
        {"ch_room_culture1","ch_room_culture2"}, // 문화
        {"ch_room_tired1", "ch_room_tired2"}
    };
    static readonly string[] STATE_KEY = new string[]{
            Strings.TYPE_NONE, Strings.TYPE_CHAR, Strings.TYPE_AWAR, Strings.TYPE_NATU, Strings.TYPE_CHARM,
            Strings.TYPE_TALK, Strings.TYPE_DIPLO, Strings.TYPE_ECO, Strings.TYPE_CUR, Strings.TYPE_HP
    };

    int reutrnKeyIndex(string stateKey)
    {
        int index = 0;
        switch (stateKey)
        {
            case Strings.TYPE_NONE: { index = 0; break; }
            case Strings.TYPE_CHAR: { index = 1; break; }
            case Strings.TYPE_AWAR: { index = 2; break; }
            case Strings.TYPE_NATU: { index = 3; break; }
            case Strings.TYPE_CHARM: { index = 4; break; }
            case Strings.TYPE_TALK: { index = 5; break; }
            case Strings.TYPE_DIPLO: { index = 6; break; }
            case Strings.TYPE_ECO: { index = 7; break; }
            case Strings.TYPE_CUR: { index = 8; break; }
            case Strings.TYPE_HP: { index = 9; break; }
        }

        return index;
    }
    List<xmlStandTalkBox> getRandomTalk;
    string tempState = "";
    ///<summery>
    /// 현재 내 스텟중에 제일 높은거 가져와서 
    /// 그 스텟에 맞게 이미지 변경 함 
    ///</summery>
    public void SetCatImgSetting()
    {
        SceneBase.Instance.PLAY_BGM("MAIN");

        Dictionary<string, int> dicCatState = new Dictionary<string, int>();
        for (int i = 0; i < STATE_KEY.Length; i++)
        {
            int crrPoint = UserInfoManager.Instance.getSaveState(STATE_KEY[i]);
            dicCatState.Add(STATE_KEY[i], crrPoint);
        }
        // Debug.LogError(dicCatState);
        /// 오름 차순 정렬
        var queryDesc = dicCatState.OrderByDescending(x => x.Value);

        List<string> listKey = new List<string>();
        List<int> listIndex = new List<int>();

        foreach (var dictionary in queryDesc)
        {
            listKey.Add(dictionary.Key);
            listIndex.Add(dictionary.Value);
        }
        int index_ = 0;

        index_ = (Mathf.Abs(listIndex[0] - listIndex[1]) >= 20) ? reutrnKeyIndex(listKey[0]) : 0;
        tempState = (Mathf.Abs(listIndex[0] - listIndex[1]) >= 20) ? listKey[0] : Strings.TYPE_NONE;

        // Debug.Log("제일높은 스텟 : " + tempState + " + 점수 : " + UserInfoManager.Instance.getSaveState(STATE_KEY[index_]) + "//  인덱스 번호 : " + index_);
        string anim_path = "Image/Cat/";
        /// 기본 케릭터 이미지 
        if (tempState.Equals("") || tempState.Equals(Strings.TYPE_NONE))
        {
            tempState = Strings.TYPE_NONE;
            /// 고양이 피곤한 이미지 
            if (UserInfoManager.Instance.getSaveHP() <= 20)
            {
                tempState = Strings.TYPE_HP;
                /// ch_room_tired1, ch_room_tired2
                SpriteAnimCatStand.frames[0] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, "ch_room_tired1"));
                SpriteAnimCatStand.frames[1] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, "ch_room_tired2"));
            }
            else
            {
                /// ch_defalut_1,ch_defalut_2
                SpriteAnimCatStand.frames[0] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, "ch_defalut_1"));
                SpriteAnimCatStand.frames[1] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, "ch_defalut_2"));
            }
        }
        /// 스텟에 맞는 케릭터 이미지 
        else
        {
            /// 고양이 피곤한 이미지 
            if (UserInfoManager.Instance.getSaveHP() <= 20)
            {
                tempState = Strings.TYPE_HP;
                /// ch_room_tired1, ch_room_tired2
                SpriteAnimCatStand.frames[0] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, "ch_room_tired1"));
                SpriteAnimCatStand.frames[1] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, "ch_room_tired2"));
            }
            else
            {
                SpriteAnimCatStand.frames[0] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, CAT_ANIM_IMG[index_, 0]));
                SpriteAnimCatStand.frames[1] = Resources.Load<Sprite>(string.Format("{0}{1}", anim_path, CAT_ANIM_IMG[index_, 1]));

            }

        }

        getRandomTalk = SceneBase.Instance.dataManager.GetSandTalkData(tempState);
        int random_0 = UnityEngine.Random.Range(0, (getRandomTalk.Count - 1));
        labelStandCatTalk.text = getRandomTalk[random_0].standTalk.ToString();

        int random_1 = UnityEngine.Random.Range(0, (getRandomTalk.Count - 1));
        labelStateTalkBox.text = getRandomTalk[random_1].standTalk.ToString();

        CoinCheck();
    }

    /****************************************************************************************************/
    int endingIndex = 0;
    Dictionary<string, int> dicStateRank = new Dictionary<string, int>();
    List<string> listEndingState = new List<string>();
    List<int> listEndingPoint = new List<int>();

    void OnClickEnding()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        /// 날짜가 다 됐을 경우
        Dictionary<string, object> mDicData = new Dictionary<string, object>();
        mDicData.Add(Strings.MOVE_TYPE, string.Format("{0}@{1}", Strings.SCENE_INGAME, endingIndex));

        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_REPLACE, Strings.SCENE_ENDING, mDicData);

    }

    void SetEndingCheck()
    {
        /// 0순위 체력 

        if (UserInfoManager.Instance.getSaveHP() <= 0)
        {
            isActionEnding = true;

            objAction.SetActive(false);
            objStand.SetActive(true);
            objStandTalkBox.SetActive(false);
            objSchedule.SetActive(false);
            objRest.SetActive(false);

            objEnding.SetActive(true);

            endingIndex = 0;

            labelStandCatTalk.text = "체력이 고갈됐다옹...\n우린 여기까진가봐옹...";
        }
        /// 1순위 날짜종료 
        else
        {
            /// 날짜가 엔딩일 경우에만
            if (Integers.CAT_DAY <= UserInfoManager.Instance.getSaveDay())
            {
                isActionEnding = true;

                objAction.SetActive(false);
                objStand.SetActive(true);
                objStandTalkBox.SetActive(false);
                objSchedule.SetActive(false);
                objRest.SetActive(false);

                objEnding.SetActive(true);

                for (int i = 0; i < STATE_KEY.Length; i++)
                {
                    if (STATE_KEY[i].Equals(Strings.TYPE_NONE) || STATE_KEY[i].Equals(Strings.TYPE_HP))
                    {
                        continue;
                    }

                    if (dicStateRank.ContainsKey(STATE_KEY[i]) == false)
                    {
                        dicStateRank.Add(STATE_KEY[i], UserInfoManager.Instance.getSaveState(STATE_KEY[i]));
                    }
                    else
                    {
                        dicStateRank[STATE_KEY[i]] = UserInfoManager.Instance.getSaveState(STATE_KEY[i]);
                    }

                }

                /// 오름 차순 정렬
                var queryDesc = dicStateRank.OrderByDescending(x => x.Value);

                foreach (var dictionary in queryDesc)
                {
                    // Debug.LogError("########################### dictionary.key : " + dictionary.Key + " // dictionary.val : " + dictionary.Value);
                    listEndingState.Add(dictionary.Key);
                    listEndingPoint.Add(dictionary.Value);
                }

                endingIndex = CheckEndingIndex(listEndingState, listEndingPoint);
            }
        }

    }


    int CheckEndingIndex(List<string> state, List<int> point)
    {
        int returnPoint = 0;
        bool specialCheck = false;
        List<int> Listspecial = new List<int>();
        if (point[0] < 30)
        {
            /// 이도저도 아닌엔딩 
            return returnPoint = 1;
        }
        else if (point[0] > 30 && point[0] < 50)
        {
            /// 재출ㄹ마 엔딩 
            return returnPoint = 2;
        }
        else
        {

            //특정 스탯들의 합이 특정 수치 이상일 때 
            // 뜨는 복합엔딩(?)

            // 엔딩 no. 20부터 엔딩 no.24 까지 총 5개.

            // 상태별 타입값
            // 카리스마 / CHAR,  인지도 / AWAR, 성품 / NATU	, 매력 / CHARM	,화술 / TALK	,
            // 외교 / DIPLO	,경제 / ECO	,문화 / CUR

            // 엔딩 no. 20 -> 카리스마 + 외교지식 스탯의 합이 130 이상인 경우.
            if (dicStateRank[Strings.TYPE_CHAR] + dicStateRank[Strings.TYPE_DIPLO] >= 130)
            {
                returnPoint = 19;
                specialCheck = true;
                Listspecial.Add(returnPoint);
            }

            // 엔딩 no. 21 -> 성품 + 경제지식 스탯의 합이 130 이상인 경우. 
            if (dicStateRank[Strings.TYPE_NATU] + dicStateRank[Strings.TYPE_ECO] >= 130)
            {
                returnPoint = 20;
                specialCheck = true;
                Listspecial.Add(returnPoint);
            }
            // 엔딩 no. 22 -> 인지도 + 문화지식 스탯의 합이 130 이상인 경우. 
            if (dicStateRank[Strings.TYPE_AWAR] + dicStateRank[Strings.TYPE_CUR] >= 130)
            {
                returnPoint = 21;
                specialCheck = true;
                Listspecial.Add(returnPoint);
            }
            // 엔딩 no. 23 -> 외교지식 + 경제지식 + 문화지식 스탯의 합이 200 이상인 경우. 
            if (dicStateRank[Strings.TYPE_DIPLO] + dicStateRank[Strings.TYPE_ECO] + dicStateRank[Strings.TYPE_CUR] >= 200)
            {
                returnPoint = 22;
                specialCheck = true;
                Listspecial.Add(returnPoint);
            }
            // 엔딩 no. 24 -> 매력 + 화술 스탯의 합이 130 이상인 경우. 
            if (dicStateRank[Strings.TYPE_CHARM] + dicStateRank[Strings.TYPE_TALK] >= 130)
            {
                returnPoint = 23;
                specialCheck = true;
                Listspecial.Add(returnPoint);
            }


            // "(가장 높은 스탯) - (두 번째로 높은 스탯) 의 값이 50 이상인 경우
            // 예: 카리스마 60, 매력 10, 인품 5, ….이런 경우"
            if ((point[0] - point[1]) >= 50)
            {
                // Strings.TYPE_CHAR, Strings.TYPE_AWAR, Strings.TYPE_NATU, Strings.TYPE_CHARM,
                // Strings.TYPE_TALK, Strings.TYPE_DIPLO, Strings.TYPE_ECO, Strings.TYPE_CUR
                switch (state[0])
                {
                    case Strings.TYPE_CHAR: { returnPoint = 3; break; }
                    case Strings.TYPE_AWAR: { returnPoint = 4; break; }
                    case Strings.TYPE_NATU: { returnPoint = 5; break; }
                    case Strings.TYPE_CHARM: { returnPoint = 6; break; }
                    case Strings.TYPE_TALK: { returnPoint = 7; break; }
                    case Strings.TYPE_DIPLO: { returnPoint = 8; break; }
                    case Strings.TYPE_ECO: { returnPoint = 9; break; }
                    case Strings.TYPE_CUR: { returnPoint = 10; break; }
                }
            }
            // (가장 높은 스탯) - (두 번째로 높은 스탯) 의 값이 50 미만인 경우
            // 예: 카리스마 60, 매력 40, 인품 10, ….이런 경우
            else if ((point[0] - point[1]) < 50)
            {
                switch (state[0])
                {
                    case Strings.TYPE_CHAR: { returnPoint = 11; break; }
                    case Strings.TYPE_AWAR: { returnPoint = 12; break; }
                    case Strings.TYPE_NATU: { returnPoint = 13; break; }
                    case Strings.TYPE_CHARM: { returnPoint = 14; break; }
                    case Strings.TYPE_TALK: { returnPoint = 15; break; }
                    case Strings.TYPE_DIPLO: { returnPoint = 16; break; }
                    case Strings.TYPE_ECO: { returnPoint = 17; break; }
                    case Strings.TYPE_CUR: { returnPoint = 18; break; }
                }
            }
        }

        if (specialCheck == true)
        {
            int tempPoint = -1;
            for (int i = 0; i < Listspecial.Count; i++)
            {
                tempPoint = Listspecial[i] > tempPoint ? Listspecial[i] : tempPoint;
            }
            returnPoint = tempPoint;
        }

        return returnPoint;
    }

    /****************************************************************************************************/
    public void onClickVideoUntiyHp()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (SceneBase.Instance.adsManager.GetIsActionAds == true)
        {
            SceneBase.Instance.ShowToast("짧은 영상을 불러오는 중입니다\n잠시만 기다려주세요.");
        }
        else
        {
            StartCoroutine(SceneBase.Instance.adsManager.IEShowUnityAdsVideo(Strings.VIDEO_REWARD_HP));
        }
    }

    public void onClickVideoUntiyCoin()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (SceneBase.Instance.adsManager.GetIsActionAds == true)
        {
            SceneBase.Instance.ShowToast("짧은 영상을 불러오는 중입니다\n잠시만 기다려주세요.");
        }
        else
        {
            StartCoroutine(SceneBase.Instance.adsManager.IEShowUnityAdsVideo(Strings.VIDEO_REWARD_COIN));
        }
    }




    void VideoReward(string sender, string args)
    {
        // SceneBase.Instance.adsManager.GetIsActionAds = true;
        // StartCoroutine(SceneBase.Instance.adsManager.IEAdsCheck());

        // Debug.LogError("VideoReward Sender : " + sender.ToString() + " // EventArgs : " + args.ToString());
        switch (sender)
        {
            case Strings.VIDEO_REWARD_COIN:
                {
                    int plusCoint = int.Parse(UserInfoManager.Instance.GetCoin()) + int.Parse(args);
                    UserInfoManager.Instance.SetSaveCoin(plusCoint.ToString());
                    StartCoroutine(IEToast(string.Format("쮸르 {0}개를 받았습니다.", args)));

                    CoinCheck();
                    break;
                }

            case Strings.VIDEO_REWARD_HP:
                {
                    int plusHP = UserInfoManager.Instance.getSaveHP() + Convert.ToInt32(args);
                    UserInfoManager.Instance.setSaveHP(plusHP);
                    StartCoroutine(IEToast(string.Format("현재 체력에서 +{0}가\n더해져 {1}됩니다.", args, plusHP)));

                    SetCatHPSetting();
                    break;
                }
        }

    }

    void CatRefrash()
    {
        SetCatImgSetting();
        SetCatHPSetting();
    }

    IEnumerator IEToast(string text)
    {
        yield return YieldHelper.waitForSeconds(1000);
        SceneBase.Instance.ShowToast(text);
    }

    /****************************************************************************************************/
    //// 스토리 이벤트 
    bool isActionTalk = false;
    void InvoeTalk()
    {
        isActionTalk = false;
    }
    void OnClickStoryEvent()
    {
        if (isActionTalk == false)
        {
            endStoryIndex++;
            NextXmlStory(listStoryEvent[endStoryIndex]);

            isActionTalk = true;
            Invoke("InvoeTalk", 0.5f);

            // LoadEndEventXmlRead();
        }
    }

    int endStoryIndex = 0;
    string leftCheck = "";
    string rightCheck = "";
    Vector2 LeftPos = new Vector2(-144f, 0f);
    Vector2 RightPos = new Vector2(158f, 0f);

    // bool dayCheck = false;
    ///<summary>
    /// 스토리 이벤트 체크
    ///</summary>
    void SetStoryEventCheck(bool day)
    {
        // dayCheck = day;
        // 3, 8, 15, 20, 27, 32, 38, 45, 48
        if (day == true)
        {
            objBubbleTalkBox.SetActive(false);
            objLeftButton.SetActive(false);
            objRightButton.SetActive(false);

            objStandTalkBox.GetComponent<BoxCollider2D>().enabled = true;

            btnSatanTalkBox.onClick.Clear();
            btnSatanTalkBox.onClick.Add(new EventDelegate(OnClickStoryEvent));

            btnStoryLeft.onClick.Clear();
            btnStoryRight.onClick.Clear();

            btnStoryLeft.onClick.Add(new EventDelegate(OnClickStoryLeft));
            btnStoryRight.onClick.Add(new EventDelegate(OnClickStoryRight));

            if (listStoryEvent != null)
            {
                listStoryEvent.Clear();
            }

            int randomPos = UnityEngine.Random.Range(0, 1);
            int randomFile = UnityEngine.Random.Range(0, 29);
            string path_ = string.Format("Data/Xml/StoryEvent/choice_{0}", randomFile);
            // string path_ = "Data/Xml/StoryEvent/choice_25";
            Debug.LogError("################## path : " + path_);

            listStoryEvent = XmlParser.Read<xmlAlice>(path_);
            for (int i = 0; i < listStoryEvent.Count; i++)
            {
                if (listStoryEvent[i].type.Equals("CHECK"))
                {
                    leftCheck = listStoryEvent[i].checkL;
                    rightCheck = listStoryEvent[i].checkR;

                    labelStoryLeft.text = listStoryEvent[i].checkState;
                    labelStoryRight.text = listStoryEvent[i].checkValue;
                    break;
                }

            }


            if (randomPos == 0)
            {
                btnStoryLeft.transform.localPosition = LeftPos;
                btnStoryRight.transform.localPosition = RightPos;
            }
            else
            {
                btnStoryLeft.transform.localPosition = RightPos;
                btnStoryRight.transform.localPosition = LeftPos;
            }

            LoadEndEventXmlRead();
        }

    }

    void LoadEndEventXmlRead()
    {
        NextXmlStory(listStoryEvent[endStoryIndex]);
    }

    void NextXmlStory(xmlAlice Data_)
    {
        if (listStoryEvent != null)
        {
            switch (Data_.type)
            {
                case "NORMAL":
                case "FAIL":
                case "SUCCESS":
                    {
                        labelStateTalkBox.text = Data_.talk;
                        if (string.IsNullOrEmpty(Data_.upState) == false && string.IsNullOrEmpty(Data_.upPoint) == false)
                        {
                            int crrPoint = 0;
                            if (Data_.upState.Equals("HP"))
                            {
                                crrPoint = UserInfoManager.Instance.getSaveHP() + int.Parse(Data_.upPoint);
                                UserInfoManager.Instance.setSaveHP(Mathf.Min(100, crrPoint));
                            }
                            else if (Data_.upState.Equals("COIN"))
                            {
                                crrPoint = int.Parse(UserInfoManager.Instance.GetCoin()) + int.Parse(Data_.upPoint);
                                UserInfoManager.Instance.SetSaveCoin(crrPoint.ToString());
                            }
                            else
                            {
                                crrPoint = UserInfoManager.Instance.getSaveState(Data_.upState) + int.Parse(Data_.upPoint);
                                UserInfoManager.Instance.setSaveState(Data_.upState, crrPoint <= 0 ? 0 : Mathf.Min(100, crrPoint));
                            }
                        }
                        break;
                    }
                case "CHECK":
                    {
                        objStoryEvent.SetActive(true);
                        break;
                    }
                case "END":
                    {
                        endStoryIndex = 0;
                        objLeftButton.SetActive(true);
                        objRightButton.SetActive(true);
                        objBubbleTalkBox.SetActive(true);
                        objStandTalkBox.GetComponent<BoxCollider2D>().enabled = false;

                        CatActionScedule.initActionScedule();

                        listStoryEvent.Clear();
                        listStoryEvent = null;

                        SetCatHPSetting();
                        SetCatImgSetting();

                        break;
                    }
            }
        }
    }

    void OnClickStoryLeft()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        while (true)
        {
            if (listStoryEvent[endStoryIndex].type == leftCheck)
            {
                // NextXmlStory(listStoryEvent[endStoryIndex]);
                LoadEndEventXmlRead();
                objStoryEvent.SetActive(false);
                break;
            }
            endStoryIndex++;
        }

    }

    void OnClickStoryRight()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        while (true)
        {
            if (listStoryEvent[endStoryIndex].type == rightCheck)
            {
                LoadEndEventXmlRead();
                objStoryEvent.SetActive(false);
                break;
            }
            endStoryIndex++;
        }
    }

    void onClickCatStanAnim()
    {
        if (isActionCatStanAnima == false)
        {

            SceneBase.Instance.PLAY_SE("BTN_CAT_ANIM");

            // Debug.LogError("##################################### onClickCatStanAnim");
            isActionCatStanAnima = true;
            animCatStand.enabled = true;
            animCatStand.Play();
            Invoke("InvokeCatStanAnim", 0.3f);

            getRandomTalk = SceneBase.Instance.dataManager.GetSandTalkData(tempState);
            int random_0 = UnityEngine.Random.Range(0, (getRandomTalk.Count - 1));
            labelStandCatTalk.text = getRandomTalk[random_0].standTalk.ToString();

        }

    }

    void InvokeCatStanAnim()
    {
        isActionCatStanAnima = false;
    }

    ///<summary>
    /// 액션때문에 만듬 여기선 동작 안함
    ///</summary>
    void VideoRefrash(string type)
    {
        CoinCheck();

        switch (type)
        {
            case "Unity": { break; }
            case "AdMob": { break; }
        }
    }


}
