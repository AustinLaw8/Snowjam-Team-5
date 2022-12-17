using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<Enemy> currentEnemies;
    [SerializeField] protected float waitTime;
    [SerializeField] protected GameObject ENEMY_PREFAB;

    // [SerializeField] private Enemy[] 
    [SerializeField] public Vector3[] tempNodes;
    private Queue<int> waves;

    void Start()
    {
        waves = new Queue<int>();
        waves.Enqueue(5);
        // waves.Enqueue(new Queue<Enemy>(tempNodes));
    }

    void Update()
    {
        // if currentEnemies is empty
            // end wave
        // something about giving player money for wave end or somethi nidk
    }

    // Starts next wave
    public void StartNextWave()
    {
        if (waves.Count == 0)
        {
            // Win Game
        }
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
            GameObject newEnemy = GameObject.Instantiate(ENEMY_PREFAB);
            newEnemy.GetComponent<Enemy>().SetNodes(tempNodes);
            yield return new WaitForSeconds(waitTime);
        }
    }

    // ArrayList<Enemy> GetEnemies() { return enemies; }
}
