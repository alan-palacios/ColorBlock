using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds;

public class AdManager : MonoBehaviour
{
    public GeneralGameControl generalGC;
    private static InterstitialAd interstitialAd;
    private static RewardedAd extraLifeAd;
    private static RewardedAd doubleStarsAd;

    private static int timer = 0;
    private static bool displayAd = false;
    public int timeBetweenAds;
     static bool giveExtraLife;
     static bool giveDoubleStars;
     public bool production;
     string doubleStarsCode;
     string extraLifeCode;
     string onDeadCode;

    void Start()
    {
        if (production) {
            doubleStarsCode = "ca-app-pub-1685233160171415/4638904461";
            extraLifeCode = "ca-app-pub-1685233160171415/9999042356";
            onDeadCode = "ca-app-pub-1685233160171415/2527638645";
        }else{
            doubleStarsCode = "ca-app-pub-3940256099942544/5224354917";
            extraLifeCode = "ca-app-pub-3940256099942544/5224354917";
            onDeadCode = "ca-app-pub-3940256099942544/1033173712";
        }
        //Debug.Log("id: "+SystemInfo.deviceUniqueIdentifier);
        MobileAds.Initialize(initStatus => { });

        /*#if UNITY_EDITOR
                string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_ANDROID
                string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #elif UNITY_IPHONE
                string adUnitId = "ca-app-pub-3940256099942544/5224354917";
        #else
                string adUnitId = "unexpected_platform";
        #endif*/

        if (Purchaser.adsPersistance != null) {
            if (!Purchaser.adsPersistance.getAdsRemoved()) {
                if (interstitialAd == null) {
                    interstitialAd = RequestAndLoadInterstitialAd(onDeadCode);
                }
            }
        }else{
            if (interstitialAd == null) {
                interstitialAd = RequestAndLoadInterstitialAd(onDeadCode);
            }
        }

        if (doubleStarsAd == null) {
            doubleStarsAd = RequestAndLoadRewardedAd(doubleStarsCode, "DoubleStars");
        }
        if (extraLifeAd == null) {
            extraLifeAd = RequestAndLoadRewardedAd(extraLifeCode, "ExtraLife");
        }

        StartCoroutine(Contar());
    }

    void Update(){

        if (giveExtraLife) {
            giveExtraLife = false;
            generalGC.ShowExtraLifeBtn();
        }

        if (giveDoubleStars) {
            giveDoubleStars = false;
            generalGC.ShowDoubleStarsBtn();
        }
    }

    public RewardedAd RequestAndLoadRewardedAd(string adUnitId, string adName){
            Debug.Log("Requesting RewardedAd Ad.");
            RewardedAd ad = new RewardedAd(adUnitId);
            switch (adName) {
                case "DoubleStars":
                    ad.OnUserEarnedReward += HandleUserEarnedStars;
                    ad.OnAdClosed += HandleOnRewardedAdDoubleStarsClosed;
                    break;
                case "ExtraLife":
                    ad.OnUserEarnedReward += HandleUserEarnedLife;
                    ad.OnAdClosed += HandleOnRewardedAdExtraLifeClosed;
                    break;
            }

            // Load an interstitial ad
            ad.LoadAd(CreateAdRequest());
            return ad;
    }

    public InterstitialAd RequestAndLoadInterstitialAd(string adUnitId)
    {
                Debug.Log("Requesting Interstitial Ad.");
                InterstitialAd ad = new InterstitialAd(adUnitId);
                // Add Event Handlers
                //interstitialAd.OnAdFailedToLoad += (sender, args) => OnAdFailedToLoadEvent.Invoke();
                ad.OnAdClosed += HandleOnInterstitialAdClosed;

                ad.LoadAd(CreateAdRequest());
                return ad;
    }

    private AdRequest CreateAdRequest()
    {
        Debug.Log("Requesting new ad");
        return new AdRequest.Builder()
                //.AddTestDevice(AdRequest.TestDeviceSimulator)
            //.AddTestDevice("3cc162965ce260b056da4b04ec7252a3")
                /*.AddKeyword("unity-admob-sample")
                .TagForChildDirectedTreatment(false)
                .AddExtra("color_bg", "9B30FF")*/
            .Build();
    }

    public bool RewardedAdIsLoaded(string adName){
        switch (adName) {
            case "DoubleStars":
                return doubleStarsAd.IsLoaded();
            case "ExtraLife":
                return extraLifeAd.IsLoaded();
        }
        return true;
    }

    public void ShowRewardedAd(string adName)
    {
        switch (adName) {
            case "DoubleStars":
                if (doubleStarsAd.IsLoaded()) {
                        Debug.Log("entra "+adName);
                    doubleStarsAd.Show();
                }
                break;
            case "ExtraLife":
                if (extraLifeAd.IsLoaded()) {
                    extraLifeAd.Show();
                }
                break;
        }
    }

    public void ShowInterstitialAd()
    {
        if (displayAd) {
            if (interstitialAd.IsLoaded())
            {
                StartCoroutine(showIntersAd());

            }else{
                Debug.Log("Interstitial ad is not ready yet");
            }
        }else{
            Debug.Log("not enough time since the last Ad");
        }
    }

    public IEnumerator showIntersAd(){
        yield return new WaitForSeconds(1.05f);
        Debug.Log("SHOW AD");
        Debug.Log("TIME: " +timer);
        interstitialAd.Show();
        displayAd = false;
        timer = 0;
    }

    public void HandleOnInterstitialAdClosed(object sender, EventArgs args)
    {
        ((InterstitialAd)sender).LoadAd(CreateAdRequest());
    }
    public void HandleOnRewardedAdDoubleStarsClosed(object sender, EventArgs args)
    {
        doubleStarsAd = RequestAndLoadRewardedAd(doubleStarsCode, "DoubleStars");
    }
    public void HandleOnRewardedAdExtraLifeClosed(object sender, EventArgs args)
    {
        extraLifeAd = RequestAndLoadRewardedAd(extraLifeCode, "ExtraLife");
    }

    public void HandleUserEarnedLife(object sender, Reward args)
    {
            giveExtraLife=true;
            Debug.Log("extra life setted true");

    }
    public void HandleUserEarnedStars(object sender, Reward args)
    {
            giveDoubleStars = true;
            Debug.Log("double coins setted true");

    }


    IEnumerator Contar(){
        while(true){
            timer++;
            //Debug.Log("TIME: " +timer);
            if (timer >= timeBetweenAds) {
                displayAd = true;
                timer = 0;
            }
            yield return new WaitForSecondsRealtime(1);
        }
    }

}
