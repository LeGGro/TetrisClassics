using System;
using Photon.Pun;
using UnityEngine;
public class rando : MonoBehaviour
{
    public static bool isLobbyReady = false;
    public static string combination = "";
    public static int[] mass = new int[400];
    
    PhotonView PV;
    
    public void Awake()
    {
        PV = GetComponent<PhotonView>();

        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < 400; i++)
            {
                mass[i] = UnityEngine.Random.Range(1, 8);
                combination += Convert.ToString(mass[i]);
            }
            PV.RPC("SetSequency", RpcTarget.AllBuffered, combination);
        }
    }
    
    [PunRPC]
    void SetSequency(string comb)
    {
        combination = comb;
        
        isLobbyReady = true;
        
    }
}
