using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MenuUIManager : MonoBehaviourPunCallbacks
{
    [Header("Windows")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject donate;
    [SerializeField]
    private GameObject rooms;
    [SerializeField]
    private GameObject exit;
    [SerializeField]
    private GameObject createRoom;
    [SerializeField]
    private GameObject currRoom;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public override void OnJoinedRoom()
    {
        createRoom.SetActive(false);
        rooms.SetActive(false);
        RoomScreen();
        
    }
    public override void OnLeftRoom()
    {
        ClearUI();
        mainMenu.SetActive(true);
    }
    private void ClearUI()
    {
        mainMenu.SetActive(false);
        donate.SetActive(false);
        rooms.SetActive(false);
        exit.SetActive(false);
        currRoom.SetActive(false);
        createRoom.SetActive(false);
    }
    public void RoomScreen()
    {
        currRoom.SetActive(true);
    }
    public void MenuScreen()
    {
        ClearUI();
        mainMenu.SetActive(true);
    }
    public void ViewRoomsScreen()
    {
        
        rooms.SetActive(true);
    }
    public void CreateRoomScreen()
    {
        //ClearUI();
        createRoom.SetActive(true);
    }
    public void DonateScreen()
    {
        ClearUI();
        donate.SetActive(true);    
    }
    
    public void ExitScreen()
    {
        exit.SetActive(true);
    }
}
