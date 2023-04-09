using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Enums;

public class StoreInapp : MonoBehaviour
{

    public UIButton btnVideoAdMob;
    public UIButton btnVideoUnity;
    public UIButton btnInapp_0;
    public UIButton btnInapp_1;

    public GameObject objInApp_0;
    public GameObject objInApp_1;

    public UIGrid gridInApp;

    public UILabel labelCoin;

    string inapp_0 = "";
    string inapp_1 = "";

    public VideoTimeCheck timeCheck;

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {

    }


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {


        btnVideoAdMob.onClick.Add(new EventDelegate(OnClickVideoAdMob));
        btnVideoUnity.onClick.Add(new EventDelegate(onClickVideoUntiy));

        btnInapp_0.onClick.Add(new EventDelegate(OnClickInapp_0));
        btnInapp_1.onClick.Add(new EventDelegate(OnClickInapp_1));

#if UNITY_ANDROID || UNITY_EDITOR
        inapp_0 = Strings.INAPP_1000;
        inapp_1 = Strings.INAPP_3000;
#elif UNITY_IOS
        inapp_0 = Strings.IOS_INAPP_1000;
        inapp_1 = Strings.IOS_INAPP_3000;

        objInApp_0.gameObject.SetActive(false);
        objInApp_1.gameObject.SetActive(false);
#endif

        gridInApp.Reposition();
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        SceneBase.RefreshNotification += ConinRefrash;
        JHAdsManager.ACTION_REWARD_UNITY += ConinRefrash;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        SceneBase.RefreshNotification -= ConinRefrash;
        JHAdsManager.ACTION_REWARD_UNITY -= ConinRefrash;
    }

    void OnClickVideoAdMob()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (SceneBase.Instance.adsManager.GetIsActionAdMob == true)
        {
            SceneBase.Instance.ShowToast("1번 충전소는 1분에 한번씩만 볼 수 있습니다.\n잠시만 기다려주세요.");
            return;
        }
        else
        {
            JHAdsManager.RefreshNotification += VideoReward;
        }

        if (SceneBase.Instance.adsManager.GetIsActionAds == true)
        {
            SceneBase.Instance.ShowToast("짧은 영상을 불러오는 중입니다\n잠시만 기다려주세요.");
        }
        else
        {
            StartCoroutine(SceneBase.Instance.adsManager.IEShowAdMobVideo(Strings.VIDEO_REWARD_COIN));
        }

    }

    void onClickVideoUntiy()
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

    string PurchaseText = "";
    void OnClickInapp_0()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        JHIAPManager.Instance.BuyProduct(inapp_0, PurchaseCallBack);
        Debug.LogError("############################## OnClickInapp_0");

    }

    void OnClickInapp_1()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        JHIAPManager.Instance.BuyProduct(inapp_0, PurchaseCallBack);
        Debug.LogError("############################## OnClickInapp_1");
    }

    void ConinRefrash()
    {
        labelCoin.text = string.Format("내 쮸르 : {0}개", UserInfoManager.Instance.GetCoin());
    }

    void VideoReward(string sender, string args)
    {

        Debug.LogError("##################### 얘는 타냐 1111111111111");
        switch (sender)
        {
            case Strings.VIDEO_REWARD_COIN:
                {
                    StartCoroutine(IERewardProcess(args));
                    Debug.LogError("##################### 얘는 타냐 22222222222222");
                    break;
                }

            case Strings.VIDEO_REWARD_HP:
                {
                    int plusHP = UserInfoManager.Instance.getSaveHP() + Convert.ToInt32(args);
                    UserInfoManager.Instance.setSaveHP(plusHP);
                    break;
                }
        }
        Debug.LogError("##################### 얘는 타냐 33333333333333");
        // JHAdsManager.ACTION_REWARD_ADMOB = null;

        // ConinRefrash();

        Debug.LogError("##################### 얘는 타냐 44444444444444");
    }

    IEnumerator IERewardProcess(string args)
    {
        PopupToast popup = Instantiate(SceneBase.Instance.showToast, gameObject.transform) as PopupToast;
        popup.transform.localPosition = Vector2.zero;
        popup.transform.localScale = Vector3.one;
        yield return new WaitForEndOfFrame();

        int plusCoint = int.Parse(UserInfoManager.Instance.GetCoin()) + int.Parse(args);
        UserInfoManager.Instance.SetSaveCoin(plusCoint.ToString());
        yield return new WaitForEndOfFrame();
        
        popup.SetPopupToast(string.Format("내 쮸르 : {0}개", UserInfoManager.Instance.GetCoin()));
        yield return new WaitForEndOfFrame();

        labelCoin.text = string.Format("내 쮸르 : {0}개", UserInfoManager.Instance.GetCoin());
        yield return new WaitForEndOfFrame();

        JHAdsManager.RefreshNotification -= VideoReward;

    }


    void PurchaseCallBack(eInAppActionCoce _code, int errCode)
    {

        // Debug.LogError("############### RAY_ PurchaseCallBack : " + _code);
        // Debug.LogError("############### RAY_ PurchaseCallBack : " + errCode);

        // if (errCode == -1)
        // {
        //     switch (_code)
        //     {
        //         /// NONE일 경우 취소로 판단
        //         case eInAppActionCoce.NONE: { break; }
        //         case eInAppActionCoce.COMPLETE:
        //             {
        //                 SceneBase.Instance.ShowToast(PurchaseText);
        //                 break;
        //             }
        //         /// 초기화 에러 
        //         case eInAppActionCoce.ERR_INITALIZED:
        //             {
        //                 SceneBase.Instance.ShowToast("인앱 상품이 초기화 중입니다");
        //                 break;
        //             }
        //         /// 결제에러
        //         case eInAppActionCoce.ERR_PURCHASING:
        //             {
        //                 SceneBase.Instance.ShowToast("상품 구매 에러입니다 잠시 후 다시 시도해주세요");
        //                 break;
        //             }
        //         /// 미상품 에러
        //         case eInAppActionCoce.ERR_PRODUCT:
        //             {
        //                 SceneBase.Instance.ShowToast("존재하지 않는 상품입니다");
        //                 break;
        //             }

        //         default:
        //             {
        //                 SceneBase.Instance.ShowToast("알수없는 에러가 발생하였습니다\n잠시 후 다시 시도해주세요");
        //                 break;
        //             }
        //     }

        // }

        // PurchaseText = "";

    }
}
