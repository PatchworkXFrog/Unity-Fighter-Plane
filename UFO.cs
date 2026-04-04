using UnityEngine;

public class UFO : MonoBehaviour
{
    private float UFOSpeed = 2f;
    private float zigzagSpeed = 3f;
    private float zigzagSwing = 7f;

    private float startX;

    void Start()
    {
        startX = transform.position.x;
    }

    void Update()
    {
        // Move downward
        float moveY = -UFOSpeed * Time.deltaTime;

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