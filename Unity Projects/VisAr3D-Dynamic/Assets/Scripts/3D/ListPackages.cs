using UnityEngine;
using System.Collections;
using Core;
using Scripts;

public class ListPackages : MonoBehaviour {

    public GameObject Package;
    private float dist = .5f;

    // Use this for initialization
    void Start () {
        TheCore core = new TheCore(PlayerPrefs.GetString("XMIFile"));

        float x = 0;
        foreach (Package p in core.Packages)
        {
            GameObject package = (GameObject)Instantiate(Package, new Vector3(x, 0, 0), Quaternion.identity);
            Transform textPackage = package.transform.FindChild("name");

            package.name = p.Id;
            //package.AddComponent<ChangeScene>().GoToScene = "PackageOpened";
            package.AddComponent<ChangeScene>().GoToPackageOpened = true;
            package.AddComponent<ChangeScene>().IdPackage = p.Id;
            textPackage.GetComponent<TextMesh>().name = p.Name;
            textPackage.GetComponent<TextMesh>().text = p.Name;

            x = ++dist;
        }

    }

    // Update is called once per frame
    void Update () {
	
	}
}
