using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public enum BulletType { BULLET_ENEMY, BULLET_PLAYER, BULLET_NEUTRAL };
	public enum BulletElement { BULLET_EARTH, BULLET_WATER, BULLET_AIR, BULLET_FIRE};

	public float lifetime;
	public BulletType bulletType;
	public BulletElement bulletElement;
	public float damage;
	public string shooter;

	void Start() { lifetime = 8f + Random.value; }

	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0)	leaveShellCasingOnGround ();
	}

	void OnTriggerEnter2D(Collider2D other){
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
