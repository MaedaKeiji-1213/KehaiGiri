using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputter : MonoBehaviour,IInputter
{

    public Vector3 InputMove(Transform transform)
    {
        Vector3 input_direction = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized;  /*new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;*/
        return input_direction;
    }
    public float Input_Mouse()
    {
        return Input.GetAxis("Mouse X");
    }

    public bool Is_Attack()
    {
        if (Input.GetMouseButtonDown(0)) return true;
        else return false;
    }
}
