using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Realtime;
public class PlayerListenerUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text playerName;
    [SerializeField]
    private TMP_Text isReadyText;

    public Player Player { get; private set; }
   
    public void SetPlayerInfo(Player _player)
    {
        Player = _player;
        playerName.text = Player.NickName;
    
    }
    public void Update()
    {
        
        if (Player.CustomProperties["isReady"].ToString() == "True")
        {
            isReadyText.text = "√Œ“Œ¬";
            
            isReadyText.color = new Color(0.5f, 1, 0.5f);
        }
        else if (Convert.ToString(Player.CustomProperties["isReady"]) == "False")
        {
            isReadyText.text = "Õ≈ √Œ“Œ¬";
            
            isReadyText.color = new Color(1, 0.5f, 0.5f) ;
        }
    }
}
