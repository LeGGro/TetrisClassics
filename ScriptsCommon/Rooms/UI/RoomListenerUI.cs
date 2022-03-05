using System;
using Photon.Realtime;
using TMPro;
using UnityEngine;
public class RoomListenerUI : MonoBehaviour
{
    [SerializeField]
    private TMP_Text Name;
    [SerializeField]
    private TMP_Text peoples;
    [SerializeField]
    private TMP_Text bet;

    public string NAME;

    public RoomInfo RoomInfo { get; private set; }
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        peoples.text = Convert.ToString(roomInfo.PlayerCount) + " / " + Convert.ToString(roomInfo.MaxPlayers);
        NAME = roomInfo.Name;
        string rawName = roomInfo.Name;
        string betS = "";
        string betType = "";
        string NameS = "";
        for (int i = 0; i < rawName.Length; i++)
        {
            if (rawName[i] != '#' && rawName[i] != '$')
            {
                NameS += rawName[i];
            }
            else
            {
                if (rawName[i] == '$')
                    betType = "Золото - ";
                if (rawName[i] == '#')
                    betType = "Алмазы - ";
                i++;
                for (int j = i; j < rawName.Length; j++)
                {
                    betS += rawName[j];
                }
                break;
            }
        }
        bet.text = betType + betS;
        Name.text = NameS;
    }
}
