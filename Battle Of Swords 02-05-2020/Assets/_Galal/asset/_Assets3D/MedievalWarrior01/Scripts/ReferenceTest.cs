using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReferenceTest : MonoBehaviour {
	public GameObject prefabButton;
	public GameObject prefabSlider;
	public RectTransform ParentPanel;
	public RectTransform ParentPanel2;
	// Use this for initialization
	public int typeA;
	public int typeB;
	void Start () {
		createButtonType (typeA, "Change Type A");
		createButtonType (typeB, "Change Type B");
		prefabButton.SetActive (false);
		prefabSlider.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void createButtonType (int type, string btnText)
	{
		GameObject goButton = (GameObject)Instantiate (prefabButton);
		goButton.transform.SetParent (ParentPanel, false);
		goButton.transform.localScale = new Vector3 (1, 1, 1);

		
		Button tempButton = goButton.GetComponent<Button> ();
		tempButton.GetComponentInChildren<Text> ().text = btnText;
		
		tempButton.onClick.AddListener (() => ButtonClickedType (type));	

	}
	void ButtonClickedType (int typeClick)
	{
		typeClick++;
		Debug.Log ("Typ" + typeClick);
	}
}
