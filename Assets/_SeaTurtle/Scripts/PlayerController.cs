using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration;
    public float maxVelocity = 10;
    public float maxTorque = 2;
    private Rigidbody rb;
    private Transform tf;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tf = GetComponent<Transform>();
    }

    // called just before performing any Physics operations
    void FixedUpdate(){
        // At the start, X is forward, Z is to the side

        float torque = Input.GetAxis("Horizontal");
        if(torque == 0){ // stop turning if nothing is being pressed
            rb.AddTorque(new Vector3(0, -rb.angularVelocity.y/2));
        }
        else if(Mathf.Abs(rb.angularVelocity.y + torque) < maxTorque){
            rb.AddTorque(new Vector3(0, Mathf.Min(torque, maxTorque-torque), 0));
        }

        Vector3 movement = tf.forward * acceleration * Input.GetAxis("Vertical");
        rb.velocity += movement * Time.deltaTime;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
    }

    // We'll use this function to allow the player to eat & such
    /* we'll use
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
        }
    }
    */
}
