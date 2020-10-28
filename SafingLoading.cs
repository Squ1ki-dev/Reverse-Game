using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SafingLoading : MonoBehaviour 
{
	
	public Slider slider;
	public int Level;

	private int slidV;
	private float timer;

	void Start () {
		
	}
	
	void Update () {
		slider.value = slidV;
		timer += 2  * Time.deltaTime;
		if (timer >= 1) {
			slidV += 7;
			timer = 0;
		}
		if (slider.value >= 100) {
			SceneManager.LoadScene (Level);
		}
	}
}
