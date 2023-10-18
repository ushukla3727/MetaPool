using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class ClaimCoinManager : MonoBehaviour
{
    public Animator animator;
    public GameObject ClaimButton;
    public void Show()
    {
        GameManager.Instance.CoinClaimDialog = this.gameObject;
        animator = GetComponent<Animator>();
        Debug.Log("h2");
    }

    public void claimCoins()
    {
        StartCoroutine(claimIntialCoin());
        Debug.Log("h3");
    }

    IEnumerator claimIntialCoin()
    {
        Debug.Log("h4");
        WWWForm form = new WWWForm();
        form.AddField("id", 1);
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
                Debug.Log("email saved!");
            }
        }
    }
}
