using UnityEngine;
using System.Collections;

public class ChangeSocket : MonoBehaviour {

	public GameObject RHandParent;
	public GameObject BeltParent;
	public GameObject LHandParent;
	public GameObject BackParent;
	public GameObject WeaponRoot;
	public GameObject ShieldRoot;
	public  bool isAB;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("e")) {
			isAB = !isAB; // toggles onoff at each click
			if(isAB){
				SetParent (WeaponRoot, RHandParent);
				SetParent (ShieldRoot, LHandParent);
			}
			else{
				SetParent (WeaponRoot, BeltParent);
				SetParent (ShieldRoot, BackParent);
			}
		}
	}

	public void SetParent(GameObject obj, GameObject newParent)
	{
		obj.transform.parent = newParent.transform;	//makes new parent
		obj.transform.localPosition = new Vector3 (0, 0, 0);
		obj.transform.localRotation = new Quaternion (0, 0, 0,0);
		Debug.Log("obj's Parent: " + obj.transform.parent.name); //show parent name

	}
}
