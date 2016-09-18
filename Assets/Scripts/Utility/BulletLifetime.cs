using UnityEngine;
using System.Collections;

public class BulletLifetime : MonoBehaviour {

	public float lifetime;

	void Start()
	{
		lifetime = 10f + Random.value;
	}

	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			GameMaster.Destroy (gameObject.GetComponent<Collider2D> ());
			GameMaster.Destroy (gameObject.GetComponent<Animator> ());
			//GameMaster.DeactivateObject(this.gameObject);
		}
	}
}
