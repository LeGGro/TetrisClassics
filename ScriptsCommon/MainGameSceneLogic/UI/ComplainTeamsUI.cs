using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
public class ComplainTeamsUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text firstPtext;
    [SerializeField]
    private TMP_Text secondPtext;
    [SerializeField]
    private TMP_Text thirdPtext;
    [SerializeField]
    private TMP_Text foursPtext;

    public void OnEnable()
    {
        if (PhotonNetwork.CurrentRoom.Players[0].NickName == PhotonNetwork.NickName)
        {
            firstPtext.text = PhotonNetwork.NickName + " Это вы ";
            firstPtext.color = Color.gray;
        }
        else { firstPtext.text = PhotonNetwork.CurrentRoom.Players[0].NickName; firstPtext.color = Color.white; }

        if (PhotonNetwork.CurrentRoom.Players[0].NickName == PhotonNetwork.NickName)
        {
            secondPtext.text = PhotonNetwork.NickName + " Это вы ";
            secondPtext.color = Color.gray;
        }
        else { secondPtext.text = PhotonNetwork.CurrentRoom.Players[0].NickName; secondPtext.color = Color.white; }

        if (PhotonNetwork.CurrentRoom.Players[0].NickName == PhotonNetwork.NickName)
        {
            thirdPtext.text = PhotonNetwork.NickName + " Это вы ";
            thirdPtext.color = Color.gray;
        }
        else { thirdPtext.text = PhotonNetwork.CurrentRoom.Players[0].NickName; thirdPtext.color = Color.white; }

        if (PhotonNetwork.CurrentRoom.Players[0].NickName == PhotonNetwork.NickName)
        {
            foursPtext.text = PhotonNetwork.NickName + " Это вы ";
            foursPtext.color = Color.gray;
        }
        else { foursPtext.text = PhotonNetwork.CurrentRoom.Players[0].NickName; foursPtext.color = Color.white; }
    }
   
}
