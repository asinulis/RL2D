using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class GameMaster : MonoBehaviour {
	public static GameMaster GM;

	public List<GameObject> prefabPlayers;
	public List<GameObject> prefabEnemys;
	public List<Weapon> prefabWeapons;
	public GameObject prefabBackground;
	public GameObject prefabBullets;
	public Text UIHPText;
	public GameObject playerObj;
	Player playerScript;
	GameObject enemyObj;
	GameObject weaponObj;
	GameObject background;
	GameObject bullets;
	KeyMap keymap;

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

	void Start(){ 
		//GameObject obj = Resources.Load<GameObject> ("Bullet");
		keymap = new KeyMap();
		checkComponents (); 
		instantiateObjects (); 
		setupCamera (); 
		playerScript = playerObj.GetComponent<Player> ();
		playerScript.initialize ();
		Debug.Log ("GameMaster has finished.");
	}

	void FixedUpdate(){ checkPlayerStatus (); displayUI (); handleKeyInputs (); }

	public GameObject createBullet(Vector3 position, Bullet.BulletType type, Bullet.BulletElement element){
		bullets = Instantiate (prefabBullets, position, Quaternion.identity, this.transform) as GameObject;
		return bullets;	
	}

	void displayUI ()
	{
		UIHPText.text = "HP: " + playerObj.GetComponent<Player>().stats.hp.ToString () + "\nRunes: " + "0/0/0/0";
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
		Debug.Log ("GameMaster successfully checked all components. Instantiating objects.");
		background = Instantiate (prefabBackground, Vector3.zero, Quaternion.identity) as GameObject;
		Debug.Log ("Instantiated background.");
		playerObj = Instantiate(prefabPlayers[0], Vector3.zero, Quaternion.identity, GameObject.Find("Units").transform) as GameObject;
		Debug.Log ("Instantiated player.");
		for (int i = 0; i < 1; i++) {
			Vector2 pos = Random.insideUnitCircle;
			Vector3 spawnPos = new Vector3 (pos.x+i, pos.y+i, 0);
			enemyObj = Instantiate (prefabEnemys [0], spawnPos, Quaternion.identity, GameObject.Find("Units").transform) as GameObject;
		}
		Debug.Log ("Instantiated enemies.");
	}

	void checkPlayerStatus(){
		if (playerObj.GetComponent<Player>().stats.hp <= 0) {
			Debug.Log ("You are dead.");
			GameMaster.DeactivateObject (playerObj);
		}
	}

	void handleKeyInputs(){
		if (Input.GetKey (keymap.shiftKey)) {
			playerObj.GetComponent<Player>().stats.triggerEffect ();
		} else {
			playerObj.GetComponent<Player>().stats.untriggerEffect ();
		}

		if (Time.time > playerScript.nextShot) {
			if (Input.GetKeyDown (keymap.shootUp)) {
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (playerObj, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.up);
			}
			else if(Input.GetKeyDown(keymap.shootDown)){
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (playerObj, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.down);
			}
			else if(Input.GetKeyDown(keymap.shootLeft)){
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (playerObj, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.left);
			}
			else if(Input.GetKeyDown(keymap.shootRight)){
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (playerObj, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.right);
			}
		}

		float vertical = 0;
		float horizontal = 0;

		if (Input.GetKey (keymap.moveDown))
			vertical = -1;
		else if (Input.GetKey (keymap.moveUp))
			vertical = +1;
		if (Input.GetKey (keymap.moveLeft))
			horizontal = -1;
		else if (Input.GetKey (keymap.moveRight))
			horizontal = +1;

		Vector3 direction = new Vector3 (Math.Sign (horizontal), Math.Sign (vertical), 0f); // Math.Sign(vertical));
		direction *= Time.deltaTime * playerScript.stats.speed;
		playerObj.GetComponent<Player> ().moveBy (direction);

		if (vertical == 0 && horizontal == 0) {
			playerScript.setAnimatorSpeed(0);
		} else {
			playerScript.setAnimatorSpeed(playerScript.stats.speed / UnitStats.BASE_SPEED);
			if (vertical > 0) {
				playerScript.playAnimation ("PlayerMovementNorth");
			} else if (vertical < 0) {
				playerScript.playAnimation ("PlayerMovementSouth");
			} else if (horizontal > 0) {
				playerScript.playAnimation ("PlayerMovementEast");
			} else if (horizontal < 0) {
				playerScript.playAnimation ("PlayerMovementWest");
			}
		}
	}
}