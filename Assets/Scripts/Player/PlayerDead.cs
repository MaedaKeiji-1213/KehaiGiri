using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeader:IDeader
{
    GameObject my_gameobject;
    bool is_dead=false;
    public bool IsDead { get{return is_dead;}}
    public PlayerDeader(GameObject gameObject)
    {
        my_gameobject=gameObject;
    }

    public void Dead()
    {
        Debug.Log(my_gameobject);
        is_dead=true;
        AudioController.instance.AddSound("Dead",my_gameobject.transform.position);
        my_gameobject.GetComponent<MeshRenderer>().material.color=Color.blue;
        MonoBehaviour.Destroy(my_gameobject.GetComponent<Rigidbody>());
        MonoBehaviour.Destroy(my_gameobject.GetComponent<Collider>());
    }
}
