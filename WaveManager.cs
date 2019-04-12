using System;
using System.Linq;
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

[Serializable]
public struct Enemy
{
    public GameObject mobOject;
    public int EnemyID;
}

[Serializable]
public struct EnemyManager
{
    public int EnemyID;
    public float Rate;
    public int Count;
}

[Serializable]
public struct Wave
{
    public string WaveName;
    public EnemyManager[] Enemies;
}

public class WaveManager : MonoBehaviour
{
    public int Index = 0;

    public bool DontDestroy = true;
    public bool RunInBackground = true;

    [SerializeField]
    private Enemy[] Enemies;

    [SerializeField]
    private Wave[] Waves;

    public static WaveManager Manager { get; private set; }

    private WaveStatus status = WaveStatus.Init;

    public WaveStatus Status { get { return status; } set { status = value; } }


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

    void OnValidate()
    {
#if UNITY_EDITOR
        if (Enemies.Length < 1)
        {
            Debug.LogError("You must set the Enemies in the manager");
        }

        if (Waves.Length < 0)
        {
            Debug.LogError("You must set the Waves in the manager");
        }

#endif
    }

    void InitSystem()
    {
        switch ((int)status)
        {
            case 0:
                Init();
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

    private void Init()
    {
        OnInitializedWave();

        if (Enemies.Length < 1) return;     
        if (Waves.Length < 0) return;
        

        
    }





    /// <summary>
    /// 
    /// </summary>
    public virtual void OnInitializedWave()
    {

    }
}
