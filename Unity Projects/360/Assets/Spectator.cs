using UnityEngine;
using System.Collections;

public class Spectator : MonoBehaviour
{

    public static float speed = 30.0f;

    public GameObject oculusLeftEye;

    Rigidbody rr;

    Vector3 axis;

    float rotationY;
    float rotationX;
    float rotationZ;


    void Start()
    {
        rr = GetComponent<Rigidbody>();
    }

    void Update()
    {


        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || Input.GetKey("joystick button 5"))
        {
            speed = 60.0f;
        }

        else
        {
            speed = 30.0f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            rotationX = oculusLeftEye.transform.localRotation.x / 2;
            rotationY = oculusLeftEye.transform.localRotation.y / 2;
            rotationZ = oculusLeftEye.transform.localRotation.z;

            axis = new Vector3(rotationX, rotationY, rotationZ);

            rr.velocity = oculusLeftEye.transform.forward * speed;

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            rr.velocity = oculusLeftEye.transform.forward * 0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            rotationX = oculusLeftEye.transform.localRotation.x / 2;
            rotationY = oculusLeftEye.transform.localRotation.y / 2;
            rotationZ = oculusLeftEye.transform.localRotation.z;

            axis = new Vector3(rotationX, rotationY, rotationZ);

            rr.velocity = oculusLeftEye.transform.forward * -1 * speed;

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            rr.velocity = oculusLeftEye.transform.forward * -1 * 0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotationX = oculusLeftEye.transform.localRotation.x / 2;
            rotationY = oculusLeftEye.transform.localRotation.y / 2;
            rotationZ = oculusLeftEye.transform.localRotation.z;

            axis = new Vector3(rotationX, rotationY, rotationZ);

            rr.velocity = oculusLeftEye.transform.right * speed;

        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            rr.velocity = oculusLeftEye.transform.right * 0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotationX = oculusLeftEye.transform.localRotation.x / 2;
            rotationY = oculusLeftEye.transform.localRotation.y / 2;
            rotationZ = oculusLeftEye.transform.localRotation.z;

            axis = new Vector3(rotationX, rotationY, rotationZ);

            rr.velocity = oculusLeftEye.transform.right * -1 * speed;

        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            rr.velocity = oculusLeftEye.transform.forward * -1 * 0f;
        }

    }
}