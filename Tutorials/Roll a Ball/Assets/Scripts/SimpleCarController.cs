using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class SimpleCarController : MonoBehaviour {

	private float m_horizontalInput;
	private float m_verticalInput;
//	private float m_steerAngle;
//
//	public WheelCollider frontLeftW, frontRightW;
//	public WheelCollider rearLeftW, rearRightW;
//
//	public Transform frontLeftT, frontRightT;
//	public Transform rearLeftT, rearRightT;

//	public float maxSteerAngle = 30;
//	public float motorForce = 50;
//
//	public float moveforce=90, boostMultiplier=2;

	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;

	private Rigidbody rb;
	public GameObject stage1;
	public GameObject stage2;
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

//	public void GetInputKeyboard(){
//		m_horizontalInput = Input.GetAxis ("Horizontal");
//		m_verticalInput = Input.GetAxis ("Vertical");
//	}
//
//
//	// this is where I have to use joy stick.
//	public void GetInputMobile(){
//		m_horizontalInput = CrossPlatformInputManager.GetAxis ("Horizontal");
//		m_verticalInput = CrossPlatformInputManager.GetAxis ("Vertical");
//
//	}

	public void Update(){
		float translation = CrossPlatformInputManager.GetAxis ("Vertical") * speed;
		float rotation = CrossPlatformInputManager.GetAxis ("Horizontal") * rotationSpeed;
		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;
		transform.Translate (0, 0, translation);
		transform.Rotate (0, rotation, 0);
	}

//	private void Steer(){
//		m_steerAngle = maxSteerAngle * m_horizontalInput;
//		frontLeftW.steerAngle = m_steerAngle;
//		frontRightW.steerAngle = m_steerAngle;
//	}
//
//	private void Accelerate(){
//		rearLeftW.motorTorque = m_verticalInput * motorForce;
//		rearRightW.motorTorque = m_verticalInput * motorForce;
//		frontLeftW.motorTorque = m_verticalInput * motorForce;
//		frontRightW.motorTorque = m_verticalInput * motorForce;
//	}
//
//	private void UpdateWheelPoses(){
//		UpdateWheelPose (frontLeftW, frontLeftT);
//		UpdateWheelPose (frontRightW, frontRightT);
//		UpdateWheelPose (rearLeftW, rearLeftT);
//		UpdateWheelPose (rearRightW, rearRightT);
//	}
//
//	private void UpdateWheelPose(WheelCollider _collider, Transform _transform){
//		Vector3 _pos = _transform.position;
//		Quaternion _quat = _transform.rotation;
//
//		_collider.GetWorldPose (out _pos, out _quat);
//		_transform.position = _pos;
//		_transform.rotation = _quat;
//
//	}
//
//	private void FixedUpdate(){
//		// GetInputKeyboard ();
//		GetInputMobile ();
//		Steer ();
//		Accelerate ();
//		UpdateWheelPoses ();
//	}

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
		case "Stage2IncorrectOption1":
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			stage2PickUp.SetActive (true);
			stage1CorrectOption.SetActive (false);
			stage1IncorrectOption2.SetActive (false);
			gameObject.SetActive (false);
			hintText.text = "Hint: Pickup the object!";
			break;
		case "Stage1IncorrectOption2":
		case "Stage2IncorrectOption2":
			SceneManager.LoadScene (SceneManager.GetActiveScene().name);
			stage2PickUp.SetActive (true);
			stage1CorrectOption.SetActive (false);
			stage1IncorrectOption1.SetActive (false);
			gameObject.SetActive (false);
			hintText.text = "Hint: Pickup the object!";
			break;
		case "Task3CorrectOption1":
			stage2PickUp.SetActive (true);
			stage1.SetActive (false);
			break;
		case "Stage2PickUp":
			stage2PickUp.SetActive (false);
			stage2.SetActive (true);
			break;
		case "Task3CorrectOption2":
			stage2.SetActive (false);
			break;
		}
	}
}
