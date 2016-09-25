using UnityEngine;

public enum EffectType {
	USE_INSTANTLY,
	USE_ON_TRIGGER
}

public abstract class AbstractEffect {
	public abstract EffectType Type{ get; }

	public abstract void apply(UnitStats stats, GameObject player);
}
