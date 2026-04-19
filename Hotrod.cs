using UnityEngine;

public class Hotrod : MonoBehaviour
{
    private float hotrodSpeed = 2f;
    private float zigzagSpeed = 3f;
    private float zigzagSwing = 7f;
    private GameManager gameManager;

    private float startX;

    void Start()
    {
        startX = transform.position.x;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        // Move downward
        float moveY = -hotrodSpeed * Time.deltaTime;

        // Zigzag side-to-side using sine wave
        float moveX = Mathf.Sin(Time.time * zigzagSpeed) * zigzagSwing;

        transform.position = new Vector3(startX + moveX, transform.position.y + moveY, 0);

        // Destroys object
        if (transform.position.y < -6.5f)
        {
            Destroy(this.gameObject);
        }

    }
}