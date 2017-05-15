using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class BannerAds : MonoBehaviour {

	// Use this for initialization
	void Start () {
        RequestBanner();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-9367868530630439/1553647109";
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-9367868530630439/1553647109";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Show();
    }
}
