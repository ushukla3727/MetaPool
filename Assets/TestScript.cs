using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestScript : MonoBehaviour
{
    [SerializeField] private ConfirmationWindow myConfirmationWindow;
    //[SerializeField] private StatusBar_Group statusbar;

    void Start()
    {
        int isClaimed = PlayerPrefs.GetInt("is_cliamed");
        if(isClaimed == 0)
        {
            Debug.Log("active window");
            myConfirmationWindow.gameObject.SetActive(true);
            OpenConfirmationWindow("Claim your initial gold game coins");
        }
    }

    public void OpenConfirmationWindow(string message)
    {
        int isClaimed = PlayerPrefs.GetInt("is_cliamed");
        Debug.Log("active window");
        if (isClaimed == 0)
        {
            myConfirmationWindow.gameObject.SetActive(true);
            myConfirmationWindow.ClaimButton.onClick.AddListener(YesClicked);
            myConfirmationWindow.MessageText.text = message;
        }
    }
    public void YesClicked()
    {
        StartCoroutine(claimIntialCoin());
       // myConfirmationWindow.gameObject.SetActive(false);
        //Debug.Log("Yes Clicked");
    }

    IEnumerator claimIntialCoin()
    {
        WWWForm form = new WWWForm();
        int id = PlayerPrefs.GetInt("id");
        form.AddField("id", id);
        //form.AddField("claim_type", "INT");
        //form.AddField("coins", "100");
        using (UnityWebRequest www = UnityWebRequest.Post("https://www.vibhoragrawal.in/8ballpool/api/claimInitialCoin", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
                myConfirmationWindow.gameObject.SetActive(false);
            }
        }

    }
}
