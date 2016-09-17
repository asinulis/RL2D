using UnityEngine;
using System.Collections;

public class BulletLifetime : MonoBehaviour {

	public float lifetime;
	// Use this for initialization
	void Start () {
		lifetime = 10f;
	}
	
	// Update is called once per frame
	void Update () {
		lifetime -= Time.deltaTime;
		if (lifetime <= 0) {
			GameMaster.DeactivateObject(this.gameObject);
		}
	}
}
