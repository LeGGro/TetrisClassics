using System.Collections.Generic;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using Photon.Realtime;
using System;
using UnityEngine.UI;

public class ComplainLeaderboard : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Toggle[] firstP = new Toggle[4];
    [SerializeField]
    private Toggle[] secondP = new Toggle[4];
    [SerializeField]
    private Toggle[] thirdP = new Toggle[4];
    [SerializeField]
    private Toggle[] foursP = new Toggle[4];
    

    public PhotonView PV;
    public int Bots;
    public int Teams;

    public string teammates;


    public void Start()
    {

        PV = GetComponent<PhotonView>();
        GetUserData();
        //SendBotLeaderboard(Bots);
        
    }
    
    public void SendBotLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Complaints_bots",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, UpdateLBS, UpdateLBE);
        Debug.Log("Rpc обработано");
    }
    private void SendTeamLeaderboard(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Complaints_teaming",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, UpdateLBS, UpdateLBE);
    }
    private void UpdateLBE(PlayFabError res)
    {
        Debug.Log("Error to send leaderboard");
    }
    private void UpdateLBS(UpdatePlayerStatisticsResult res)
    {
        Debug.Log("Success to send leaderboard");
    }
    private void GetBotLeaderboard()
    {
        var req = new GetLeaderboardAroundPlayerRequest
        {
            StatisticName = "Complaints_bot"
        };
        PlayFabClientAPI.GetLeaderboardAroundPlayer(req, OnLBGet, UpdateLBE);

    }

    private void OnLBGet(GetLeaderboardAroundPlayerResult obj)
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(),
           result =>
           {
                foreach (var item in obj.Leaderboard)
                {
                   if (item.DisplayName == result.AccountInfo.TitleInfo.DisplayName)
                   {
                       Bots = item.StatValue;
                   }   
                }
           },
           error =>
           {
               Debug.Log("fail");
           });
    }

    [PunRPC]
    void _ComplainBot()
    {
        //SendBotLeaderboard(Bots + 1);


        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Complaints_bots",
                    Value = Bots
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, UpdateLBS, UpdateLBE);
        Debug.Log("Rpc обработано" + PhotonNetwork.NickName);


        Bots++;

        //SetUserData(Convert.ToString(Bots), Convert.ToString(Teams));
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"Complains_Bot",Convert.ToString(Bots)}
        }
        },
        result => Debug.Log("Successfully set default user data"),
        error => {
            Debug.Log("Got error with set up the data");
            Debug.Log(error.GenerateErrorReport());
        });
        Debug.Log("Rpc taked");

    }
    [PunRPC]
    void _ComplainTeam(string teamms)
    {
        //SendBotLeaderboard(Bots + 1);


        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Complaints_teaming",
                    Value = Teams
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, UpdateLBS, UpdateLBE);
        Debug.Log("Rpc обработано" + PhotonNetwork.NickName);


        Teams++;

        //SetUserData(Convert.ToString(Bots), Convert.ToString(Teams));
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"Complains_Team",Convert.ToString(Teams)},
                {"Complains_Teammates", teamms }
        }
        },
        result => Debug.Log("Successfully set default user data"),
        error => {
            Debug.Log("Got error with set up the data");
            Debug.Log(error.GenerateErrorReport());
        });
        Debug.Log("Rpc taked");

    }
    public void SetDefaultUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"Complains_Bot", "0"},
            {"Complains_Team", "0"},
            {"Complains_Teammates", ""}
        }
        },
        result => Debug.Log("Successfully set default user data"),
        error => {
            Debug.Log("Got error with set up the data");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    public void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = null
        },
        result =>
        {

            if (!result.Data.ContainsKey("Complains_Bot") || !result.Data.ContainsKey("Complains_Team") || !result.Data.ContainsKey("Complains_Teammates")) SetDefaultUserData();
            else { this.Bots =Convert.ToInt32( result.Data["Complains_Bot"].Value); this.Teams = Convert.ToInt32(result.Data["Complains_Team"].Value); this.teammates = result.Data["Complains_Teammates"].Value; }
            
        },
        (error) => {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    private void ComplainBot(Player player)
    {
        PV.RPC("_ComplainBot",player);
        Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Complain to "+ player.NickName);
    }
    public void ComplainFirstBot()
    {
        ComplainBot(PhotonNetwork.PlayerList[0]);
    }
    public void ComplainSecondBot()
    {
        ComplainBot(PhotonNetwork.PlayerList[1]);
    }
    public void ComplainThirdBot()
    {
        ComplainBot(PhotonNetwork.PlayerList[2]);
    }
    public void ComplainFoursBot()
    {
        ComplainBot(PhotonNetwork.PlayerList[3]);
    }
    private void ComplainTeam(Player player, string teams)
    {
        PV.RPC("_ComplainTeam", player);
        //Debug.Log(PhotonNetwork.LocalPlayer.NickName + " Complain to " + player.NickName);
    }
    public void ComplainFirstTeam()
    {
        if ((secondP[0].isOn|| secondP[1].isOn || secondP[2].isOn || secondP[3].isOn) && secondP != null)
        {
            teammates += PhotonNetwork.PlayerList[1]+ ", ";
        }
        if ((thirdP[0].isOn || thirdP[1].isOn || thirdP[2].isOn || thirdP[3].isOn) && thirdP != null)
        {
            teammates += PhotonNetwork.PlayerList[2] + ", ";
        }
        if ((foursP[0].isOn || foursP[1].isOn || foursP[2].isOn || foursP[3].isOn) && foursP != null)
        {
            teammates += PhotonNetwork.PlayerList[3] + ", ";
        }
        ComplainTeam(PhotonNetwork.PlayerList[0], teammates);
        teammates = "";
    }
    public void ComplainSecondTeam()
    {
        if ((firstP[0].isOn || firstP[0].isOn || firstP[0].isOn || firstP[0].isOn) && firstP != null)
        {
            teammates += PhotonNetwork.PlayerList[1] + ", ";
        }
        if ((thirdP[0].isOn || thirdP[1].isOn || thirdP[2].isOn || thirdP[3].isOn) && thirdP != null)
        {
            teammates += PhotonNetwork.PlayerList[2] + ", ";
        }
        if ((foursP[0].isOn || foursP[1].isOn || foursP[2].isOn || foursP[3].isOn) && foursP != null)
        {
            teammates += PhotonNetwork.PlayerList[3] + ", ";
        }
        ComplainTeam(PhotonNetwork.PlayerList[1], teammates);
        teammates = "";
    }
    public void ComplainThirdTeam()
    {
        if ((secondP[0].isOn || secondP[1].isOn || secondP[2].isOn || secondP[3].isOn) && secondP != null)
        {
            teammates += PhotonNetwork.PlayerList[1] + ", ";
        }
        if ((firstP[0].isOn || firstP[1].isOn || firstP[2].isOn || firstP[3].isOn) && firstP != null)
        {
            teammates += PhotonNetwork.PlayerList[2] + ", ";
        }
        if ((foursP[0].isOn || foursP[1].isOn || foursP[2].isOn || foursP[3].isOn) && foursP != null)
        {
            teammates += PhotonNetwork.PlayerList[3] + ", ";
        }
        ComplainTeam(PhotonNetwork.PlayerList[2], teammates);
        teammates = "";
    }
    public void ComplainFoursTeam()
    {
        if ((secondP[0].isOn || secondP[1].isOn || secondP[2].isOn || secondP[3].isOn) && secondP != null)
        {
            teammates += PhotonNetwork.PlayerList[1] + ", ";
        }
        if ((thirdP[0].isOn || thirdP[1].isOn || thirdP[2].isOn || thirdP[3].isOn) && thirdP != null)
        {
            teammates += PhotonNetwork.PlayerList[2] + ", ";
        }
        if ((firstP[0].isOn || firstP[1].isOn || firstP[2].isOn || firstP[3].isOn) && firstP != null)
        {
            teammates += PhotonNetwork.PlayerList[3] + ", ";
        }
        ComplainTeam(PhotonNetwork.PlayerList[3], teammates);
        teammates = "";
    }

}
