 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerData
{
   public Vector3 position;//position
   public float angle;
}

public class ReplayManager : MonoBehaviour
{
    private Queue<PlayerData[]> player_data_queue = new Queue<PlayerData[]>();
    PlayerData player_date;
    [SerializeField] Transform[] _transforms;
    int count = 0;
    const int a = 1200;
    float timer = 0;
    float replay_interval=0.025f;
    bool isDead = false;

    [SerializeField]Transform[] gameobject;
   
    void Start()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer<=Time.time)
        {
            Date();
            timer = Time.time+replay_interval;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            isDead = true;
           
        }
    }
    
    public void Date()
    {
        PlayerData[] current_date = new PlayerData[4];
        for(int i =0;i<4;i++)
        {
            Transform _transform = _transforms[i];
            current_date[i] = new PlayerData()
            {
                position = _transform.position,
                angle = _transform.eulerAngles.y
                
            };
            //Debug.Log(current_date);
            Debug.Log(player_data_queue.Count);
        }
        player_data_queue.Enqueue(current_date);
            if (a < player_data_queue.Count)
            {
                player_data_queue.Dequeue();
            }
        if (isDead)
        {

            PlayerData[] dequeueData = player_data_queue.Dequeue();
            foreach (PlayerData player in dequeueData)
            {
                gameobject[count].position = player.position+Vector3.up*7;
                gameobject[count].Rotate(new Vector3(0, player.angle, 0));
                //Debug.Log(player.position);
                //Debug.Log(player.angle);
                count++;
                if (count > 3) count = 0;


            }
        }
    }

    public Queue<PlayerData[]> PlayerDataQueue
    {
        get
        {
            return player_data_queue;
        }
    }


}

   