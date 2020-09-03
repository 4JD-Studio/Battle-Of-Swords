using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static string GEMS_1000 = "gems1000";    //  100 $
    public static string GEMS_500 = "gems500";      //  50  $
    public static string GEMS_150 = "gems150";      //  25  $
    public static string GEMS_80 = "gems80";        //  9   $

    public static IAPManager Instance { set; get; }

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (m_StoreController == null)
            InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(GEMS_1000, ProductType.Consumable);
        builder.AddProduct(GEMS_500, ProductType.Consumable);
        builder.AddProduct(GEMS_150, ProductType.Consumable);
        builder.AddProduct(GEMS_80, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");
            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, GEMS_1000, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            ActionAfterBoughtSuccessfully(1000);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GEMS_500, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            ActionAfterBoughtSuccessfully(500);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GEMS_150, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            ActionAfterBoughtSuccessfully(150);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, GEMS_80, StringComparison.Ordinal))
        {
            Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
            ActionAfterBoughtSuccessfully(80);
        }
        else
        {
            Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        StartCoroutine(GeneralController.Inistance.ToastDisplayer(string.Format("OnPurchaseFailed: FAIL. \nProduct: '{0}', \nPurchaseFailureReason: \n{1}", product.definition.storeSpecificId, failureReason)));
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void BuyGems_1000()
    {
        BuyProductID(GEMS_1000);
    }

    public void BuyGems_500()
    {
        BuyProductID(GEMS_500);
    }

    public void BuyGems_150()
    {
        BuyProductID(GEMS_150);
    }

    public void BuyGems_80()
    {
        BuyProductID(GEMS_80);
    }

    private void ActionAfterBoughtSuccessfully(int Gems)
    {
        PlayerVariables.Inistance.setCurrentGems(PlayerVariables.Inistance.getCurrentGems() + Gems);
        GeneralController.Inistance.GemsText.text = PlayerVariables.Inistance.getCurrentGems().ToString();
        StartCoroutine(GeneralController.Inistance.ToastDisplayer(Gems + " Gems added."));
        //save to playfab
    }
}