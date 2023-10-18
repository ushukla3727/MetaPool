﻿using UnityEngine;
using System.Collections;
using PlayFab.ClientModels;
using PlayFab;
using UnityEngine.SceneManagement;
using AssemblyCSharp;
using GoogleMobileAds.Api;

public class PlayFabAddFriend : MonoBehaviour {

    public GameObject menuObject;

    // Use this for initialization
    void Start() {
        RequestInterstitial();
    }

    // Update is called once per frame
   

    public void AddFriend() {
        menuObject.GetComponent<Animator>().Play("hideMenuAnimation");
        if (!GameManager.Instance.offlineMode) {
            PhotonNetwork.RaiseEvent(192, 1, true, null);



            AddFriendRequest request = new AddFriendRequest() {
                FriendPlayFabId = PhotonNetwork.otherPlayers[0].name
            };



            PlayFabClientAPI.AddFriend(request, (result) => {
                Debug.Log("Added friend successfully");
                GameManager.Instance.friendButtonMenu.SetActive(false);
                GameManager.Instance.smallMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(GameManager.Instance.smallMenu.GetComponent<RectTransform>().sizeDelta.x, 260.0f);
                //GameManager.Instance.playfabManager.chatClient.AddFriends(new string[] {PhotonNetwork.otherPlayers[0].name});
            }, (error) => {
                Debug.Log("Error adding friend: " + error.Error);
            }, null);
        }

    }

    public void showMenu() {
        menuObject.GetComponent<Animator>().Play("ShowMenuAnimation");
    }

    public void hideMenu() {
        menuObject.GetComponent<Animator>().Play("hideMenuAnimation");
    }

    public void LeaveGame() {
        print(StaticStrings.showAdWhenLeaveGame);
        // if (StaticStrings.showAdWhenLeaveGame)
        // {
        //     GameManager.Instance.showInterstitial();
        // }
        showInterstitial();

        SceneManager.LoadScene("Menu");
        PhotonNetwork.BackgroundTimeout = 0;
        Debug.Log("Timeout 3");
        GameManager.Instance.cueController.removeOnEventCall();
        PhotonNetwork.LeaveRoom();

        GameManager.Instance.playfabManager.roomOwner = false;
        GameManager.Instance.roomOwner = false;
        GameManager.Instance.resetAllData();

    }

    /////////////////////////// Ads ///////////////////////////////////
    private InterstitialAd interstitial;

    private void RequestInterstitial()
    {
        Debug.Log("Vansh RequestInterAds");
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(ADS_ID.INTER);
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void showInterstitial() 
    {
       
            interstitial.Show();
            Debug.Log("Vansh InterAds");  
       
            RequestInterstitial();
        
    }
}