using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;

public class UserDataManager : MonoBehaviour
{
    public string coins;
    public string gems;
    public string NAME;
    public TMP_Text coinsText;
    public TMP_Text gemsText;
    public TMP_Text nameText;
    public TMP_Text coinsText2;
    public TMP_Text gemsText2;
    public TMP_Text nameText2;
    
    public event UserDataManager DataRecieve;
    
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GetUserData();
    }
    
    public void UpdateInfo()
    {
        GetInventory();
        coinsText.text = coins;
        gemsText.text = gems;
        nameText.text = NAME;
        coinsText2.text = coins;
        gemsText2.text = gems;
        nameText2.text = NAME;
    }
    
    public void SetUserData(string coins, string gems)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {"Complains_Bot", coins},
            {"Complains_Team", gems}
        }
        },
        result => Debug.Log("Successfully updated user data"),
        error =>
        {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    
    public void SetDefaultUserData()
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
                {"Complains_Bot", "0"},
                {"Complains_Team", "0"},
                {"Complains_Teammates", "-"}
        }
        },
        result => Debug.Log("Successfully set default user data"),
        error =>
        {
            Debug.Log("Got error with set up the data");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    
    private void GetUserData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = null
        },
        result =>
        {
            DataRecieve?.Invoke(Indicate());
            
            if (!result.Data.ContainsKey("Complains_Bot") || !result.Data.ContainsKey("Complains_Team")|| !result.Data.ContainsKey("Complains_Teammates")) SetDefaultUserData();
            //else { this.coins = result.Data["Complains_Bot"].Value; gems = result.Data["Complains_Team"].Value; }
            SetValueToScreen();
        },
        (error) =>
        {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
        
    }
    
    private void Indicate()
    {
        Debug.Log("Data Recieved");
    }
    
    public void SetValueToScreen()
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(),
        result =>
        {
            GetInventory();
            NAME = result.AccountInfo.TitleInfo.DisplayName;
            //UpdateInfo(result.AccountInfo.TitleInfo.DisplayName, coins, gems);
            Com.MyCompany.MyGame.Launcher.ChangeNick(result.AccountInfo.TitleInfo.DisplayName);


        },
        error =>
        {
            Debug.Log("fail");


        });
    }
    
    void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), LogSuccess, LogFailure);
    }

    private void LogFailure(PlayFabError obj)
    {
        Debug.LogError("Dont read data");
    }

    private void LogSuccess(GetUserInventoryResult obj)
    {
        coins =  Convert.ToString( obj.VirtualCurrency["GO"]);
        gems = Convert.ToString(obj.VirtualCurrency["GE"]);
        UpdateInfo();
    }
    
    protected virtual OnDataRecieve(EventArgs e)
    {
        EventHandler handler = DataRecieve;
        handler?.Invoke(this, e);
    }
}
