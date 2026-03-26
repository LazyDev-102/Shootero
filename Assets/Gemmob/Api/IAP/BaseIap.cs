using System;
using UnityEngine;
using UnityEngine.Purchasing;

#if IAP_ENABLE
using UnityEngine.Purchasing.Security;

#endif

namespace Gemmob.Api.Iap {

    public struct ProductInfo {
        public string key;
        public float defaultPrice;
        public ProductType productType;

        public ProductInfo(string key, float defaultPrice, ProductType productType) {
            this.key = key;
            this.defaultPrice = defaultPrice;
            this.productType = productType;
        }
    }

#if IAP_ENABLE
    public abstract class BaseIap<T> : SingletonFreeAlive<T>, IStoreListener where T : MonoBehaviour {
#else
    public abstract class BaseIap<T> : SingletonFreeAlive<T> where T : MonoBehaviour {
#endif
        //public bool isRequesting;
        //public Action onBuyFailed;
        //public Action onBuyCompleted;
        public event Action<string> OnPurchasingComplete;
        private const char DefaultSymbol = '$';
        private const string DefaultIsoCurrencyCode = "USD";
        protected System.Collections.Generic.List<IAPRequestData> iapRequestList = new System.Collections.Generic.List<IAPRequestData>();

        public class Meta {
            public readonly string isoCurrencyCode;
            public readonly string localizedPriceString;
            public readonly Decimal localizedPrice;
            public readonly char symbol;

            public Meta(decimal localizedPrice, char symbol, string isoCurrencyCode) {
                this.isoCurrencyCode = isoCurrencyCode;
                this.localizedPrice = localizedPrice;
                if (!string.IsNullOrEmpty(isoCurrencyCode)) {
                    localizedPriceString = this.localizedPrice + " " + isoCurrencyCode;
                }

                this.symbol = symbol;
            }

            public Meta(decimal localizedPrice, string localizedPriceString, string isoCurrencyCode) {
                this.isoCurrencyCode = isoCurrencyCode;
                this.localizedPriceString = localizedPriceString;
                this.localizedPrice = localizedPrice;

                if (string.IsNullOrEmpty(this.localizedPriceString)) {
                    symbol = DefaultSymbol;
                }
                else {
                    if (!char.IsDigit(this.localizedPriceString[0])) {
                        symbol = this.localizedPriceString[0];
                    }
                    else if (!char.IsDigit(this.localizedPriceString[this.localizedPriceString.Length - 1])) {
                        symbol = this.localizedPriceString[this.localizedPriceString.Length - 1];
                    }
                    else {
                        symbol = DefaultSymbol;
                    }
                }
            }
        }

        private static readonly Meta emptyMeta = new Meta(0, DefaultSymbol, DefaultIsoCurrencyCode);
#if IAP_ENABLE
        private IStoreController storeController;
        private IExtensionProvider storeExtensionProvider;
#endif

        public void RegisterOnPurchasingComplete(Action<string> onPurchasingComplete) {
            OnPurchasingComplete += onPurchasingComplete;
        }

        public void UnRegisterOnPurchasingComplete(Action<string> onPurchasingComplete) {
            OnPurchasingComplete -= onPurchasingComplete;
        }

        public virtual void OnPurchaseStart() { }

        public virtual void OnPurchaseEnd() { }

        protected override void OnAwake() {

        }

        public override void Preload() {
            base.Preload();
#if IAP_ENABLE
            StandardPurchasingModule module = StandardPurchasingModule.Instance();
#if UNITY_EDITOR
            module.useFakeStoreUIMode = FakeStoreUIMode.StandardUser;
#endif
            var builder = ConfigurationBuilder.Instance(module);
            IapInit(builder);
            UnityPurchasing.Initialize(this, builder);
#endif
        }

#if IAP_ENABLE
        protected abstract System.Collections.Generic.IEnumerable<ProductInfo> InitProductsInfo();
        public System.Collections.Generic.List<ProductInfo> CacheProducts { get; private set; } = new System.Collections.Generic.List<ProductInfo>();

