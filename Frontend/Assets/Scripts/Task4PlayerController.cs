using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task4PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public GameObject cube;
	public GameObject capsule;
	public GameObject cylinder;
	public GameObject cubeArea;
	public GameObject capsuleArea;
	public GameObject cylinderArea;
	public Text titleText;
	public Text hintText;
	public Text userName;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		titleText.text = "Object Instantiation topic";
		hintText.text = "Hint: Pickup the object!";
		titleText.gameObject.SetActive (true);
		hintText.gameObject.SetActive (true);

		//hack
		userName.text = StaticGameInfo.userName;
	}

	void FixedUpdate() {
		// mobileMovement();
		keyboardMovement();
	}

	void keyboardMovement() {
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.AddForce (movement * speed);
	}

	void mobileMovement() {
		Vector3 dir = Vector3.zero;
		// we assume that device is held parallel to the ground
		// and Home button is in the right hand
		// remap device acceleration axis to game coordinates:
		//  1) XY plane of the device is mapped onto XZ plane
		//  2) rotated 90 degrees around Y axis
		dir.x = -Input.acceleration.y;
		dir.z = Input.acceleration.x;

		// clamp acceleration vector to unit sphere
		if (dir.sqrMagnitude > 1)
			dir.Normalize();

		// Make it move 10 meters per second instead of 10 meters per frame...
		dir *= Time.deltaTime;

		// Move object
		rb.transform.Translate(dir * speed);
		// rb.AddForce(dir * speed);
	}

	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.name) {
			case "Stage1PickUp":
				other.gameObject.SetActive (false);
				cube.gameObject.SetActive (true);
				cylinder.gameObject.SetActive (true);
				capsule.gameObject.SetActive (true);
				hintText.text = "Hint: Choose a shape";
				break;
			case "Cube":
				other.gameObject.SetActive (false);
				cubeArea.gameObject.SetActive (true);
				break;
			case "Cylinder":
				other.gameObject.SetActive (false);
				cylinderArea.gameObject.SetActive (true);
				break;
			case "Capsule":
				other.gameObject.SetActive (false);
				capsuleArea.gameObject.SetActive (true);
				break;
		}
	}

}