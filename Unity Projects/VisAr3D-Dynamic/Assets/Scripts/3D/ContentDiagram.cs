using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core;

public class ContentDiagram : MonoBehaviour {

    public GameObject Class;
    public Material lineMaterial;
    public string dirsourcecode;

    private TheCore Core { get; set; }
    private ClassDiagram TheClassDidagram { get; set; }
    private SequenceDiagram TheSequenceDiagram { get; set; }
    private Dictionary<Class,GameObject> Classes { get; set; } //key:class value:class like gameobject
                                                 //origin    destination
    private Dictionary< LineRenderer , Dictionary<GameObject,GameObject> > LineRenderes = new Dictionary<LineRenderer, Dictionary<GameObject, GameObject>>();

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
        updateLineRenderer();
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
                Classes = new Dictionary<Class, GameObject>();

                foreach(Class c in classdiagram.SoftwareEntities)
                {
                    GameObject go_class = (GameObject)Instantiate(Class,new Vector3(x,0,0),Quaternion.identity);
                    go_class.gameObject.name = c.Name;
                    go_class.AddComponent<OpenCode>();
                    Transform text = go_class.transform.FindChild("classname");
                    text.GetComponent<TextMesh>().text = c.Name;
                    text.name = c.Name;
                    Classes.Add(c,go_class);
                    x += dist_x;
                    //Debug.Log("Geometry: " + c.Name + " - " + c.Geometry + "\n:"+c.Left+" - "+c.Top+" - "+c.Right+" - "+c.Bottom);
                }

                //create relationships
                foreach(KeyValuePair<Class,GameObject> c in Classes)
                {
                    string s = c.Key.Name+"\n";
                    foreach( KeyValuePair<IXmlNode,IXmlNode> r in c.Key.Relationships )
                    {
                        s += "\t"+r.Value.Name;

                        //if(c.Key.Name.Equals("ClassA"))
                        //{
                        //LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                            GameObject line = new GameObject("Line Renderer");
                            line.name = c.Value.name ;
                            Dictionary<GameObject, GameObject> pairs = new Dictionary<GameObject, GameObject>();
                            pairs.Add(c.Value , FindClasses(r.Value));
                            LineRenderes.Add(line.AddComponent<LineRenderer>(), pairs );
                        //}
                    }
                    print(s);
                }
            }
        }
    }

    void updateLineRenderer()
    {
        foreach(KeyValuePair<LineRenderer, Dictionary<GameObject, GameObject>> l in LineRenderes)
        {
            foreach(KeyValuePair<GameObject,GameObject> g in l.Value)
            {
                l.Key.SetPosition(0 ,g.Key.transform.position);
                l.Key.SetPosition(1, g.Value.transform.position);
                l.Key.SetWidth(.25f, .25f);
                l.Key.material = lineMaterial;
                l.Key.SetColors(Color.green, Color.green);
            }
        }
    }

    GameObject FindClasses(IXmlNode c)
    {
        GameObject g = new GameObject("FindClasses");

        foreach(KeyValuePair<Class,GameObject> gg in Classes)
        {
            if(c.Equals(gg.Key))
            {
                g = gg.Value;
            }
        }
        return g;
    }
}
