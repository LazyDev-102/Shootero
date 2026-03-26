using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gemmob.Api.Iap;
using UnityEngine;
using UnityEngine.Purchasing;

public class GameIAP : BaseIap<GameIAP> {
#if IAP_ENABLE

    protected override IEnumerable<ProductInfo> InitProductsInfo() {
        foreach (var pack in GameResources.Instance.ShopData.Packs) {
            yield return new ProductInfo(pack.IapKey, pack.DefaulIap, pack.ProductType);
            if (pack.IsSale && !pack.IsFake) {
                yield return new ProductInfo(pack.IapSaleKey, pack.DefaulSale, pack.ProductType);
            }
        }

        foreach (var gem in GameResources.Instance.ShopData.Gems) {
            yield return new ProductInfo(gem.IapKey, gem.DefaulIap, gem.ProductType);
            if (gem.IsSale && !gem.IsFake) {
                yield return new ProductInfo(gem.IapSaleKey, gem.DefaulSale, gem.ProductType);
            }
        }
        foreach (var dailyPack in GameResources.Instance.DailyPacksData.Packs) {
            if (!dailyPack.IsFree)
                yield return new ProductInfo(dailyPack.IAPKey, dailyPack.DefaulIap, dailyPack.ProductType);
        }
        var battlePass = GameResources.Instance.BattlePass;
        yield return new ProductInfo(battlePass.PurchaseKey, battlePass.DefaulIap, battlePass.ProductType);
        yield return new ProductInfo(battlePass.OriginIapKey, battlePass.DefaulIap, battlePass.ProductType);

        var resourcePack = GameResources.Instance.IapPack.ResourcePack;
        yield return new ProductInfo(resourcePack.IapKey, resourcePack.DefaulIap, resourcePack.ProductType);
        yield return new ProductInfo(resourcePack.OriginPrice, resourcePack.DefaulIap, resourcePack.ProductType);

        var smartPack = GameResources.Instance.IapPack.SmartOffer;
        foreach (var pack in smartPack.Packs) {
            yield return new ProductInfo(pack.IapKey, pack.DefaulIap, pack.ProductType);
            yield return new ProductInfo(pack.OriginPrice, pack.DefaulIap, pack.ProductType);
        }
        var shipPack = GameResources.Instance.ShipPackData;
        foreach (var pack in shipPack.Packs) {
            yield return new ProductInfo(pack.IAPKey, pack.DefaulIap, pack.ProductType);
            yield return new ProductInfo(pack.IapKeySale, pack.DefaulIap, pack.ProductType);
        }
    }

#endif

    public override void Buy(string productId, Action onBuyCompleted = null, Action onBuyFailed = null) {
        base.Buy(productId, () => {
            Tracking.Instance.LogIap(productId);
            onBuyCompleted?.Invoke();
        }, onBuyFailed);

    }

    public bool IsRemovedAds {
        get {
            return IsOwned("keyRemoveAds");
        }
    }

}
