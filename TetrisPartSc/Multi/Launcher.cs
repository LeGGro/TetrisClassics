using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
namespace Com.MyCompany.MyGame
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        
        public GameObject createRoomBut;
        public GameObject allRoomsBut;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
        public override void OnConnectedToMaster()
        {
            PhotonNetwork.JoinLobby();
        }
        public override void OnJoinedLobby()
        {
            createRoomBut.SetActive(true);
            allRoomsBut.SetActive(true);
        }
        public static void ChangeNick(string stra)
        {
            PhotonNetwork.NickName = stra;
        
        }
    }
}
