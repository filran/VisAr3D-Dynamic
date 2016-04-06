using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ControllMsgs : MonoBehaviour {

    public GameObject goLifeline;
    private List<GameObject> Lifelines;
    private bool stop = false;
    public GameObject bt;

	// Use this for initialization
	void Start () {
        GameObject diagram = new GameObject("Diagram");

        GameObject obj1 = (GameObject)(GameObject)Instantiate(goLifeline, new Vector3(0, 1.5f, 0), Quaternion.identity);
        GameObject obj2 = (GameObject)(GameObject)Instantiate(goLifeline, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject obj3 = (GameObject)(GameObject)Instantiate(goLifeline, new Vector3(0, -1.5f, 0), Quaternion.identity);

        obj1.transform.parent = diagram.transform;
        obj2.transform.parent = diagram.transform;
        obj3.transform.parent = diagram.transform;

        Lifelines = new List<GameObject>();
        Lifelines.Add(obj1);
        Lifelines.Add(obj2);
        Lifelines.Add(obj3);

        //add CLICK on the button
        bt.GetComponent<Button>().onClick.AddListener(delegate() {
            Stop();
        });
    }
	
	// Update is called once per frame
	void Update () {
        if (!stop)
        {
            foreach (GameObject l in Lifelines)
            {
                l.transform.position += Vector3.right * Time.deltaTime;
            }
        }
	}

    bool Stop()
    {
        Debug.Log(!stop);
        return stop = !stop;
    }
}
