using System;
using UnityEngine;
using UnityEngine.Purchasing;
// using UnityEngine.Store; // UnityChannel

public class IAPManager : MonoBehaviour //, IStoreListener
{

    // public static IAPManager instance;
    // private static IStoreController m_StoreController;
    // private static IExtensionProvider m_StoreExtensionProvider;
    // private ITransactionHistoryExtensions m_TransactionHistoryExtensions;

    // public const string productId = "제품아이디"; //여러개라면 여러개의 변수선언

    // private bool m_PurchaseInProgress;
    // private string productIdInProcessing;

    // private void Awake()
    // {
    //     if (instance == null)
    //         instance = this;
    //     else if (instance != this)
    //         Destroy(gameObject);

    //     DontDestroyOnLoad(gameObject);
    // }

    // void Start()
    // {
    //     if (m_StoreController == null)
    //     {
    //         // Begin to configure our connection to Purchasing
    //         InitializePurchasing();
    //     }
    // }

    // private bool IsInitialized()
    // {
    //     // Only say we are initialized if both the Purchasing references are set.
    //     return m_StoreController != null && m_StoreExtensionProvider != null;
    // }

    // public void InitializePurchasing()
    // {
    //     if (IsInitialized()) return;

    //     var module = StandardPurchasingModule.Instance();
    //     var builder = ConfigurationBuilder.Instance(module);

    //     //setup product id.
    //     builder.AddProduct(productId, ProductType.Consumable, new IDs{
    //         { productId, AppleAppStore.Name },
    //         { productId, GooglePlay.Name }
    //     });

    //     UnityPurchasing.Initialize(this, builder);
    // }

    // public void BuyConsumable(IAPConsumableType consumableType)
    // {
    //     // Buy the consumable product using its general identifier. Expect a response either 
    //     // through ProcessPurchase or OnPurchaseFailed asynchronously.

    //     var productID = string.Empty;
    //     switch (consumableType)
    //     {
    //         case IAPConsumableType.donate1:
    //             productID = productId_donate1;
    //             break;
    //         case IAPConsumableType.donate10:
    //             productID = productId_donate10;
    //             break;
    //     }

    //     if (m_PurchaseInProgress == true)
    //     {
    //         Debug.Log("Please wait, purchase in progress");
    //         return;
    //     }

    //     if (!IsInitialized())
    //     {
    //         Debug.LogError("Purchasing is not initialized");
    //         return;
    //     }

    //     if (m_StoreController.products.WithID(productID) == null)
    //     {
    //         Debug.LogError("No product has id " + productID);
    //         return;
    //     }

    //     //m_PurchaseInProgress = true;
    //     BuyProductID(productID);
    // }

    // void BuyProductID(string productID)
    // {
    //     // If Purchasing has been initialized ...
    //     if (IsInitialized())
    //     {
    //         m_PurchaseInProgress = true;

    //         // ... look up the Product reference with the general product identifier and the Purchasing 
    //         // system's products collection.
    //         Product product = m_StoreController.products.WithID(productID);

    //         // If the look up found a product for this device's store and that product is ready to be sold ... 
    //         if (product != null && product.availableToPurchase)
    //         {
    //             Debug.Log(string.Format("Purchasing product asychronously: {0}", product.definition.id));
    //             productIdInProcessing = product.definition.id;
    //             m_StoreController.InitiatePurchase(product);
    //         }
    //         else
    //         {
    //             Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
    //         }
    //     }
    //     else
    //     {
    //         // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
    //         // retrying initiailization.
    //         InitializePurchasing();
    //         Debug.Log("BuyProductID FAIL. Not initialized.");
    //     }
    // }

    // public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    // {
    //     // Purchasing has succeeded initializing. Collect our Purchasing references.
    //     Debug.Log("OnInitialized: PASS");
    //     Debug.Log("Available items:");
    //     foreach (var item in controller.products.all)
    //     {
    //         if (item.availableToPurchase)
    //         {
    //             Debug.Log(string.Join(" - ",
    //                 new[]
    //                 {
    //                     item.transactionID,
    //                     item.metadata.localizedTitle,
    //                     item.metadata.localizedDescription,
    //                     item.metadata.isoCurrencyCode,
    //                     item.metadata.localizedPrice.ToString(),
    //                     item.metadata.localizedPriceString,
    //                     item.transactionID,
    //                     item.receipt
    //             }));
    //         }
    //     }
    //     m_StoreController = controller;
    //     m_StoreExtensionProvider = extensions;
    //     m_TransactionHistoryExtensions = extensions.GetExtension<ITransactionHistoryExtensions>();
    // }

    // public void OnInitializeFailed(InitializationFailureReason error)
    // {
    //     Debug.Log("Billing failed to initialize!");
    //     switch (error)
    //     {
    //         case InitializationFailureReason.AppNotKnown:
    //             Debug.LogError("Is your App correctly uploaded on the relevant publisher console?");
    //             break;
    //         case InitializationFailureReason.PurchasingUnavailable:
    //             // Ask the user if billing is disabled in device settings.
    //             Debug.Log("Billing disabled!");
    //             break;
    //         case InitializationFailureReason.NoProductsAvailable:
    //             // Developer configuration error; check product metadata.
    //             Debug.Log("No products available for purchase!");
    //             break;
    //     }
    // }

    // // public void OnPurchaseFailed(Product item, PurchaseFailureReason error)
    // // {
    // //     Debug.Log("Purchase failed: " + item.definition.id + " / error : " + error);
    // //     Debug.Log("Store specific error code: " + m_TransactionHistoryExtensions.GetLastStoreSpecificPurchaseErrorCode());

    // //     if (m_TransactionHistoryExtensions.GetLastPurchaseFailureDescription() != null)
    // //     {
    // //         Debug.Log("Purchase failure description message: " +
    // //                   m_TransactionHistoryExtensions.GetLastPurchaseFailureDescription().message);
    // //     }

    // //     m_PurchaseInProgress = false;
    // //     if (UserMenu.DonateMngr.IsActive) UserMenu.DonateMngr.ClosePanel();
    // // }

    // // public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    // // {
    // //     Debug.Log("Purchase OK: " + args.purchasedProduct.definition.id + "===" + productIdInProcessing);
    // //     Debug.Log("Receipt: " + args.purchasedProduct.receipt);

    // //     // A consumable product has been purchased by this user.
    // //     if (string.Equals(args.purchasedProduct.definition.id, productIdInProcessing, StringComparison.Ordinal))
    // //     {
    // //         Debug.Log(string.Format("ProcessPurchase: PASS. Product: {0}", args.purchasedProduct.definition.id));
    // //         // The consumable item has been successfully purchased, add 100 coins to the player's in-game score.

    // //         UserMenu.DonateMngr.DonateCompleted();
    // //     }

    // //     m_PurchaseInProgress = false;

    // //     // Return a flag indicating whether this product has completely been received, or if the application needs 
    // //     // to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
    // //     // saving purchased products to the cloud, and when that save is delayed. 
    // //     return PurchaseProcessingResult.Complete;
    // // }

    // public void OnInitialized(IStoreController controller, IExtensionProvider extensions) { }
    // public void OnInitializeFailed(InitializationFailureReason error) { }
    // public void OnPurchaseFailed(Product i, PurchaseFailureReason p) { }
    // public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
    // {
    //     return PurchaseProcessingResult.Complete;
    // }
}