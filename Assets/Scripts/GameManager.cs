using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState
{
    Building, Defending,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject airdrop;
    [SerializeField] Transform chosenSpawnPoint;
    [SerializeField] Transform[] spawnPoints;
    private HashSet<Enemy> currentEnemies;
    [SerializeField] private float waitTime;
    [SerializeField] private GameObject[] ENEMY_PREFABS;
    GameObject nextEnemy;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private int cash;
    [SerializeField] private int health;

    [SerializeField] private TMP_Text cashText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text waveCountText;
    [SerializeField] private TMP_Text enemyCountText;

    [SerializeField] private GameObject endGameCanvas;

    public Transform[] goal;
    private Queue<int> waves;

    [SerializeField] private GameState gameState;
    public bool paused;

    private int currentWave;
    private int totalWaves;

    public static GameManager self;
    public Transform[] nodes;

    public enum EnemyType {water, cave, giant, flying }

    [System.Serializable]
    public struct EnemyWaves
    {
        [SerializeField] public EnemyType[] enemies;
    }
    [SerializeField] EnemyWaves[] enemyWaves;

    private void Awake()
    {
        self = GetComponent<GameManager>();
    }

    void Start()
    {
        currentWave = 0;
        totalWaves = enemyWaves.Length;
        currentEnemies = new HashSet<Enemy>();
        if (player == null) player = GameObject.Find("Player");
        if (cashText == null) cashText = GameObject.Find("CashText").GetComponent<TMP_Text>();
        if (hpText == null) hpText = GameObject.Find("HPText").GetComponent<TMP_Text>();
        gameState = GameState.Building;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Time.timeScale == 1) Pause();
            else Unpause();
        }
        if (gameState == GameState.Building && Input.GetKeyDown(KeyCode.K))
        {
            StartNextWave();
        }

        if (gameState == GameState.Defending && currentEnemies.Count == 0)
        {
            Invoke("EndWave", 1);
        }

        UpdateText();
    }

    void UpdateText()
    {
        cashText.text = $"{cash}";
        hpText.text = $"{health}";
        waveCountText.text = $"Wave {currentWave}/{totalWaves}";
        if(gameState == GameState.Defending)
        {
            enemyCountText.text = $"{currentEnemies.Count} enemies remaining";
        }
        else if (gameState == GameState.Building)
        {
            enemyCountText.text = $"Start next wave (k)";
        }

    }

    public void StartNextWave()
    {
        if (currentWave == enemyWaves.Length) return;
        gameState = GameState.Defending;

        // Drops powerups
        for (int i = 0; i < currentWave; i++)
        {
            float xcoord = Random.Range(-50f, 30f);
            float zcoord = Random.Range(-50f, 16f);
            Instantiate(airdrop, new Vector3(xcoord, 27f, zcoord), Quaternion.identity);
        }

        IEnumerator spawnRoutine = SpawnWave(currentWave);
        currentWave++;

        StartCoroutine(spawnRoutine);
    }

    // Spawns the next enemy every `waitTime` seconds
    IEnumerator SpawnWave(int curWave)
    {
        for (int i = 0; i < enemyWaves[curWave].enemies.Length; i++)
        {
            if (enemyWaves[curWave].enemies[i] == EnemyType.water)
            {
                nextEnemy = ENEMY_PREFABS[0];
                chosenSpawnPoint = spawnPoints[0];
            }
            else if (enemyWaves[curWave].enemies[i] == EnemyType.cave)
            {
                nextEnemy = ENEMY_PREFABS[1];
                chosenSpawnPoint = spawnPoints[1];
            }
            else if (enemyWaves[curWave].enemies[i] == EnemyType.giant)
            {
                nextEnemy = ENEMY_PREFABS[2];
                chosenSpawnPoint = spawnPoints[1];
            }
            else if (enemyWaves[curWave].enemies[i] == EnemyType.flying)
            {
                //nextEnemy = ENEMY_PREFABS[2];
            }

            Enemy newEnemy = GameObject.Instantiate(nextEnemy, chosenSpawnPoint.position, Quaternion.identity).GetComponent<Enemy>();
            currentEnemies.Add(newEnemy);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void EndWave()
    {
        Debug.Log("Wave complete");
        if (currentWave == enemyWaves.Length)
        {
            Debug.Log("No more waves, gg");
            gameOver(false);

        }
        gameState = GameState.Building;
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
            gameOver(true);
        }
    }


    public void gameOver(bool died)
    {
        if(died)
        {
            endGameCanvas.GetComponentInChildren<TMP_Text>().text = "GGWP, you died!";
        }
        else
        {
            endGameCanvas.GetComponentInChildren<TMP_Text>().text = "GGWP, you won!";
        }
        endGameCanvas.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public GameState GetGameState() { return gameState; }

    public void RemoveEnemy(Enemy enemy) { currentEnemies.Remove(enemy); }
    public HashSet<Enemy> GetEnemies() { return currentEnemies; }

    public int GetCash() { return cash; }
    public void IncreaseCash(int amount) { cash += amount; }
    
    public bool SpendCash(int amount)
    {
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

    public void Pause()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        paused = true;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        paused = false;
    }
}
