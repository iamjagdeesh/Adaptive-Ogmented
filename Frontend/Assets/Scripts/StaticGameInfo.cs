using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public static class StaticGameInfo {

	public const string TASK_1 = "Task 1";
	public const string TASK_2 = "Task 2";
	public const string TASK_3 = "Task 3";
	public const string TASK_4 = "Task 4";

	public static string userName;
	public static int currentTask = 1;
	public static string currentScene = TASK_1;
	public static int speed = 10;

	public const string DEFAULT_HINT = "Pickup the object visible!";
	public const string HINT_T1_AFTER_S1_PICKUP = "Instantiate into an object of type Cube!";
	public const string HINT_T2_AFTER_S1_PICKUP = "Choose the base class for human evolution!";
	public const string HINT_T3_AFTER_S1_PICKUP = "A car has a _____";
	public const string HINT_T4_AFTER_S1_PICKUP = "Choose different objects to see how the same area method is called!";
	public const string HINT_T1_AFTER_S2_PICKUP = "Set the object's color attribute to Green!";
	public const string HINT_T2_AFTER_S2_PICKUP = "Choose the derived class for given object!";
	public const string HINT_T3_AFTER_S2_PICKUP = "A car has a _____";
	public const string LEVEL_COMPLETE = "Congratulations! Task completed.";

	public static string hint = "Pickup the object visible!";

	public static long timeTaken = 0;
	public static bool complete = false;
	public static int noOfWallCollisions = 0;

	public static string url = "http://localhost:8080/";

	public static Stopwatch stopWatch = null;

	public static void EndGame(bool status, GameObject exitObjects, GameObject arObjects) {
		UnityEngine.Debug.Log ("In EndGame: Current Scene: "+StaticGameInfo.currentScene + ", currentTaskNumber: "+StaticGameInfo.currentTask);
		StaticGameInfo.stopWatch.Stop ();
		long timeElapsed = (long) StaticGameInfo.stopWatch.ElapsedMilliseconds;
		StaticGameInfo.timeTaken = timeElapsed;
		StaticGameInfo.complete = status;
		writeToLog ();
		arObjects.SetActive (false);
		exitObjects.SetActive (true);

	}

	public static async void writeToLog () {
		HttpClient client = new HttpClient();
		UnityEngine.Debug.Log ("In writeLog: Current Scene: "+StaticGameInfo.currentScene + ", currentTaskNumber: "+StaticGameInfo.currentTask);
		HttpResponseMessage response = await client.PostAsJsonAsync(StaticGameInfo.url+"user/logs/addUserLog?" +
			"speed="+StaticGameInfo.speed+"&numberOfWallCollisions="+StaticGameInfo.noOfWallCollisions+"&taskNumber="+StaticGameInfo.currentTask
			+"&userId="+StaticGameInfo.userName+"&isSuccess="+StaticGameInfo.complete+"&timeTaken="+StaticGameInfo.timeTaken,new MultipartContent());
		response.EnsureSuccessStatusCode();
		await response.Content.ReadAsAsync<UnityEngine.Object>();
	}
//
//	public const Dictionary<int, string> levelToSceneDict = new Dictionary<int, string>() {
//		{1, "Task 1"},
//		{2, "Task 2"},
//		{3, "Task 3"},
//		{4, "Task 4"}
//	};
//
//	public const Dictionary<string, int> sceneToLevelDict = new Dictionary<int, string>() {
//		{"Task 1", 1},
//		{"Task 2", 2},
//		{"Task 3", 3},
//		{"Task 4", 4}
//	};
//
}