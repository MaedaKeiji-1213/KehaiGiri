using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover :IMover
{
    Transform player;
    Rigidbody rb;
    float sound_cool_time=0;
    const float COOL_TIME_LENGTH=0.5f;

    public  PlayerMover(GameObject player_object)
    {        
        player=player_object.transform;
        rb=player_object.GetComponent<Rigidbody>();
    }
    void IMover.Move(Vector3 direction,float speed)
    {
        direction=direction.normalized*speed;
        float max_speed=speed/2;
        if(direction!=Vector3.zero)
        {
            if(sound_cool_time<Time.time)
            {
                sound_cool_time=Time.time+COOL_TIME_LENGTH;
                AudioController.instance.AddSound("Walk",player.position);
            }
            if(rb.velocity.magnitude<max_speed)
                rb.AddForce(direction.x,0,direction.z);
        }
        else
        {
            rb.AddForce(-rb.velocity.x,0,-rb.velocity.z);
        }
        
    }
    void IMover.ViewpointMove(Transform transform,float mouse_X,float sensitivity)
    {
        float rot_X = mouse_X * sensitivity;
        transform.Rotate(0, rot_X, 0);
    }
}
