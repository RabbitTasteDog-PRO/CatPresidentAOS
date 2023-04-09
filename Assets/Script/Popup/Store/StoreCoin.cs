using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StoreCoin : MonoBehaviour
{

    public UIScrollView scrollView;
    public UIGrid gridState;
    public BuyState prefabBuyState;

    string[] STATE_KEY = new string[]{
            Strings.TYPE_CHAR, Strings.TYPE_AWAR, Strings.TYPE_NATU, Strings.TYPE_CHARM,
            Strings.TYPE_TALK, Strings.TYPE_DIPLO, Strings.TYPE_ECO, Strings.TYPE_CUR
        };

    public UIButton btnVideoAdMob;
    public UIButton btnVideoUnity;

    public VideoTimeCheck timeCheck;

    public UILabel labelCoin;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        btnVideoAdMob.onClick.Add(new EventDelegate(OnClickVideoAdMob));
        btnVideoUnity.onClick.Add(new EventDelegate(OnClickVideoUnity));

        // JHAdsManager.ACTION_REWARD_ADMOB = VideoReward;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        // JHAdsManager.ACTION_REWARD_ADMOB = null;
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

    // Start is called before the first frame update
    void Start()
    {
        SetStoreCoin();
    }
    void OnClickVideoAdMob()
    {
        SceneBase.Instance.PLAY_SE("BTN_BASIC");

        if (SceneBase.Instance.adsManager.GetIsActionAdMob == true)
        {
            // SceneBase.Instance.ShowToast(" 못뿌앙앙앙 ");
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

    void OnClickVideoUnity()
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


    public void SetStoreCoin()
    {

        Dictionary<string, xmlStore> data = JHDataManager.Instance.GetDicStoreData();
        for (int i = 0; i < data.Count; i++)
        {
            BuyState obj = Instantiate(prefabBuyState, gridState.transform) as BuyState;
            obj.transform.localPosition = Vector2.zero;
            obj.transform.localScale = Vector3.one;
            obj.SetBuyState(data[STATE_KEY[i]], gameObject, scrollView.GetComponent<UIPanel>().depth);
            obj.name = data[STATE_KEY[i]].key;
        }
        gridState.Reposition();
    }


    void VideoReward(string sender, string args)
    {

        switch (sender)
        {
            case Strings.VIDEO_REWARD_COIN:
                {
                    StartCoroutine(IERewardProcess(args));
                    break;
                }

            case Strings.VIDEO_REWARD_HP:
                {
                    int plusHP = UserInfoManager.Instance.getSaveHP() + Convert.ToInt32(args);
                    UserInfoManager.Instance.setSaveHP(plusHP);
                    /// 토스트에서 에러남 확인해볼것
                    StartCoroutine(IEToast(string.Format("현재 체력에서 +{0}의\n체력이 더해집니다", plusHP)));
                    break;
                }
        }

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

    void ConinRefrash()
    {
        labelCoin.text = string.Format("내 쮸르 : {0}개", UserInfoManager.Instance.GetCoin());
    }

    IEnumerator IEToast(string text)
    {
        yield return YieldHelper.waitForEndOfFrame();
        JHAdsManager.RefreshNotification -= VideoReward;
        yield return YieldHelper.waitForEndOfFrame();
        SceneBase.Instance.ShowToast(text);
    }

}
