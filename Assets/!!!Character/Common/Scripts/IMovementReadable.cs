using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementReadable
{

    Vector3 GetVelocity();
    float GetWalkSpeed();
    float GetRunSpeed();
    float GetJumpSpeed();
    bool GetGrounded();
}
