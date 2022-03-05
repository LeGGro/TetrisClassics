using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class GameLogicUI : MonoBehaviour
{
    #region //// NICKS Arrays ////
    [Header("Nicks")]
    [SerializeField]
    private TMP_Text[] firstPlayerNickText = new TMP_Text[5];
    [SerializeField]
    private TMP_Text[] secondPlayerNickText = new TMP_Text[5];
    [SerializeField]
    private TMP_Text[] thirdPlayerNickText = new TMP_Text[5];
    [SerializeField]
    private TMP_Text[] foursPlayerNickText = new TMP_Text[5];
    #endregion

    void Start()
    {
        SetupNicks(firstPlayerNickText, 0);
        SetupNicks(secondPlayerNickText, 1);
        SetupNicks(thirdPlayerNickText, 2);
        SetupNicks(foursPlayerNickText, 3);
    }
    private void SetupNicks(TMP_Text[] _Texts, int index)
    {
        for (int i = 0; i < _Texts.Length; i++)
        {
            if (_Texts[i] != null)
            {
                _Texts[i].text = PhotonNetwork.PlayerList[index].NickName;
            }
        }
    }
    
}
