using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    // Button Image -->         0 >> Level Number,  1 >> Lock Image
    [Header("Buttons & Game Objects")]
    public GameObject[] ButtonsLevels;
    public Button ButtonRight, ButtonLeft;
    public Image ImageSword, ImageSwordLock; // 0 >> Lock Image

    [Header("Sprites")]
    public Sprite[] SpritesLevels;
    public Sprite[] SpritesSwords;
    public Sprite SpriteLockLevel, SpriteLockSword;

    private int CurrentPanel = 0;

    private void Start()
    {
        int PlayerLevel = PlayerVariables.Inistance.getCurrentLevel();
        if (PlayerLevel > 100)
            PlayerLevel = 100;
        int PlayerPanel = Mathf.FloorToInt((float)((PlayerLevel / 10f) - 0.1));// 0 to 9
        CurrentPanel = PlayerPanel;
        //setDataOnUI();
    }

    public void setDataOnUI()
    {
        if(CurrentPanel <= 0)
        {
            ButtonLeft.gameObject.SetActive(false);
            ButtonRight.gameObject.SetActive(true);
        }
        else if (CurrentPanel >= 9)
        {
            ButtonLeft.gameObject.SetActive(true);
            ButtonRight.gameObject.SetActive(false);
        }
        else
        {
            ButtonLeft.gameObject.SetActive(true);
            ButtonRight.gameObject.SetActive(true);
        }

        int PlayerLevel = PlayerVariables.Inistance.getCurrentLevel();
        if (PlayerLevel > 100)
            PlayerLevel = 100;
        int PlayerPanel = Mathf.FloorToInt((float)((PlayerLevel / 10f) - 0.1));// 0 to 9

        int LevelIndex = (PlayerLevel - (PlayerPanel * 10)) - 1;//0 to 9
        for(int i = 0; i < ButtonsLevels.Length; i++)
        {
            ButtonsLevels[i].GetComponent<Image>().sprite = SpritesLevels[CurrentPanel];
            ButtonsLevels[i].transform.GetChild(0).GetComponent<Text>().text = "";

            if (CurrentPanel < PlayerPanel)
            {
                //unlocked
                ButtonsLevels[i].GetComponent<Button>().interactable = true;
                ButtonsLevels[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
                ButtonsLevels[i].transform.GetChild(1).gameObject.SetActive(false);
                ButtonsLevels[i].transform.GetChild(0).GetComponent<Text>().text = ((CurrentPanel * 10) + i + 1).ToString();
            }
            else if(CurrentPanel > PlayerPanel)
            {
                //locked
                ButtonsLevels[i].GetComponent<Button>().interactable = false;
                ButtonsLevels[i].transform.GetChild(1).GetComponent<Image>().sprite = SpriteLockLevel;
                ButtonsLevels[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            else
            {
                if (i <= LevelIndex)
                {
                    //unlocked
                    ButtonsLevels[i].GetComponent<Button>().interactable = true;
                    ButtonsLevels[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
                    ButtonsLevels[i].transform.GetChild(1).gameObject.SetActive(false);
                    ButtonsLevels[i].transform.GetChild(0).GetComponent<Text>().text = ((CurrentPanel * 10) + i + 1).ToString();
                }
                else
                {
                    //locked
                    ButtonsLevels[i].GetComponent<Button>().interactable = false;
                    ButtonsLevels[i].transform.GetChild(1).GetComponent<Image>().sprite = SpriteLockLevel;
                    ButtonsLevels[i].transform.GetChild(1).gameObject.SetActive(true);
                }
            }
            
        }

        ImageSword.sprite = SpritesSwords[CurrentPanel];
        if (CurrentPanel >= PlayerPanel && PlayerLevel < 100)
        {
            ImageSwordLock.sprite = SpriteLockSword;
            ImageSwordLock.gameObject.SetActive(true);
        }
        else
        {
            ImageSwordLock.sprite = null;
            ImageSwordLock.gameObject.SetActive(false);
        }

    }

    public void OnRightButtonClick()
    {
        CurrentPanel++;
        setDataOnUI();
    }

    public void OnLeftButtonClick()
    {
        CurrentPanel--;
        setDataOnUI();
    }

    public void OnLevelButtonClick()
    {
        Debug.Log("Opening Level............");
    }

}
