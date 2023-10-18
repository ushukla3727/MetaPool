using System;
using UnityEngine;
using GoogleMobileAds.Api;
using AssemblyCSharp;
using UnityEngine.PlayerLoop;
using System.Collections;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
    private RewardBasedVideoAd rewardBasedVideo;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    private void Start()
    {
       
        DontDestroyOnLoad(gameObject);

        string appId = ADS_ID.APP_ID;

        MobileAds.Initialize(appId);

        this.rewardBasedVideo = RewardBasedVideoAd.Instance;


        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        RequestVideo();

    }
    public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        print("Started Vansh");
    }
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        print("Closed Vansh");
        RequestVideo();
    }
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        reward = true;
    }

    public void RequestVideo()
    {
        string adUnitId = ADS_ID.VIDEO;
       
        AdRequest request = new AdRequest.Builder().Build();

        this.rewardBasedVideo.LoadAd(request, adUnitId);
    }

    public void ShowVideo()
    {
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
      
    }

    private bool reward;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.W))
        {
            reward = true;
        }

        if (reward)
        {
            GameManager.Instance.playfabManager.addCoinsRequest(StaticStrings.rewardForVideoAd);
            reward = false;
        }
    }

    // bool EligibleReward = false;

    // public void onRewardedVideoClosed(bool finished)
    // {
    //     EligibleReward = finished;
    // }
    
    // private void OnApplicationPause(bool pause)
    // {
    //     if (EligibleReward && !pause)
    //     {
    //         RewardPlayer();
    //         EligibleReward = false;
    //     }
    // }
}