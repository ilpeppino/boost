using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ObstaclesMovement : MonoBehaviour
{

    [SerializeField] [Range(-1, 1)] float x_Oscillation;
    //[SerializeField] [Range(-1, 1)] float y_Oscillation;
    //[SerializeField] [Range(-1, 1)] float z_Oscillation;
    [SerializeField] float x_Amplitude = 10f;
    //[SerializeField] float y_Amplitude = 10f;
    //[SerializeField] float z_Amplitude = 10f;

    [SerializeField] private float _periodOscillation = 3f;
    private float _cyclesOscillation;
    private Vector3 _initialPosition;
    private const float tau = Mathf.PI * 2;

    private void Start()
    {
        _initialPosition = transform.position;
    }


    void Update()
    {

        _cyclesOscillation = Time.time / _periodOscillation;

        // One cycle starts at 0, peaks at 1 at 1/2pi, goes to 0 at pi, peaks -1 at 3/2pi and goes to 0 to 2pi
        x_Oscillation = Mathf.Sin(_cyclesOscillation * tau) * x_Amplitude;
        //x_Oscillation = Mathf.Sin(_cyclesOscillation) * x_Amplitude;

        transform.position = new Vector3(
            _initialPosition.x + x_Oscillation, 
            _initialPosition.y, 
            _initialPosition.z);
    }
}
