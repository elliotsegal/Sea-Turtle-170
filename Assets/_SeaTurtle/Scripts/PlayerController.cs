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

		// For now lock the x rotation to it stays flat
		tf.localEulerAngles = new Vector3(0, tf.localEulerAngles.y, tf.localEulerAngles.z);

		float torque = Input.GetAxis("Horizontal");
		if(torque == 0){ // stop turning if nothing is being pressed
			rb.AddTorque(new Vector3(0, -rb.angularVelocity.y*0.85f, 0));

			// // Return to flat. Should really be in its own "if left/right aren't being pressed" block but here is ok
			// if(Mathf.Abs(tf.localEulerAngles.z) < 0.5f){
			// 	//tf.localEulerAngles = new Vector3(tf.localEulerAngles.x, tf.localEulerAngles.y, 0f); // idk why I can't just set z directly
			// 	tf.Rotate(0, 0, tf.localEulerAngles.z > 0f ? -tf.localEulerAngles.z : tf.localEulerAngles.z);
			// }
			// else{
			// 	tf.Rotate(0, 0, (tf.localEulerAngles.z > 0f ? -1 : 1), Space.Self);
			// }

		}
		else if(Mathf.Abs(rb.angularVelocity.y + torque) < maxTorqueY){ // turn if left/right is being pressed
			rb.AddTorque(new Vector3(0, Mathf.Min(torque*2f, maxTorqueY-torque*2f), 0));

			// // Make it bank visually
			// if(Mathf.Abs(tf.localEulerAngles.z) < 30f){
			// 	tf.Rotate(0, 0, (torque > 0 ? torque/2f : -torque/2f), Space.Self);
			// 	Debug.Log("Torque: " + torque);
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
