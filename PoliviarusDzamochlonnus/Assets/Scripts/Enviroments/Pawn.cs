using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pawn : MonoBehaviour
{
    public delegate void CreatedDelegate(Pawn pawn);
    public delegate void DestroyedDelegate(Pawn pawn);
    public delegate void DiseaseChangedDelegate(Pawn pawn, Disease oldDisease, Disease newDisease);

    public static event CreatedDelegate CreatedEvent = delegate { };
    public static event DestroyedDelegate DestroyedEvent = delegate { };
    public static event DiseaseChangedDelegate DiseaseChangedEvent = delegate { };

    public static IReadOnlyList<Pawn> All => pawns;

    private static List<Pawn> pawns = new List<Pawn>();
    private Disease disease;

    public Disease Disease 
    {
        get => disease;
        set
        {
            if (value == disease) return;
            var oldDisease = disease;
            disease = value;
            DiseaseChangedEvent(this, oldDisease, disease);
        }
    }

    protected void Start()
    {
        pawns.Add(this);
        CreatedEvent(this);
    }

    protected void OnDestroy()
    {
        DestroyedEvent(this);
        pawns.Remove(this);
    }
}
