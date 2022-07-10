using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Characters : MonoBehaviour
{
    int[,] arrCharacter = new int[3,5];
    [SerializeField]
    private GameObject 
         mCraftPanel , mPickAmmoPanel, mPickRK01Panel, mPickRK02Panel, mPickRK03Panel, mPickLockPickPanel
        , mGameOverPanel;
    [SerializeField]
    private Button 
         mFeedStevenButton, mFeedCindyButton, mFeedLisaButton
        , mBtnBedSteven, mBtnBedCindy, mBtnBedLisa
        , mBtnStevenSleep, mBtnStevenHeal, mBtnCindySleep, mBtnCindyHeal, mBtnLisaSleep, mBtnLisaHeal
        , mWorkPlaceButton, mCloseWorkPlaceButton
        ,mAmmoCraftButton, mRK1CraftButton, mRK2CraftButton, mRK3CraftButton, mLockPickButton
        ,mBtnAmmoClose, mBtnRK01Close, mBtnRK02Close, mBtnRK03Close, mBtnLockPickClose
        , mStevenAmmoButton, mStevenRK01Button, mStevenRK02Button, mStevenRK03Button, mStevenLockPickButton
        , mCindyAmmoButton, mCindyRK01Button, mCindyRK02Button, mCindyRK03Button, mCindyLockPickButton
        , mLisaAmmoButton, mLisaRK01Button, mLisaRK02Button, mLisaRK03Button, mLisaLockPickButton;
    [SerializeField]
    private Text mTextStevenHealth, mTextStevenHunger, mTextStevenState
        , mTextCindyHealth, mTextCindyHunger, mTextCindyState
        , mTextLisaHealth, mTextLisaHunger, mTextLisaState;
    private bool mIsStevenSleeping= false, mIsCindySleeping = false, mIsLisaSleeping = false;
    private void Start()
    {
        arrCharacter[StringHelper.STEVEN, StringHelper.HEALTH] = 100;
        arrCharacter[StringHelper.STEVEN, StringHelper.MAX_HEALTH] = 100;
        arrCharacter[StringHelper.STEVEN, StringHelper.HUNGER] = 0;
        arrCharacter[StringHelper.STEVEN, StringHelper.MAX_HUNGER] = 100;
        arrCharacter[StringHelper.STEVEN, StringHelper.STATUS] = StringHelper.NORMAL;
        arrCharacter[StringHelper.CINDY, StringHelper.HEALTH] = 80;
        arrCharacter[StringHelper.CINDY, StringHelper.MAX_HEALTH] = 80;
        arrCharacter[StringHelper.CINDY, StringHelper.HUNGER] = 0;
        arrCharacter[StringHelper.CINDY, StringHelper.MAX_HUNGER] = 100;
        arrCharacter[StringHelper.CINDY, StringHelper.STATUS] = StringHelper.NORMAL;
        arrCharacter[StringHelper.LISA, StringHelper.HEALTH] = 80;
        arrCharacter[StringHelper.LISA, StringHelper.MAX_HEALTH] = 80;
        arrCharacter[StringHelper.LISA, StringHelper.HUNGER] = 0;
        arrCharacter[StringHelper.LISA, StringHelper.MAX_HUNGER] = 100;
        arrCharacter[StringHelper.LISA, StringHelper.STATUS] = StringHelper.NORMAL;

        mFeedStevenButton.onClick.AddListener(()=>feedWho(StringHelper.STEVEN));
        mFeedCindyButton.onClick.AddListener(() => feedWho(StringHelper.CINDY));
        mFeedLisaButton.onClick.AddListener(() => feedWho(StringHelper.LISA));
        mBtnBedSteven.onClick.AddListener(openStevenBedOption);
        mBtnBedCindy.onClick.AddListener(openCindyBedOption);
        mBtnBedLisa.onClick.AddListener(openLisaBedOption);
        mBtnStevenSleep.onClick.AddListener(()=>sleepWho(mIsStevenSleeping, mBtnStevenSleep, mBtnStevenHeal));
        mBtnStevenHeal.onClick.AddListener(()=>healWho(StringHelper.STEVEN, mBtnStevenSleep, mBtnStevenHeal, mBtnBedSteven));
        mBtnCindySleep.onClick.AddListener(() => sleepWho(mIsCindySleeping, mBtnCindySleep, mBtnCindyHeal));
        mBtnCindyHeal.onClick.AddListener(() => healWho(StringHelper.CINDY, mBtnCindySleep, mBtnCindyHeal, mBtnBedCindy));
        mBtnLisaSleep.onClick.AddListener(() => sleepWho(mIsLisaSleeping, mBtnLisaSleep, mBtnLisaHeal));
        mBtnLisaHeal.onClick.AddListener(() => healWho(StringHelper.LISA, mBtnLisaSleep, mBtnLisaHeal, mBtnBedLisa));
        mWorkPlaceButton.onClick.AddListener(() => controlCraftPanel(true));
        mCloseWorkPlaceButton.onClick.AddListener(() => controlCraftPanel(false));
        mAmmoCraftButton.onClick.AddListener(()=> openItemPanel(mPickAmmoPanel, mStevenAmmoButton, mCindyAmmoButton, mLisaAmmoButton));
        mRK1CraftButton.onClick.AddListener(() => openItemPanel(mPickRK01Panel, mStevenRK01Button, mCindyRK01Button, mLisaRK01Button));
        mRK2CraftButton.onClick.AddListener(() => openItemPanel(mPickRK02Panel, mStevenRK02Button, mCindyRK02Button, mLisaRK02Button));
        mRK3CraftButton.onClick.AddListener(() => openItemPanel(mPickRK03Panel, mStevenRK03Button, mCindyRK03Button, mLisaRK03Button));
        mLockPickButton.onClick.AddListener(() => openItemPanel(mPickLockPickPanel, mStevenLockPickButton, mCindyLockPickButton, mLisaLockPickButton));
        mBtnAmmoClose.onClick.AddListener(()=>closeItemPanel(mPickAmmoPanel));
        mBtnRK01Close.onClick.AddListener(() => closeItemPanel(mPickRK01Panel));
        mBtnRK02Close.onClick.AddListener(() => closeItemPanel(mPickRK02Panel));
        mBtnRK03Close.onClick.AddListener(() => closeItemPanel(mPickRK03Panel));
        mBtnLockPickClose.onClick.AddListener(() => closeItemPanel(mPickLockPickPanel));
        mStevenAmmoButton.onClick.AddListener(() => craftAmmoWho(StringHelper.STEVEN));
        mStevenRK01Button.onClick.AddListener(() => craftRK01Who(StringHelper.STEVEN));
        mStevenRK02Button.onClick.AddListener(() => craftRK02Who(StringHelper.STEVEN));
        mStevenRK03Button.onClick.AddListener(() => craftRK03Who(StringHelper.STEVEN));
        mStevenLockPickButton.onClick.AddListener(() => craftLockPickWho(StringHelper.STEVEN));
        mCindyAmmoButton.onClick.AddListener(() => craftAmmoWho(StringHelper.CINDY));
        mCindyRK01Button.onClick.AddListener(() => craftRK01Who(StringHelper.CINDY));
        mCindyRK02Button.onClick.AddListener(() => craftRK02Who(StringHelper.CINDY));
        mCindyRK03Button.onClick.AddListener(() => craftRK03Who(StringHelper.CINDY));
        mCindyLockPickButton.onClick.AddListener(() => craftLockPickWho(StringHelper.CINDY));
        mLisaAmmoButton.onClick.AddListener(() => craftAmmoWho(StringHelper.LISA));
        mLisaRK01Button.onClick.AddListener(() => craftRK01Who(StringHelper.LISA));
        mLisaRK02Button.onClick.AddListener(() => craftRK02Who(StringHelper.LISA));
        mLisaRK03Button.onClick.AddListener(() => craftRK03Who(StringHelper.LISA));
        mLisaLockPickButton.onClick.AddListener(() => craftLockPickWho(StringHelper.LISA));
    }
    private void FixedUpdate()
    {
        mTextStevenHealth.text 
            = string.Format("Health : {0} / {1}"
            , arrCharacter[StringHelper.STEVEN, StringHelper.HEALTH]
            , arrCharacter[StringHelper.STEVEN, StringHelper.MAX_HEALTH]);
        mTextStevenHunger.text 
            = string.Format("Hunger : {0} / {1}"
            , arrCharacter[StringHelper.STEVEN, StringHelper.HUNGER]
            , arrCharacter[StringHelper.STEVEN, StringHelper.MAX_HUNGER]);
        mTextStevenState.text
            = string.Format("State : {0}"
            , makeStateVisible(arrCharacter[StringHelper.STEVEN, StringHelper.STATUS]));
        mTextCindyHealth.text
            = string.Format("Health : {0} / {1}"
            , arrCharacter[StringHelper.CINDY, StringHelper.HEALTH]
            , arrCharacter[StringHelper.CINDY, StringHelper.MAX_HEALTH]);
        mTextCindyHunger.text
            = string.Format("Hunger : {0} / {1}"
            , arrCharacter[StringHelper.CINDY, StringHelper.HUNGER]
            , arrCharacter[StringHelper.CINDY, StringHelper.MAX_HUNGER]);
        mTextCindyState.text
            = string.Format("State : {0}"
            , makeStateVisible(arrCharacter[StringHelper.CINDY, StringHelper.STATUS]));
        mTextLisaHealth.text
            = string.Format("Health : {0} / {1}"
            , arrCharacter[StringHelper.LISA, StringHelper.HEALTH]
            , arrCharacter[StringHelper.LISA, StringHelper.MAX_HEALTH]);
        mTextLisaHunger.text
            = string.Format("Hunger : {0} / {1}"
            , arrCharacter[StringHelper.LISA, StringHelper.HUNGER]
            , arrCharacter[StringHelper.LISA, StringHelper.MAX_HUNGER]);
        mTextLisaState.text
            = string.Format("State : {0}"
            , makeStateVisible(arrCharacter[StringHelper.LISA, StringHelper.STATUS]));
        checkCafeteriaButton(StringHelper.STEVEN, mFeedStevenButton);
        checkCafeteriaButton(StringHelper.CINDY, mFeedCindyButton);
        checkCafeteriaButton(StringHelper.LISA, mFeedLisaButton);
        if (arrCharacter[StringHelper.STEVEN, StringHelper.HEALTH] == 0)
        {
            mGameOverPanel.SetActive(true);
        }
    }
    private void controlCraftPanel(bool pIsOpened)
    {
        mCraftPanel.SetActive(pIsOpened);
    }
    private void openItemPanel(GameObject pItemPanel, Button pStevenItem, Button pCindyItem, Button pLisaItem)
    {
        pItemPanel.SetActive(true);
        if (mIsStevenSleeping == true)
        {
            pStevenItem.interactable = false;
        }
        if (mIsCindySleeping == true)
        {
            pCindyItem.interactable = false;
        }
        if (mIsLisaSleeping == true)
        {
            pLisaItem.interactable = false;
        }
    }

    private void closeItemPanel(GameObject pItemPanel)
    {
        pItemPanel.SetActive(false);
    }

    private string makeStateVisible(int pArrStatus)
    {
        string status = "";
        switch(pArrStatus)
        {
            case 0 : 
                status = "NORMAL";
                break;
            case 1:
                status = "DISEASE";
                break;
            case 2:
                status = "INFECTION";
                break;
            case 3:
                status = "DRAINED";
                break;
            case 4:
                status = "HUNGRY";
                break;
        }
        return status;
    }
    public void outNight(int pChar, int pValueHp, int pValue)
    {
        arrCharacter[pChar, StringHelper.HEALTH] -= pValueHp;
        arrCharacter[pChar, StringHelper.HUNGER] += pValue;
    }
    public void feed(int pWho)
    {
        if(arrCharacter[pWho, StringHelper.HEALTH] + 20 <= arrCharacter[pWho, StringHelper.MAX_HEALTH])
        {
            arrCharacter[pWho, StringHelper.HEALTH] += 20;
        }
        else if(arrCharacter[pWho, StringHelper.HEALTH] + 20 > arrCharacter[pWho, StringHelper.MAX_HEALTH])
        {
            arrCharacter[pWho, StringHelper.HEALTH] = arrCharacter[pWho, StringHelper.MAX_HEALTH];
        }
        if (arrCharacter[pWho, StringHelper.HUNGER] - 40 >= 0 )
        {
            arrCharacter[pWho, StringHelper.HUNGER] -= 40;
        }
        else if (arrCharacter[pWho, StringHelper.HUNGER] - 40 < arrCharacter[pWho, StringHelper.MAX_HUNGER])
        {
            arrCharacter[pWho, StringHelper.HUNGER] = 0;
        }
    }
    public void eat(int pWho, int pHunger)
    {
        if(arrCharacter[pWho, StringHelper.HUNGER] - pHunger >= 0)
        {
            arrCharacter[pWho, StringHelper.HUNGER] -= pHunger;
        }
        else if (arrCharacter[pWho, StringHelper.HUNGER] - pHunger < 0)
        {
            arrCharacter[pWho, StringHelper.HUNGER] = 0;
        }
    }
    public void getStatus(int pWho,int  pStatus)
    {
        arrCharacter[pWho, StringHelper.STATUS] = pStatus;
    }
    private void feedWho(int pWho)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controllerFood = controllObj.GetComponent<Items>();
        controllerFood.useItem(StringHelper.FOOD, 1);
        feed(pWho);
    }
    private bool checkCharacterEatable(int pCharacter)
    {
        if(arrCharacter[pCharacter, StringHelper.HEALTH] != arrCharacter[pCharacter, StringHelper.MAX_HEALTH]
            || arrCharacter[pCharacter, StringHelper.HUNGER] != arrCharacter[pCharacter, StringHelper.MAX_HUNGER])
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void checkCafeteriaButton(int pCharacter, Button pButton)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        if (controller.checkItemAmount(StringHelper.FOOD, 1) && checkCharacterEatable(pCharacter))
        {
            pButton.interactable = true;
        }
        else
        {
            pButton.interactable = false;
        }
    }

    private void openStevenBedOption()
    {
        mBtnBedSteven.interactable = false;
        if(mIsCindySleeping == false)
        {
            mBtnBedCindy.interactable = true;
        }
        if (mIsLisaSleeping == false)
        {
            mBtnBedLisa.interactable = true;
        }
        mBtnStevenSleep.gameObject.SetActive(true);
        mBtnStevenHeal.gameObject.SetActive(true);
        if(arrCharacter[StringHelper.STEVEN,StringHelper.STATUS] == StringHelper.NORMAL)
        {
            mBtnStevenHeal.interactable = false;
        }
    }
    private void openCindyBedOption()
    {
        mBtnBedCindy.interactable = false;
        if (mIsStevenSleeping == false)
        {
            mBtnBedSteven.interactable = true;
        }
        if (mIsLisaSleeping == false)
        {
            mBtnBedLisa.interactable = true;
        }
        mBtnCindySleep.gameObject.SetActive(true);
        mBtnCindyHeal.gameObject.SetActive(true);
        if (arrCharacter[StringHelper.CINDY, StringHelper.STATUS] == StringHelper.NORMAL)
        {
            mBtnStevenHeal.interactable = false;
        }
    }
    private void openLisaBedOption()
    {
        mBtnBedLisa.interactable = false;
        if (mIsStevenSleeping == false)
        {
            mBtnBedSteven.interactable = true;
        }
        if (mIsCindySleeping == false)
        {
            mBtnBedCindy.interactable = true;
        }
        mBtnLisaSleep.gameObject.SetActive(true);
        mBtnLisaHeal.gameObject.SetActive(true);
        if (arrCharacter[StringHelper.LISA, StringHelper.STATUS] == StringHelper.NORMAL)
        {
            mBtnStevenHeal.interactable = false;
        }
    }
    
    private void sleepWho(bool pWhoSleeping, Button pWhoSleep, Button pWhoHeal)
    {
        pWhoSleeping = true;
        pWhoSleep.gameObject.SetActive(false);
        pWhoHeal.gameObject.SetActive(false);
    }
    private void healWho(int pWho, Button pWhoSleep, Button pWhoHeal, Button pBtnWhoBed)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        controller.useItem(StringHelper.MEDIC_KITS, 1);
        arrCharacter[pWho, StringHelper.STATUS] = StringHelper.NORMAL;
        pWhoSleep.gameObject.SetActive(false);
        pWhoHeal.gameObject.SetActive(false);
        pBtnWhoBed.interactable = true;
    }

    private void craftAmmoWho(int pWho)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        controller.useItem(StringHelper.GUN_POWDER, 4);
        controller.useItem(StringHelper.IRONS, 2);
        controller.getItem(StringHelper.AMMO, 1);
        arrCharacter[pWho, StringHelper.HEALTH] -= 10;
        mPickAmmoPanel.SetActive(false);
    }
    private void craftRK01Who(int pWho)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        controller.useItem(StringHelper.WOODS, 4);
        controller.useItem(StringHelper.NAILS, 2);
        controller.getItem(StringHelper.REPAIR_KIT_1, 1);
        arrCharacter[pWho, StringHelper.HEALTH] -= 10;
        mPickAmmoPanel.SetActive(false);
    }
    private void craftRK02Who(int pWho)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        controller.useItem(StringHelper.IRONS, 2);
        controller.useItem(StringHelper.NAILS, 2);
        controller.getItem(StringHelper.REPAIR_KIT_2, 1);
        arrCharacter[pWho, StringHelper.HEALTH] -= 20;
        mPickRK02Panel.SetActive(false);
    }
    private void craftRK03Who(int pWho)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        controller.useItem(StringHelper.IRONS, 4);
        controller.useItem(StringHelper.NAILS, 2);
        controller.getItem(StringHelper.REPAIR_KIT_3, 1);
        arrCharacter[pWho, StringHelper.HEALTH] -= 30;
        mPickRK03Panel.SetActive(false);
    }
    private void craftLockPickWho(int pWho)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        controller.useItem(StringHelper.IRONS, 1);
        controller.useItem(StringHelper.WOODS, 1);
        controller.getItem(StringHelper.LOCK_PICKS, 1);
        arrCharacter[pWho, StringHelper.HEALTH] -= 30;
        mPickLockPickPanel.SetActive(false);
    }
    public void wakeSteven()
    {
        mIsStevenSleeping = false;
    }
    public void wakeCindy()
    {
        mIsCindySleeping = false;
    }
    public void wakeLisa()
    {
        mIsLisaSleeping = false;
    }
}
