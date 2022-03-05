using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
public class PayeerCatcher : MonoBehaviour
{


    public void Start()
    {
        ListenerForDonates();
        Debug.Log("Listened");
    }
    private void ListenerForDonates()
    {


        //Uri absoluteUrl = new Uri("https://tetrisclassics.firebaseapp.com/index.html?m_operation_id=1615177583&m_operation_ps=2609&m_operation_date=04.03.2022%2016:16:33&m_operation_pay_date=04.03.2022%2016:16:39&m_shop=1614587644&m_orderid=96542551259037372921597369259322&m_amount=0.01&m_curr=RUB&m_desc=MSBHZW1zIGZvciBUZXRyaXMgQ2xhc3NpY3M%3D&m_status=success&m_sign=9D5A500175647411BDE16530FF20053FABF19B14C2D2516E46096655F95DE3B9&lang=ru");
        Uri absoluteUrl = new Uri(Application.absoluteURL);
        string st = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_status");
        if (st == "success")
        {
            
            string opid = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_operation_id");
            string opps = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_operation_ps");
            string opdt = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_operation_date");
            string oppd = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_operation_pay_date");
            string sh = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_shop");
            string or = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_orderid");
            string am = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_amount");
            string cu = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_curr");
            string de = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_desc");
            
            string si = HttpUtility.ParseQueryString(absoluteUrl.Query).Get("m_sign");
            string m_key = "Banzp5eNctUockLS";


            var arr = new string[] { opid,opps, opdt, oppd, sh, or, am, cu, de, m_key };
            var sign = sign_hash(String.Join(":", arr));
            Debug.Log(sign);

            if (sign==si)
            {
                am = am.Replace(".", ",");
                ValidateSign(Convert.ToDouble(am), sign);
            }

        }
    }

    private void ValidateSign(double am, string key)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest()
        {
            Keys = null
        },
        result =>
        {
            if (!result.Data.ContainsKey(key))
            {
                if (result.Data[key].Value != "success")
                { Donate(am, key); }

            }
        },
        (error) =>
        {
            Debug.Log("Got error retrieving user data:");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    private void SetUserData(string state, string key)
    {
        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest()
        {
            Data = new Dictionary<string, string>() {
            {key, state},
        }
        },
        result =>
        {
            Debug.Log("Successfully updated user data");
        },
        error =>
        {
            Debug.Log("Got error");
            Debug.Log(error.GenerateErrorReport());
        });
    }
    private string sign_hash(string text)
    {
        byte[] data = Encoding.Default.GetBytes(text);
        var result = new SHA256Managed().ComputeHash(data);
        return BitConverter.ToString(result).Replace("-", "").ToUpper();
    }
    private void Donate(double rm, string key)
    {

        PlayFabClientAPI.AddUserVirtualCurrency(
        new AddUserVirtualCurrencyRequest
        {
            Amount = Convert.ToInt32(rm * 100),
            VirtualCurrency = "GE"
        },
        (result) =>
        {
            var UM = FindObjectOfType<UserDataManager>();
            UM.UpdateInfo();
            SetUserData("success", key);
        },
        (error) =>
        {
            Debug.LogError(error.GenerateErrorReport());
            SetUserData("fail, " + Convert.ToString(rm), key);
        }); ;

    }
}
