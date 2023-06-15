using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacker :IAttacker
{
    Transform parent;
    GameObject attack_prefab = null;
    GameObject attack_object = null;
    Rigidbody rb;
    const float ATTACK_SPEED=0.5f;
    const float ATTACK_INTERVAL=2f;
    float attack_cooltimer=0;
    void IAttacker.Attack(bool is_attack)
    {
        if (attack_prefab == null)
        {
            attack_prefab=Resources.Load<GameObject>("AttackPrefab");
        }
        if(attack_object!=null)
        {
            rb.velocity=Vector3.zero;
        }
        else if(is_attack && attack_cooltimer <= Time.time)
        {
            attack_cooltimer=Time.time+ATTACK_INTERVAL;
            AudioController.instance.AddSound("Attack",parent.position);
            attack_object=MonoBehaviour.Instantiate<GameObject>(attack_prefab,parent.position,parent.rotation,parent);
            MonoBehaviour.Destroy(attack_object, ATTACK_SPEED);
        }
    }
    public PlayerAttacker(GameObject player_object)
    {
        parent = player_object.transform;
        rb=player_object.GetComponentInChildren<Rigidbody>();
    }
}
