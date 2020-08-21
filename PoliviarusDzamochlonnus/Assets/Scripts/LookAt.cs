using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public IPositionProvider Target { get; set; }

    public void SetTarget(IPositionProvider target) => Target = target;
    public void SetTarget(Vector3 target) => Target = new Position(target);
    public void SetTarget(Transform target) => Target = new ObjectPosition(target);

    protected void Update()
    {
        if (Target == null) return;
        transform.rotation = Quaternion.LookRotation(Target.Value - transform.position, Vector3.up);
    }
}
