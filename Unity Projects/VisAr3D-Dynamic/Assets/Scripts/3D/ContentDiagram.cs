﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine.UI;
using DG.Tweening;
using Core;

public class ContentDiagram : MonoBehaviour
{

    //Class
    public GameObject Class;
    public Material lineMaterial;

    //Sequence
    public GameObject Lifeline;
    public GameObject Message;

    public string dirsourcecode;

    //UI
    public GameObject Bt_PlayPause;
    public GameObject Slider;

    private TheCore Core { get; set; }
    private ClassDiagram TheClassDiagram { get; set; }
    private SequenceDiagram TheSequenceDiagram { get; set; }

    private bool play = false;
    private float timer = 0.0f;
    private float current = 0; //Slider's value current while update
    private int maximum = 0; //Amount of messages in sequence diagram
    private float tmp = 0;
    private string direction = "right";
    private float timealpha = 0.5f;

    //Classes
    private Dictionary<Class, GameObject> Classes { get; set; } //key:class value:class like gameobject
                                                                //origin    destination
    private Dictionary<LineRenderer, Dictionary<GameObject, GameObject>> LineRenderes = new Dictionary<LineRenderer, Dictionary<GameObject, GameObject>>();

    //Lifelines
    private Dictionary<Lifeline, GameObject> Lifelines { get; set; }
    private Dictionary<Method, GameObject> Methods { get; set; }
    //private Dictionary<int , GameObject> Orders { get; set; } //save each object in order runtime

    private float x = 0;
    private float y = 0;
    private float z = 0;
    private float dist_x = 1.5f;
    private float dist_y = .5f;
    private float dist_z = .5f;

