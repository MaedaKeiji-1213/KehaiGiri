using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    bool is_game_start=false;
    GameObject my_player;
    PlayerManager player_manager;
    [SerializeField]Transform[] players;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.Server="192.168.0.13";//"172.18.86.199";// ;
        PhotonNetwork.PhotonServerSettings.AppSettings.Port=5055;
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions(), TypedLobby.Default);
        RoomOptions options=new RoomOptions()
        {
            MaxPlayers=4
        };
        PhotonNetwork.JoinRandomOrCreateRoom(null,4,MatchmakingMode.FillRoom,null,null,null,options,null);
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(GeneratePlayer());
    }

    //接続状態の表示
    int status = 0;
    // Update is called once per frame
    void Update()
    {
        if(PhotonNetwork.CurrentRoom!=null&&PhotonNetwork.CurrentRoom.IsOpen&&PhotonNetwork.IsMasterClient&&PhotonNetwork.CurrentRoom.PlayerCount==PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            PhotonNetwork.CurrentRoom.IsOpen=false;
            photonView.RPC(nameof(StartGame),RpcTarget.All);
        }

        //Debug.Log(PhotonNetwork.NetworkClientState.ToString());
        if (PhotonNetwork.NetworkClientState.ToString() == "ConnectingToMasterServer" && status == 0)
        {
            status = 1;
            Debug.Log("サーバーに接続中･･･");
        }
        if (PhotonNetwork.NetworkClientState.ToString() == "Authenticating" && status == 1)
        {
            status = 2;
            Debug.Log("認証中･･･");
        }
        if (PhotonNetwork.NetworkClientState.ToString() == "Joined" && status == 2)
        {
            status = 3;
            Debug.Log("ルームに参加中");
        }
    }


    IEnumerator GeneratePlayer()
    {
        yield return null;
        Transform player_seat=null;
        while(player_seat==null)
        {
            Transform random_seat=players[Random.Range(0,players.Length)];
            Collider[] colliders=Physics.OverlapSphere(random_seat.position,0.05f);
            bool is_hit=false;
            foreach (var col in colliders)
            {
                is_hit=true;
            }
            if(!is_hit)
                player_seat=random_seat;

        }
        my_player=PhotonNetwork.Instantiate("Player",(Vector3)player_seat.position,Quaternion.identity);
        my_player.transform.SetParent(player_seat);
        ChangeTag(my_player.transform,"Player");
        Transform camera_transform=Camera.main.transform;
        camera_transform.SetParent(my_player.transform);
        camera_transform.localPosition=Vector3.zero;
        camera_transform.localEulerAngles=Vector3.zero;

        player_manager=my_player.gameObject.GetComponent<PlayerManager>();
        player_manager.enabled=false;
    }

    void ChangeTag(Transform target,string tag_name)
    {
        var transforms=target.GetComponentsInChildren<Transform>();
        foreach (var monotransform in transforms)
        {
            monotransform.tag=tag_name;
        }

    }

    IEnumerator StartCount()
    {
        for(int count=3;count>=0;count--)
        {
            //カウント音を鳴らす
            Debug.Log(count);
            yield return new WaitForSeconds(1f);
        }
        player_manager.enabled=true;
    }
    [PunRPC]
    private void StartGame()
    {
        Debug.Log("DoStartGame");
        StartCoroutine(StartCount());
        //プレイヤーのinputをセットする
    }
}