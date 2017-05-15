using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class AdPreloader : MonoBehaviour {

    int i;
    public TextMeshProUGUI tm;
    RewardBasedVideoAd rewardBasedVideo;
    InterstitialAd interstitial;
	// Use this for initialization
	void Start () {

        rewardBasedVideo = RewardBasedVideoAd.Instance;
        RequestRewardBasedVideo();
        RequestInterstitial();

        if (PlayerPrefs.HasKey("adsload"))
        {
            i = PlayerPrefs.GetInt("adsload");
        }
        else
        {
            PlayerPrefs.SetInt("adsload", 2);
            i = PlayerPrefs.GetInt("adsload");
        }

        if (i > 0)
        {
            //jgn load ads
            tm.SetText(i.ToString() + " more game for showing ads");
            i--;
        }
        else
        {
            //load ads
            showAdd(rewardBasedVideo);
            i = 2;
        }
        PlayerPrefs.SetInt("adsload", i);
        PlayerPrefs.Save();
	}

    public void RequestRewardBasedVideo()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-9367868530630439/8996188704";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-9367868530630439/8996188704";
#else
        string adUnitId = "unexpected_platform";
#endif

        AdRequest request = new AdRequest.Builder().Build();
        rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void showAdd(RewardBasedVideoAd rewardBasedVideo)
    {
        if (rewardBasedVideo.IsLoaded())
        {
            //Subscribe to Ad event
            rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
            rewardBasedVideo.Show();
        }
        else
        {
            if (interstitial.IsLoaded())
            {
                interstitial.Show();
            }
        }
    }

    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9367868530630439/9076913903";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-9367868530630439/9076913903";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        interstitial = new InterstitialAd(adUnitId);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        //Reawrd User here
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerAction>().infiniteMana();
        print("User rewarded with: " + amount.ToString() + " " + type);
    }

}
