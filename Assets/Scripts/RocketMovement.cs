using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    #region Cached references

    private Rigidbody rb;
    private AudioSource audioSource;

    #endregion

    #region Local variables

    [SerializeField] float thrustForce = 500f;
    [SerializeField] float rotationForce = 10f;
    private Vector3 _rotation, _thrust;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
        PlayAudio();

    }

    private void Thrust()
    {
        
        _thrust = new Vector3(0f, Input.GetAxis("Fire1"), 0f);
        rb.AddRelativeForce(_thrust * thrustForce * Time.deltaTime);

    }

    private void PlayAudio()
    {
        if (_thrust == Vector3.zero)
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
        else
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

        }
        
    }

    private void Rotate()
    {
        rb.freezeRotation = true;
        _rotation = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));
        transform.Rotate(_rotation * rotationForce * Time.deltaTime);
        rb.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;

            case "Obstacle":
                print("Game over");
                break;
        }
    }

}
