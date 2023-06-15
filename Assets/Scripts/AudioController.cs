using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class AudioController : MonoBehaviourPunCallbacks
{
    static public AudioController instance=null;
    bool is_mute=false;
    bool IsMute{set{is_mute=value;}}
    private AudioSource audioSource;
    //public Dictionary<string, AudioClip> soundDictionary;
    public List<KeyValuePair<string, Vector3>> player_sounds_tips = new List<KeyValuePair<string, Vector3>>();
    GameObject sound_pref;

    // private AudioSource audioSource;

    public void Awake()
    {
        if(instance!=null)
            Destroy(instance);
        instance=this;
        sound_pref = Resources.Load<GameObject>("SoundPlayer");   
    }
    private void Update()
    {
        if(player_sounds_tips.Count>0)
            photonView.RPC(nameof(SendSounds),RpcTarget.Others,SoundsData2String(player_sounds_tips));
        PlaySounds(player_sounds_tips);
    }
    public void PlaySounds(List<KeyValuePair<string, Vector3>> sounds_tips)
    {
        if(!is_mute)
        {
            foreach (var sound in sounds_tips)
            {
                AudioClip[] loadedClip = Resources.LoadAll<AudioClip>("Sounds/"+sound.Key);//Debug.Log(loadedClip);
                GameObject sound_object = Instantiate(sound_pref, sound.Value, Quaternion.identity);//.GetComponent<AudioSource>();//.PlayOneShot(loadedClip);
                if(loadedClip.Length>0)
                    sound_object.GetComponent<AudioSource>().PlayOneShot(loadedClip[Random.Range(0,loadedClip.Length)]);
                Destroy(sound_object, 1f);
            }
        }
        player_sounds_tips = new List<KeyValuePair<string, Vector3>>();
    }
    public void AddSound(string name,Vector3 position)
    {
        player_sounds_tips.Add(new KeyValuePair<string, Vector3>(name, position));
    }

    [PunRPC]
    void SendSounds(string recieve_data)
    {
        PlaySounds(String2SoundsData(recieve_data));
    }

    string SoundsData2String(List<KeyValuePair<string, Vector3>> sounds_data)
    {
        string send_data="";
        foreach (var _sound_tips in sounds_data)
        {
            if(send_data!="")
                send_data+=";";
            send_data+=_sound_tips.Key+":"+_sound_tips.Value;//Debug.Log(_sound_tips.Key+":"+_sound_tips.Value);
        }
        Debug.Log("send_data:"+send_data);
        return send_data;        
    }

    List<KeyValuePair<string, Vector3>> String2SoundsData(string recieve_data)
    {
        List<KeyValuePair<string, Vector3>> recieved_sounds_tips=new List<KeyValuePair<string, Vector3>>();
        recieve_data.Replace(" ","");
        string[] sounds_tips_data=recieve_data.Split(";");
        foreach (var sound_tips_data in sounds_tips_data)
        {
            string[] sound_tip_data=sound_tips_data.Split(":");
            recieved_sounds_tips.Add(new KeyValuePair<string, Vector3>(sound_tip_data[0],Str2Vec3(sound_tip_data[1])));
        }
        return recieved_sounds_tips;
    }


    //文字列をVector3に変換
    Vector3 Str2Vec3(string str){
        string[] xyz = str.Trim('(', ')').Split(',');   //カッコを削除してカンマで分割
        return(new Vector3(float.Parse(xyz[0]), float.Parse(xyz[1]), float.Parse(xyz[2])));
    }
}
