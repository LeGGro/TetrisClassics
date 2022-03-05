using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class GM : MonoBehaviourPunCallbacks
{
    public static int queue = 0;

    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public GameObject cameraPlayer1;
    public GameObject cameraPlayer2;
    public GameObject cameraPlayer3;
    public GameObject cameraPlayer4;
    void Start()
    {
        
        if (PhotonNetwork.PlayerList[0].NickName == PhotonNetwork.NickName)
        {
            queue = 1;
            //FindObjectOfType<Game1>().isReady = true;
            //Debug.Log("Instantiating Player 1");
            player1 = PhotonNetwork.Instantiate("FirstPlayer", new Vector3Int(0, 0, 0), Quaternion.identity);
            player1.SetActive(false);
            //PhotonNetwork.Instantiate("Randomaizer", new Vector3(0, 0, 0), Quaternion.identity);
            cameraPlayer1.SetActive(true);
        }
        else if (PhotonNetwork.PlayerList[1].NickName == PhotonNetwork.NickName)
        {
            queue = 2;
            //FindObjectOfType<Game2>().isReady = true;
            // Debug.Log("Instantiating Player 2");
            player2 = PhotonNetwork.Instantiate("SecondPlayer", new Vector3(30f, 0f, 0f), Quaternion.identity);
            player2.SetActive(false);
            cameraPlayer2.SetActive(true);
            
        }
        else if (PhotonNetwork.PlayerList[2].NickName == PhotonNetwork.NickName)
        {
            queue = 3;
            //FindObjectOfType<Game4>().isReady = true;
            player3.SetActive(true);
            //Debug.Log("Instantiating Player 3");
            player3 = PhotonNetwork.Instantiate("ThirdPlayer", new Vector3Int(60, 0, 0), Quaternion.identity);
            player3.SetActive(false);
            cameraPlayer3.SetActive(true);
        }
        else if (PhotonNetwork.PlayerList[3].NickName == PhotonNetwork.NickName)
        {
            queue = 4;
            //FindObjectOfType<Game4>().isReady = true;
            //Debug.Log("Instantiating Player 4");
            player4 = PhotonNetwork.Instantiate("FoursPlayer", new Vector3Int(90, 0, 0), Quaternion.identity);
            player4.SetActive(false);
            cameraPlayer4.SetActive(true);
        }
    }
    public void Update()
    {
        if (PhotonNetwork.PlayerList.Length == 2)
        {
            switch (queue)
            {
                case 1:
                    player1.SetActive(true);
                    break;
                case 2:
                    player2.SetActive(true);
                    break;
            }
        }
        if (PhotonNetwork.PlayerList.Length == 3)
        {
            switch (queue)
            {
                case 1:
                    player1.SetActive(true);
                    break;
                case 2:
                    player2.SetActive(true);
                    break;
                case 3:
                    player3.SetActive(true);
                    break;
            }
        }
        if (PhotonNetwork.PlayerList.Length == 4)
        {
            switch (queue)
            {
                case 1:
                    player1.SetActive(true);
                    break;
                case 2:
                    player2.SetActive(true);
                    break;
                case 3:
                    player3.SetActive(true);
                    break;
                case 4:
                    player4.SetActive(true);
                    break;
            }
        }
    }
}
