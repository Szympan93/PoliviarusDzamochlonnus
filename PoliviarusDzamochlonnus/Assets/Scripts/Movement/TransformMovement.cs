using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMovement : MonoBehaviour, IPlayerMovement
{
    public void Move(float distance)
    {
        transform.position += transform.forward * distance;
    }

    public void Rotate(float angle)
    {
        transform.localEulerAngles += Vector3.up * angle;
    }
}