    // Use this for initialization
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1.0f);

        //TEST --------------------------------------------------------------------------
        
        //END TEST --------------------------------------------------------------------------


        //UI-------------------
        //Play Button 
        Bt_PlayPause.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            Play();
        });

        //When Slider is changed
        Slider.GetComponent<Slider>().onValueChanged.AddListener(delegate {
            string r;
            current = Slider.GetComponent<Slider>().value;

            r = current.ToString();
            //Debug.Log("Direction: "+ Slider.GetComponent<Slider>().direction.ToString());
            //Debug.Log("Current:"+current+"\tValue:"+ Slider.GetComponent<Slider>().value);

            if( current < tmp)
            {
                direction = "left";
                ++current;
            }
            else if( current > tmp )
            {
                direction = "right";
            }

            foreach (KeyValuePair<Method, GameObject> m in Methods)
            {
                if (current == float.Parse(m.Key.Seqno, CultureInfo.InvariantCulture.NumberFormat))
                {
                    //r += " seq:"+ m.Key.Seqno+"\tname:"+ m.Key.Name+"\tlabel:"+ m.Key.Label+"\tkind"+ m.Key.MessageSort;

                    if (direction == "left")
                    {
                        m.Value.transform.FindChild("line").GetComponent<Renderer>().material.DOFade(0, timealpha);
                        m.Value.transform.FindChild("msg_name").GetComponent<Renderer>().material.DOFade(0, timealpha);
                        tmp = Slider.GetComponent<Slider>().value;
                    }
                    else
                    {
                        //m.Value.transform.FindChild("line").GetComponent<Renderer>().material.DOFade(1, timealpha);
                        //m.Value.transform.FindChild("msg_name").GetComponent<Renderer>().material.DOFade(1, timealpha);
                        AnimationMessage(m.Key , m.Value);
                        tmp = Slider.GetComponent<Slider>().value;

                        Debug.Log(m.Key.Label+"\tDist:"+scale(m.Key.Dist)*0.25f+"\tLeft:");
                    }

                    //m.Value.GetComponent<Renderer>().material.DOFade(1, 1);
                    //Debug.Log(m.Key.Label +"\tAlpha:" + m.Value.GetComponent<Renderer>().material.color.a );
                }
            }
            //r = "direction:" + direction + "\ttmp:" + tmp + "\tcurrent:" + current + "\tvalue:" + Slider.GetComponent<Slider>().value;
            //Debug.Log(r);
        });

        //Debug.Log("IdDiagram: "+PlayerPrefs.GetString("IdDiagram"));

        Core = new TheCore(PlayerPrefs.GetString("XMIFile"));

        IXmlNode diagram = Core.FindById(Core.AllDiagrams, PlayerPrefs.GetString("IdDiagram"));

        if (diagram.Id != null)
        {
            //Debug.Log("diagram.Type:"+ diagram.Type);
            switch (diagram.Type)
            {
                case "Logical":
                    //Debug.Log("Diagram de classe "+diagram.Name+" id:"+diagram.Id);
                    BuildClassDiagram(diagram);
                    break;

                case "Sequence":
                    BuildSequenceDiagram(diagram);
                    //Debug.Log("Methods:"+Methods.Count);
                    break;
            }
        }

    }

    void AnimationMessage( Method method , GameObject go_method )
    {
        //go_method.transform.FindChild("line").GetComponent<Renderer>().material.DOFade(1, timealpha);
        //go_method.transform.FindChild("msg_name").GetComponent<Renderer>().material.DOFade(1, timealpha);

        go_method.transform.FindChild("line").GetComponent<Renderer>().material.DOFade(1, timealpha);
        go_method.transform.FindChild("line").DOScaleX(scale(method.Dist), timealpha);

        float localmovex = 0;
        if(method.Direction == "left")
        {
            localmovex = (scale(method.Dist) / 2) * -1;
        }
        else
        {
            localmovex = scale(method.Dist) / 2;
        }

        go_method.transform.FindChild("line").DOLocalMoveX(localmovex, timealpha);
        go_method.transform.FindChild("msg_name").GetComponent<Renderer>().material.DOFade(1, timealpha);

    }

    // Update is called once per frame
    void Update()
    {
        UpdateLineRenderer();

        //if true, render!
        if (play)
        {
            if(current == Slider.GetComponent<Slider>().value+1)
            {
                --current;
            }

            if (current < maximum)
            {
                timer += Time.deltaTime;
                if (timer > 1.0f)
                {
                    current += 1;
                    timer -= 1.0f;

                    //implementation here
                    Slider.GetComponent<Slider>().value = current;
                    tmp = current;
                    //Debug.Log("Timer:"+timer+"\t Current:"+current+"\tMaximu:"+maximum);
                }
            }
        }
        else
        {
            
        }

    }

    void Play()
    {
        play = !play;
    }

    void BuildClassDiagram(IXmlNode diagram)
    {
        Package package = (Package)Core.FindById(Core.Packages, diagram.IdPackage);
        if (package.Id != null)
        {
            //Debug.Log("Achei o pacote");
            ClassDiagram classdiagram = (ClassDiagram)package.FindById(package.ClassDiagrams, diagram.Id);
            if (classdiagram.Id != null)
            {
                //print("Achei o diagrama");
                Classes = new Dictionary<Class, GameObject>();

                foreach (Class c in classdiagram.SoftwareEntities)
                {
                    GameObject go_class = (GameObject)Instantiate(Class, new Vector3(x, 0, 0), Quaternion.identity);
                    go_class.gameObject.name = c.Name;
                    go_class.AddComponent<OpenCode>();
                    Transform text = go_class.transform.FindChild("classname");
                    text.GetComponent<TextMesh>().text = c.Name;
                    text.name = c.Name;
                    Classes.Add(c, go_class);
                    x += dist_x;
                    //Debug.Log("Geometry: " + c.Name + " - " + c.Geometry + "\n:"+c.Left+" - "+c.Top+" - "+c.Right+" - "+c.Bottom);
                }

                //create relationships
                foreach (KeyValuePair<Class, GameObject> c in Classes)
                {
                    string s = c.Key.Name + "\n";
                    foreach (KeyValuePair<IXmlNode, IXmlNode> r in c.Key.Relationships)
                    {
                        s += "\t" + r.Value.Name;

                        //if(c.Key.Name.Equals("ClassA"))
                        //{
                        //LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
                        GameObject line = new GameObject("Line Renderer");
                        line.name = c.Value.name;
                        Dictionary<GameObject, GameObject> pairs = new Dictionary<GameObject, GameObject>();
                        pairs.Add(c.Value, FindClasses(r.Value));
                        LineRenderes.Add(line.AddComponent<LineRenderer>(), pairs);
                        //}
                    }
                    print(s);
                }
            }
        }
    }

    void UpdateLineRenderer()
    {
        foreach (KeyValuePair<LineRenderer, Dictionary<GameObject, GameObject>> l in LineRenderes)
        {
            foreach (KeyValuePair<GameObject, GameObject> g in l.Value)
            {
                l.Key.SetPosition(0, g.Key.transform.position);
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
                //refresh Slider's values
                Slider.GetComponent<Slider>().wholeNumbers = true;
                Slider.GetComponent<Slider>().minValue = 0;
                Slider.GetComponent<Slider>().maxValue = sequencediagram.CountMessages;

                maximum = sequencediagram.CountMessages;
                //Debug.Log("CountMessages:"+maximum);

                Lifelines = new Dictionary<Lifeline, GameObject>();
                Methods = new Dictionary<Method, GameObject>();

                foreach (Lifeline l in sequencediagram.SoftwareEntities)
                {
                    //render lifeline
                    GameObject go_lifeline = (GameObject)Instantiate(Lifeline, new Vector3(scale(l.Left), scale(l.Top), 0), Quaternion.identity);
                    go_lifeline.gameObject.name = l.Name;
                    Transform text = go_lifeline.transform.FindChild("lifeline_name");
                    text.GetComponent<TextMesh>().text = l.Name;

                    //Debug.Log("Lifeline:"+l.Seqno+" "+l.Name);

                    //render vertical line
                    Transform verticalline = go_lifeline.transform.FindChild("line");
                    verticalline.localScale = new Vector3(verticalline.localScale.x, scale(l.Bottom), verticalline.localScale.z);
                    verticalline.position = new Vector3(verticalline.position.x, scale(l.Bottom * -0.49f), verticalline.position.z);

                    //fades
                    //go_lifeline.GetComponent<Renderer>().material.DOFade(0, 0);
                    //text.GetComponent<Renderer>().material.DOFade(0, 0);
                    //verticalline.GetComponent<Renderer>().material.DOFade(0, 0);

                    Lifelines.Add(l, go_lifeline); //save

                    //Debug.Log("Lifeline:"+l.Name);
                    //render messages
                    foreach (Method m in l.Methods)
                    {
                        //float msg_posX = scale(l.Left);

                        //if (m.Direction == "right")
                        //{
                        //    msg_posX = scale(l.Left) + scale(m.Dist) / 2;
                        //}
                        //else if (m.Direction == "left")
                        //{
                        //    msg_posX = scale(l.Left) - scale(m.Dist) / 2;
                        //}

                        GameObject go_message = (GameObject)Instantiate(Message, new Vector3(scale(l.Left), scale(m.PtStartY), 0), Quaternion.identity);
                        go_message.name = m.Name;
                        //go_message.transform.FindChild("line").localScale = new Vector3(scale(m.Dist), go_message.transform.FindChild("line").localScale.y, go_message.transform.FindChild("line").localScale.z);

                        Transform text_msg = go_message.transform.FindChild("msg_name");
                        text_msg.GetComponent<TextMesh>().text = m.Name +"\n"+m.MessageSort;

                        //Debug.Log("\t"+m.Seqno+" "+m.Name +"\t"+m.Label);

                        //fades
                        go_message.transform.FindChild("line").GetComponent<Renderer>().material.DOFade(0, 0);
                        text_msg.GetComponent<Renderer>().material.DOFade(0, 0);

                        Methods.Add(m, go_message); //save
                        //Debug.Log("Message saved: " + m.Seqno+" "+m.Label);
                        //Debug.Log("oi");

                        go_message.transform.parent = go_lifeline.transform; //set parent
                        //Debug.Log("\t"+m.Name+"\t"+m.MessageSort+"\tStartX:"+scale(m.PtStartX)+"\tStartY"+scale(m.PtStartY));
                    }

                }
            }
        }
    }


    GameObject FindClasses(IXmlNode c)
    {
        GameObject g = new GameObject("FindClasses");

        foreach (KeyValuePair<Class, GameObject> gg in Classes)
        {
            if (c.Equals(gg.Key))
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
