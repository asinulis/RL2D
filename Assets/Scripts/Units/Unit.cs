using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Abstract class for Players, Enemies and Companians / Orbitals.
 * Initializes the shared objects, i.e. RB2D, Animator, Transform, ... of a RLObject, along with specific variables such as the Gun and Barrel object and the weapon script.
*/

public abstract class Unit : RLObject {
	public Animator animator { get; protected set; }
	public Rigidbody2D rb { get; protected set; }
	public Transform trans { get; protected set; }
	public UnitStats stats{ get; protected set; }
	public GameObject staff { get; protected set; }
	public GameObject firePoint { get; protected set; }
	public Weapon mainWeapon { get; protected set; }

	public override void Start()
	{
		try{
			base.Start ();

			rb = GetComponent<Rigidbody2D> ();
			trans = GetComponent<Transform> ();
			animator = GetComponent<Animator> ();
			stats = new UnitStats (this.gameObject, new List<AbstractEffect>{new SprintEffect()});
			staff = gameObject.transform.FindChild ("Staff").gameObject;
			firePoint = staff.transform.FindChild ("Firepoint").gameObject;
			sprRenderer = GetComponent<SpriteRenderer>();
			mainWeapon = staff.transform.GetComponent<Weapon> ();
		}
		catch (System.NullReferenceException){
			if (animator == null) {
				throw new InitializationException ("Unit: Missing Animator component during initialization of " + gameObject.name);
			} else if (rb == null) {
				throw new InitializationException ("Unit: Missing Rigidbody2D component during initialization of " + gameObject.name);
			} else if (trans == null) {
				throw new InitializationException ("Unit: Missing transform component during initialization of " + gameObject.name);
			} else if (stats == null) {
				throw new InitializationException ("Unit: Missing Stats component during initialization of " + gameObject.name);
			} else if (staff == null) {
				throw new InitializationException ("Unit: Missing Staff child during initialization of " + gameObject.name);
			} else if (firePoint == null) {
				throw new InitializationException ("Unit: Missing Firepoint child of Staff during initialization of " + gameObject.name);
			}  else if (mainWeapon == null) {
				throw new InitializationException ("Unit: Missing Weapon component during initialization of " + gameObject.name);
			}
		}
	}

	protected void setUnitStats(UnitStats stats){
		this.stats = stats;
	}

	public void changeHP(int diff){
		stats.hp += diff;
	}

	void OnTriggerEnter2D(Collider2D other){
		base.OnTriggerEnter2D (other);
		GameMaster.GM.handleTriggerWithUnit (this, other);
	}

	void OnCollisionEnter2D(Collision2D other){
		GameMaster.GM.handleCollisionWithUnit (this, other);
	}
}
