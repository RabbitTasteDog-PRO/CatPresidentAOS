using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Popup_Store : UIPopup
{
    public static event Action ACTION_REFRASH;

    public StoreInapp storeInapp;
    public StoreCoin storeCoin;

    public UIButton btnInapp;
    public UIButton btnCoin;

    public UIButton btnClosed;

    public UILabel labelCoin;


    public VideoTimeCheck videoTime;
    public GameObject objTest;




    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

        JHIAPManager.Instance.ACTION_COIN = ConinRefrash;
        StatePurchase.ACTION_STATE_CALC += ConinRefrash;

        JHSceneManager.Instance.m_PopupQueue.Enqueue(this.gameObject);
        if (JHSceneManager.Instance.m_DicPopupQueue.ContainsKey(this.gameObject.name) == false)
        {
            JHSceneManager.Instance.m_DicPopupQueue.Add(this.gameObject.name, this.gameObject);
        }

        btnClosed.onClick.Add(new EventDelegate(onClose));


        // JHAdsManager.ACTION_VIDEO_TIMECHECK += VideoRefrash;
        // JHAdsManager.ACTION_REWARD_ADMOB = AAAAA;
        SceneBase.RefreshNotification += ConinRefrash;


    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        ConinRefrash();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        // JHAdsManager.ACTION_REWARD_ADMOB = VideoReward;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        JHIAPManager.Instance.ACTION_COIN = null;
        SceneBase.RefreshNotification -= ConinRefrash;
        StatePurchase.ACTION_STATE_CALC -= ConinRefrash;


        // JHAdsManager.ACTION_VIDEO_TIMECHECK -= VideoRefrash;
        // JHAdsManager.ACTION_REWARD_ADMOB = null;

        if (ACTION_REFRASH != null && ACTION_REFRASH.GetInvocationList().Length > 0)
        {
            ACTION_REFRASH();
        }
    }

    //// 10ro 
    void VideoRefrash(string type)
    {
        // JHAdsManager.ACTION_REWARD_ADMOB = VideoReward;
        if (type.Equals("AdMob"))
        {

        }
    }

    void ConinRefrash()
    {
        labelCoin.text = string.Format("내 쮸르 : {0}개", UserInfoManager.Instance.GetCoin());
    }

    protected override void onClose()
    {
        base.onClose();
    }


    // void AAAAA()
    // {
    //     VideoReward(SceneBase.Instance.sender, SceneBase.Instance.amount);
    // }

    // void VideoReward(string sender, string args)
    // {

    //     switch (sender)
    //     {
    //         case Strings.VIDEO_REWARD_COIN:
    //             {
    //                 int plusCoint = int.Parse(UserInfoManager.Instance.GetCoin()) + int.Parse(args);
    //                 UserInfoManager.Instance.SetSaveCoin(plusCoint.ToString());
    //                 /// 토스트에서 에러남 확인해볼것
    //                 // SceneBase.Instance.ShowToast(string.Format("쮸르 {0}개를 받았습니다.", args));
    //                 StartCoroutine(IEToast(string.Format("쮸르 {0}개를 받았습니다.", args)));
    //                 break;
    //             }

    //         case Strings.VIDEO_REWARD_HP:
    //             {
    //                 int plusHP = UserInfoManager.Instance.getSaveHP() + Convert.ToInt32(args);
    //                 UserInfoManager.Instance.setSaveHP(plusHP);
    //                 /// 토스트에서 에러남 확인해볼것
    //                 // SceneBase.Instance.ShowToast( string.Format("현재 체력에서 +{0}의\n체력이 더해집니다", plusHP) );
    //                 StartCoroutine(IEToast(string.Format("현재 체력에서 +{0}의\n체력이 더해집니다", plusHP)));
    //                 break;
    //             }
    //     }

    //     ConinRefrash();

    // }

    // IEnumerator IEToast(string text)
    // {
    //     yield return YieldHelper.waitForSeconds(1000);
    //     SceneBase.Instance.ShowToast(text);
    // }


}
