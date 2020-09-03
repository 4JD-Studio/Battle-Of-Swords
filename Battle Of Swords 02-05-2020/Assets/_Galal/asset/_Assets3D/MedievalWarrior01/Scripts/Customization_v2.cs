using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Customization_v2 : MonoBehaviour
{
	[System.Serializable]
	public struct GameObjStruct
	{
		[SerializeField] public GameObject[] Model;
		[SerializeField] public Material[] Mat;
	}

	public GameObject prefabButton;
	public GameObject prefabSlider;
	public RectTransform ParentPanel;
	public RectTransform ParentPanel2;
	public GameObject[] Shoulders;
	public int ShouldersID;
	public Material[] ShouldersMats;
	public int ShouldersMatID;
	public bool IsShoulders;
	public GameObject[] Beard;
	public int BeardID;
	public Material[] BeardsMats;
	public int BeardsMatsID;
	public bool IsBeard;
	public GameObject[] Boots;
	public int BootsID;
	public Material[] BootsMats;
	public int BootsMatsID;
	public GameObject[] Chest;
	public int ChestID;
	public Material[] ChestMats;
	public int ChestMatID;
	public int SkirtMatID;

	private static int GlovesType;
	public GameObjStruct[] GlovesStruct;
	//public GameObject[] Gloves;
	public int GlovesID;
	//public Material[] GlovesMats;
	public int GlovesMatsID;
	public bool IsGloves;

	public GameObject[] Hair;
	public int HairID;
	public Material[] HairMats;
	public int HairMatsID;
	public bool IsHair;
	public GameObject[] Head;
	public Material[] HeadMats;
	public int HeadMatsID;
	public GameObject[] Helmet;
	public int HelmetID;
	public Material[] HelmetMats;
	public int HelmetMatsID;
	public bool IsHelmet;
	public GameObject[] Hood;
	public int HoodID;
	public Material[] HoodMats;
	public int HoodMatsID;
	public bool IsHood;

	public int PantsType;
	public GameObjStruct[] PantsStruct;
	//public GameObject[] Pants;
	//public Material[] PantsMats;

	public int PantsMatsID;
	public GameObject[] Shields;
	public int ShieldsID;
	public Material[] ShieldsMats;
	public int ShieldsMatID;
	public bool IsShield;
	public GameObject[] Weapons;
	public int WeaponsID;
	public bool IsWeapon;
	//public GameObjStruct[] test1;




	// Use this for initialization
	void Start ()
	{

		//Head.GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (1, 100);
		createButtonRandom ();

		createButtonModel (ref ShouldersID, Shoulders, IsShoulders, true, "Sholder model");
		createButtonMaterial (ShouldersID, ref ShouldersMatID, Shoulders, ShouldersMats,0, "Shoulder material");

		createButtonModel (ref BeardID, Beard, IsBeard, true, "Beard model");
		createButtonMaterial (BeardID, ref BeardsMatsID, Beard, BeardsMats,0, "Beard material");

		createButtonModel (ref BootsID, Boots, true, false, "Boots model");
		createButtonMaterial (BootsID, ref BootsMatsID, Boots, BootsMats,0, "Boots material");

		createButtonModel (ref ChestID, Chest, true,false,"Chest model");

		//SetMaterialSlot(0,ChestID, ChestMatID, Chest, ChestMats);

		createButtonMaterial (ChestID, ref ChestMatID, Chest, ChestMats,0,"Chest material");
		createButtonMaterial (ChestID, ref SkirtMatID, Chest, ChestMats,1,"Skirt material");

		createButtonType (GlovesStruct, ref GlovesType,"Gloves type");
		createButtonModel_2 (ref GlovesID,GlovesStruct,GlovesType, IsGloves,false,"Gloves model");
		createButtonMaterial_2 (GlovesID, ref GlovesMatsID, GlovesStruct,0,"Gloves material");


		//createButtonModel (ref GlovesID, Gloves, IsGloves,true,"Gloves model");
		//createButtonMaterial (GlovesID, ref GlovesMatsID, Gloves, GlovesMats,0,"Gloves material");

		createButtonModel (ref HairID, Hair, IsHair,true,"Hair model");
		createButtonMaterial (HairID, ref HairMatsID, Hair, HairMats,0,"Hair material");

		createButtonModel (ref HelmetID, Helmet, IsHelmet,true,"Helmet model");
		createButtonMaterial (HelmetID, ref HelmetMatsID, Helmet, HelmetMats,0,"Helmet material");

		createButtonMaterial (0, ref HeadMatsID, Head, HeadMats,0,"Head material");

		createButtonModel (ref HoodID, Hood, IsHood,true, "Hood model");
		createButtonMaterial (HoodID, ref HoodMatsID, Hood, HoodMats,0,"Hood material");

		createButtonType (PantsStruct, ref PantsType,"Pants Type");
		createButtonMaterial_2 (0, ref PantsMatsID, PantsStruct,0, "Pants material");
		//createButtonMaterial (0, ref PantsMatsID, Pants, PantsMats,0, "Pants material");

		createButtonModel (ref ShieldsID, Shields, IsShield,true, "Shield model");
		createButtonMaterial (ShieldsID, ref ShieldsMatID, Shields, ShieldsMats,0,"Shield material");

		createButtonModel (ref WeaponsID, Weapons, IsWeapon,true,"Weapon model");

		createButtonBlendshape (0, "Cheeks");
		createButtonBlendshape (1, "EyesHeight");
		createButtonBlendshape (2, "NoseBulb");
		createButtonBlendshape (3, "NoseCurve");	
		createButtonBlendshape (4, "NoseSpike");
		createButtonBlendshape (5, "NoseLength");
		createButtonBlendshape (6, "Asymmetry");
		createButtonBlendshape (7, "LipsBulb");
		createButtonBlendshape (8, "ForeheadUp");
		createButtonBlendshape (9, "KlinnDown");
		createButtonBlendshape (10, "EyesWidth");
		createButtonBlendshape (12, "LipsWidth");
		createButtonBlendshape (14, "Age");

		prefabButton.SetActive (false);
		prefabSlider.SetActive (false);


	}
	
	// Update is called once per frame
	void Update ()
	{
		//Head.GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (13, tmpSlider.value*100);
	}
	void createButtonBlendshape (int index, string name)
	{
		GameObject goSlider = (GameObject)Instantiate (prefabSlider);
		goSlider.transform.SetParent (ParentPanel2, false);
		goSlider.transform.localScale = new Vector3 (1, 1, 1);

		
		Slider tempSlider = goSlider.GetComponent<Slider> ();
		tempSlider.GetComponentInChildren<Text> ().text = "Blendshape " + name;
		tempSlider.onValueChanged.AddListener (delegate {SliderChangeCheck(index,ref tempSlider); });

	}

	void SliderChangeCheck(int index,ref Slider tmpSlider)
	{
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (index, tmpSlider.value*100);
	}
		

	int setType (int lenght, string name, bool isAdd){
		string[] words = name.Split();

		switch(words[0])
		{
			case "Gloves":
			if(isAdd == true){
				GlovesType++;
				if (lenght == GlovesType)
					GlovesType = 0;	
			}
			return GlovesType;
		case "Pants":
			if(isAdd == true){
				PantsType++;
				if (lenght == PantsType)
					PantsType = 0;
			}
			return PantsType;
		}
		return 0;


	
	}


	void createButtonRandom ()
	{
		GameObject goButton = (GameObject)Instantiate (prefabButton);
		goButton.transform.SetParent (ParentPanel, false);
		goButton.transform.localScale = new Vector3 (1, 1, 1);

		Button tempButton = goButton.GetComponent<Button> ();
		tempButton.GetComponentInChildren<Text> ().text = "Go crazy!!";
		
		tempButton.onClick.AddListener (() => ButtonClickedRandom ());

	}
	void ButtonClickedRandom ()
	{
		randomModelsAndMaterials ();
		
	}

	void createButtonType ( GameObjStruct[] goStr, ref int type, string btnText)
	{
		GameObject goButton = (GameObject)Instantiate (prefabButton);
		goButton.transform.SetParent (ParentPanel, false);
		goButton.transform.localScale = new Vector3 (1, 1, 1);
		int typeRef = type;

		//GameObjStruct[] goStrRef = goStr;


		Button tempButton = goButton.GetComponent<Button> ();
		tempButton.GetComponentInChildren<Text> ().text = btnText;

		tempButton.onClick.AddListener (() => ButtonClickedType (goStr, btnText));

//		foreach (GameObjStruct goStrOne in goStr) {	
//			foreach (GameObject model in goStrOne.Model) {
//				model.SetActive (false);
//			}
//		}
//		foreach (GameObject model in goStr[type].Model) {
//			model.SetActive (true);
//		}
	}
	void ButtonClickedType ( GameObjStruct[] goStrRef, string btnText)
	{
		int type = setType(0,btnText,false);
		foreach (GameObject model in goStrRef[type].Model) {
			model.SetActive (false);
		}
		type = setType(goStrRef.Length,btnText,true);
		//type = type + 1;

		if (goStrRef.Length == type)
			type = 0;	
		SetModel (1, goStrRef [type].Model, true);
		Debug.Log ("Typ"+type);
//		foreach (GameObject model in goStrRef[type].Model) {
//			model.SetActive (true);
//		}
//		go = goStrRef [type].Model;
//		goMat = goStrRef [type].Mat;

	}


	void createButtonModel (ref int ID, GameObject[] go, bool isVisible, bool checkVisibility, string btnText)
	{
		GameObject goButton = (GameObject)Instantiate (prefabButton);
		goButton.transform.SetParent (ParentPanel, false);
		goButton.transform.localScale = new Vector3 (1, 1, 1);
		int ID2 = ID;

		Button tempButton = goButton.GetComponent<Button> ();
		tempButton.GetComponentInChildren<Text> ().text = btnText;

		tempButton.onClick.AddListener (() => ButtonClickedModel (ref ID2, go, isVisible, checkVisibility));
		SetModel (ID, go, isVisible);
	}

	void ButtonClickedModel (ref int ID, GameObject[] go, bool isVisible, bool checkVisibility)
	{

		ID = ID + 1;
		if (ID == go.Length && checkVisibility == true) {
			SetModel (ID, go, false);
			ID = ID - go.Length - 1;
		} else
			SetModel (ID, go, isVisible);

	}

	void createButtonModel_2 (ref int ID, GameObjStruct[] goStr,int type, bool isVisible, bool checkVisibility, string btnText)
	{
		GameObject goButton = (GameObject)Instantiate (prefabButton);
		goButton.transform.SetParent (ParentPanel, false);
		goButton.transform.localScale = new Vector3 (1, 1, 1);
		int ID2 = ID;

		Button tempButton = goButton.GetComponent<Button> ();
		tempButton.GetComponentInChildren<Text> ().text = btnText;

		tempButton.onClick.AddListener (() => ButtonClickedModel_2 (ref ID2, goStr,type, isVisible, checkVisibility,btnText));
		SetModel (ID, goStr[type].Model, isVisible);
//		foreach (GameObjStruct goStrOne in goStr) {
//			SetModel (ID, goStrOne.Model, isVisible);
//		}
	}
	
	void ButtonClickedModel_2 (ref int ID, GameObjStruct[] goStr, int type, bool isVisible, bool checkVisibility,string btnText)
	{

		Debug.Log ("Gloves type" + GlovesType);
		ID = ID + 1;
		type = setType (0, btnText, false);
		if (ID == goStr[type].Model.Length && checkVisibility == true) {
			SetModel (ID, goStr[type].Model, false);
			ID = ID - goStr[type].Model.Length - 1;
		} else
			SetModel (ID, goStr[type].Model, isVisible);
//		foreach (GameObjStruct goStrOne in goStr) {
//			if (ID == goStrOne.Model.Length && checkVisibility == true) {
//				SetModel (ID, goStrOne.Model, false);
//				ID = ID - goStrOne.Model.Length - 1;
//			} else
//				SetModel (ID, goStrOne.Model, isVisible);
//		}
		
	}


	void createButtonMaterial_2 (int ID, ref int matID, GameObjStruct[] goStr,int slotNum, string btnText)
	{
		GameObject goButton = (GameObject)Instantiate (prefabButton);
		goButton.transform.SetParent (ParentPanel, false);
		goButton.transform.localScale = new Vector3 (1, 1, 1);
		//int ID2 = ID;
		int matID2 = matID;
		
		Button tempButton = goButton.GetComponent<Button> ();
		tempButton.GetComponentInChildren<Text> ().text = btnText;
		
		tempButton.onClick.AddListener (() => ButtonClickedMaterial_2 (ID, ref matID2, goStr,slotNum));
	}
	
	void ButtonClickedMaterial_2 (int ID, ref int matID, GameObjStruct[] goStr, int slotNum)
	{
		matID = matID + 1;
		if (slotNum == 0)
			foreach (GameObjStruct goStrOne in goStr) {	
			SetMaterial (ID, matID, goStrOne.Model, goStrOne.Mat);
			}
		else
		foreach (GameObjStruct goStrOne in goStr) {	
			SetMaterialSlot(slotNum,ID, matID, goStrOne.Model, goStrOne.Mat);
		}
			
	}




	void createButtonMaterial (int ID, ref int matID, GameObject[] go, Material[] mats,int slotNum, string btnText)
	{
		GameObject goButton = (GameObject)Instantiate (prefabButton);
		goButton.transform.SetParent (ParentPanel, false);
		goButton.transform.localScale = new Vector3 (1, 1, 1);
		//int ID2 = ID;
		int matID2 = matID;
		
		Button tempButton = goButton.GetComponent<Button> ();
		tempButton.GetComponentInChildren<Text> ().text = btnText;
		
		tempButton.onClick.AddListener (() => ButtonClickedMaterial (ID, ref matID2, go, mats,slotNum));
	}
	
	void ButtonClickedMaterial (int ID, ref int matID, GameObject[] go, Material[] mats, int slotNum)
	{
		Debug.Log ("In Material model is:  " + ID);
		matID = matID + 1;
		if(slotNum==0)
			SetMaterial (ID, matID, go, mats);
		else
			SetMaterialSlot(slotNum,ID, matID, go, mats);
	}

	int CheckOffset (int value, int max)
	{
		while (value >= max) {
			value = value - max;
		}
		return value; 
	}

	void randomModelsAndMaterials ()
	{
	
	//	Debug.Log ("Type test   " + GlovesType);
		ShouldersID = Random.Range (0, Shoulders.Length);
		ShouldersMatID = Random.Range (0, ShouldersMats.Length);
		BeardID = Random.Range (0, Beard.Length);
		BeardsMatsID = Random.Range (0, BeardsMats.Length);
		BootsID = Random.Range (0, Boots.Length);
		BootsMatsID = Random.Range (0, BootsMats.Length);
		ChestID = Random.Range (0, Chest.Length);
		ChestMatID = Random.Range (0, ChestMats.Length);
		SkirtMatID = Random.Range (0, ChestMats.Length);
		GlovesType = Random.Range (0, 2);
		GlovesID = Random.Range (0, GlovesStruct[GlovesType].Model.Length);
		GlovesMatsID = Random.Range (0, GlovesStruct[GlovesType].Mat.Length);
		HairID = Random.Range (0, Hair.Length);
		HairMatsID = Random.Range (0, HairMats.Length);
		HelmetID = Random.Range (0, Helmet.Length);
		HelmetMatsID = Random.Range (0, HelmetMats.Length);
		HeadMatsID = Random.Range (0, HeadMats.Length);
		HoodID = Random.Range (0, Hood.Length);
		HoodMatsID = Random.Range (0, HoodMats.Length);
		PantsType = Random.Range (0, 2);
		PantsMatsID = Random.Range (0, PantsStruct[PantsType].Mat.Length);
		ShieldsID = Random.Range (0, Shields.Length);
		ShieldsMatID = Random.Range (0, ShieldsMats.Length);
		WeaponsID = Random.Range (0, Weapons.Length);

		
		IsShoulders = randomBoolean (0.5F);
		IsBeard = randomBoolean (0.3F);
		IsGloves = randomBoolean (0.35F);
		IsHair = randomBoolean (0.4F);
		IsHelmet = randomBoolean (0.5F);
		IsHood = randomBoolean (0.5F);
		IsShield = randomBoolean (0.1F);
		IsWeapon = randomBoolean (0.05F);

		if (IsHelmet == true || HoodID == 0)
			IsHair = false;

		SetModel (ShouldersID, Shoulders, IsShoulders);
		SetMaterial (ShouldersID, ShouldersMatID, Shoulders, ShouldersMats);

		SetModel (BeardID, Beard, IsBeard);
		SetMaterial (BeardID, BeardsMatsID, Beard, BeardsMats);
		
		SetModel (BootsID, Boots, true);
		SetMaterial (BootsID, BootsMatsID, Boots, BootsMats);
		
		SetModel (ChestID, Chest, true);
		if(ChestID!=2)
			SetMaterialSlot (1, ChestID, SkirtMatID, Chest, ChestMats);
		SetMaterial (ChestID, ChestMatID, Chest, ChestMats);
		
	//	SetModel (GlovesID, Gloves, IsGloves);
		SetModelStruct (GlovesID, GlovesStruct, IsGloves,GlovesType);
		SetMaterial (GlovesID, GlovesMatsID, GlovesStruct[GlovesType].Model, GlovesStruct[GlovesType].Mat);
		
		SetModel (HairID, Hair, IsHair);
		SetMaterial (HairID, HairMatsID, Hair, HairMats);

		SetMaterial (0, HeadMatsID, Head, HeadMats);
		
		SetModel (HelmetID, Helmet, IsHelmet);
		SetMaterial (HelmetID, HelmetMatsID, Helmet, HelmetMats);
		
		SetModel (HoodID, Hood, IsHood);
		SetMaterial (HoodID, HoodMatsID, Hood, HoodMats);

		SetModelStruct (0, PantsStruct, true,PantsType);
		SetMaterial (0, PantsMatsID, PantsStruct [GlovesType].Model, PantsStruct [GlovesType].Mat);
		
		SetModel (ShieldsID, Shields, IsShield);
		SetMaterial (ShieldsID, ShieldsMatID, Shields, ShieldsMats);
		
		SetModel (WeaponsID, Weapons, IsWeapon);

		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (1, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (2, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (3, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (4, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (5, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (6, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (7, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (8, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (9, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (10, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (12, Random.Range(0,100));
		Head[0].GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (14, Random.Range(0,100));


	}

	bool randomBoolean (float probality)
	{
		if (Random.value >= probality) {
			return true;
		}
		return false;
	}

	void SetModel (int index, GameObject[] mesh, bool isVisible)
	{
		index = CheckOffset (index, mesh.Length);
		for (int i=0; i < mesh.Length; i++) {
			if (i != index) {
				mesh [i].SetActive (false);
			} else
				mesh [i].SetActive (isVisible);
		}
	}

	void SetModelStruct (int index, GameObjStruct[] goStruct, bool isVisible,int type)
	{

		foreach (GameObjStruct goStructOne in goStruct) {
			foreach(GameObject mesh in goStructOne.Model)
			{
				mesh.SetActive(false);
			}
		}

			index = CheckOffset (index, goStruct[type].Model.Length);
		for (int i=0; i < goStruct[type].Model.Length; i++) {
				if (i != index) {
				goStruct[type].Model [i].SetActive (false);
				} else
				goStruct[type].Model [i].SetActive (isVisible);
			}

	}

	void SetMaterial (int meshIndex, int matIndex, GameObject[] mesh, Material[] mats)
	{
		meshIndex = CheckOffset (meshIndex, mesh.Length);
		matIndex = CheckOffset (matIndex, mats.Length);
		foreach (GameObject meshOne in mesh) {		

			meshOne.GetComponent<Renderer> ().material = mats [matIndex];

			Renderer[] Childs = meshOne.GetComponentsInChildren<Renderer> ();
			foreach (Renderer child in Childs) {
				child.material = mats [matIndex];
			}
		}
	}

	void SetMaterialSlot (int slot, int meshIndex, int matIndex, GameObject[] mesh, Material[] mats)
	{
		meshIndex = CheckOffset (meshIndex, mesh.Length);
		matIndex = CheckOffset (matIndex, mats.Length);
		foreach (GameObject meshOne in mesh) {	

			Material[] tmp = meshOne.GetComponent<Renderer> ().sharedMaterials;
			if (tmp.Length>1)
			{
				tmp [slot] = mats [matIndex];

				Renderer[] Childs = meshOne.GetComponentsInChildren<Renderer> ();
				foreach (Renderer child in Childs) {
					child.sharedMaterials = tmp;
				
				}
			}
		}
	}








//		ProceduralMaterial InstanceMat = mesh [meshIndex].GetComponent<Renderer> ().material as ProceduralMaterial;
//		InstanceMat.SetProceduralFloat("R_saturation",1);
//		InstanceMat.SetProceduralFloat("G_saturation",1);
//		InstanceMat.SetProceduralFloat("B_saturation",1);
//		InstanceMat.RebuildTextures ();
//
//		mesh [meshIndex].GetComponent<Renderer> ().material = InstanceMat;
//		mesh [meshIndex].GetComponentInChildren<Renderer>().material = InstanceMat;



}

