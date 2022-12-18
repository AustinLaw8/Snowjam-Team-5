using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private HashSet<Enemy> currentEnemies;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject[] ENEMY_PREFABS;
    GameObject nextEnemy;

    [SerializeField] private GameObject player;
    [SerializeField] private int cash;
    [SerializeField] private int health;

    [SerializeField] public TMP_Text cashText;
    [SerializeField] public TMP_Text hpText;

    [SerializeField] public Vector3[] tempNodes;
    public static Vector3[] s_tempNodes;
    public Transform[] goal;
    private Queue<int> waves;

    public bool waveInProgress;
    public bool controllable;

    int currentWave;

    public enum EnemyType {water, cave, flying }

    [System.Serializable]
    public struct Fdsa
    {
        [SerializeField] public EnemyType[] enemies;
    }
    [SerializeField] Fdsa[] enemyWaves;

    void Start()
    {
        controllable = true;
        waveInProgress = false;
        waves = new Queue<int>();
        waves.Enqueue(5);
        waves.Enqueue(5);
        currentEnemies = new HashSet<Enemy>();
        if (player == null) player = GameObject.Find("Player");

        player.GetComponent<PlayerShooting>().enabled = false;
        player.GetComponent<TowerPlacement>().enabled = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartNextWave();
        }

        if (waveInProgress && currentEnemies.Count == 0)
        {
            EndWave();
        }

        UpdateText();
    }

    void UpdateText()
    {
        cashText.text = $"{cash}";
        hpText.text = $"{health}";
    }

    public void StartNextWave()
    {
        waveInProgress = true;
        //int curWave = waves.Peek();
        //waves.Dequeue();

        player.GetComponent<PlayerShooting>().enabled = true;
        player.GetComponent<TowerPlacement>().enabled = false;

        IEnumerator spawnRoutine = SpawnWave(currentWave);
        currentWave++;

        StartCoroutine(spawnRoutine);
    }

    // Spawns the next enemy every x seconds
    IEnumerator SpawnWave(int curWave)
    {
        for (int i = 0; i < enemyWaves[curWave].enemies.Length; i++)
        {
            if (enemyWaves[curWave].enemies[i] == EnemyType.water)
            {
                nextEnemy = ENEMY_PREFABS[0];
            }
            else if (enemyWaves[curWave].enemies[i] == EnemyType.cave)
            {
                nextEnemy = ENEMY_PREFABS[1];
            }
            else if (enemyWaves[curWave].enemies[i] == EnemyType.flying)
            {
                //nextEnemy = ENEMY_PREFABS[2];
            }

            Enemy newEnemy = GameObject.Instantiate(nextEnemy).GetComponent<Enemy>();
            // newEnemy.SetNodes(tempNodes);
            currentEnemies.Add(newEnemy);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void EndWave()
    {
        Debug.Log("Wave complete");

        player.GetComponent<PlayerShooting>().enabled = false;
        player.GetComponent<TowerPlacement>().enabled = true;

        if (waves.Count == 0)
        {
            Debug.Log("No more waves, gg");
        }
        waveInProgress = false;
        // TODO:
        // Give player cash for winning
        // Mini UI thing of "Wave X/Y"
    }

    public void DecreaseHealth(int amount) {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("you died, gg");
            // Handle death
        }
    }

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
