using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core;

public class ContentDiagram : MonoBehaviour {

    //Class
    public GameObject Class;
    public Material lineMaterial;

    //Sequence
    public GameObject Lifeline;
    public GameObject Message;

    public string dirsourcecode;

    private TheCore Core { get; set; }
    private ClassDiagram TheClassDiagram { get; set; }
    private SequenceDiagram TheSequenceDiagram { get; set; }

    //Classes
    private Dictionary<Class,GameObject> Classes { get; set; } //key:class value:class like gameobject
                                                 //origin    destination
    private Dictionary< LineRenderer , Dictionary<GameObject,GameObject> > LineRenderes = new Dictionary<LineRenderer, Dictionary<GameObject, GameObject>>();

    //Lifelines
    private Dictionary<Lifeline , GameObject> Lifelines { get; set; }

    private float x = 0;
    private float y = 0;
    private float z = 0;
    private float dist_x = 1.5f;
    private float dist_y = .5f;
    private float dist_z= .5f;

    // Use this for initialization
    void Start () {
        //Debug.Log("IdDiagram: "+PlayerPrefs.GetString("IdDiagram"));

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

                case "Sequence":
                    BuildSequenceDiagram(diagram);
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


    void BuildSequenceDiagram(IXmlNode diagram)
    {
        //Debug.Log(diagram.Type + " Diagram, Id:" + diagram.Id + ", name:" + diagram.Name);

        Package package = (Package)Core.FindById(Core.Packages, diagram.IdPackage);
        if (package.Id != null)
        {
            SequenceDiagram sequencediagram = (SequenceDiagram)package.FindById(package.SequenceDiagrams, diagram.Id);
            if (sequencediagram.Id != null)
            {
                Lifelines = new Dictionary<Lifeline, GameObject>();

                foreach (Lifeline l in sequencediagram.SoftwareEntities)
                {
                    //render lifeline
                    GameObject go_lifeline = (GameObject)Instantiate(Lifeline, new Vector3(scale(l.Left), scale(l.Top), 0), Quaternion.identity);
                    go_lifeline.gameObject.name = l.Name;
                    Transform text = go_lifeline.transform.FindChild("lifeline_name");
                    text.GetComponent<TextMesh>().text = l.Name;

                    Debug.Log("Lifeline:"+l.Name);
                    //render messages
                    foreach(Method m in l.Methods)
                    {
                        GameObject go_message = (GameObject)Instantiate(Message, new Vector3(scale(l.Left), scale(m.PtStartY), 0), Quaternion.identity);
                        go_message.name = m.Name;
                        Transform text_msg = go_message.transform.FindChild("msg_name");
                        text_msg.GetComponent<TextMesh>().text = m.Name;

                        go_message.transform.parent = go_lifeline.transform;
                        //Debug.Log("\t"+m.Name+"\t"+m.MessageSort+"\tStartX:"+scale(m.PtStartX)+"\tStartY"+scale(m.PtStartY));
                    }

                    //render vertical line
                    Transform verticalline = go_lifeline.transform.FindChild("line");
                    verticalline.localScale = new Vector3(verticalline.localScale.x, scale(l.Bottom), verticalline.localScale.z);
                    verticalline.position = new Vector3(verticalline.position.x, scale(l.Bottom*-0.49f), verticalline.position.z);
                }
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

    private float scale(float n)
    {
        return n * 0.19f * 0.1f;
    }
}
