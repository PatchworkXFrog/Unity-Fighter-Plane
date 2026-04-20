using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public int lives;
    private GameManager gameManager;
    public GameObject explosionPrefab;
    private float playerSpeed;
    private float verticalMinimum = -5f;
    private float verticalMaximum = -1.5f;
    private float horizontalInput;
    private float verticalInput;

    public GameObject thrusters;
    public GameObject shield;
    public int weaponType;
    public bool shieldActive;

    public GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        shieldActive = false;
        weaponType = 1;
        playerSpeed = 6f;
        lives = 3;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    public void LoseALife()
    {
        if (!shieldActive)
        {
            lives--;
        }
        if (shieldActive)
        {
            shield.SetActive(false);
            shieldActive=false;
        }

        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            gameManager.GameOver();


            Destroy(this.gameObject);
        }
    }

    IEnumerator ShieldPowerDown()
    {
        yield return new WaitForSeconds(5);
        shield.SetActive(false);
        shieldActive = false;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5);
        playerSpeed = 5f;
        thrusters.SetActive(false);
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5);
        weaponType = 1;
        gameManager.PlaySound(2);
        gameManager.ManagePowerupText(5);
    }



    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if(whatDidIHit.tag == "PowerUp")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerUp = Random.Range(1, 5);
            gameManager.PlaySound(1);
            switch (whichPowerUp)
            {
                case 1:
                    playerSpeed = 10f;
                    thrusters.SetActive(true);
                    gameManager.ManagePowerupText(1);
                    StartCoroutine(SpeedPowerDown());
                    break;
                case 2:
                    weaponType = 2;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
                    shield.SetActive(true);
                    shieldActive = true;
                    gameManager.ManagePowerupText(4);
                    StartCoroutine(ShieldPowerDown());
                    break;
            }
        }
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.5f, 0.5f, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.5f, 0.5f, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
        }
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x > horizontalScreenSize || transform.position.x <= -horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y > verticalScreenSize || transform.position.y <= -verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }

        //Locks player position near bottom of screen
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, verticalMinimum, verticalMaximum);
        transform.position = pos;
    }
}
