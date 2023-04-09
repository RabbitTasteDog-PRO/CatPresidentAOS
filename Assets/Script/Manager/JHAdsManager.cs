using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

using UnityEngine.Advertisements;



public class JHAdsManager : MonoBehaviour
{

    public delegate void NotificationAdMob(string type, string args);
    public static event NotificationAdMob RefreshNotification;

    public static Action ACTION_REWARD_UNITY;

    // ca-app-pub-6343007243551468~9415518226

    protected static JHAdsManager _instance = null;
    public static JHAdsManager Instance
    {
        ///중복 호출 방지
        // [MethodImpl(MethodImplOptions.Synchronized)]
        get
        {
            if (_instance == null)
            {
                ///싱글톤 객체를 찾아서 넣는다.
                _instance = (JHAdsManager)FindObjectOfType(typeof(JHAdsManager));

                ///없다면 생성한다.
                if (_instance == null)
                {
                    string goName = typeof(JHAdsManager).ToString();
                    GameObject go = GameObject.Find(goName);
                    if (go == null)
                    {
                        go = new GameObject();
                        go.name = goName;
                    }
                    _instance = go.AddComponent<JHAdsManager>();
                }
            }
            return _instance;
        }
    }

    BannerView bannerView;
    InterstitialAd interstitial;
    RewardedAd rewardedAd;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        string appId = "";
        string gameID = "";
        // /// 테스트용
        // #if UNITY_ANDROID
        //         appId = "ca-app-pub-3940256099942544~3347511713";
        // #elif UNITY_IPHONE
        //             appId = "ca-app-pub-3940256099942544~1458002511";
        // #else
        //             appId = "unexpected_platform";
        // #endif

        /***********************************************************************************/
        //// 배포용

#if UNITY_ANDROID || UNITY_EDITOR
        appId = Strings.AOS_ADMOB_ID;
        gameID = Strings.AOS_UNITY_ID;
#elif UNITY_IPHONE
        appId = Strings.IOS_ADMOB_ID;
        gameID = Strings.IOS_UNITY_ID;
#endif



        MobileAds.SetiOSAppPauseOnBackground(true);

