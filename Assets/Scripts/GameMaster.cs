using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
using Random = UnityEngine.Random;


/*
 * GameMaster (with static member GM) is responsible for preloading and instantiating most objects. 
 * 1) Loading all prefabs for players, enemies, weapons, the background, ...
 * 2) Handles the user interface.
 * 3) Creating objects such as player, enemies, and bullets.
 * 4) Setting up the camera control script.
 * 
 * 
 * 
*/

public enum Elements { ELEMENT_EARTH, ELEMENT_WATER, ELEMENT_AIR, ELEMENT_FIRE};
static class BulletElementMethods {
	public static string giveName(this Elements elem)
	{
		switch (elem) {
		case Elements.ELEMENT_AIR:
			return "Air";
		case Elements.ELEMENT_EARTH:
			return "Earth";
		case Elements.ELEMENT_FIRE:
			return "Fire";
		case Elements.ELEMENT_WATER:
			return "Water";
		default:
			return "Error";
		}
	}
}

public class GameMaster : MonoBehaviour {
	public static GameMaster GM;
	public static bool logMessages = false;

	// (Lists for) prefabs
	List<GameObject> prefabPlayers;
	List<GameObject> prefabEnemys;
	List<GameObject> prefabWeapons;
	GameObject prefabBackground;
	GameObject prefabBullets;
	GameObject prefabUI;
	GameObject UnitHolder;
	Sprite[] headsprites;

	public int noOfPlayers;
	public int noOfEnemies;

	// Player
	List<GameObject> playerObj;			
	Player playerScript;

	// GameObjects for various objects that are instantiated
	List<GameObject> enemyObj;		
	GameObject weaponObj;
	GameObject background;
	GameObject bullets;
	GameObject UI;
	GameObject Audio;

	public static void LogMsg(string message, string methodName = null)
	{
		Debug.Log(Time.time.ToString() + "(" + methodName + "): " + message);
	}

