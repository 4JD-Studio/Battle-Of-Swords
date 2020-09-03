using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldsInShopController : MonoBehaviour
{
    public GameObject ContentHolder;
    public GameObject ItemHolderPrefab;
    public float ScrollBarWidth = 20;
    public List<ShieldsModel> Shields;

    public ObjectLocalizedWithImage SelectButton, SelectedButton;
    public Sprite PriceSprite;

    ShieldsModel LastShield;
    GameObject LastItemInList;

    public static ShieldsInShopController Inistance;

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

        for (int i = 0; i < Shields.Count; i++)
        {

            GameObject item;
            ShieldsModel tempCur = Shields[i];
            int TempI = i;
            if (tempCur.IsFree || tempCur.IsOpenToUser)
            {
                item = Instantiate(ItemHolderPrefab, ContentHolder.transform, false);
                item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = tempCur.ShieldImage;
                if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                    item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInEnglish;
                else
                    item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectButton.ImageInArabic;
                item.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
                    () => { OnSelectButtonClick(tempCur, item, TempI); }
                    );

                //get selected shield from variables
                //if (Shields[i] == PlayerVariables.Inistance.getCurrentShield())

                if (PlayerVariables.Inistance.getCurrentShield() != null)
                {
                    if (Shields[i] == PlayerVariables.Inistance.getCurrentShield())
                    {
                        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
                            item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInEnglish;
                        else
                            item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInArabic;
                        item.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                        LastItemInList = item;
                    }
                }
                else if (PlayerVariables.Inistance.getShieldIndex() != null)
                {
                    if (i == int.Parse(PlayerVariables.Inistance.getShieldIndex()))
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
                item.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = tempCur.ShieldImage;
                item.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = PriceSprite;
                item.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = tempCur.ShieldPrice.ToString();
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                item.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
                    () => { OnBuyButtonClick(tempCur, item, TempI); }
                    );
                
                if (tempCur.ShieldPrice > PlayerVariables.Inistance.getCurrentGold())
                {
                    item.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
                }
            }
            item.GetComponent<RectTransform>().sizeDelta =
                new Vector2(Screen.width - ScrollBarWidth, item.GetComponent<RectTransform>().sizeDelta.y);
        }
    }

    public void OnSelectButtonClick(ShieldsModel CurrentShield, GameObject CurrentItemHolder, int Index)
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

        PlayerVariables.Inistance.setCurrentShield(CurrentShield);
        PlayerVariables.Inistance.setShieldIndex(Index.ToString());
        //save to playfab as selected

    }

    public void OnBuyButtonClick(ShieldsModel CurrentShield, GameObject CurrentItemHolder, int Index)
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
        CurrentShield.IsOpenToUser = true;


        CurrentItemHolder.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = CurrentShield.ShieldImage;
        if (PlayerPrefs.GetString("Language", "EN").Equals("EN"))
            CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInEnglish;
        else
            CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = SelectedButton.ImageInArabic;
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>().text = "";
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
        CurrentItemHolder.transform.GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
            () => { OnSelectButtonClick(CurrentShield, CurrentItemHolder, Index); }
            );
        


        PlayerVariables.Inistance.setCurrentShield(CurrentShield);
        PlayerVariables.Inistance.setOpenListShields(PlayfabDataSetter.Inistance.DataToStringFromList(ListType.Shields));
        PlayerVariables.Inistance.setCurrentGold(PlayerVariables.Inistance.getCurrentGold() - CurrentShield.ShieldPrice);
        PlayerVariables.Inistance.setShieldIndex(Index.ToString());
        GeneralController.Inistance.GoldText.text = PlayerVariables.Inistance.getCurrentGold().ToString();

        //show message
        StartCoroutine(GeneralController.Inistance.ToastDisplayer("Shield Added"));

        //update other shield can be bought
        for (int i = 0; i < Shields.Count; i++)
        {
            if (!(Shields[i].IsFree || Shields[i].IsOpenToUser) && Shields[i].ShieldPrice > PlayerVariables.Inistance.getCurrentGold())
            {
                ContentHolder.transform.GetChild(i).GetChild(0).GetChild(1).GetComponent<Button>().interactable = false;
            }
        }

        //save in play fab
        //as bought -- and selected -- and money
    }
}
