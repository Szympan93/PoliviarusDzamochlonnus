using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StudentCounter : MonoBehaviour
{
    [SerializeField] private List<LayoutCounter> playerCounters;
    [SerializeField] private LayoutCounter remainingCounter;

    protected void Update()
    {
        remainingCounter.Redraw(Pawn.All.Count(e => e.Disease == null));
        for(int i = 0; i < playerCounters.Count; i++)
        {
            playerCounters[i].Redraw(Pawn.All.Count(e=>e.Disease?.GetComponent<PlayerController>().PlayerId == i));
        }
    }
}
