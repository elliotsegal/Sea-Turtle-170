using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float maxVelocity = 10;
	private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
		rb = GetComponent<Rigidbody>();
    }

	// called just before performing any Physics operations
	void FixedUpdate(){
		float moveHorizontal = speed * Input.GetAxis("Horizontal");
		float moveVertical = speed * Input.GetAxis("Vertical");

		// Very inelegant but it should do the job
		if(Mathf.Abs(rb.velocity.x + moveHorizontal) > maxVelocity){
			moveHorizontal = Input.GetAxis("Horizontal") * (maxVelocity - Mathf.Abs(rb.velocity.x));
		}

		if(Mathf.Abs(rb.velocity.z + moveVertical) > maxVelocity){
			moveVertical = Input.GetAxis("Vertical") * (maxVelocity - Mathf.Abs(rb.velocity.z));
		}

		rb.AddForce(new Vector3(moveHorizontal, 0, moveVertical));
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
