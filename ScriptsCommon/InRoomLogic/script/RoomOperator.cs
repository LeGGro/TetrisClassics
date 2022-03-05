using System;
using TMPro;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
public class RoomOperator : MonoBehaviourPunCallbacks
{

    private ExitGames.Client.Photon.Hashtable _isReady = new ExitGames.Client.Photon.Hashtable();
    public TMP_Text isReadyText;
    public GameObject isReadyButton;
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        SetState(false);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void ToggleButton()
    {
        if (isReadyText.text == "√Œ“Œ¬")
        {
            isReadyButton.GetComponent<Image>().color = new Color(1,0.5f,0.5f);
            isReadyText.text = "Õ≈ √Œ“Œ¬";
            SetState(true);
        }
        else if (isReadyText.text == "Õ≈ √Œ“Œ¬" )
        {
            isReadyButton.GetComponent<Image>().color = new Color(0.5f, 1, 0.5f);
            SetState(false);
            isReadyText.text = "√Œ“Œ¬";
        }

    }
    public void SetState(bool isReady)
    {
        _isReady["isReady"] = isReady.ToString();
        PhotonNetwork.LocalPlayer.SetCustomProperties(_isReady);
        Debug.Log("sended");
    }

}
