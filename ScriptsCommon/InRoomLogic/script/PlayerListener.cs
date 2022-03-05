using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerListener : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private PlayerListenerUI _playerPrefab;
    [SerializeField]
    private Transform _content;

    public bool isOnce = true;
    private List<PlayerListenerUI> _listings = new List<PlayerListenerUI>();

    private void Awake()
    {
        GetCurrenyRoomPlayers();
    }

    private void GetCurrenyRoomPlayers()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }
    public void AddPlayerListing(Player newPlayer)
    {
        PlayerListenerUI listenerUI = Instantiate(_playerPrefab, _content);
        if (listenerUI != null)
        {
            listenerUI.SetPlayerInfo(newPlayer);
            _listings.Add(listenerUI);
        }

    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
    public void Update()
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            if (playerInfo.Value.CustomProperties["isReady"].ToString() == "False")
            {
                return;
            }

        }
        if (PhotonNetwork.CurrentRoom.MaxPlayers == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            if (isOnce)
            {
                isOnce = false;

                if (PhotonNetwork.CurrentRoom.MaxPlayers == 4)
                    PhotonNetwork.LoadLevel("MainGameSceneFor4Players");
                if (PhotonNetwork.CurrentRoom.MaxPlayers == 3)
                    PhotonNetwork.LoadLevel("MainGameSceneFor3Players");
                if (PhotonNetwork.CurrentRoom.MaxPlayers == 2)
                    PhotonNetwork.LoadLevel("MainGameSceneFor2Players");
            }
        }
    }

}
