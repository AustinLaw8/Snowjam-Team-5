using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // private ArrayList<Enemy> currentEnemies;

    // private queue<queue<enemy>> waves

    void Start()
    {
        
    }

    void Update()
    {
        // if currentEnemies is empty
            // end wave
        // something about giving player money for wave end or somethi nidk
    }

    // From the way i think about it the player will call this function to start next wave
    public void StartNextWave()
    {
        // queue<enemy> curwave = waves.Peek()
        // waves.Deque()
        // something somthing invoke an ienumerator to spawn the enemies in the wave

    }

    // Spawns the next enemy every x seconds
    // IEnumerator SpawnWave(curwave)
    // {
            // for enemy in curwave
            //     spawn enemy
            //     wait x time
    // }

    // ArrayList<Enemy> GetEnemies() { return enemies; }
}
