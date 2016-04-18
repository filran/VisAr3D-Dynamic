using UnityEngine;
using System.Collections;

public class Turn : MonoBehaviour
{
    float mfX;
    float mfY;
    // Use this for initialization
    void Start()
    {
        mfX = transform.position.x - transform.localScale.x / 2.0f;
        mfY = transform.position.y - transform.localScale.y / 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 v3Scale = transform.localScale;
            transform.localScale = new Vector3(v3Scale.x + 0.1f, v3Scale.y + 0.1f, v3Scale.z);
            transform.position = new Vector3(mfX + transform.localScale.x / 2.0f, mfY + transform.localScale.y / 2.0f, 0);
        }
    }
}