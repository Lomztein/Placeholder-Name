using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    public static Canvas mainCanvas;

	// Use this for initialization
	void Awake () {
        mainCanvas = GameObject.Find ("MainCanvas").GetComponent<Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
