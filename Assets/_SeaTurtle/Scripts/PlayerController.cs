﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float acceleration = 4;
	public float maxVelocity = 6;
	public float maxClimb = 45;
    [Space]
    public float rotateSpeed = 60;
    public float bankAmount;

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
		float torque = Input.GetAxis("Horizontal"); // it should probably be called "inputLR" or something since it doesn't use AddTorque() anymore

        Vector3 angles = rb.rotation.eulerAngles;
        angles.x += Input.GetAxis("Vertical") * rotateSpeed * Time.fixedDeltaTime;
		if(angles.x < 180 && angles.x > maxClimb) angles.x = maxClimb;
		else if(angles.x > 180 && angles.x < 360-maxClimb) angles.x = 360-maxClimb;

        angles.y += torque * rotateSpeed * Time.fixedDeltaTime;

		if(Mathf.Abs((angles.z < 180 ? angles.z : -360+angles.z) - (-torque*bankAmount)) < 3){ // If our current "bank" is less than 3 degrees off what it would be if we started at rest
			angles.z = -torque * bankAmount; // Bank based on how quickly we're turning
		}
		else{ // if the tutrle was turning one way and now needs to turn the other way
			angles.z += 3*(torque > 0 ? -1 : 1); // The 3 is just to approximate how much it would usually turn per frame
		}
        rb.MoveRotation(Quaternion.Euler(angles));

        bool throttle = Input.GetKey("space");

		// add drag while turning to allow for sharper turns
		rb.drag = (throttle ? Mathf.Abs(torque) : 0);

        Vector3 movement = tf.forward * acceleration * (throttle ? 1 : 0);
		rb.velocity += movement * Time.deltaTime * (1+Mathf.Abs(torque/2)); // the last bit is to offset drag while turning
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

		// slow the player to a stop if they're not pressing forward
		if(!throttle){
			rb.velocity *= 0.97f;
		}
	}

	// We'll use this function to allow the player to eat & such
	/* we'll use
	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive(false);
		}
	}
	*/

	public void OnFoodEaten()
	{
        Debug.Log("eat");
    }
}
