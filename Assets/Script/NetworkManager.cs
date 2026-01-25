using NUnit.Framework;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject PlayerSample;
    public List<Transform> SpawnPoints;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        RoomOptions options = new RoomOptions
        {
            MaxPlayers = 3,
            IsVisible = false,
        };
        PhotonNetwork.JoinOrCreateRoom(roomName:"Test", options, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        var id = PhotonNetwork.LocalPlayer.ActorNumber;
        Debug.Log(message:"Joned Room with" + PhotonNetwork.CurrentRoom.PlayerCount + "player and ID is" + id);
        if (id > (SpawnPoints.Count + 1))
        {
            Debug.LogError("NO SPAWNER POINT");
        }
        else
        {
            PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoints[id - 1].position, Quaternion.identity);
        } 

    }
}
