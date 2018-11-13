using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	private Rigidbody rb;
	public GameObject stage1;
	public GameObject stage2;
	public GameObject stage2PickUp;
	public GameObject cube;
	public GameObject cylinder;
	public GameObject capsule;
	public Text titleText;
	public Text hintText;

	//hack
	public Text userName;
	public Text userID;
	public Text performance;


	void Start() {
		rb = GetComponent<Rigidbody> ();
		titleText.text = "Object Instantiation topic";
		hintText.text = "Hint: Pickup the object!";
		titleText.gameObject.SetActive (true);
		hintText.gameObject.SetActive (true);

		//hack
		Debug.Log("Static name: " + StaticGameInfo.userName);
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
			case "CubeObject":
				stage2PickUp.SetActive (true);
				capsule.SetActive (false);
				cylinder.SetActive (false);
				gameObject.SetActive (false);
				hintText.text = "Hint: Pickup the object!";
				break;
			case "CylinderObject":
				stage2PickUp.SetActive (true);
				cube.SetActive (false);
				capsule.SetActive (false);
				gameObject.SetActive (false);
				hintText.text = "Hint: Pickup the object!";
				break;
			case "CapsuleObject":
				stage2PickUp.SetActive (true);
				cube.SetActive (false);
				cylinder.SetActive (false);
				gameObject.SetActive (false);
				hintText.text = "Hint: Pickup the object!";
				break;
		}


	}

	void ChangeColor() {
		GameObject player = rb.gameObject;
		Color whateverColor = new Color(0.6f,0.5f,0.5f,1);

		MeshRenderer gameObjectRenderer = player.GetComponent<MeshRenderer>();

		Material newMaterial = new Material(Shader.Find("Standard"));

		newMaterial.color = whateverColor;
		gameObjectRenderer.material = newMaterial ;
	}
}