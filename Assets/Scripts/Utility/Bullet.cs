using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public enum BulletType { BULLET_ENEMY, BULLET_PLAYER, BULLET_NEUTRAL };
	public enum BulletElement { BULLET_EARTH, BULLET_WATER, BULLET_AIR, BULLET_FIRE};

	public float lifetime;
	public BulletType bulletType;
	public BulletElement bulletElement;
	public int damage;
	public string shooter;
	public Vector2 destination;

	void Start() { lifetime = 1.5f + Random.value; }

	void Update () {
		lifetime -= Time.deltaTime;
		transform.position += new Vector3(destination.x * 2.5f * Time.deltaTime, destination.y * 2.5f * Time.deltaTime);
		if (lifetime <= 0)	leaveShellCasingOnGround ();
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Bullet collided with " + other.name);
		//if(!other.transform.parent.GetComponent<Unit>()) leaveShellCasingOnGround ();
	}

	void leaveShellCasingOnGround(){
		Vector3 currPosition = transform.position;
		GameObject shell = new GameObject (); shell.transform.position = currPosition; 
		shell.transform.parent = GameMaster.GM.transform; 
		shell.AddComponent<SpriteRenderer> (); 
		shell.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Rocket_TX");
		shell.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
		shell.GetComponent<SpriteRenderer> ().sortingOrder = -2;
		Destroy (this.gameObject);
	}
}
