using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HashSet<Enemy> currentEnemies;
    [SerializeField] protected float waitTime;
    [SerializeField] protected GameObject ENEMY_PREFAB;

    // [SerializeField] private Enemy[] 
    [SerializeField] public Vector3[] tempNodes;
    public static Vector3[] s_tempNodes;
    private Queue<int> waves;

    public bool waveInProgress;
    public bool controllable;

    void Start()
    {
        controllable = true;
        waveInProgress = false;
        waves = new Queue<int>();
        waves.Enqueue(5);
        currentEnemies = new HashSet<Enemy>();
        // waves.Enqueue(new Queue<Enemy>(tempNodes));
    }

    void Update()
    {
        if (currentEnemies.Count < 0)
        {
            EndWave();
        }
        // something about giving player money for wave end or somethi nidk
    }

    // Starts next wave
    public void StartNextWave()
    {
        waveInProgress = true;
        int curWave = waves.Peek();
        waves.Dequeue();

        IEnumerator spawnRoutine = SpawnWave(curWave);
        StartCoroutine(spawnRoutine);
    }

    // Spawns the next enemy every x seconds
    IEnumerator SpawnWave(int curWave)
    {
        for (int i = 0; i < curWave; i++)
        {
            Enemy newEnemy = GameObject.Instantiate(ENEMY_PREFAB).GetComponent<Enemy>();
            // newEnemy.SetNodes(tempNodes);
            currentEnemies.Add(newEnemy);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void EndWave()
    {
        Debug.Log("Wave complete");
        if (waves.Count == 0)
        {
            // Win Game
        }
        waveInProgress = false;
    }

    public void RemoveEnemy(Enemy enemy) { currentEnemies.Remove(enemy); }
    public HashSet<Enemy> GetEnemies() { return currentEnemies; }
}
