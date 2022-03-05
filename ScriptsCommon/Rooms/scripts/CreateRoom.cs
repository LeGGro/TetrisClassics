using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System;
public class CreateRoom : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField _name;
    [SerializeField]
    private Toggle gems;
    [SerializeField]
    private Toggle gold;
    [SerializeField]
    private Slider amount;
    [SerializeField]
    private Slider countPeop;
    public bool isGems;
    public bool isGold;
    public string lastChoise;
    private string formattedName;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (gems.isOn&& !isGems && lastChoise != "ge")
        {
            gold.isOn = false;
            lastChoise = "ge";
            isGems = true;
            isGold = false;
        }
        if (gold.isOn && !isGold )
        {
            gems.isOn = false;
            isGold = true;
            lastChoise = "go";
            isGems = false;
        }
        if (lastChoise == "go" && !gold.isOn)
        {
            gold.isOn = true;
        }
        if (lastChoise == "ge" && !gems.isOn)
        {
            gems.isOn = true;
        }
    }
    public void CreateNewRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = Convert.ToByte(Convert.ToInt32(countPeop.value));
        if (gold.isOn)
        {
            formattedName += _name.text + "$" + amount.value*100;
        }
        if (gems.isOn)
        {
            formattedName += _name.text + "#" + amount.value*100;
        }
        PhotonNetwork.CreateRoom(formattedName, roomOptions);
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("CreateSuccess");
        
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("CreateFailed" + message);
    }
}
