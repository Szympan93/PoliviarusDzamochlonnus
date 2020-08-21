using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObjectPosition : IPositionProvider
{
    private Transform transform;

    public Vector3 Value => transform.position;

    public ObjectPosition(GameObject gameObject) : this(gameObject.transform) { }
    public ObjectPosition(Transform transform)
    {
        this.transform = transform;
    }

    public static implicit operator Transform(ObjectPosition position) => position.transform;
    public static implicit operator Vector3(ObjectPosition position) => position.transform.position;
}
