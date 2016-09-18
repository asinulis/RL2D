using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public enum BulletType { BULLET_ENEMY, BULLET_PLAYER, BULLET_NEUTRAL };
	public enum BulletElement { BULLET_EARTH, BULLET_WATER, BULLET_AIR, BULLET_FIRE};

	public float lifetime;
	public BulletType bulletType;
	public BulletElement bulletElement;

	void Start() { lifetime = 8f + Random.value; }

	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			GameMaster.Destroy (gameObject.GetComponent<Collider2D> ());
			GameMaster.Destroy (gameObject.GetComponent<Animator> ());
			//this.gameObject.GetComponent<SpriteRenderer>().sprite = 
		}
	}
}
