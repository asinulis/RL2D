using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Abstract class for Players, Enemies and Companians / Orbitals.
 * Initializes the shared objects, i.e. RB2D, Animator, Transform, ... of a RLObject, along with specific variables such as the Gun and Barrel object and the weapon script.
*/

public abstract class Unit : RLObject {
	public Animator animator { get; private set; }
	public Rigidbody2D rb { get; private set; }
	public Transform trans { get; private set; }
	public UnitStats stats{ get; private set; }
	public GameObject gun { get; private set; }
	public GameObject barrel { get; private set; }
	public Weapon mainWeapon { get; private set; }

	public override void Start()
	{
		try{
			base.Start ();

			animator = GetComponent<Animator>();
			rb = GetComponent<Rigidbody2D> ();
			trans = GetComponent<Transform> ();
			stats = new UnitStats (this.gameObject, new List<AbstractEffect>{new SprintEffect()});
			gun = gameObject.transform.FindChild ("Gun").gameObject;
			mainWeapon = gun.transform.GetComponent<Weapon> ();
			barrel = gun.transform.FindChild ("Barrel").gameObject;
			setUnitStats (stats);
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
			} else if (gun == null) {
				throw new InitializationException ("Unit: Missing Gun child during initialization of " + gameObject.name);
			} else if (barrel == null) {
				throw new InitializationException ("Unit: Missing Barrel child of Gun during initialization of " + gameObject.name);
			} else if (mainWeapon == null) {
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

	public virtual void OnTriggerEnter2D(Collider2D other){
		GameMaster.GM.handleCollision (this, other);
	}
}
