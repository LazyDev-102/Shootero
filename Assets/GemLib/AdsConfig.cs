using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gemmob.Lib.Adsv2
{
    public class AdsConfig
    {
        public string unityGameIdAndroid = "5197639";
        public string unityGameIdIos = "5197638";

        public readonly AdsConfigAdmob admobConfig = new AdsConfigAdmob(
                //"ca-app-pub-3940256099942544/6300978111", // banner android
                //"ca-app-pub-3940256099942544/1033173712", //interstitial android
                //"ca-app-pub-3940256099942544/5224354917", // reward ad android
                "ca-app-pub-4985960383417873/5922306671", // banner android
                "ca-app-pub-4985960383417873/8368490703", //interstitial android
                "ca-app-pub-4985960383417873/3696702232", // reward ad android

                "ca-app-pub-4985960383417873/6905034091", // banner ios
                "ca-app-pub-4985960383417873/6193769415", //interstitial ios
                "ca-app-pub-4985960383417873/9339625745", // reward ad ios

                "ca-app-pub-4985960383417873/5922306671", //admob mediation banner android
                "ca-app-pub-4985960383417873/8368490703", //admob mediation interstitial android
                "ca-app-pub-4985960383417873/3696702232", //admob mediation  reward ad android

                "ca-app-pub-4985960383417873/6905034091", //admob mediation  banner ios
                "ca-app-pub-4985960383417873/6193769415", //admob mediation interstitial ios
                "ca-app-pub-4985960383417873/9339625745" //admob mediation  reward ad ios
            );
    }

    public class AdsConfigAdmob
    {
        public string bannerUnitIdAndroid, interstitialUnitIdAndroid, rewardAdUnitIdAndroid; //native
        public string bannerUnitIdIos, interstitialUnitIdIos, rewardAdUnitIdIos; //native

        public string bannerMediationAndroid, interstitialMediationAndroid, rewardAdMediationAndroid; //admob mediation
        public string bannerMediationIos, interstitialMediationIos, rewardAdMediationIos; //admob mediation

        public AdsConfigAdmob(string bannerUnitIdAndroid, string interstitialUnitIdAndroid, string rewardAdUnitIdAndroid,
            string bannerUnitIdIos, string interstitialUnitIdIos, string rewardAdUnitIdIos,
            string bannerMediationAndroid, string interstitialMediationAndroid, string rewardAdMediationAndroid,
            string bannerMediationIos, string interstitialMediationIos, string rewardAdMediationIos
            )
        {
            this.bannerUnitIdAndroid = bannerUnitIdAndroid;
            this.interstitialUnitIdAndroid = interstitialUnitIdAndroid;
            this.rewardAdUnitIdAndroid = rewardAdUnitIdAndroid;

            this.bannerUnitIdIos = bannerUnitIdIos;
            this.interstitialUnitIdIos = interstitialUnitIdIos;
            this.rewardAdUnitIdIos = rewardAdUnitIdIos;

            this.bannerMediationAndroid = bannerMediationAndroid;
            this.interstitialMediationAndroid = interstitialMediationAndroid;
            this.rewardAdMediationAndroid = rewardAdMediationAndroid;

            this.bannerMediationIos = bannerMediationIos;
            this.interstitialMediationIos = interstitialMediationIos;
            this.rewardAdMediationIos = rewardAdMediationIos;
        }
    }
}