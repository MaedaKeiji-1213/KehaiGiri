using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputter
{
    public Vector3 InputMove(Transform transform);
    public float Input_Mouse();
    public bool Is_Attack();
}
