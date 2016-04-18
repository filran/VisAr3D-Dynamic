using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Default : MonoBehaviour {

    public GameObject lifeline;
    private List<string> Lifelines { get; set; }
    private List<string> Messages { get; set; }

    void Awake()
    {
        Lifelines = new List<string>();
        Lifelines.Add("Object 1");
        Lifelines.Add("Object 2");
        Lifelines.Add("Object 3");

        Messages = new List<string>();
        Messages.Add("msg1");
        Messages.Add("msg2");
        Messages.Add("msg3");

    }

	// Use this for initialization
	void Start () {
        render(Lifelines);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void render(List<string> lifelines)
    {
        float count = 0;
        foreach(string l in lifelines)
        {
            GameObject life = (GameObject)Instantiate(lifeline, new Vector3(count,0,0) , Quaternion.identity);
            count += 1.5f;
        }
    }
}
