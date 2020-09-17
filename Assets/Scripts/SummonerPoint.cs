using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerPoint : MonoBehaviour
{
    public bool occupied;

    void Start()
    {
        occupied = false;
    }

    public void Occupy()
    {
        occupied = true;
    }

    public void Free()
    {
        occupied = false;
    }
}
