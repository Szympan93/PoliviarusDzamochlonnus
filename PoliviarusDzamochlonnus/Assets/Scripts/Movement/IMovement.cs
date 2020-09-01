using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovement
{
    void Move(float distance);
    void Rotate(float angle);
}
