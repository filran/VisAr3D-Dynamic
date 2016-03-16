using UnityEngine;
using System.Collections;
using Core;

public class ContentDiagram : MonoBehaviour {

    public GameObject Class;

    private TheCore Core { get; set; }
    private ClassDiagram TheClassDidagram { get; set; }
    private SequenceDiagram TheSequenceDiagram { get; set; }

    private float x = 0;
    private float y = 0;
    private float z = 0;
    private float dist_x = 1.5f;
    private float dist_y = .5f;
    private float dist_z= .5f;

    // Use this for initialization
    void Start () {
        Debug.Log("IdDiagram: "+PlayerPrefs.GetString("IdDiagram"));

        Core = new TheCore(PlayerPrefs.GetString("XMIFile"));

        IXmlNode diagram = Core.FindById(Core.AllDiagrams , PlayerPrefs.GetString("IdDiagram"));

        if( diagram.Id != null)
        {
            //Debug.Log("diagram.Type:"+ diagram.Type);
            switch(diagram.Type)
            {
                case "Logical":
                    //Debug.Log("Diagram de classe "+diagram.Name+" id:"+diagram.Id);
                    BuildClassDiagram(diagram);
                    break;
            }
        }
    
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void BuildClassDiagram(IXmlNode diagram)
    {
        Package package = (Package)Core.FindById(Core.Packages,diagram.IdPackage);
        if(package.Id != null)
        {
            //Debug.Log("Achei o pacote");
            ClassDiagram classdiagram = (ClassDiagram)package.FindById(package.ClassDiagrams,diagram.Id);
            if(classdiagram.Id != null)
            {
                //print("Achei o diagrama");
                foreach(Class c in classdiagram.SoftwareEntities)
                {
                    GameObject go_class = (GameObject)Instantiate(Class,new Vector3(x,0,0),Quaternion.identity);
                    Transform text = go_class.transform.FindChild("classname");

                    text.GetComponent<TextMesh>().text = c.Name;

                    x += dist_x;
                }
            }
        }
    }
}
