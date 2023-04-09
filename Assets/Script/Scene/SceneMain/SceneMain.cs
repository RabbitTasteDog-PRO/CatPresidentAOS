using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneMain : SceneBase
{
    public GameObject objRoot;

    public GameObject[] Object_Foot;

    // public UIButton[] btnMain;
    public UIButton btnStart;
    public UIButton btnAlbum;
    public UIButton btnQuit;


    bool tset = false;
    public GameObject objTest;
    public UIButton btnDel;
    public UIButton btnDayTest;
    public UIButton btnTestCoint;
    public UIButton btnCredit;
    public UIButton btnHP;

    FadeInOut fade;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        // fade.gameObject.SetActive(true);

        /// 테스트용
        btnStart.onClick.Add(new EventDelegate(OnClickStart));
        btnAlbum.onClick.Add(new EventDelegate(OnClickAlbum));
        btnQuit.onClick.Add(new EventDelegate(OnClickQuit));


        objTest.SetActive(tset);
        btnDel.onClick.Add(new EventDelegate(OnClickDel));
        btnDayTest.onClick.Add(new EventDelegate(OnClickDayTest));
        btnTestCoint.onClick.Add(new EventDelegate(OnClickTestCoint));
        btnCredit.onClick.Add(new EventDelegate(OnClickCredit));
        btnHP.onClick.Add(new EventDelegate(OnClickHP));


        // fade = SceneBase.Instance.mFadeInOut;
    }


    // Use this for initialization
    IEnumerator Start()
    {

        if (UserInfoManager.Instance.GetSaveSound() == true)
        {
            SceneBase.Instance.PLAY_BGM("TITLE_TUTO");
        }

        SceneBase.Instance.adsManager.RequestBanner();

        while (Instance.GetActionPrecess() == true)
        {
            yield return new WaitForSeconds(1.0f);
        }

        yield return new WaitForSeconds(0.6f);
        StartCoroutine(FootAnimation());

        // float fElapsedTime = 0f;
        // float fDuration = 1.0f;
        // float fUnitTime = 1.0f / fDuration;

        // while(true)
        // {
        //     fElapsedTime = fElapsedTime + (Time.deltaTime * fUnitTime);
        //     yield return new WaitForSeconds(1.0f);
        //     Debug.LogError("########## IEUnityAdsVideo fElapsedTime : " + fElapsedTime);

        // }


        // List<xmlData> list_ = XmlParser.Read<xmlData>("Data/Xml/CAT_MENT");
        // for(int i =0 ; i < list_.Count; i++)
        // {
        //     Debug.LogError("######### Active : " + list_[i].key + "\n ment1 : " + list_[i].ment_1 + " // ment2 : " + list_[i].ment_2);
        // }

    }

    IEnumerator FootAnimation()
    {

        // SceneBase.Instance.PLAY_BGM("MAIN_TUTO");


        yield return new WaitForSeconds(.5f);
        Object_Foot[0].SetActive(true);
        yield return new WaitForSeconds(.3f);
        Object_Foot[1].SetActive(true);
        yield return new WaitForSeconds(.3f);
        Object_Foot[2].SetActive(true);
        yield return new WaitForSeconds(.3f);
        Object_Foot[3].SetActive(true);

    }

    void OnClickStart()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_PUSH, Strings.SCENE_INGAME);
    }

    void OnClickAlbum()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");
        // SceneManager.LoadScene(Strings.SCENE_ALBUM);
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_PUSH, Strings.SCENE_ALBUM);
        // BCSceneManager.Instance.Action(BCSceneManager.ACTION.ACTION_PUSH, Strings.SCENE_INGAME);
    }

    void OnClickQuit()
    {
        Application.Quit();
    }

    void OnClickDel()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");
        SecurityPlayerPrefs.DeleteAll();
        SceneBase.Instance.ShowToast("데이터 삭제");
    }

    void OnClickDayTest()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");
        UserInfoManager.Instance.setSaveDay(50);
        SceneBase.Instance.ShowToast("날짜 50일");
    }

    void OnClickTestCoint()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");
        UserInfoManager.Instance.SetSaveCoin("10000");
        SceneBase.Instance.ShowToast("코인 10000");
    }

    void OnClickCredit()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_PUSHFORADD, Strings.SCENE_CREDIT);
    }

    void OnClickHP()
    {
        // UserInfoManager.Instance.setSaveHP(0);
        // if (SceneBase.Instance.adsManager.GetIsActionAds == true)
        // {
        //     SceneBase.Instance.ShowToast("동영상 광고는 1분마다 시청 가능합니다");
        //     Debug.LogError("################### GetIsActionAds " + SceneBase.Instance.adsManager.GetIsActionAds);
        //     return;
        // }
        // SceneBase.Instance.adsManager.GetIsActionAds = true;
        // StartCoroutine(SceneBase.Instance.adsManager.IEAdsCheck());

    }

    // Update is called once per frame
    // void Update()
    // {

    //     if (Input.GetKeyDown(KeyCode.Escape))
    //     {
    //         // 메인에서 백키 누르면 종료됨 
    //         Application.Quit();
    //     }

    // }

    IEnumerator Move_Scene(string scene_name)
    {
        SceneBase.Instance.ACTION_PROCESS = true;
        SceneBase.Instance.mFadeInOut.gameObject.SetActive(true);
        AnimationUtil.PopupAlphaIn(SceneBase.Instance.mFadeInOut.gameObject, null, 0.4f);
        TweenAlpha alpha_ = TweenAlpha.Begin(SceneBase.Instance.mFadeInOut.gameObject, 0.3f, 1);
        alpha_.PlayForward();
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(scene_name);
        yield return new WaitForEndOfFrame();
        AnimationUtil.PopupAlphaOut(SceneBase.Instance.mFadeInOut.gameObject, null, 0.6f);
        yield return new WaitForSeconds(0.4f);
        mFadeInOut.gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        SceneBase.Instance.ACTION_PROCESS = false;
    }

}