        private void IapInit(ConfigurationBuilder builder) {
            CacheProducts.AddRange(InitProductsInfo());

            foreach (var p in CacheProducts) {
                if (!string.IsNullOrEmpty(p.key)) {
                    builder.AddProduct(p.key, p.productType);
                }
            }
        }


        /// <summary>
        /// Use after IAP initialized only.
        /// </summary>
        public void AdditionalProducts(Action onSuccess, Action onFailed, params ProductInfo[] products) {
            if (IsInitialized()) {
                System.Collections.Generic.HashSet<ProductDefinition> productDefinitions = new System.Collections.Generic.HashSet<ProductDefinition>();
                foreach (var p in products) {
                    if (!string.IsNullOrEmpty(p.key)) {
                        productDefinitions.Add(new ProductDefinition(p.key, p.productType));
                    }
                }

                if (Logs.IsEnable) {
                    Logs.Log($"[IAP] Start fetch products");

                    foreach (var item in products) {
                        Logs.Log($"\n[Product] {item.key} {item.productType}");
                    }
                }

                storeController.FetchAdditionalProducts(productDefinitions, () => {
                    if (Logs.IsEnable) {
                        Logs.Log($"[IAP] Fetch products successed.");

                        foreach (var item in storeController.products.all) {
                            Logs.Log($"\n[Product] {item.availableToPurchase} {item.definition.id} {item.metadata.localizedPriceString}");
                        }
                    }
                    onSuccess?.Invoke();
                }, (v) => {
                    Logs.Log($"[IAP] Fetch products failed {v}");
                    onFailed?.Invoke();
                });
            }
        }


        public bool IsInitialized() {
            var isInitialized = storeController != null && storeExtensionProvider != null;
            if (!isInitialized) {
                Logs.LogError("[IAP] Not initialized.");
            }

            return isInitialized;
        }


        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            Logs.Log("[IAP]  OnInitialized");
            storeController = controller;
            storeExtensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error) {
            Logs.Log($"[IAP] OnInitializeFailed error: {error}");
        }


        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) {
            var validPurchase = true;

#if UNITY_EDITOR
            OnPurchasingComplete?.Invoke(args.purchasedProduct.definition.id);
            //onBuyCompleted?.Invoke();
            PopIapRequest(args.purchasedProduct.definition.id, true);
            OnPurchaseEnd();
            return PurchaseProcessingResult.Complete;
#endif

#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX

            var validator = new CrossPlatformValidator(GooglePlayTangle.Data(),
                AppleTangle.Data(), Application.identifier);

            try {
                Logs.Log("Receipt data: " + args.purchasedProduct.receipt);
                Logs.Log("avalible to Purchase: " + args.purchasedProduct.availableToPurchase);

                var result = validator.Validate(args.purchasedProduct.receipt);
                foreach (IPurchaseReceipt productReceipt in result) {
                    Logs.Log($"productID:={productReceipt.productID}, purchaseDate={productReceipt.purchaseDate}, transactionID={productReceipt.transactionID}");
                }
            }
            catch (IAPSecurityException ex) {
#if !UNITY_EDITOR
                Logs.LogErrorFormat("Invalid receipt, not unlocking content, error: {0}", ex);
                validPurchase = false;
#endif
            }
#else
            
#endif
            if (validPurchase) {
                if (OnPurchasingComplete != null)
                    OnPurchasingComplete.Invoke(args.purchasedProduct.definition.id);

                //if (onBuyCompleted != null)
                //    onBuyCompleted.Invoke();
                PopIapRequest(args.purchasedProduct.definition.id, true);
            }

            //isRequesting = false; Debug.Log(")))))) is requesting = false ProcessPurchase");

            OnPurchaseEnd();
            return PurchaseProcessingResult.Complete;
        }


        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) {
            //Events.LogIapStats(product.definition.id, "failed");
            //if (onBuyFailed != null) {
            //    onBuyFailed.Invoke();
            //}
            PopIapRequest(product.definition.id, false);

            OnPurchaseEnd();
            //isRequesting = false; Debug.Log(")))))) is requesting = false OnPurchaseFailed");
        }
