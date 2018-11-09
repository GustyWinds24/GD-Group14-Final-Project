using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RCarrierAnimationListener : MonoBehaviour {

	RygelianCarrier parent;

	private void Awake() {
		parent = GetComponentInParent<RygelianCarrier>();
	}

	public void pukeFinished() {
		parent.pukeFinished();
	}

	public void deathFinished() {
		parent.deathFinished();
	}
}
