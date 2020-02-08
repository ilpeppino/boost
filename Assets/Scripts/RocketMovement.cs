using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    #region Variables

    private Rigidbody rb;

    [SerializeField] float pushForce = 500f;
    private Vector3 rotation, thrust;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
    }

    private void Thrust()
    {
        thrust = new Vector3(0f, Input.GetAxis("Fire1"), 0f);
        rb.AddRelativeForce(thrust * pushForce * Time.deltaTime);
    }

    private void Rotate()
    {
        rotation = new Vector3(0f, 0f, -Input.GetAxis("Horizontal"));
        transform.Rotate(rotation);
    }


}
