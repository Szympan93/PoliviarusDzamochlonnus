using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Position : IPositionProvider
{
    public Vector3 Value { get; }

    public Position(Vector3 value)
    {
        Value = value;
    }

    public static implicit operator Position(Vector3 value) => new Position(value);
    public static implicit operator Vector3(Position position) => position.Value;
}
