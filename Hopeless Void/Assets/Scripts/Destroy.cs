﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {



	private BoxCollider2D box;
	private RectTransform rect;

	void Awake(){
		rect = GetComponent<RectTransform> ();
		box = GetComponent<BoxCollider2D> ();
	}

	public void Update(){
		box.size = new Vector2(rect.rect.width,rect.rect.height);
	}

	private bool pushed = false;

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			if (!pushed) {
				Pushed ();
				pushed = true;
			}
		} else if (pushed) {
			pushed = false;
		}
	}

	void Pushed(){
		SoundHandler.instance.playMusic ("debut" + Random.Range (0, 2));
		GUIHandler.instance.Intro ();
	}
}
// Update is called once per frame
