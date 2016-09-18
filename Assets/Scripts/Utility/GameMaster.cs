using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameMaster : MonoBehaviour {

	public static GameMaster GM;

	public List<GameObject> prefabPlayers;
	public List<GameObject> prefabEnemys;
	public List<Weapon> prefabWeapons;
	public GameObject prefabBackground;
	public GameObject prefabBullets;
	public Text UIHPText;
	public GameObject playerObj;
	GameObject enemyObj;
	GameObject weaponObj;
	GameObject background;
	GameObject bullets;

	public static void DeactivateObject(GameObject obj){
		// test description
		obj.SetActive (false);
	}

	public static void ActivateObject(GameObject obj){
		obj.SetActive (true);
	}

	public static void DeactivateCollider(Collider2D obj){
		obj.enabled = false;
	}

	void Awake(){
		if (GM != null)
			GameObject.Destroy (GM);
		else
			GM = this;
	}

	void Start(){ checkComponents (); instantiateObjects (); setupCamera (); }

	void FixedUpdate(){ displayUI (); }

	public void CreateBullet(Vector3 position){
		bullets = Instantiate (prefabBullets, position, Quaternion.identity, this.transform) as GameObject;
	}

	/*void cameraFollow()
	{
		Application.targetFrameRate = 25;
		if (playerObj != null) {
			Camera.main.transform.position = playerObj.transform.position - new Vector3 (0, 0, 10);

			if (Input.GetKey (KeyCode.KeypadPlus)) {
				transform.eulerAngles += new Vector3 (0, 1, 0);
			}
		}
	}*/

	void displayUI ()
	{
		UIHPText.text = "HP: " + playerObj.GetComponent<PlayerBehaviour>().stats.hp.ToString () + "\nRunes: " + "0/0/0/0";
	}

	void setupCamera(){
		Debug.Log ("Setting up camera.");
		Camera maincam = Camera.main;
		maincam.gameObject.AddComponent<CameraControl> ();
		maincam.gameObject.GetComponent<CameraControl> ().target = playerObj.transform;
	}

	void checkComponents()
	{
		if (prefabPlayers.Count == 0) {
			Debug.LogError ("Player prefabs are not set.");
		} else if (prefabEnemys.Count == 0) {
			Debug.LogError ("Enemy prefabs are not set.");
		} else if (prefabWeapons.Count == 0) {
			Debug.LogError ("Weapon prefabs are not set.");
		} else if (UIHPText == null) {
			Debug.LogError ("UI Text references are not set.");
		} else if (prefabBackground == null) {
			Debug.LogError ("Background reference is not set.");
		} else if (prefabBullets == null) {
			Debug.LogError ("Bullet prefab is not set.");
		}

	}

	void instantiateObjects()
	{
		Debug.Log ("CheckComponents() successful. Instantiating objects.");
		playerObj = Instantiate(prefabPlayers[0], Vector3.zero, Quaternion.identity, GameObject.Find("Units").transform) as GameObject;
		playerObj.GetComponent<PlayerBehaviour> ().UIText = UIHPText;
		Debug.Log ("Instantiated player instance.");
		for (int i = 0; i < 1; i++) {
			Vector2 pos = Random.insideUnitCircle;
			Vector3 spawnPos = new Vector3 (pos.x, pos.y, 0);
			enemyObj = Instantiate (prefabEnemys [0], spawnPos, Quaternion.identity, GameObject.Find("Units").transform) as GameObject;
			Debug.Log ("Instantiated enemy instance.");
		}
		background = Instantiate (prefabBackground, Vector3.zero, Quaternion.identity) as GameObject;
		Debug.Log ("Instantiated background.");
	}
}
