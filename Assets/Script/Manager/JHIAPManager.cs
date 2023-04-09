// using UnityEngine;
// using UnityEngine.Purchasing;

// public class JHIAPManager : Ray_Singleton<JHIAPManager>, IStoreListener
// {
//     private IStoreController controller;

//     //The following products must be added to the Product Catalog in the Editor:
//     // private const string coins100 = "100.gold.coins";
//     // private const string coins500 = "500.gold.coins";
//     // public int coin_count = 0;

//     void Awake()
//     {
//         StandardPurchasingModule module = StandardPurchasingModule.Instance();
//         ProductCatalog catalog = ProductCatalog.LoadDefaultCatalog();
//         ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);
//         IAPConfigurationHelper.PopulateConfigurationBuilder(ref builder, catalog);
//         UnityPurchasing.Initialize(this, builder);
//     }

//     public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//     {
//         this.controller = controller; Debug.Log("Initialization Successful");
//     }

//     public void OnInitializeFailed(InitializationFailureReason error)
//     {
//         Debug.Log("UnityIAP.OnInitializeFailed (" + error + ")");
//     }

//     public void OnPurchaseFailed(Product item, PurchaseFailureReason reason)
//     {
//         Debug.Log("UnityIAP.OnPurchaseFailed (" + item + ", " + reason + ")");
//     }

//     public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//     {
//         string purchasedItem = e.purchasedProduct.definition.id;

//         switch (purchasedItem)
//         {
//             case Strings.INAPP_1000:
//                 {
//                     int coin = int.Parse(UserInfoManager.Instance.GetCoin()) + Strings.INAPP_COIN_1000;
//                     UserInfoManager.Instance.SetSaveCoin(coin);
//                     break;
//                 }
//             case Strings.INAPP_3000:
//                 {
//                     int coin = int.Parse(UserInfoManager.Instance.GetCoin()) + Strings.INAPP_COIN_3000;
//                     UserInfoManager.Instance.SetSaveCoin(coin);
//                     break;
//                 }
//         }

//         // switch (purchasedItem)
//         // {
//         //     case coins100:
//         //         Debug.Log("Congratulations, you are richer!");
//         //         coin_count += 100;
//         //         Debug.Log("IAPLog: Coin count: " + coin_count);
//         //         break;

//         //     case coins500:
//         //         Debug.Log("Congratulations, you are richer!");
//         //         coin_count += 500;
//         //         Debug.Log("IAPLog: Coin count: " + coin_count);
//         //         break;
//         // }
//         return PurchaseProcessingResult.Complete;
//     }

//     public void Buy(string productId)
//     {
//         Debug.Log("UnityIAP.BuyClicked (" + productId + ")");
//         controller.InitiatePurchase(productId);
//     }
// }


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using Enums;


public class JHIAPManager : Ray_Singleton<JHIAPManager>, IStoreListener
{
    public Action ACTION_COIN;

    private IStoreController storeController;
    private IExtensionProvider storeExtension;
    private List<string> list_productID;
    private bool isInitializeEnd = false;   // 초기화 완료 여부( Consume 안된 아이템의 Consume이 발생하기 때문에 체크 )
    private bool isPurchasing = false;      // 구매중인지

    public bool isInitalized { get { return storeController != null && storeExtension != null; } }
    public Action initalizeCallBack;        // 초기화 완료 콜백
    public Action<eInAppActionCoce, int> purchaseEndCallBack;      // <상품결과, 에러결과>구매 완료 콜백


    /// <summary>
    /// IAP 매니저 최초 초기화( 상품 등록 및 빌더 초기화 )
    /// </summary>
    /// <param name="_listPID">상품 id string</param>
    /// <param name="_callBack">초기화 완료 콜백</param>
    public void InitalizeIAP(List<string> _listPID, Action _callBack)
    {
        isPurchasing = false;
        initalizeCallBack = null;
        purchaseEndCallBack = null;


        initalizeCallBack = _callBack;

        if (isInitalized == false)
        {
            list_productID = new List<string>();

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
#if UNITY_ANDROID || UNITY_EDITOR
            builder.AddProduct(Strings.INAPP_1000, ProductType.Consumable, new IDs
                    {
                        { Strings.INAPP_1000, GooglePlay.Name }
                    });
            builder.AddProduct(Strings.INAPP_3000, ProductType.Consumable, new IDs
                    {
                        { Strings.INAPP_3000, GooglePlay.Name }
                    });

#elif UNITY_IPHONE
        builder.AddProduct(Strings.IOS_INAPP_1000, ProductType.Consumable);
        builder.AddProduct(Strings.IOS_INAPP_3000, ProductType.Consumable);
#endif

            // for (int i = 0; i < _listPID.Count; i++)
            // {
            //     list_productID.Add(_listPID[i]);
            //     builder.AddProduct(_listPID[i], ProductType.Consumable, new IDs
            //         {
            //             { _listPID[ i ], GooglePlay.Name }
            //             { _listPID[ i ], AppleAppStore.Name }
            //         });
            // }

            UnityPurchasing.Initialize(this, builder);
        }
    }

    /// <summary>
    /// Consume이 필요한 Product가 있는지 체크. Consume이 다 될때까지 기다리기 위함
    /// </summary>
    public bool HasConsumeProduct()
    {
        if (isInitalized == true)
        {
            for (int i = 0; i < list_productID.Count; i++)
            {
                Product pd = storeController.products.WithID(list_productID[i]);

                if (pd.hasReceipt == true)
                {
                    return true;
                }
            }
        }

        return false;
    }

