using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public bool IsInitialized { get; private set; }

    public void Initialize()
    {
        IsInitialized = true;   
    }

    public void Prepare()
    {

    }

    public void Unload()
    {
        IsInitialized = false;
    }
}
