using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBehavior : MonoBehaviour {

	void Awake(){
		StartCoroutine (Disapear ());
	}

	IEnumerator Disapear(){
		yield return new WaitForSecondsRealtime (1f);
		Destroy (transform.gameObject);
		yield break;
	}
}
