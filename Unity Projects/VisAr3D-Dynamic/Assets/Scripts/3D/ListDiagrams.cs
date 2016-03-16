using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Core;

public class ListDiagrams : MonoBehaviour {

    public GameObject ClassDiagram;
    public GameObject SequenceDiagram;
    private Package package;

    private float x = 0;
    private float y = 0;
    private float z = 0;
    private float dist_x = .5f;
    private float dist_y = .5f;
    private float dist_z = .5f;

    // Use this for initialization
    void Start () {
        TheCore core = new TheCore(PlayerPrefs.GetString("XMIFile"));
        package = (Package)core.FindById(core.Packages, PlayerPrefs.GetString("IdPackage"));

        if( package.Id != null)
        {
            Debug.Log("Package found:"+ package.Id + ". Show diagrams!");

            if(package.ClassDiagrams.Count > 0)
            {
                GameObject group = new GameObject("Class Diagrams");

                foreach (ClassDiagram classdiagram in package.ClassDiagrams)
                {
                    GameObject diagram = (GameObject)Instantiate(ClassDiagram, new Vector3(x, y, z), Quaternion.identity);
                    diagram.transform.parent = group.transform;
                    diagram.name = classdiagram.Name;

                    diagram.AddComponent<ChangeScene>().GoToDiagramOpened = true;
                    diagram.AddComponent<ChangeScene>().IdDiagram = classdiagram.Id;

                    Transform text = diagram.transform.FindChild("name");
                    text.GetComponent<TextMesh>().text = classdiagram.Name;

                    x = ++dist_x;
                    y = ++dist_y;
                    z = ++dist_z;
                }
            }

            if(package.SequenceDiagrams.Count > 0)
            {
                GameObject group = new GameObject("Sequence Diagram");

                x = 0; y = -1.5f; z = 0;
                foreach (SequenceDiagram sequencediagram in package.SequenceDiagrams)
                {
                    GameObject diagram = (GameObject)Instantiate(SequenceDiagram, new Vector3(x, y, z), Quaternion.identity);
                    diagram.transform.parent = group.transform;
                    diagram.name = sequencediagram.Name;

                    diagram.AddComponent<ChangeScene>().GoToDiagramOpened = true;
                    diagram.AddComponent<ChangeScene>().IdDiagram = sequencediagram.Id;

                    Transform text = diagram.transform.FindChild("name");
                    text.GetComponent<TextMesh>().text = sequencediagram.Name;

                    x = ++dist_x;
                    y = --dist_y;
                    z = ++dist_z;
                }
            }

        }
        else
        {
            //Debug.Log("Package not found");
        }

        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
