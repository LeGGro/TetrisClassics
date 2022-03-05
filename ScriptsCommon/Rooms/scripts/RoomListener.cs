using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomListener : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private RoomListenerUI _roomPrefab;
    [SerializeField]
    private Transform _content;

    private List<RoomListenerUI> _listings = new List<RoomListenerUI>();
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo info in roomList)
        {
            if (info.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                RoomListenerUI listenerUI = Instantiate(_roomPrefab, _content);
                if (listenerUI != null)
                {
                    listenerUI.SetRoomInfo(info);
                }
            }
        }
    }
    
}
