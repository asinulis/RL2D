using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public enum BulletType { BULLET_ENEMY, BULLET_PLAYER, BULLET_NEUTRAL };

	public float lifetime;
	public BulletType bulletType;
	public Elements bulletElement;
	public int damage;
	public string shooter;
	public Vector2 destination;

	void Start() { lifetime = 1.5f + Random.value*0.3f; }

	void Update () {
		lifetime -= Time.deltaTime;
		transform.position += new Vector3(destination.x * 2.5f * Time.deltaTime, destination.y * 2.5f * Time.deltaTime);
		if (lifetime <= 0)	leaveShellCasingOnGround ();
	}

	void leaveShellCasingOnGround(){
		Vector3 currPosition = transform.position;
		GameObject shell = new GameObject ("Shell Casing"); 
		shell.SetActive (false);
		shell.transform.position = currPosition; 
		shell.transform.parent = GameMaster.GM.transform; 
		shell.AddComponent<SpriteRenderer> (); 
		shell.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Rocket_TX");
		shell.GetComponent<SpriteRenderer> ().sortingLayerName = "Foreground";
		shell.GetComponent<SpriteRenderer> ().sortingOrder = -2;
		shell.SetActive (true);
		Destroy (this.gameObject);
	}
}