	void Awake(){
		try{
		DontDestroyOnLoad (this);
		GM = this;

		prefabPlayers = new List<GameObject> ();
		prefabEnemys = new List<GameObject> ();
		prefabWeapons = new List<GameObject> ();

		UnitHolder = GameObject.Find ("Units");
		if (UnitHolder == null)		UnitHolder = new GameObject ("Units");

		if(logMessages) Debug.Log ("GM: Loading resources"); 		
		loadResources (); 			
		if(logMessages) Debug.Log ("GM: Resources loaded.");
		if(logMessages) Debug.Log ("GM: Instantiating objects.");	
		instantiateObjects ();		
		if(logMessages) Debug.Log ("GM: Objects instantiated.");

		if (playerObj.Count > 0) {
			if(logMessages) Debug.Log ("GM: Setting up camera.");
			setupCamera (playerObj [0]);
			if(logMessages) Debug.Log ("GM: Camera set up.");
			playerScript = playerObj[0].GetComponent<Player> ();
		}
		else { UI.SetActive(false); }
			
		if(logMessages) Debug.Log ("GM: Finished starting process.");
		}
		catch(System.NullReferenceException) {
			if (GM == null)
				throw new InitializationException ("GM: GM has no instance.");
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
	}

	void FixedUpdate(){ 
		if (playerObj != null) {
			displayUI (); 
			foreach (GameObject player in playerObj) {
				checkPlayerStatus (player);
				handleKeyInputs (player);
			}
		}
	}

	internal void loadResources(){
		prefabBackground = Resources.Load<GameObject> ("Background");
		prefabPlayers.Add(Resources.Load<GameObject>("Player"));
		prefabEnemys.Add(Resources.Load<GameObject>("Enemy"));
		prefabWeapons.Add (Resources.Load<GameObject>("Weapon"));
		prefabBullets = Resources.Load<GameObject> ("Bullet");
		prefabUI = Resources.Load<GameObject> ("UI");
		headsprites = Resources.LoadAll<Sprite> ("Shami_head");
	}

	internal void setupCamera(GameObject follow){
		Camera maincam = Camera.main;
		maincam.gameObject.AddComponent<CameraControl> ();
		maincam.gameObject.GetComponent<CameraControl> ().target = follow.transform;
	}

	internal void instantiateObjects(){
		playerObj = new List<GameObject>();
		enemyObj = new List<GameObject> ();
		background = Instantiate (prefabBackground, Vector3.zero, Quaternion.identity) as GameObject;
		background.name = "Background and boundary";
		this.gameObject.AddComponent<AudioSource> ();
		this.gameObject.GetComponent<AudioSource> ().clip = Resources.Load<AudioClip> ("dark fallout");
		gameObject.GetComponent<AudioSource> ().Play ();
		UI = Instantiate (prefabUI);
		UI.name = "UI";
		for (int i = 0; i < noOfPlayers; i++) {
			playerObj.Add (Instantiate (prefabPlayers [0], new Vector3(-5,-5,0), Quaternion.identity) as GameObject);
			playerObj [i].name = "Player" + (i + 1).ToString ();
		}
		for (int i = 0; i < noOfEnemies; i++) {
			Vector2 pos = Random.insideUnitCircle;
			Vector3 spawnPos = new Vector3 (pos.x+Mathf.Min(i,10), pos.y+Mathf.Min(i,10), 0);
			enemyObj.Add(Instantiate (prefabEnemys [0], pos, Quaternion.identity, UnitHolder.transform) as GameObject);
			enemyObj[i].name = "Enemy" + (i+1).ToString();
		}
	}

	internal void checkPlayerStatus(GameObject player){
		if (player.GetComponent<Player>().stats.hp <= 0) {
			UI.transform.FindChild ("Dead").gameObject.SetActive (true);
			player.gameObject.SetActive (false);
			Application.Quit ();
		}
	}

	internal void displayUI (){
		Unit player = playerObj [0].gameObject.GetComponent<Unit> ();
		int damage;
		damage = player.stats.damage + player.mainWeapon.damage;
		UI.transform.FindChild ("UIHPText").GetComponent<Text> ().text = "HP: " + player.stats.hp.ToString ();
		UI.transform.FindChild ("UIWeapon").GetComponent<Text> ().text = "Weapon: " + player.mainWeapon.element.giveName () + "\nAmmunition: " + player.mainWeapon.ammunition + "\nDamage: " + damage; 
	}

	internal void handleKeyInputs(GameObject playerGO){
		GameObject staff	 		= null;
		KeyMap keymap 				= null;
		SpriteRenderer headSprite 	= null;
		SpriteRenderer gunSprite 	= null;

		try{
			Player player = playerGO.GetComponent<Player>();
			staff 		= player.mainWeapon.gameObject;
			keymap 		= player.keymap;
			headSprite 	= player.head.GetComponent<SpriteRenderer> ();
			gunSprite 	= player.staff.GetComponent<SpriteRenderer> ();

			if (Input.GetKey (keymap.shiftKey)) {
				player.GetComponent<Player>().stats.triggerEffect ();
			} else {
				player.GetComponent<Player>().stats.untriggerEffect ();
			}

			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				player.GetComponent<Player> ().mainWeapon.changeWeapon (false);
			}
			else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				player.GetComponent<Player> ().mainWeapon.changeWeapon (true);
			}

			float vertical = 0;
			float horizontal = 0;
			Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
			pos.z = 0;
			float angle = Mathf.Atan2 (pos.x, pos.y) * Mathf.Rad2Deg;

			staff.transform.eulerAngles = new Vector3 (0, 0, -angle);

			if (-180 <= angle && angle < -135)
				headSprite.sprite = headsprites [0];
			else if (-135 <= angle && angle < -90)
				headSprite.sprite = headsprites [1];
			else if (-90 <= angle && angle < -45)
				headSprite.sprite = headsprites [2];
			else if (-45 <= angle && angle < 0)
				headSprite.sprite = headsprites [3];
			else if (0 <= angle && angle < 45)
				headSprite.sprite = headsprites [4];
			else if (45 <= angle && angle < 90)
				headSprite.sprite = headsprites [5];
			else if (90 <= angle && angle < 135)
				headSprite.sprite = headsprites [6];
			else if (135 <= angle && angle < 180)
				headSprite.sprite = headsprites [7];
			
			if (-135 <= angle && angle < -45) {			playerScript.animator.Play ("PlayerMovementWest");
			} else if (-45 <= angle && angle < 45) {	playerScript.animator.Play ("PlayerMovementNorth");
			} else if (45 <= angle && angle < 135) {	playerScript.animator.Play ("PlayerMovementEast");
			} else { 									playerScript.animator.Play ("PlayerMovementSouth");		}

			if (Input.GetKey (keymap.moveDown)) vertical = -1;
			else if (Input.GetKey (keymap.moveUp)) vertical = +1;

			if (Input.GetKey (keymap.moveLeft)) horizontal = -1;
			else if (Input.GetKey (keymap.moveRight)) horizontal = +1;


			if (vertical == 0 && horizontal == 0)
				playerScript.animator.StartPlayback (); 
			else playerScript.animator.StopPlayback(); 

			Vector3 direction = new Vector3 (Math.Sign (horizontal), Math.Sign (vertical), 0f); 
			direction *= Time.deltaTime * playerScript.stats.speed;
			player.GetComponent<Player> ().moveBy (direction);

			if(Time.time > playerScript.nextShot && Input.GetMouseButton(0))
			{
				playerScript.setShotTime (Time.time + (1 / playerScript.stats.attackRate));
				playerScript.mainWeapon.Shoot (playerGO, Bullet.BulletType.BULLET_PLAYER, pos.normalized);
			}
		}
		catch(System.NullReferenceException) {
			if (staff == null)
				throw new InitializationException ("GM  (handleInputKeys): Staff transform is not available.");
		}
		catch(UnityEngine.MissingComponentException){
			if (keymap == null)
				throw new InitializationException ("GM (handleInputKeys): Keymap is not available.");
			else if (headSprite == null)
				throw new InitializationException ("GM  (handleInputKeys): Head sprite is not available on " + playerGO.name + ".");
			else if (gunSprite == null)
				throw new InitializationException ("GM  (handleInputKeys): Gun sprite is not available.");
		}
	}

	public GameObject createBullet(Vector3 position){
		bullets = Instantiate (prefabBullets, position, Quaternion.identity, this.transform) as GameObject;
		bullets.name = "Bullet";
		return bullets;	
	}

	public Vector3 getPlayerPosition(string name){
		if (playerObj.Count == 0)
			return Vector3.zero;

		foreach (GameObject player in playerObj) {
			if (player.name == name)
				return player.transform.position;
		}

		Debug.Log ("Player with name " + name + " cannot be found.");
		return Vector3.zero;
	}

	public void handleTriggerWithUnit(Unit unit, Collider2D coll){
		if (!coll.name.Contains ("Bullet")) 
		{
			LogMsg ("Resolving trigger-collision between " + unit.gameObject.name + " and " + coll.name, "handleCollision");
			if (coll.name.Contains ("Item") && unit.name.Contains ("Player")) {
				Debug.Log (unit.name + "picked up " + coll.name);
				if (coll.name == "Item 1") {
					//obj.stats.damage = 10000;
					//setUnitStats (stats); // stats.BASE_HP = 100000;
					Destroy (coll.gameObject);
				}
			}
		}
	}

	public void handleCollisionWithUnit(Unit unit, Collision2D other)
	{
		if (other.gameObject.tag == "Bullet") {
			Bullet bullet = other.gameObject.GetComponent<Bullet> ();
			if (bullet.bulletType == Bullet.BulletType.BULLET_PLAYER && unit.GetType () == typeof(Enemy)) {
				unit.changeHP (-bullet.damage);
				GameObject.Destroy (other.gameObject);
				if (logMessages)
					Debug.Log (unit.gameObject.name + " has been hit by " + bullet.shooter + " and has taken " + bullet.damage + " damage.");
			} else if (bullet.bulletType == Bullet.BulletType.BULLET_ENEMY && unit.GetType () == typeof(Player)) {
				unit.changeHP (-bullet.damage);
				GameObject.Destroy (other.gameObject);
				if (logMessages)
					Debug.Log (unit.gameObject.name + " has been hit by " + bullet.shooter + " and has taken " + bullet.damage + " damage.");
			}
		}
	}
}