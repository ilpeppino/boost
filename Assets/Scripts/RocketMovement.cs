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

    [SerializeField] float pushForce = 500f;
    [SerializeField] float pushRotation = 10f;
    private Vector3 rotation, thrust;

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
        
        thrust = new Vector3(0f, Input.GetAxis("Fire1"), 0f);
        rb.AddRelativeForce(thrust * pushForce * Time.deltaTime);

    }

    private void PlayAudio()
    {
        if (thrust == Vector3.zero)
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
        rotation = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));
        transform.Rotate(rotation * pushRotation * Time.deltaTime);
        rb.freezeRotation = false;
    }


}
