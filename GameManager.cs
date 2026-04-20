using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;
    public GameObject gameOverMenu;

    public GameObject powerupPrefab;
    public GameObject audioPlayer;
    public AudioClip powerUpSound;
    public AudioClip powerDownSound;

    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupText;

    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int score;

    private bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 14.3f;
        verticalScreenSize = 6.5f;
        score = 0;
        gameOver = false;
        InvokeRepeating("CreateCoin", 2, 3);
        StartCoroutine(SpawnPowerup());
        powerupText.text = "No powerups currently active";

        Instantiate(playerPrefab, transform.position, Quaternion.identity);

        CreateSky();

        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 4, 8);
    }

    void Update()
    {
        if(gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }

    }

    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * .9f, verticalScreenSize, 0), Quaternion.identity);
    }

    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * .9f, verticalScreenSize, 0), Quaternion.identity);
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f),
        Random.Range(-verticalScreenSize * 0.5f, -verticalScreenSize * 0.2f), 0), Quaternion.identity);
    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 12);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "Speed Boost!";
                break;
            case 2:
                powerupText.text = "Double Weapon!";
                break;
            case 3:
                powerupText.text = "Tripple Weapon!";
                break;
            case 4:
                powerupText.text = "Armor!";
                break;
            default:
                powerupText.text = "No current powerups";
                break;
        }
    }

    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;
        }
    }

    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        scoreText.text = "Score: " + score;
    }

    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    void CreateCoin()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.5f, horizontalScreenSize * 0.5f), 
        Random.Range(-verticalScreenSize * 0.5f, -verticalScreenSize * 0.2f), 0), Quaternion.identity);
    }

    public void GameOver()
    {
        gameOverMenu.SetActive(true);
        gameOver = true;
    }
}
