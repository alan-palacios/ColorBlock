using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AdTimingManager : MonoBehaviour
{
    public GeneralGameControl generalGC;

    private static int timer = 0;
    private static bool displayAd = false;
    public int timeBetweenAds;
     static bool giveExtraLife;
     static bool giveDoubleStars;
     public bool production;
     string doubleStarsCode;
     string extraLifeCode;
     string onDeadCode;

     void OnEnable () {
        AdTimingEvents.onSdkInitSuccessEvent += SdkInitSuccessEvent;
        AdTimingEvents.onSdkInitFailedEvent += SdkInitFailedEvent;
        AdTimingEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        AdTimingEvents.onRewardedVideoRewardedEvent += RewardedVideoAdRewardedEvent;

    }

    void RewardedVideoAvailabilityChangedEvent(bool available) {
        Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " +available);
    }
    void SdkInitSuccessEvent(){
        Debug.Log("adtiming init");
    }
    void SdkInitFailedEvent(string error){
        Debug.Log("adtiming init failed");
    }

    void Start()
    {
        AdTiming.Agent.init("qkoffrHHG3qUB4kqOxt1MQFDjMaLEotC");
        
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

    public void ShowRewardedAd(string adName)
    {
        switch (adName) {
            case "DoubleStars":
                if (AdTiming.Agent.isRewardedVideoReady()) {
                    AdTiming.Agent.showRewardedVideo("DoubleStars");
                }
                break;
            case "ExtraLife":
                if (AdTiming.Agent.isRewardedVideoReady()) {
                    AdTiming.Agent.showRewardedVideo("ExtraLife");
                }
                break;
        }
    }

    public void ShowInterstitialAd()
    {
        if (displayAd) {
            if (AdTiming.Agent.isInterstitialReady())
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
        AdTiming.Agent.showInterstitial("OnLost");
        displayAd = false;
        timer = 0;
    }

    void RewardedVideoAdRewardedEvent(string scene) {
        Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent: "+scene);
        switch (scene) {
            case "DoubleStars":
                giveDoubleStars = true;
                Debug.Log("double coins setted true");
                break;
            case "ExtraLife":
                giveExtraLife=true;
                Debug.Log("extra life setted true");
                break;
        }
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