        // admob init
        MobileAds.Initialize(appId);
        /// 유니티 init
        Advertisement.Initialize(gameID);


    }

    

    /******************************************************************************************************/
    /// 동영상광고 테스트 
    public void CreateAndLoadRewardedAd(string rewardType)
    {
        string adUnitId = "";
        /******************************************************************************************************/
        // 테스트용 
        // #if UNITY_EDITOR
        //         adUnitId = "unused";
        // #elif UNITY_ANDROID
        //         adUnitId = "ca-app-pub-3940256099942544/5224354917";
        // #elif UNITY_IPHONE
        //         adUnitId = "ca-app-pub-3940256099942544/1712485313";
        // #else
        //         adUnitId = "unexpected_platform";
        // #endif
        /******************************************************************************************************/
        switch (rewardType)
        {
            case Strings.VIDEO_REWARD_COIN:
                {
                    /// 배포용 
#if UNITY_ANDROID || UNITY_EDITOR
                    adUnitId = Strings.AOS_ADMOB_VIDEO_COIN;
#elif UNITY_IPHONE
                    adUnitId = Strings.IOS_ADMOB_VIDEO_COIN;
#else
                    adUnitId = "unexpected_platform";
#endif
                    break;
                }

            case Strings.VIDEO_REWARD_HP:
                {
                    /// 배포용 
#if UNITY_ANDROID || UNITY_EDITOR
                    adUnitId = Strings.AOS_ADMOB_VIDEO_HP;
#elif UNITY_IPHONE
                    adUnitId = Strings.IOS_ADMOB_VIDEO_HP;
#else
                    adUnitId = "unexpected_platform";
#endif
                    break;
                }

            default:
#if UNITY_ANDROID || UNITY_EDITOR
                adUnitId = Strings.AOS_ADMOB_VIDEO_COIN;
#elif UNITY_IPHONE
                adUnitId = Strings.IOS_ADMOB_VIDEO_COIN;
#endif
                break;
        }

        /******************************************************************************************************/
        // Create new rewarded ad instance.
        this.rewardedAd = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = CreateAdRequest();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    // Returns an ad request with custom ad targeting.
    private AdRequest CreateAdRequest()
    {
        return new AdRequest.Builder()
            .AddTestDevice("85B25C47B718C0D52E9D7E2E62FEB203") /// 내꺼 A9
            .AddTestDevice("C4E68AA98EBC3EA2845B5162A208CEE6") // 내꺼 블루스텍
            .AddTestDevice("9c5651b029fd855014c477146c9c26fc") // 내꺼 아이폰7플러스

            .AddTestDevice("DEF41010DC7C536207BAF00D9624215C") /// 엘리스 샤오미
            .AddTestDevice("5F080DD0831CCD602F567307E2CE6236") // 에밀리 V50
            .AddTestDevice("b51142411932b21b55c4d101ef5c8c42") // 엘리스 아이폰 

            .AddKeyword("game")
            .SetGender(Gender.Male)
            .SetBirthday(new DateTime(1985, 1, 1))
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        isVideoLoad = false;
        isActionAds = false;
        // SceneBase.Instance.ShowToast("현재 재생가능한 동영상광고가 없습니다\n잠시 후 다시 시도해주세요");
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: " + args.Message);
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        isVideoLoad = false;
        isActionAds = false;
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        isVideoLoad = false;
        isActionAds = false;
        SceneBase.Instance.ShowToast("현재 재생가능한 동영상광고가 없습니다\n잠시 후 다시 시도해주세요");

        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: " + args.Message);
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        isVideoLoad = false;
        isActionAds = false;
        string type = args.Type;
        int amount = Convert.ToInt32(args.Amount);
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);

        if (RefreshNotification != null)
        {
            // Debug.LogError("################## HandleUserEarnedReward 탓냐탓 냐탓 냐탓 냐");

            Debug.LogError("############## string.IsNullOrEmpty(type) : " + string.IsNullOrEmpty(type)
                + "  // string.IsNullOrEmpty(amount.ToString()) : " + string.IsNullOrEmpty(amount.ToString()));
            if (string.IsNullOrEmpty(type) == false && string.IsNullOrEmpty(amount.ToString()) == false)
            {
                // Debug.LogError("################## RefreshNotification 드러완냐 드러완냐 드러완냐 드러완냐 ");
                RefreshNotification(type, amount.ToString());

                isActionAdMob = true;
                StartCoroutine(IEAdsCheck());
            }

        }


        // if (ACTION_REWARD_ADMOB != null && ACTION_REWARD_ADMOB.GetInvocationList().Length > 0)
        // {
        //     SceneBase.Instance.sender = type;
        //     SceneBase.Instance.amount = amount.ToString();
        //     // ACTION_REWARD_ADMOB();
        //     ACTION_REWARD_ADMOB(type, amount.ToString());
        // }
    }

    // 25  
    protected bool isActionAds = false;
    public bool GetIsActionAds
    {
        get { return isActionAds; }
        set { isActionAds = value; }
    }

    protected bool isVideoLoad = false;
    ///<summary>
    /// 광고 연속 클릭 방지해야함 
    /// 최소 3분정도
    /// 연속으로 돌리면 리젝사유라함
    ///</summary>
    public IEnumerator IEShowAdMobVideo(string _rewardType)
    {
        rewardType = _rewardType;
        isVideoLoad = true;
        isActionAds = true;
        CreateAndLoadRewardedAd(_rewardType);

        float fElapsedTime = 0f;
        float fDuration = 1.0f;
        float fUnitTime = 1.0f / fDuration;

        while (isVideoLoad == true)
        {
            // Debug.LogError("Video MobVideo fElapsedTime : " + fElapsedTime + "// rewardedAd.IsLoaded() : " + rewardedAd.IsLoaded());
            if (rewardedAd.IsLoaded())
            {
                rewardedAd.Show();
            }

            if (isVideoLoad == false)
            {
                isVideoLoad = false;
                break;
            }
            Debug.LogError("########## IEShowAdMobVideo fElapsedTime : " + fElapsedTime);
            // yield return new WaitForSeconds(1.0f);
            yield return YieldHelper.waitForSeconds(1000);
            fElapsedTime = fElapsedTime + (Time.deltaTime * fUnitTime);
            // plus++;
            // if (plus > 5)
            if (fElapsedTime > 0.2f)
            {
                isActionAds = false;
                isVideoLoad = false;
                Debug.LogError("########## IEUnityAdsVideo Not Play Next AdMob Play");
                SceneBase.Instance.ShowToast("현재 플레이가능한 짧은 광고가 없습니다\n잠시 후 다시 시도해 주세요");
                break;
            }
        }

    }

    public IEnumerator IEShowUnityAdsVideo(string rewardType)
    {
        string adUnitId = "";
        isVideoLoad = true;
        isActionAds = true;
        switch (rewardType)
        {
            case Strings.VIDEO_REWARD_COIN:
                {
                    /// 배포용 
#if UNITY_ANDROID || UNITY_IPHONE || UNITY_EDITOR
                    adUnitId = Strings.VIDEO_REWARD_COIN;
#else
                    adUnitId = "unexpected_platform";
#endif
                    break;
                }

            case Strings.VIDEO_REWARD_HP:
                {
                    /// 배포용 
#if UNITY_ANDROID || UNITY_IPHONE || UNITY_EDITOR
                    adUnitId = Strings.VIDEO_REWARD_HP;
#else
                    adUnitId = "unexpected_platform";
#endif
                    break;
                }

            default:
#if UNITY_ANDROID|| UNITY_IPHONE || UNITY_EDITOR
                adUnitId = Strings.VIDEO_REWARD_COIN;
#elif UNITY_IPHONE
                adUnitId = "";
#endif
                break;
        }

        float fElapsedTime = 0f;
        float fDuration = 1.0f;
        float fUnitTime = 1.0f / fDuration;

        while (isVideoLoad == true)
        {
            ShowUnityAds(rewardType);
            yield return YieldHelper.waitForSeconds(1000);
            fElapsedTime = fElapsedTime + (Time.deltaTime * fUnitTime);
            Debug.LogError("########## IEUnityAdsVideo fElapsedTime : " + fElapsedTime);
            if (fElapsedTime > 0.2f)
            {
                isActionAds = false;
                isVideoLoad = false;
                Debug.LogError("########## IEUnityAdsVideo Not Play Next AdMob Play");
                // StartCoroutine(IEShowAdMobVideo(rewardType));
                SceneBase.Instance.ShowToast("현재 플레이가능한 짧은 광고가 없습니다\n잠시 후 다시 시도해 주세요");
                break;
            }
        }
    }

    /******************************************************************************************************/
    /// 베너광고 테스트 

    public void RequestBanner()
    {
        string adUnitId = "";

        // //// 테스트 용 
        // #if UNITY_ANDROID
        //         adUnitId = "ca-app-pub-3940256099942544/6300978111";
        // #elif UNITY_IPHONE
        //         adUnitId = "ca-app-pub-3940256099942544/2934735716";
        // #else
        //         adUnitId = "unexpected_platform";
        // #endif


#if UNITY_ANDROID|| UNITY_EDITOR
        adUnitId = Strings.AOS_BANNER;
#elif UNITY_IPHONE 
        adUnitId = Strings.IOS_BANNER;
#endif

        bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
        .AddTestDevice("85B25C47B718C0D52E9D7E2E62FEB203") /// 내꺼 A9
        .AddTestDevice("C4E68AA98EBC3EA2845B5162A208CEE6") // 내꺼 블루스텍
        .AddTestDevice("9c5651b029fd855014c477146c9c26fc") // 내꺼 아이폰7플러스

        .AddTestDevice("DEF41010DC7C536207BAF00D9624215C") /// 엘리스 샤오미
        .AddTestDevice("5F080DD0831CCD602F567307E2CE6236") // 에밀리 V50
        .AddTestDevice("b51142411932b21b55c4d101ef5c8c42") // 엘리스 아이폰 
        .Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);

        // Register for ad events.
        this.bannerView.OnAdLoaded += this.HandleAdLoaded;
        this.bannerView.OnAdFailedToLoad += this.HandleAdFailedToLoad;
        this.bannerView.OnAdOpening += this.HandleAdOpened;
        this.bannerView.OnAdClosed += this.HandleAdClosed;
        this.bannerView.OnAdLeavingApplication += this.HandleAdLeftApplication;
    }

    #region Banner callback handlers

    public void HandleAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
    }

    public void HandleAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleAdLeftApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeftApplication event received");
    }

    #endregion

    /******************************************************************************************************/

    //// 유니티 Ads
    private void HandleShowResult(ShowResult result)
    {
        isVideoLoad = false;
        isActionAds = false;

        switch (result)
        {
            case ShowResult.Finished:
                {

                    Debug.LogError("The ad was successfully shown.");

                    if (rewardType.Equals(Strings.VIDEO_REWARD_COIN))
                    {
                        int crrCoin = int.Parse(UserInfoManager.Instance.GetCoin()) + 5;
                        UserInfoManager.Instance.SetSaveCoin(crrCoin.ToString());

                        StartCoroutine(IEToast(string.Format("쮸르 {0}개를 받았습니다.", 5)));

                    }

                    if (rewardType.Equals(Strings.VIDEO_REWARD_HP))
                    {
                        int plusHP = UserInfoManager.Instance.getSaveHP() + 5;
                        UserInfoManager.Instance.setSaveHP(plusHP);

                        StartCoroutine(IEToast(string.Format("현재 체력에서 +{0}의\n체력이 더해집니다", 5)));

                    }

                    if (ACTION_REWARD_UNITY != null && ACTION_REWARD_UNITY.GetInvocationList().Length > 0)
                    {
                        ACTION_REWARD_UNITY();
                    }

                    break;
                }
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }

        // rewardType = "";
    }

    string rewardType = "";
    public void ShowUnityAds(string rewardType_)
    {

        rewardType = rewardType_;
        if (Advertisement.IsReady())
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show(rewardType_, options);

        }
    }

    IEnumerator IEToast(string text)
    {
        yield return new WaitForEndOfFrame();
        yield return YieldHelper.waitForSeconds(1000);
        SceneBase.Instance.ShowToast(text);
    }



    protected bool isActionAdMob = false;
    public bool GetIsActionAdMob { get { return isActionAdMob; } }


    IEnumerator IEAdsCheck()
    {
        yield return YieldHelper.waitForEndOfFrame();
        float fElapsedTime = 0f;
        float fDuration = 1.0f;
        float fUnitTime = 1.0f / fDuration;
        int count = 0;
        yield return YieldHelper.waitForEndOfFrame();
        while (isActionAdMob == true)
        {

            fElapsedTime = fElapsedTime + (Time.deltaTime * fUnitTime);
            yield return YieldHelper.waitForSeconds(1000);
            if (fElapsedTime > 1.0f)
            {
                isActionAdMob = false;
                break;
            }
            else
            {
                Debug.LogError("################ fElapsedTime : " + fElapsedTime + "// count : " + count);
                count++;
            }

        }

    }


}
