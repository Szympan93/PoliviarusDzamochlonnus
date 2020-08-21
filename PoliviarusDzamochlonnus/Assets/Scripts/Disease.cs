using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour
{
    private LookAt _targetPointer;

    protected void Awake()
    {
        _targetPointer = GetComponentInChildren<LookAt>();
    }

    protected void Update()
    {
        UpdateArrow();
    }

    protected void OnTriggerEnter(Collider other)
    {
        var pawn = other.GetComponentInParent<Pawn>();
        if (!pawn) return;

        if(pawn.Disease != this)
        {
            pawn.Disease = this;
        }
    }

    protected void UpdateArrow()
    {
        var target = Pawn.All.Where(e => e.Disease != this).Select(e => e.transform).ClosestTo(transform.position);
        if (!target)
        {
            _targetPointer.gameObject.SetActive(false);
            return;
        }
        _targetPointer.gameObject.SetActive(true);
        _targetPointer.SetTarget(target);
    }
}
