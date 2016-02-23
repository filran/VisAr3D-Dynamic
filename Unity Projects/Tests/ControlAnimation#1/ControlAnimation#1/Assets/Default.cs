using UnityEngine;
using System.Collections;

public class Default : MonoBehaviour {

	public GameObject Cube;
	private bool CLICK = true;

	// Use this for initialization
	void Start () {
		createCube ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void animation()
	{
		if(CLICK == false)
		{
			this.transform.FindChild ("CUBE").GetComponent<Renderer> ().material.color = Color.white;
			CLICK = true;
		}
		else
		{
			this.transform.FindChild ("CUBE").GetComponent<Renderer> ().material.color = Color.red;
			CLICK = false;
		}


	}

	private void animationTWO()
	{

	}

	private void createCube()
	{
		GameObject cube = (GameObject)Instantiate (this.Cube, new Vector3 (0, 0, 0), Quaternion.identity);
		cube.name = "CUBE";
		cube.transform.parent = this.transform;
	}
}
