using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveStatus
{
    Init = 0,
    Start = 1,
    Break = 2,
    Update = 3,
    Finish = 4,
    Win = 5,
    Lose = 6
}

public class WaveManager : MonoBehaviour
{
    public int Index = 0;

    public bool DontDestroy = true;

	private WaveStatus status = WaveStatus.Init;

    public WaveStatus Status { get { return status; } set { status = value; } }
	
    public static WaveManager Manager { get; private set; }


        void Start()
    {
        if (DontDestroy)
            DontDestroyOnLoad(this);

        Application.runInBackground = RunInBackground;
    }

    void Update()
    {
        InitSystem();
    }

    void InitSystem()
    {
        switch ((int)status)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            case 6:

                break;
        }
    }
}
