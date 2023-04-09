using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Enums;

public class SceneBase : JHScene
{
    public bool ACTION_PROCESS = false;
    [HideInInspector]
    public GameObject UIRoot;
    [HideInInspector]
    public FadeInOut mFadeInOut;
    [HideInInspector]
    // public IOSSafeArea safeArea;
    // [HideInInspector]
    public JHDataReader dataReader;
    [HideInInspector]
    public NavigationController mNavigationController;
    [HideInInspector]
    public JHDataManager dataManager;
    [HideInInspector]
    public JHAdsManager adsManager;


    public string sender = "";
    public string amount = "";

    // public UICamera mainCamera;
    public Camera mainCamera;
    public PopupToast showToast;
    // public bool bDebugTest = false;
    protected bool isActionToast = false;
    public bool GetIsActionToast
    {
        get
        {
            return isActionToast;
        }
    }

    /***************************************************************************************/
    public EventDelegate.Callback mFadeInOutCallback;
    public EventDelegate.Callback mFadeInOutCallback_Two;
    const float FADE_DURATION = 0.25f;

    public delegate void Notification();
    public static event Notification RefreshNotification;
    /***************************************************************************************/

    ///싱글톤 객체
    private static SceneBase _instance = null;

    public static SceneBase Instance
    {
        ///중복 호출 방지
        // [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            if (_instance == null)
            {
                ///싱글톤 객체를 찾아서 넣는다.
                _instance = (SceneBase)FindObjectOfType(typeof(SceneBase));

                ///없다면 생성한다.
                if (_instance == null)
                {
                    string goName = typeof(SceneBase).ToString();
                    GameObject go = GameObject.Find(goName);
                    if (go == null)
                    {
                        go = new GameObject();
                        go.name = goName;
                    }
                    _instance = go.AddComponent<SceneBase>();
                }
            }
            return _instance;
        }
    }



    public NavigationController GetNavigationController()
    {
        if (mNavigationController = null)
        {
            mNavigationController = new NavigationController();
        }
        return mNavigationController;
    }





    // public AdMobManager adManger;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        // safeArea = new IOSSafeArea();
        dataReader = new JHDataReader();

        JHDataManager.Instance.initRelease();
        JHDataManager.Instance.initData();
        JHDataManager.Instance.LoadTextData();

        string inapp_0 = "";
        string inapp_1 = "";
#if UNITY_ANDROID || UNITY_EDITOR
        inapp_0 = Strings.INAPP_1000;
        inapp_1 = Strings.INAPP_3000;
#elif UNITY_IOS
        inapp_0 = Strings.IOS_INAPP_1000;
        inapp_1 = Strings.IOS_INAPP_3000;
