using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core;

public class ChangeScene : MonoBehaviour {

    public string IdPackage { get; set; }
    public string IdDiagram { get; set; }
    public bool GoToPackageOpened = false;
    public bool GoToDiagramOpened = false;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        PlayerPrefs.SetString("IdPackage", IdPackage);
        PlayerPrefs.SetString("IdDiagram", IdDiagram);

        if (GoToPackageOpened)
        {
            Application.LoadLevel("PackageOpened");
        }

        if(GoToDiagramOpened)
        {
            Application.LoadLevel("DiagramOpened");
        }
    }
}
