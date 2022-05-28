using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;
    public List<Wave> waves;
    [SerializeField] private int _nextWave;
    private float waveTimer;

    private Action CurrentState = delegate { };
    [SerializeField] private Transform enemyStartPoint;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        EventManager.Suscribe(Constants.StartNewWave, StartManuallyWave);
        EventManager.Suscribe(Constants.StartWaveManually, StartManuallyWave);
        EventManager.Suscribe(Constants.EndWaveString, EndWave);
        EventManager.Trigger(Constants.CountToNextWave, _nextWave);

        _nextWave = 0;
        CurrentState = Counting;
        waveTimer = 3;
    }

    void Update()
    {
        CurrentState();
    }

    private void Counting()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            EventManager.Trigger(Constants.StartNewWave, _nextWave);
            StartCoroutine(SpawningWave());
            CurrentState = delegate { };
        }
    }

    private void StartManuallyWave(params object[] parameters)
    {
        waveTimer = 3;
        CurrentState = Counting;
    }

    private void Waiting()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0)
        {
            EventManager.Trigger(Constants.CountToNextWave, _nextWave);
            waveTimer = 3;
            CurrentState = Counting;
        }
    }

    IEnumerator SpawningWave()
    {
        waves[_nextWave].InitializeWave();
        int aux = waves[_nextWave].enemyQueue.Count;
        for (int i = 0; i < aux; i++)
        {
            Enemy newEnemy = Instantiate(waves[_nextWave].enemyQueue.Peek(), enemyStartPoint.position, Quaternion.identity);
            waves[_nextWave].enemyQueue.Dequeue();
            EventManager.Trigger(Constants.SpawnNewEnemyString, newEnemy);
            yield return new WaitForSeconds(3f);
        }
        CurrentState = delegate { };
        yield break;
    }

    private bool CheckLastWave()
    {
        return _nextWave + 1 >= waves.Count;
    }

    private void EndWave(params object[] parameters)
    {
        if (CheckLastWave())
            EventManager.Trigger(Constants.EndLevelString, true); // true = win , false = defeat
        else
        {
            CurrentState = Waiting;
            _nextWave++;
            waveTimer = 60;
        } 

    }
    public int GetNextWave()
    {
        return _nextWave;
    }
}
