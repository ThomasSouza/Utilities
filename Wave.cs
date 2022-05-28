using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public List<Enemy> enemiesToSpawn;
    public Queue<Enemy> enemyQueue = new Queue<Enemy>();

    public void InitializeWave()
    {
        enemyQueue = new Queue<Enemy>();
        for (int i = 0; i < enemiesToSpawn.Count; i++)
        {
            enemyQueue.Enqueue(enemiesToSpawn[i]);
        }
    }
}
