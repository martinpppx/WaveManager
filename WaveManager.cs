using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WaveStatus : int
{
    Init = 0,
    Start,
    Update,
    Spawn,
    Wait,
    Win,
    Lose,
}

[Serializable]
public struct EnemyManager
{
    public GameObject Enemy;
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

    public float TimeToStart = 30f;

    public bool DontDestroy = true;
    public bool RunInBackground = true;

    public bool WaitForOtherPlayers = false;
    public int MinPlayers = 3;

    [SerializeField]
    private Wave[] Waves;

    public string EnemyTag = "Enemy";
    public string PlayerTag = "Player";

    public static WaveManager Manager { get; private set; }

    private WaveStatus status = WaveStatus.Init;

    public WaveStatus Status { get { return status; } set { status = value; } }


    private float WaveCountdown = 0f;

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
        switch (status)
        {
            case WaveStatus.Init:
                Init();
                break;
            case WaveStatus.Start:
                StartWave();
                break;
            case WaveStatus.Update:
                UpdateGame();
                break;
            case WaveStatus.Spawn:
                StartCoroutine(SpawnAll(Waves[Index]));
                break;
            case WaveStatus.Wait:
                //
                break;
            case WaveStatus.Win:
                Won();
                break;
            case WaveStatus.Lose:
                End();
                break;
        }
    }

    private void Init()
    {
        OnInitializedWave();

        if (Waves.Length < 0) {
            Debug.LogError("You must set the Waves in the manager");
            return;
        }

        if (WaitForOtherPlayers)
        {
            GameObject[] players = AllPlayers();

            if (players.Length >= MinPlayers)
            {
                status = WaveStatus.Start;
                WaveCountdown = Time.time;
            }
        }
        else
        {
            WaveCountdown = Time.time;
            status = WaveStatus.Start;
        }
    }

    private void StartWave()
    {
        OnStartWave();

        float _TimeToStart = (WaveCountdown + TimeToStart) - Time.time;

        ///
        Debug.Log(Mathf.Ceil(_TimeToStart).ToString());    

        if (Time.time > TimeToStart + WaveCountdown)
        {
            status = WaveStatus.Spawn;
        }
    }

    private IEnumerator SpawnAll(Wave mode)
    {
        status = WaveStatus.Wait;

        for (int b = 0; b < mode.Enemies.Length; b++)
        {
            for (int i = 0; i < mode.Enemies[b].Count; i++)
            {
                EnemyManager panel = mode.Enemies[b];
                RequestSpawnMob(panel);
                OnMobSpawned(panel.Enemy);
                yield return new WaitForSeconds(1f / mode.Enemies[b].Rate);
            }
        }

        status = WaveStatus.Update;

        yield break;
    }

    private GameObject RequestSpawnMob(EnemyManager mode, Vector3 Pos = Vector3.Zero, Quaternion Rot = Quaternion.identity)
    {
        GameObject mob = Instantiate(mode.Enemy, Pos, Rot);

        //do something like mob.getComponent<Health>().AddHealth(500) 
        return mob;
    }

    private void UpdateGame()
    {
        if (AllPlayers().Length == 0)
        {
            status = WaveStatus.Lose;
        }else if (AllEnemies().Length == 0)
        {
            NextWave();
        }
    }

    private void NextWave()
    {
        status = WaveStatus.Wait;

        if (Index + 1 >= Waves.Length)
        {
            status = WaveStatus.Win;
            return;
        }

        Index++;
        WaveCountdown = Time.time;

        SpawnDeathPlayers(); 

        status = WaveStatus.Spawn; //?
    }
    
    private void Won()
    {
        WonTheGame();
    }

    private void End()
    {
        EndTheGame();
    }



    private void SpawnDeathPlayers()
    {
        OnSpawnDeathPlayers();
    }

    public static GameObject[] AllEnemies()
    {
        return (GameObject[])GameObject.FindGameObjectsWithTag(EnemyTag);
    }
    public static GameObject[] AllPlayers()
    {
        return (GameObject[])GameObject.FindGameObjectsWithTag(PlayerTag);
    }
	


    /// <summary>
    /// 
    /// </summary>
    public virtual void OnInitializedWave()
    {

    }

    public virtual void OnStartWave()
    {

    }

    public virtual void OnMobSpawned(GameObject Mob)
    {
        //
    }

    public virtual void OnSpawnDeathPlayers()
    {
        //todo:
    }

    public virtual void WonTheGame()
    {

    }

    public virtual void EndTheGame()
    {

    }
}
