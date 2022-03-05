using Photon.Pun;

public class JoinRoomFromList : MonoBehaviourPunCallbacks
{
    
    public void JoinRoomByName()
    {
        //todo проверка баланса и возможность присоединения
        PhotonNetwork.JoinRoom(GetComponent<RoomListenerUI>().NAME);

    }
}
