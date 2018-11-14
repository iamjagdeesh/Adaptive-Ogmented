using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public GameObject stage1;
	public GameObject stage2PickUp;
	public GameObject stage1CorrectOption;
	public GameObject stage1IncorrectOption1;
	public GameObject stage1IncorrectOption2;
	public Text titleText;
	public Text hintText;
	public Text userName;
	public Text userID;
	public Text performance;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		titleText.text = "Object Instantiation topic";
		hintText.text = "Hint: Pickup the object!";
		titleText.gameObject.SetActive (true);
		hintText.gameObject.SetActive (true);
		StaticGameInfo.currentTask = 1;

		//hack
		userName.text = StaticGameInfo.userName;
		userID.text = StaticGameInfo.userID;
		performance.text = StaticGameInfo.performance;
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
				stage1.SetActive (true);
				hintText.text = "Hint: Choose a shape";
				break;
			case "Stage1CorrectOption":
				stage2PickUp.SetActive (true);
				stage1IncorrectOption1.SetActive (false);
				stage1IncorrectOption2.SetActive (false);
				gameObject.SetActive (false);
				hintText.text = "Hint: Pickup the object!";
				break;
			case "Stage1IncorrectOption1":
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
				stage2PickUp.SetActive (true);
				stage1CorrectOption.SetActive (false);
				stage1IncorrectOption2.SetActive (false);
				gameObject.SetActive (false);
				hintText.text = "Hint: Pickup the object!";
				break;
			case "Stage1IncorrectOption2":
				SceneManager.LoadScene (SceneManager.GetActiveScene().name);
				stage2PickUp.SetActive (true);
				stage1CorrectOption.SetActive (false);
				stage1IncorrectOption1.SetActive (false);
				gameObject.SetActive (false);
				hintText.text = "Hint: Pickup the object!";
				break;
		}
	}

}