    eInAppActionCoce eInappAction;
    bool isError = false;
    /// <summary>
    /// 상품 구매
    /// </summary>
    public void BuyProduct(string _pID, Action<eInAppActionCoce, int> _buyCallBack)
    {
        purchaseEndCallBack = _buyCallBack;

        eInappAction = eInAppActionCoce.NONE;
        // 초기화 상태 여부
        if (isInitalized == false)
        {
            eInappAction = eInAppActionCoce.ERR_INITALIZED;
            Debug.LogError("Cannot Buy Product [ Initalize : " + isInitalized + " ]");
            isError = true;
        }

        // 구매처리중인지 여부
        if (isPurchasing == true)
        {
            eInappAction = eInAppActionCoce.ERR_PURCHASING;
            Debug.LogError("Cannot Buy Product [ Purchasing ]");
            isError = true;
        }

        // 상품 보유 여부
        if (storeController.products.WithID(_pID) == null)
        {
            eInappAction = eInAppActionCoce.ERR_PRODUCT;
            Debug.LogError("Cannot Buy Product [ Product is Null : " + _pID + " ]");
            isError = true;
        }

        if (isError == true)
        {
            // 에러일 경우 CallBack 호출
            // TODO : 에러코드 필요
            purchaseEndCallBack(eInappAction, ErrCode);

        }
        else
        {
            eInappAction = eInAppActionCoce.COMPLETE;
            isPurchasing = true;
            storeController.InitiatePurchase(_pID);
        }
    }

    /// <summary>
    /// 초기화 완료 콜백
    /// </summary>
    private void InitializeCallBack()
    {
        if (initalizeCallBack != null)
        {
            initalizeCallBack();
        }
    }

    /// <summary>
    /// 구매 완료 CallBack
    /// </summary>
    private void PurchaseCallBack()
    {
        if (purchaseEndCallBack != null)
        {
            purchaseEndCallBack(eInappAction, (int)ErrCode);
            ErrCode = -1;
        }
    }


    /// <summary>
    /// 서버로 부터 지급완료 받음
    /// </summary>
    private void BuyItemCallBack()
    {
        // 추가로 Consume해야 하는 아이템이 없다면 콜백
        if (HasConsumeProduct() == false)
        {
            // 초기화 중에 Consume으로 인해서 들어왔다면 초기화 콜백 호출
            // 일반 구매였을 경우 구매 콜백 호출
            if (isInitializeEnd == false)
            {
                isInitializeEnd = true;
                InitializeCallBack();
            }
            else
            {
                PurchaseCallBack();
            }
        }
    }

    #region IStoreListener 인터페이스 함수
    /// <summary>
    /// UnityPurchasing.Initialize 성공 Call Back
    /// </summary>
    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtension = extensions;

        // 해당 콜백 후 Consume 안된 아이템의 Consume처리( ProcessPurchase 자동호출 )가 되어서 체크 필요
        // Consume할 아이템이 없다면 
        if (HasConsumeProduct() == false)
        {
            // 초기화 완료 CallBack
            InitializeCallBack();
        }
    }

    /// <summary>
    /// UnityPurchasing.Initialize 실패 Call Back
    /// </summary>
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // TODO : 초기화 에러시 처리 필요
        // 1. title로 돌아가기
        // 2. 그냥 게임에 접속( IAP 못함 )
        Debug.LogError("OnInitializeFailed : " + error);
    }

    /// <summary>
    /// Purchase 성공 Call Back
    /// </summary>
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    {

        string purchasedItem = e.purchasedProduct.definition.id;
        string PurchaseText = "";
        switch (purchasedItem)
        {
            case Strings.INAPP_1000:
                {
                    int coin = int.Parse(UserInfoManager.Instance.GetCoin()) + Strings.INAPP_COIN_1000;
                    UserInfoManager.Instance.SetSaveCoin(coin.ToString());
                    PurchaseText = string.Format("쮸르 {0}개를 지급합니다!", Strings.INAPP_COIN_1000);
                    StartCoroutine(IEShowToast(PurchaseText));

                    break;
                }
            case Strings.INAPP_3000:
                {
                    int coin = int.Parse(UserInfoManager.Instance.GetCoin()) + Strings.INAPP_COIN_3000;
                    UserInfoManager.Instance.SetSaveCoin(coin.ToString());
                    PurchaseText = string.Format("쮸르 {0}개를 지급합니다!", Strings.INAPP_COIN_3000);
                    StartCoroutine(IEShowToast(PurchaseText));
                    break;
                }
        }

        if (ACTION_COIN != null && ACTION_COIN.GetInvocationList().Length > 0)
        {
            ACTION_COIN();
        }

        isPurchasing = false;

        // PurchaseProcessingResult.Complete 리턴시 자동으로 Consume 처리
        // PurchaseProcessingResult.Pending 리턴시 Consume 대기( controller.InitiatePurchase를 호출해야만 Consume 처리 )
        return PurchaseProcessingResult.Complete;
    }

    IEnumerator IEShowToast(string text)
    {
        yield return YieldHelper.waitForSeconds(500);
        SceneBase.Instance.ShowToast(text);
    }

    int ErrCode = -1;
    /// <summary>
    /// Purchase 실패 Call BacK
    /// </summary>
    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    {
        // TODO : 에러 콜백
        isPurchasing = false;
        // PurchaseCallBack();
        ErrCode = (int)p;
        Debug.LogError("OnPurchaseFailed : " + p);
    }

    // /// <summary>
    // /// 원본
    // /// Purchase 실패 Call BacK
    // /// </summary>
    // public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
    // {
    //     // TODO : 에러 콜백
    //     isPurchasing = false;
    //     PurchaseCallBack();
    //     Debug.LogError("OnPurchaseFailed : " + p);
    // }
    #endregion IStoreListener 인터페이스 함수


}
