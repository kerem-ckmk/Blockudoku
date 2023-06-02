using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public bool IsInitiailized { get; private set; }


    public void Initialize()
    {
        IsInitiailized = true;
    }

}

