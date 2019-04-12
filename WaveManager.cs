using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int Index = 0;

    public bool DontDestroy = true;

    public static WaveManager Manager { get; private set; }


    void Start()
    {
        if (DontDestroy)
            DontDestroyOnLoad(this);
    }

    void Update()
    {
        
    }
}
