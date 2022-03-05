using System.Collections;
using System;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class CurrentRoomUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text betType;
    [SerializeField]
    private TMP_Text betAmount;
    [SerializeField]
    private TMP_Text playersCount;



    public void Start()
    {
        string rawName = PhotonNetwork.CurrentRoom.Name;
        Debug.Log(rawName);
        string betS = "";
        string betTypeS = "";
        for (int i = 0; i < rawName.Length; i++)
        {
            if (rawName[i] == '#' || rawName[i] == '$')
            {
              
                if (rawName[i] == '$')
                    betTypeS = "Золото";
                
                if (rawName[i] == '#')
                    betTypeS = "Алмазы";
                i++;
                for (int j = i; j < rawName.Length; j++)
                {
                    betS += rawName[j];
                }
                break;
            }
        }
        betType.text = betTypeS;
        betAmount.text = betS;

        
    }
    private void FixedUpdate()
    {
        playersCount.text = Convert.ToString(PhotonNetwork.CurrentRoom.PlayerCount) + " / " + Convert.ToString(PhotonNetwork.CurrentRoom.MaxPlayers);
    }
}
