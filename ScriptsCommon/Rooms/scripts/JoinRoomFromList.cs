using Photon.Pun;

public class JoinRoomFromList : MonoBehaviourPunCallbacks
{
    
    public void JoinRoomByName()
    {
        //todo �������� ������� � ����������� �������������
        PhotonNetwork.JoinRoom(GetComponent<RoomListenerUI>().NAME);

    }
}
