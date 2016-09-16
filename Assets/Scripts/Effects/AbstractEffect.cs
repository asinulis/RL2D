using UnityEngine;

public abstract class AbstractEffect {
	public abstract EffectType Type{ get; }

	public abstract void apply(UnitStats stats, GameObject player);
}
