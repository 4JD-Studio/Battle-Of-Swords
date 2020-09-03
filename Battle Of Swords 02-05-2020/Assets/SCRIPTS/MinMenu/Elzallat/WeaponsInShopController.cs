using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsInShopController : MonoBehaviour
{
    public GameObject ContentHolder;
    public GameObject ItemHolderPrefab;
    public float ScrollBarWidth = 20;
    public List<WeaponModel> Weapons;

    public ObjectLocalizedWithImage SelectButton, SelectedButton;
    public Sprite PriceSprite;

    WeaponModel LastWeapon;
    GameObject LastItemInList;

    public static WeaponsInShopController Inistance;

    private void Awake()
    {
        Inistance = this;
    }

    public void setDataOnScreen()
    {
        //if nothing is selected in playfav
        //make first as selected

        //clear ContentHolder
        foreach (Transform transform in ContentHolder.transform)
        {
            GameObject.Destroy(transform.gameObject);
        }

        for (int i = 0; i < Weapons.Count; i++)
        {

            GameObject item;
            WeaponModel tempCur = Weapons[i];
            int TempI = i;
            if (tempCur.IsFree || tempCur.IsOpenToUser)
            {
                item = Instantiate(ItemHolderPrefab, ContentHolder.transform, false);
                item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = tempCur.WeaponImage;
                if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                    item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInEnglish;
                else
                    item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInArabic;
                item.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
                    () => { OnSelectButtonClick(tempCur, item, TempI); }
                    );

                //get selected weapon from variables
                if(PlayerVariables.Inistance.getCurrentWeapon() != null)
                {
                    if (Weapons[i] == PlayerVariables.Inistance.getCurrentWeapon())
                    {
                        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                            item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInEnglish;
                        else
                            item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInArabic;

                        item.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                        LastItemInList = item;
                    }
                }else if(PlayerVariables.Inistance.getWeaponIndex() != null)
                {
                    if (i == int.Parse(PlayerVariables.Inistance.getWeaponIndex()))
                    {
                        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                            item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInEnglish;
                        else
                            item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInArabic;
                        item.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                        LastItemInList = item;
                    }
                }
            }
            else
            {
                item = Instantiate(ItemHolderPrefab, ContentHolder.transform, false);
                item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = tempCur.WeaponImage;
                item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = PriceSprite;
                item.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = tempCur.WeaponPrice.ToString();
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
                    () => { OnBuyButtonClick(tempCur, item, TempI); }
                    );
                
                if (tempCur.WeaponPrice > PlayerVariables.Inistance.getCurrentGold())
                {
                    item.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                }
            }
            item.GetComponent<RectTransform>().sizeDelta =
                new Vector2(Screen.width - ScrollBarWidth, item.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public void OnSelectButtonClick(WeaponModel CurrentWeapon, GameObject CurrentItemHolder, int Index)
    {
        if(LastItemInList != null)
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInEnglish;
            else
                LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInArabic;
            LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = true;
        }
        LastItemInList = CurrentItemHolder;
        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInEnglish;
        else
            LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInArabic;
        LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;

        PlayerVariables.Inistance.setCurrentWeapon(CurrentWeapon);
        PlayerVariables.Inistance.setWeaponIndex(Index.ToString());
        //save to playfab as selected

    }

    public void OnBuyButtonClick(WeaponModel CurrentWeapon, GameObject CurrentItemHolder, int Index)
    {
        if (LastItemInList != null)
        {
            if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInEnglish;
            else
                LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInArabic;
            LastItemInList.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = true;
            LastItemInList.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
        }
        LastItemInList = CurrentItemHolder;
        CurrentWeapon.IsOpenToUser = true;


        CurrentItemHolder.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = CurrentWeapon.WeaponImage;
        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInEnglish;
        else
            CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInArabic;
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            () => { OnSelectButtonClick(CurrentWeapon, CurrentItemHolder, Index); }
            );


        PlayerVariables.Inistance.setCurrentWeapon(CurrentWeapon);
        PlayerVariables.Inistance.setOpenListWeapons(PlayfabDataSetter.Inistance.DataToStringFromList(ListType.Weapons));
        PlayerVariables.Inistance.setCurrentGold(PlayerVariables.Inistance.getCurrentGold() - CurrentWeapon.WeaponPrice);
        PlayerVariables.Inistance.setWeaponIndex(Index.ToString());
        GeneralController.Inistance.GoldText.text = PlayerVariables.Inistance.getCurrentGold().ToString();

        //show message
        StartCoroutine(GeneralController.Inistance.ToastDisplayer("Weapon Added"));

        //update other wepons can be bought
        for (int i = 0; i < Weapons.Count; i++)
        {
            if (!(Weapons[i].IsFree || Weapons[i].IsOpenToUser) && Weapons[i].WeaponPrice > PlayerVariables.Inistance.getCurrentGold())
            {
                ContentHolder.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            }
        }

        //save in play fab
        //as bought -- and selected -- and money
    }
}
