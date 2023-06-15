using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMover
{
    public void Move(Vector3 direction,float speed);
    public void ViewpointMove(Transform transform, float mouse_X, float sensitivity);
}
