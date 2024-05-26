using System;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    public Vector3 startingPos;
    public Vector3 movementVector;
    [SerializeField][Range(0, 1)] float movementFactor;
    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        const float tau = Mathf.PI * 2;
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / (period);
        float rawSineWave = Mathf.Sin(cycles * tau);
        movementFactor = (rawSineWave + 1f) / 2;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
