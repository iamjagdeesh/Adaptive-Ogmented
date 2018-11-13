using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CapsuleController : MonoBehaviour {

	public GameObject stage2PickUp;
	public GameObject stage2;
	public float speed;
	public GameObject player;
	private Rigidbody rb;
	public Text titleText;
	public Text hintText;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		titleText.text = "Setter method topic";
		titleText.gameObject.SetActive (true);
		hintText.gameObject.SetActive (true);
	}

	// Update is called once per frame
	void FixedUpdate () {
		keyboardMovement();
		// mobileMovement();
	}

	void mobileMovement() {
		if (!player.activeSelf) {
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
	}

	void keyboardMovement() {
		//		float moveHorizontal = Input.GetAxis ("Horizontal");
		//		float moveVertical = Input.GetAxis ("Vertical");
		//
		//		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		//		rb.AddForce (movement * speed);
		if (!player.activeSelf) {
			if (Input.GetKey (KeyCode.UpArrow)) {
				transform.Translate (Vector3.up * Time.deltaTime * speed);
			}
			if (Input.GetKey (KeyCode.DownArrow)) {
				transform.Translate (Vector3.down * Time.deltaTime * speed);
			}
			if (Input.GetKey (KeyCode.LeftArrow)) {
				transform.Translate (Vector3.left * Time.deltaTime * speed);
			}
			if (Input.GetKey (KeyCode.RightArrow)) {
				transform.Translate (Vector3.right * Time.deltaTime * speed);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		switch (other.gameObject.name) {
		case "Stage2PickUp":
			other.gameObject.SetActive (false);
			stage2.gameObject.SetActive (true);
			hintText.text = "Hint: Choose a color for setter";
			break;
		case "GreenColor":
			stage2.gameObject.SetActive (false);
			ChangeColor ("Green");
			hintText.text = "Congratulations! Level 1 complete.";
			break;
		case "RedColor":
			stage2.gameObject.SetActive (false);
			ChangeColor ("Red");
			hintText.text = "Congratulations! Level 1 complete.";
			break;
		case "YellowColor":
			stage2.gameObject.SetActive (false);
			ChangeColor ("Yellow");
			hintText.text = "Congratulations! Level 1 complete.";
			break;
		}
	}

	void ChangeColor(string color) {
		Color whateverColor;
		if (color == "Green") {
			whateverColor = Color.green;
		} else if (color == "Red") {
			whateverColor = Color.red;
		} else {
			whateverColor = Color.yellow;
		}

		Material newMaterial = new Material(Shader.Find("Standard"));
		newMaterial.color = whateverColor;

		GameObject player = rb.gameObject;
		MeshRenderer gameObjectRenderer = player.GetComponent<MeshRenderer>();
		gameObjectRenderer.material = newMaterial ;
	}

}
