﻿using UnityEngine;
using System.Collections;

public class SprintEffect : StatEffect {
	public override EffectType Type {
		get {
			return EffectType.USE_ON_TRIGGER;
		}
	}

	protected override void applyStatChange(UnitStats stats){
		stats.speed *= 2.0f;
	}
}
