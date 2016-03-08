using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Composite
{
    class Client : MonoBehaviour
    {
        void Start()
        {
            IComponent<string> mainalbum = new Composite<string>("Main Album");
            IComponent<string> Aalbum = new Composite<string>("A Album");
            IComponent<string> AAalbum = new Composite<string>("AA Album");
            IComponent<string> Balbum = new Composite<string>("B Album");

            Aalbum.Add(new Component<string>("filho de A"));
            mainalbum.Add(Aalbum);

            AAalbum.Add(new Component<string>("filho de AA"));
            Aalbum.Add(AAalbum);

            Balbum.Add(new Component<string>("filho de B"));
            mainalbum.Add(Balbum);

            Debug.Log(mainalbum.Display());
        }
    }
}
