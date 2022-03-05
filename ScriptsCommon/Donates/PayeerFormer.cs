using System;
using System.Security.Cryptography;
using System.Text;
using Photon.Pun;
using TMPro;
using UnityEngine;
using System.Linq;
public class PayeerFormer : MonoBehaviour
{
    [SerializeField]
    private TMP_Text buySum;
    [SerializeField]
    private TMP_InputField buyInput;
    string m_desc;
    private void Start()
    {
        //m_desc = "";
        //PlayFab.PlayFabClientAPI.GetAccountInfo(new PlayFab.ClientModels.GetAccountInfoRequest(),
        //    result =>
        //    {
        //        m_desc = Base64Encode(result.AccountInfo.PlayFabId.ToString());
        //        Debug.Log(m_desc);
        //       // m_desc = m_desc.Replace("==", "%3D%3D");
        //    },
        //    error =>
        //    {
        //        Debug.Log("error");
        //        return;
        //    }
        //    );
    }
    void Update()
    {
        if (buyInput.text != string.Empty)
        {
            buySum.text = Convert.ToString(Convert.ToDecimal(buyInput.text) / 100);
            
            
        }
        else buySum.text = "...";
    }
    
    public void Buy()
    {
        _Buy();
    }
    private void _Buy()
    {
        if (buySum.text.Length < 1)
        {
            buyInput.image.color = Color.red;
            return;
        }
        if (buySum.text.Length >= 1)
        {
            buyInput.image.color = Color.white;
        }
        var m_shop = "1614587644";

        var m_orderid = OrderID();

        var summ = Convert.ToString(Convert.ToDecimal(buyInput.text) / 100).Replace(",", ".");
        var m_amount = summ;
        var m_curr = "RUB";
        m_desc = Base64Encode(buyInput.text + " Gems for Tetris Classics");
        var m_key = "Banzp5eNctUockLS";
        var arr = new string[] { m_shop, m_orderid, m_amount, m_curr, m_desc, m_key };
        var sign = sign_hash(String.Join(":", arr));
        var link = "https://payeer.com/merchant/?m_shop=" + m_shop + "&m_orderid=" + m_orderid + "&m_amount=" + m_amount + "&m_curr=" + m_curr + "&m_desc=" + m_desc + "&m_sign=" + sign + "&lang=ru";

        Application.OpenURL(link);
    }
    private string sign_hash(string text)
    {
        byte[] data = Encoding.Default.GetBytes(text);
        var result = new SHA256Managed().ComputeHash(data);
        return BitConverter.ToString(result).Replace("-", "").ToUpper();
    }
    private static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    private string OrderID()
    {
        System.Random random = new System.Random();

        
       const string chars = "0123456789";
       return new string(Enumerable.Repeat(chars, 32).Select(s => s[random.Next(s.Length)]).ToArray());
        
    }
}
