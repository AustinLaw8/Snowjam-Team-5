using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private HashSet<Enemy> currentEnemies;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject ENEMY_PREFAB;

    [SerializeField] private PlayerScript player;
    [SerializeField] private int cash;
    
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
        waves.Enqueue(5);
        currentEnemies = new HashSet<Enemy>();
        if (player == null) player = GameObject.Find("Player").transform.GetChild(2).GetComponent<PlayerScript>();
    }

    void Update()
    {
        if (waveInProgress && currentEnemies.Count == 0)
        {
            EndWave();
        }
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
            Debug.Log("No more waves, gg");
        }
        waveInProgress = false;
        // TODO:
        // Give player cash for winning
        // Mini UI thing of "Wave X/Y"
    }

    public PlayerScript GetPlayer() { return player; } 

    public void RemoveEnemy(Enemy enemy) { currentEnemies.Remove(enemy); }
    public HashSet<Enemy> GetEnemies() { return currentEnemies; }

    public int GetCash() { return cash; }
    public void IncreaseCash(int amount) { cash += amount; }
    
    public bool SpendCash(int amount) {
        if (amount > cash)
        {
            return false;
        }
        else
        {
            cash -= amount;
            return true;
        }
    }

}
