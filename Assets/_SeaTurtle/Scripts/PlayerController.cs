using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float acceleration;
	public float maxVelocity = 10;
    [Space]
    public float rotateSpeed = 90;
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
		// At the start, X is forward, Z is to the side

		// For now lock the x rotation to it stays flat
		//tf.localEulerAngles = new Vector3(0, tf.localEulerAngles.y, tf.localEulerAngles.z);

		float torque = Input.GetAxis("Horizontal"); // it should probably be called "inputLR" or something since it doesn't use AddTorque() anymore
        Vector3 angles = tf.localEulerAngles;
        angles.y += torque * rotateSpeed * Time.fixedDeltaTime;
		if(Mathf.Abs((angles.z < 180 ? angles.z : -360+angles.z) - (-torque*bankAmount)) < 3){ // If our current "bank" is less than 3 degrees off what it would be if we started at rest
			angles.z = -torque * bankAmount; // Bank based on how quickly we're turning
		}
		else{ // if the tutrle was turning one way and now needs to turn the other way
			angles.z += 3*(torque > 0 ? -1 : 1); // The 3 is just to approximate how much it would usually turn per frame
		}
        tf.localEulerAngles = angles;

		// add drag while turning to allow for sharper turns
		rb.drag = Mathf.Abs(torque)/2f;

        Vector3 movement = tf.forward * acceleration * Input.GetAxis("Vertical");
		rb.velocity += movement * Time.deltaTime * (1+Mathf.Abs(torque/10)); // the last bit is to offset drag while turning
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

		// slow the player to a stop if they're not pressing forward
		if(Input.GetAxis("Vertical") == 0){
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
}
