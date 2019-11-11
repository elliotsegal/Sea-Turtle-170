using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float acceleration;
	public float maxVelocity = 10;
	public float maxTorqueY = 2;
	public float maxTorqueZ = 2;
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

			// // Return to flat
			// if(Mathf.Abs(tf.localEulerAngles.z) < 1){
			// 	tf.localEulerAngles = new Vector3(0, 0, 0); // idk why I can't just set z directly
			// }
			// else{
			// 	tf.Rotate(0, 0, (tf.localEulerAngles.z > 0 ? -1 : 1));
			// }

		}
		else if(Mathf.Abs(rb.angularVelocity.y + torque) < maxTorqueY){ // turn
			rb.AddTorque(new Vector3(0, Mathf.Min(torque, maxTorqueY-torque), 0));

			// // Make it bank visually
			// if(Mathf.Abs(tf.rotation.z) < 30){ // I can't seem to get the "local" rotation!
			// 	rb.AddTorque(new Vector3(0, 0, Mathf.Min(torque, maxTorqueZ-torque)));
			// }

		}

		Vector3 movement = tf.forward * acceleration * Input.GetAxis("Vertical");
		rb.velocity += movement * Time.deltaTime;
		rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

		// slow the player down to about minVelocity if they're not pressing forward
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
