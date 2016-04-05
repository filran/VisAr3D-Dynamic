using UnityEngine;
using System.Collections;

public class TestingOK : MonoBehaviour {

    public GameObject sameobject;
    float x;
    bool stop = false;

    // Use this for initialization
    void Start () {
        x = transform.localScale.x;
    }
	
	// Update is called once per frame
	void Update () {   
        if (x < 4)
        {
            if (!stop)
            {
                x += 0.05f;
                sameobject.transform.position = transform.TransformPoint(Vector3.right / 2);
                transform.position = new Vector3(x / 2, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);
            }                
        }
    }

    public void Stop()
    {
        stop = !stop;
    }

}
