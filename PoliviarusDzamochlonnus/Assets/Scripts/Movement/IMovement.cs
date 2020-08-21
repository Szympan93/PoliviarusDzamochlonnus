using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovement
{
    void Move(float distance);
    void Rotate(float angle);
}
