using UnityEngine;

public class Dalek : MonoBehaviour
{
    public float exterminate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, exterminate);
    }
}
