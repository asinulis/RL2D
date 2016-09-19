using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class GameMaster : MonoBehaviour {
	public static GameMaster GM;

	// (Lists for) prefabs
	List<GameObject> prefabPlayers;
	List<GameObject> prefabEnemys;
	List<GameObject> prefabWeapons;
	GameObject prefabBackground;
	GameObject prefabBullets;
	GameObject prefabUI;

	// Player
	public List<GameObject> playerObj;
	public Player playerScript;

	// GameObjects for various objects that are instantiated
	public GameObject UnitFolder;
	GameObject enemyObj;
	GameObject weaponObj;
	GameObject background;
	GameObject bullets;
	GameObject UI;
	GameObject Audio;

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
		DontDestroyOnLoad (this);
		if (GM != null)
			GameObject.Destroy (GM);
		else
			GM = this;

		prefabPlayers = new List<GameObject> ();
		prefabEnemys = new List<GameObject> ();
		prefabWeapons = new List<GameObject> ();

		if (GameObject.Find ("Units") == null) {
			UnitFolder = new GameObject ();
			UnitFolder.name = "Units";
		} else {
			UnitFolder = GameObject.Find("Units");
		}

		Debug.Log ("GM: Loading resources"); 		loadResources (); 			Debug.Log ("GM: Resources loaded.");
		Debug.Log ("GM: Checking components.");		checkComponents (); 		Debug.Log ("GM: Components checked.");
		Debug.Log ("GM: Instantiating objects.");	instantiateObjects ();		Debug.Log ("GM: Objects instantiated.");
		Debug.Log ("GM: Setting up camera.");		setupCamera (playerObj[0]); Debug.Log ("GM: Camera set up.");

		playerScript = playerObj[0].GetComponent<Player> ();
		playerScript.initialize ();
		Debug.Log ("GM: Finished starting process.");
	}

	void FixedUpdate(){ displayUI (); foreach (GameObject player in playerObj) {checkPlayerStatus (player); handleKeyInputs (player);} }

	void loadResources(){
		prefabBackground = Resources.Load<GameObject> ("Background");
		prefabPlayers.Add(Resources.Load<GameObject>("Player"));
		prefabEnemys.Add(Resources.Load<GameObject>("Enemy"));
		prefabWeapons.Add (Resources.Load<GameObject>("Weapon"));
		prefabBullets = Resources.Load<GameObject> ("Bullet");
		prefabUI = Resources.Load<GameObject> ("UI");
	}

	void setupCamera(GameObject follow){
		Camera maincam = Camera.main;
		maincam.gameObject.AddComponent<CameraControl> ();
		maincam.gameObject.GetComponent<CameraControl> ().target = follow.transform;
	}

	void checkComponents(){
		if (prefabPlayers.Count == 0) {
			throw new InitializationException("GM: Player prefabs are not set.");
		} else if (prefabEnemys.Count == 0) {
			throw new InitializationException("GM: Enemy prefabs are not set.");
		} else if (prefabWeapons.Count == 0) {
			throw new InitializationException("GM: Weapon prefabs are not set.");
		} else if (prefabUI == null) {
			throw new InitializationException("GM: UI prefab is not set.");
		} else if (prefabBackground == null) {
			throw new InitializationException("GM: Background prefab are not set.");
		} else if (prefabBullets == null) {
			throw new InitializationException("GM: Bullet prefabs are not set.");
		}
	}

	void instantiateObjects(){
		background = Instantiate (prefabBackground, Vector3.zero, Quaternion.identity) as GameObject;
		/*this.gameObject.AddComponent<AudioSource> ();
		this.gameObject.GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("dark fallout");
		gameObject.GetComponent<AudioSource> ().Play ();*/
		UI = Instantiate (prefabUI);
		UI.name = "UI";
		playerObj.Add(Instantiate(prefabPlayers[0], Vector3.zero, Quaternion.identity, UnitFolder.transform) as GameObject);
		playerObj [0].name = "Player 1";
		//playerObj.Add(Instantiate(prefabPlayers[0], Vector3.zero, Quaternion.identity, GameObject.Find("Units").transform) as GameObject); playerObj [1].name = "Player 2";
		for (int i = 0; i < 2; i++) {
			Vector2 pos = Random.insideUnitCircle;
			Vector3 spawnPos = new Vector3 (pos.x+Mathf.Min(i,10), pos.y+Mathf.Min(i,10), 0);
			enemyObj = Instantiate (prefabEnemys [0], spawnPos, Quaternion.identity, UnitFolder.transform) as GameObject;
			enemyObj.name = "Enemy" + i.ToString();
		}
	}

	void checkPlayerStatus(GameObject player){
		if (player.GetComponent<Player>().stats.hp <= 0) {
			Debug.LogError ("You are dead.");
			GameMaster.DeactivateObject (player);
			Application.CancelQuit ();
		}
	}

	void displayUI (){
		UI.transform.FindChild ("UIHPText").GetComponent<Text> ().text = "HP: " + playerScript.stats.hp.ToString ();//playerObj[0].GetComponent<Player>().stats.hp.ToString () + "\nRunes: " + "0/0/0/0";
	}

	void handleKeyInputs(GameObject player){
		KeyMap keymap = player.GetComponent<Player> ().keymap;
		Transform gunTransform = player.transform.FindChild ("Gun").transform;
		SpriteRenderer gunRenderer = player.transform.FindChild ("Gun").GetComponent<SpriteRenderer> ();
		if (Input.GetKey (keymap.shiftKey)) {
			player.GetComponent<Player>().stats.triggerEffect ();
		} else {
			player.GetComponent<Player>().stats.untriggerEffect ();
		}

		float vertical = 0;
		float horizontal = 0;

		Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
		pos.z = 0;
		float angle = Mathf.Atan2 (pos.x, pos.y) * Mathf.Rad2Deg;

		if (-135 <= angle && angle < -45) {
			playerScript.playAnimation ("PlayerMovementWest");
			gunTransform.position = player.transform.position + new Vector3 (-0.215f, -0.207f);
			gunRenderer.flipX = true;
			gunRenderer.flipY = false;
			gunRenderer.sprite = Resources.Load<Sprite> ("MGlr");
		} else if (-45 <= angle && angle < 45) {
			playerScript.playAnimation ("PlayerMovementNorth");
			gunTransform.position = player.transform.position + new Vector3 (0.096f, -0.056f);
			gunRenderer.flipX = false;
			gunRenderer.flipY = true;
			gunRenderer.sprite = Resources.Load<Sprite> ("MGud");
		} else if (45 <= angle && angle < 135) {
			playerScript.playAnimation ("PlayerMovementEast");
			gunTransform.position = player.transform.position + new Vector3 (0.166f, -0.194f);
			gunRenderer.flipX = false;
			gunRenderer.flipY = false;
			gunRenderer.sprite = Resources.Load<Sprite> ("MGlr");
		} else { 
			playerScript.playAnimation ("PlayerMovementSouth");
			gunTransform.position = player.transform.position + new Vector3 (-0.099f, -0.272f);
			gunRenderer.flipX = false;
			gunRenderer.flipY = true;
			gunRenderer.sprite = Resources.Load<Sprite> ("MGud");
		}

		if (Input.GetKey (keymap.moveDown)) vertical = -1;
		else if (Input.GetKey (keymap.moveUp)) vertical = +1;

		if (Input.GetKey (keymap.moveLeft)) horizontal = -1;
		else if (Input.GetKey (keymap.moveRight)) horizontal = +1;

		if (vertical == 0 && horizontal == 0) playerScript.setAnimatorSpeed (0);
		else playerScript.setAnimatorSpeed(playerScript.stats.speed / UnitStats.BASE_SPEED);

		Vector3 direction = new Vector3 (Math.Sign (horizontal), Math.Sign (vertical), 0f); 
		direction *= Time.deltaTime * playerScript.stats.speed;
		player.GetComponent<Player> ().moveBy (direction);

		if(Time.time > playerScript.nextShot)
		{
			if (Input.GetMouseButton(0)) {
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (player, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, pos.normalized);
			}
			/*
			if (Input.GetKey (keymap.shootUp)) {
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (player, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.up);
			} else if (Input.GetKey (keymap.shootDown)) {
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (player, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.down);
			} else if (Input.GetKey (keymap.shootLeft)) {
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (player, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.left);
			} else if (Input.GetKey (keymap.shootRight)) {
				playerScript.nextShot = Time.time + (1 / playerScript.stats.attack_rate);
				playerScript.mainWeapon.Shoot (player, Bullet.BulletType.BULLET_PLAYER, Bullet.BulletElement.BULLET_WATER, Vector2.right);
			}
			*/
		}
	}

	public GameObject createBullet(Vector3 position){
		bullets = Instantiate (prefabBullets, position, Quaternion.identity, this.transform) as GameObject;//shooter.transform.FindChild("Gun").transform) as GameObject;
		bullets.name = "Bullet";
		return bullets;	
	}

	public Vector3 getPlayerPosition(string name){
		return playerObj[0].transform.position;
	}

	public void handleCollision(Unit obj, Collider2D coll){
		if (coll.gameObject.tag == "Bullet"  && coll.transform.GetComponent<Bullet>().bulletType == Bullet.BulletType.BULLET_PLAYER && obj.GetType() == typeof(Enemy) ) {
			Bullet bull = coll.GetComponent<Bullet>();
			obj.changeHP (-bull.damage);
			GameObject.Destroy (coll.gameObject);
			Debug.Log (obj.gameObject.name + " has been hit by " + bull.shooter + " and has taken " + bull.damage + " damage.");
		}
		else if (coll.gameObject.tag == "Bullet"  && coll.transform.GetComponent<Bullet>().bulletType == Bullet.BulletType.BULLET_ENEMY && obj.GetType() == typeof(Player)) {
			Bullet bull = coll.GetComponent<Bullet>();
			obj.changeHP(-bull.damage);
			GameObject.Destroy (coll.gameObject);
			Debug.Log (obj.gameObject.name + " has been hit by " + bull.shooter + " and has taken " + bull.damage + " damage.");
		}
	}
}