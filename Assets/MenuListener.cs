using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuListener : MonoBehaviour {

	public void changeDifficulty(string diff) {
		GameManager.instance.setDifficulty(diff);
	}
}
