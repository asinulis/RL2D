using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public UnitStats stats;
	public Weapon mainWeapon;

	// Use this for initialization
	void Start () {
		stats = new UnitStats (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 rnd0 = Random.insideUnitCircle;
		Vector3 rnd = new Vector3(rnd0.x, rnd0.y, 0);
		this.transform.position += rnd*0.1f;

		if (stats.hp <= 0) {
			GameMaster.DeactivateObject (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Bullet") {
			stats.hp -= 10;
			GameObject.Destroy (other.gameObject);
			//GameMaster.DeactivateCollider (other.gameObject.GetComponent<Collider2D> ());
			//GameMaster.DeactivateObject (other.gameObject);
			Debug.Log ("Enemy " + this.gameObject.name + " has been hit.");
		}
	}
}