#endif

        List<string> _inappList = new List<string>();
        _inappList.Add(inapp_0);
        _inappList.Add(inapp_1);
        JHIAPManager.Instance.InitalizeIAP(_inappList, InappCheck);


        dataManager = JHDataManager.Instance;
        adsManager = JHAdsManager.Instance;


        // Camera.main.
        // float height = mainCamera.orthographicSize;
        // float heightX2 = 2 * mainCamera.orthographicSize;
        // float width = heightX2 * mainCamera.aspect;



    }

    void GetADID()
    {
        Application.RequestAdvertisingIdentifierAsync(AdvertiveCallback);
    }

    void AdvertiveCallback(string id, bool check, string error)
    {
        Debug.LogError("Get IDFA => id : " + id + " // check : " + check + "// error : " + error);

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                {
                    Debug.LogError("Get ADID => id : " + id + " // check : " + check + "// error : " + error);
                    break;
                }

            case RuntimePlatform.IPhonePlayer:
                {
                    Debug.LogError("Get IDFA => id : " + id + " // check : " + check + "// error : " + error);
                    break;
                }

            default:
                {
                    Debug.LogError("Get ADID => id : " + id + " // check : " + check + "// error : " + error);
                    break;
                }
        }
    }

    void InappCheck()
    {
        Debug.Log("Inapp Check");
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        PopupToast.ACTION_TOAST = isActonToast;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        PopupToast.ACTION_TOAST = null;
    }

    // Use this for initialization 
    IEnumerator Start()
    {
        GetADID();

        JHSceneManager.Instance.onFadeIn = fadeIn;
        JHSceneManager.Instance.onFadeOut = fadeOut;
        yield return YieldHelper.waitForSeconds(3000);
        JHSceneManager.Instance.Action(JHSceneManager.ACTION.ACTION_REPLACE, Strings.SCENE_MAIN);

    }

    public static void refresh()
    {
        if (RefreshNotification != null)
        {
            RefreshNotification();
        }
    }

    public IEnumerator fadeIn()
    {
        //FadeIn 효과
        mFadeInOut.gameObject.SetActive(true);
        TweenAlpha.Begin(mFadeInOut.gameObject, FADE_DURATION, 1f);
        yield return new WaitForSeconds(FADE_DURATION);
    }

    public IEnumerator fadeOut()
    {
        //FadeOut 효과
        TweenAlpha.Begin(mFadeInOut.gameObject, FADE_DURATION, 0f);
        yield return new WaitForSeconds(FADE_DURATION);

        mFadeInOut.gameObject.SetActive(false);
    }

    //페이드 in/out시키는 코르틴..
    public IEnumerator FadeInOutCoroutine()
    {

        float duration = 1f;
        mFadeInOut.gameObject.SetActive(true);

        //FadeIn 
        TweenAlpha.Begin(mFadeInOut.gameObject, duration, 1f);
        yield return new WaitForSeconds(duration);
        mFadeInOutCallback();

        //FadeOut 
        TweenAlpha.Begin(mFadeInOut.gameObject, duration, 0f);
        yield return new WaitForSeconds(duration);

        mFadeInOut.gameObject.SetActive(false);

    }
    /// <summary>
    /// 오버라이드 해서 사용 
    /// fadeout :  까만거 나타나는 속도 , outTime :  데이터 처리 
    /// fadein : 까만거 사라지는 속도 , startTime :  mFadeInOut.SetActive (false); 속도
    /// mFadeInOutCallback : 첫번째 콜백 
    /// mFadeInOutCallback_Two : 두번쨰 콜백 
    /// </summary>
    public IEnumerator FadeInOutCoroutine_Two(float fadeout, float outTime, float fadein, float startTime)
    {
        mFadeInOut.gameObject.SetActive(true);
        //FadeIn 
        TweenAlpha.Begin(mFadeInOut.gameObject, fadeout, 1f);
        yield return new WaitForSeconds(outTime);
        TweenAlpha.Begin(mFadeInOut.gameObject, fadein, 0f);
        mFadeInOutCallback();
        //FadeOut 
        yield return new WaitForSeconds(startTime);
        mFadeInOut.gameObject.SetActive(false);
        mFadeInOutCallback_Two();
    }

    public void ShowToast(string msg)
    {
        isActionToast = true;
        PopupToast popup = Instantiate(showToast, gameObject.transform) as PopupToast;
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        popup.SetPopupToast(msg);
    }

    void isActonToast()
    {
        isActionToast = false;
    }

    // public void openPopup_Setting()
    // {
    //     // Popup_Setting popup = Instantiate<Popup_Setting>(Resources.Load<Popup_Setting>("Popup/Popup_Setting"));
    //     // popup.transform.parent = this.transform;
    //     // popup.transform.localScale = new Vector3(1f, 1f, 1f);
    //     // popup.transform.localPosition = new Vector3(0f, 0f, 0f);
    //     // popup.gameObject.SetActive(true);
    //     // BCSceneManager.Instance.CurrentScene().scene.GetComponent<BCScene>().AddLayer(popup);
    //     // popup.SetPopup_Setting();

    // }


    public bool GetActionPrecess()
    {
        return ACTION_PROCESS;
    }

    public void PLAY_BGM(string bgm)
    {
        if (UserInfoManager.Instance.GetSaveSound() == true)
        {
            AudioController.PlayMusic(bgm);
        }

    }

    public void PLAY_SE(string se)
    {
        if (UserInfoManager.Instance.GetSaveSound() == true)
        {
            AudioController.Play(se);
        }
    }

}
