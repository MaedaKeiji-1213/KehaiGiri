using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviourPunCallbacks
{

    public Text txt;
    [SerializeField] private float move_speed;
    [SerializeField] private float camera_speed;
    [SerializeField] private float attack_interval;
    private float timer = 0;
    Vector3 vector;
    IInputter inputter;
    IMover mover;
    IAttacker attacker;
    IDeader deader;
    void Start()
    {
        Initialize();
        Cursor.visible=false;

        if(photonView.IsMine)
            txt=GameObject.Find("Text").GetComponent<Text>();
    }

    void Update()
    {
        if(!photonView.IsMine||deader.IsDead){return ;}

        if(txt!=null)txt.text=photonView.ViewID.ToString();

        attacker.Attack(inputter.Is_Attack());
    }

    private void FixedUpdate()
    {
        if(!photonView.IsMine||deader.IsDead){return ;}

        mover.ViewpointMove(transform, inputter.Input_Mouse(), camera_speed);
        mover.Move(inputter.InputMove(transform),move_speed);
    }

    void Initialize()
    {
        inputter=new PlayerInputter();
        mover = new PlayerMover(gameObject);
        attacker = new PlayerAttacker(gameObject);
        deader=new PlayerDeader(gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.transform.tag==transform.tag||col.isTrigger)return;

        AudioController.instance.AddSound("Hit",col.transform.position);
        PhotonView dead_photon_view=col.gameObject.GetComponent<PhotonView>();
        var col_player_manager=col.gameObject.GetComponent<PlayerManager>();
        if(col_player_manager!=null)col_player_manager.Killed();
    }


    public void Killed()
    {
        photonView.RPC(nameof(Dead),RpcTarget.All);
    }

    [PunRPC]
    void Dead()
    {
        Debug.Log("deader:"+deader);
        deader.Dead();
    }
}
