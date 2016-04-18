using UnityEngine;
using System.Collections;

public class TestingTransformPoint : MonoBehaviour {

    public GameObject someObject;
    public Vector3 thePosition;

    void Start()
    {
        // Instantiate an object to the right of the current object
        thePosition = transform.TransformPoint(Vector3.right * 1.5f);
        Instantiate(someObject, thePosition, someObject.transform.rotation);
    }
}