#endif
            public Meta GetLocalPrice(string id, Decimal defaultPrice = 0, string defaultSymbol = "$",
            string defaultCurencyCode = DefaultIsoCurrencyCode) {
#if IAP_ENABLE
            if (storeController != null) {
                Product product = storeController.products.WithID(id);
                if (product != null) {
                    var productMetadata = storeController.products.WithID(id).metadata;
                    return new Meta(productMetadata.localizedPrice, productMetadata.localizedPriceString,
                        productMetadata.isoCurrencyCode);
                }
            }

            if (defaultPrice > 0) {
                return new Meta(defaultPrice, defaultSymbol, defaultCurencyCode);
            }

            return emptyMeta;
#else
#if UNITY_EDITOR
            Logs.LogError("IAP is disable please check build config");
#endif
            if (defaultPrice > 0) {
                return new Meta(defaultPrice, defaultSymbol, defaultCurencyCode);
            }

            return emptyMeta;
#endif
        }

        public void RestorePurchases(System.Action success = null) {
#if IAP_ENABLE
            if (IsInitialized()) {
                if (Application.platform == RuntimePlatform.IPhonePlayer ||
                    Application.platform == RuntimePlatform.OSXPlayer) {
                    Logs.Log("RestorePurchases started ...");

                    var apple = storeExtensionProvider.GetExtension<IAppleExtensions>();

                    apple.RestoreTransactions(result => {
                        if (result && success != null) {
                            success.Invoke();
                        }
                        Logs.Log($"RestorePurchases continuing: {result}. If no further messages, no purchases available to restore.");
                    });
                }
                else {
                    Logs.Log($"RestorePurchases FAIL. Not supported on this platform. Current = {Application.platform}");
                }

                //isRequesting = false; Debug.Log(")))))) is requesting = false RestorePurchases");
            }
#elif UNITY_EDITOR
            Logs.LogError("IAP is disable please check build config");
#endif
        }

        public virtual void Buy(string productId, Action onBuyCompleted = null, Action onBuyFailed = null) {
#if IAP_ENABLE
            //if (isRequesting)
            //{
            //    Debug.Log(" ++++++ isRequesting true");
            //    return;
            //}
            //this.onBuyCompleted = onBuyCompleted;
            //this.onBuyFailed = onBuyFailed;
            //isRequesting = true; Debug.Log("))++++++)) is requesting = true BUY");
            iapRequestList.Add(new IAPRequestData(productId, onBuyCompleted, onBuyFailed));

            if (IsInitialized()) {
                Product product = storeController.products.WithID(productId);
                if (product != null) {
                    //Events.LogIapStats(product.definition.id, "click");
                    if (product.availableToPurchase) {
                        Logs.Log($"Purchasing product asychronously: '{product.definition.id}'");
                        storeController.InitiatePurchase(product);
                        OnPurchaseStart();
                    }
                    else {
                        //Events.LogIapStats(product.definition.id, "unavailable");
                        Logs.Log(
                            "BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                    }
                }
            }
#else
            //if (Build.IsDebug) {
            //	if (onBuyCompleted != null) {
            //		onBuyCompleted.Invoke();
            //	}

            //	Logs.LogError("IAP is disable please check build config");
            //}
#endif
        }

        public bool IsOwned(string productID) {
#if IAP_ENABLE
            if (!IsInitialized())
                return false;
            var product = storeController.products.WithID(productID);
            if (product == null)
                return false;
            return product.hasReceipt;
#else
            Logs.Log("IAP is disable. Please enableIAP at BuildConfig.");
            return false;
#endif
        }

#if IAP_ENABLE
        public Product GetProduct(string id) {
            if (!IsInitialized())
                return null;
            return storeController.products.WithID(id);
        }

        public bool HasProduct(string id) {
            return GetProduct(id) != null;
        }
#endif

#if IAP_ENABLE
        /**<summary> Get the information of subscription product. 
         * <para>You shoud use these value:</para>
         * <para>isSubcribed (== Result.True): to check if currently subcribed or not </para>
         * <para>isExpired (!= Result.False): to check if has expired </para>
         * <para>getRemainingTime: to show countdown timer </para>
         * </summary>*/
        public SubscriptionInfo GetSubscriptionInfo(string id, string intro_json = "") {
            Product p = GetProduct(id);
            if (p == null || p.definition.type != ProductType.Subscription || !p.hasReceipt)
                return null;
#if UNITY_EDITOR
            return null;// new SubscriptionInfo(id, true,DateTime.Today, false, false, false, "");
                        //return new SubscriptionInfo(id);
#endif
            SubscriptionManager sub = new SubscriptionManager(p, intro_json);
            SubscriptionInfo info = sub.getSubscriptionInfo();
            if (info == null) {
                Logs.LogError($"Product had been purchase but cannot get SubscriptionInfo: {p.definition.id}");
                return null;
            }

            if (Config.IsDebug) {
                Logs.Log($"[Subscription] id = {info.getProductId()}");
                Logs.Log($"[Subscription] isSubscribed = {info.isSubscribed().ToString()}. (Currently subscribed or not)");
                Logs.Log($"[Subscription] isExpired = {info.isExpired().ToString()}");
                Logs.Log($"[Subscription] isAutoRenewing = {info.isAutoRenewing()}");
                Logs.Log($"[Subscription] isCancelled = {info.isCancelled()}. (A cancelled subscription means the Product is currently subscribed, but will not renew on the next billing date)");
                Logs.Log($"[Subscription] isFreeTrial = {info.isFreeTrial()}");
                Logs.Log($"[Subscription] PurchaseDate = {info.getPurchaseDate()} (For Apple, the purchase date is the date when the subscription was either purchased or renewed. For Google, the purchase date is the date when the subscription was originally purchased)");
                Logs.Log($"[Subscription] ExpireDate = {info.getExpireDate()} (the date of the Product’s next auto-renew or expiration)");
                Logs.Log($"[Subscription] RemainingTime = {info.getRemainingTime()} (How much time remains until the next billing date)");
                Logs.Log($"[Subscription] isIntroductoryPricePeriod = {info.isIntroductoryPricePeriod()} (is within an introductory price period)");
                Logs.Log($"[Subscription] IntroductoryPrice = {info.getIntroductoryPrice()} (the introductory price of the Product)");
                Logs.Log($"[Subscription] IntroductoryPricePeriod = {info.getIntroductoryPricePeriod()} (How much time remains for the introductory price period)");
                Logs.Log($"[Subscription] IntroductoryPricePeriodCycles = {info.getIntroductoryPricePeriodCycles()} (the number of introductory price periods that can be applied to this Product. Products in the Apple store return 0 if the application does not support iOS version 11.2+, macOS 10.13.2+, or tvOS 11.2+)");
            }
            return info;
        }

        public enum SubscriptionStatus { NoSubscribe, Subcribing, Expired }

        public SubscriptionStatus GetSubscriptionStatus(string id) {
            var subInfor = GetSubscriptionInfo(id);
            if (subInfor == null || subInfor.isSubscribed() != Result.True)
                return SubscriptionStatus.NoSubscribe;
            if (subInfor.isExpired() != Result.True)
                return SubscriptionStatus.Subcribing;
            return SubscriptionStatus.Expired;
        }
#endif

        public void PopIapRequest(string iapId, bool isSuccessful)
        {
            try
            {
                for (int i = iapRequestList.Count - 1; i >= 0; i--)
                    if (System.String.Equals(iapId, iapRequestList[i].iapId, System.StringComparison.Ordinal))
                    {
                        if (isSuccessful) iapRequestList[i].onBuyCompleted?.Invoke();
                        else iapRequestList[i].onBuyFailed?.Invoke();
                        iapRequestList.RemoveAt(i);
                        return;
                    }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            throw new NotImplementedException();
        }
    }

    public class IAPRequestData
    {
        public string iapId;
        public Action onBuyFailed;
        public Action onBuyCompleted;

        public IAPRequestData(string iapId, Action onBuyCompleted, Action onBuyFailed)
        {
            this.iapId = iapId;
            this.onBuyFailed = onBuyFailed;
            this.onBuyCompleted = onBuyCompleted;
        }
    }


}