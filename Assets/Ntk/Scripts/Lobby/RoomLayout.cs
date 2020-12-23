using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Photon.Realtime
{
    public class RoomLayout : MonoBehaviour, ILobbyCallbacks
    {
        [SerializeField]
        private GameObject _roomList;

        private GameObject RoomList
        {
            get { return _roomList; }
        }

        private List<RoomListing> _roomListingBtn = new List<RoomListing>();

        private List<RoomListing> RoomListingBtn
        {
            get { return _roomListingBtn; }
        }

        public void OnJoinedLobby()
        {

        }

        public void OnLeftLobby()
        {

        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            var rooms = roomList.ToArray();
            for (int i = 0; i < rooms.Length; i++)
            {
                Debug.Log("Room found");
                foreach (RoomInfo room in rooms)
                {
                    RoomReceived(room);
                }

                /**
                if (rooms[i].CustomProperties.ContainsKey("pc"))
                {
                    Debug.Log("Room found");
                    foreach (RoomInfo room in rooms)
                    {
                        RoomReceived(room);
                    }
                    //GameManager.Instance.payoutCoins = int.Parse(rooms[i].CustomProperties["pc"].ToString());

                    //if (GameManager.Instance.myPlayerData.GetCoins() >= GameManager.Instance.payoutCoins)
                    //{
                    //    PhotonNetwork.JoinRoom(roomID);
                    //}
                    //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
                }
                else
                {
                    Debug.Log("No Room found");
                    //GameManager.Instance.payoutCoins = int.MaxValue;
                    //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
                }
    */
            }
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            
        }



        private void OnEnable()
        {
            //RoomInfo[] rooms = PhotonNetwork.GetRoomList();
            //for (int i = 0; i < rooms.Length; i++)
            //{
            //    if (rooms[i].CustomProperties.ContainsKey("pc"))
            //    {
            //        Debug.Log("Room found");
            //        foreach (RoomInfo room in rooms)
            //        {
            //            RoomReceived(room);
            //        }
            //        //GameManager.Instance.payoutCoins = int.Parse(rooms[i].CustomProperties["pc"].ToString());

            //        //if (GameManager.Instance.myPlayerData.GetCoins() >= GameManager.Instance.payoutCoins)
            //        //{
            //        //    PhotonNetwork.JoinRoom(roomID);
            //        //}
            //        //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
            //    }
            //    else
            //    {
            //        Debug.Log("No Room found");
            //        //GameManager.Instance.payoutCoins = int.MaxValue;
            //        //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
            //    }
            //}
        }

        public void ReloadRoom()
        {
            //RoomInfo[] rooms = PhotonNetwork.GetRoomList();
            //for (int i = 0; i < rooms.Length; i++)
            //{
            //    if (rooms[i].CustomProperties.ContainsKey("pc"))
            //    {
            //        Debug.Log("Room found");
            //        foreach (RoomInfo room in rooms)
            //        {
            //            RoomReceived(room);
            //        }
            //        //GameManager.Instance.payoutCoins = int.Parse(rooms[i].CustomProperties["pc"].ToString());

            //        //if (GameManager.Instance.myPlayerData.GetCoins() >= GameManager.Instance.payoutCoins)
            //        //{
            //        //    PhotonNetwork.JoinRoom(roomID);
            //        //}
            //        //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
            //    }
            //    else
            //    {
            //        Debug.Log("No Room found");
            //        //GameManager.Instance.payoutCoins = int.MaxValue;
            //        //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
            //    }
            //}
        }

        private void OnReceiveRoomUpdate()
        {
            //RoomInfo[] rooms = PhotonNetwork.GetRoomList();

            //for (int i = 0; i < rooms.Length; i++)
            //{
            //    if (rooms[i].CustomProperties.ContainsKey("pc"))
            //    {
            //        Debug.Log("Room found");
            //        foreach (RoomInfo room in rooms)
            //        {
            //            RoomReceived(room);
            //        }
            //        //GameManager.Instance.payoutCoins = int.Parse(rooms[i].CustomProperties["pc"].ToString());

            //        //if (GameManager.Instance.myPlayerData.GetCoins() >= GameManager.Instance.payoutCoins)
            //        //{
            //        //    PhotonNetwork.JoinRoom(roomID);
            //        //}
            //        //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
            //    }
            //    else
            //    {
            //        Debug.Log("No Room found");
            //        //GameManager.Instance.payoutCoins = int.MaxValue;
            //        //GameConfiguration.GetComponent<GameConfigrationController>().startGame();
            //    }
            //}



        }

        public void RoomReceived(RoomInfo roomInfo)
        {
            int index = RoomListingBtn.FindIndex(x => x.RoomName == roomInfo.name);

            if (index == -1)
            {
                if (roomInfo.IsVisible && roomInfo.PlayerCount < roomInfo.MaxPlayers)
                {
                    GameObject roomListingPrefab = Instantiate(RoomList);
                    roomListingPrefab.transform.SetParent(transform, false);

                    RoomListing roomListing = roomListingPrefab.GetComponent<RoomListing>();
                    RoomListingBtn.Add(roomListing);

                    index = (RoomListingBtn.Count - 1);
                }
            }
            if (index != -1)
            {
                RoomListing roomListing = RoomListingBtn[index];

                string roomPlayer = roomInfo.PlayerCount + "/" + roomInfo.MaxPlayers;

                roomListing.SetRoomNameText(roomInfo.Name);
                //roomListing.SetPlayerCount(roomPlayer);
                roomListing.Updated = true;
            }
        }
        private void RemoveOldRoom()
        {
            List<RoomListing> removeRooms = new List<RoomListing>();

            foreach (RoomListing roomList in removeRooms)
            {
                if (!roomList.Updated)
                    removeRooms.Add(roomList);
                else
                    roomList.Updated = false;
            }

            foreach (RoomListing roomList in removeRooms)
            {
                GameObject roomListObject = roomList.gameObject;
                RoomListingBtn.Remove(roomList);
                Destroy(roomListObject);
            }
        }

    }
}