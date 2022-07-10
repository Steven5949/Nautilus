using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Button mItemButton, mCancelItemPanelButton;
    [SerializeField]
    private Button mSettingButton, mCancelSettingPanelButton, mGotoMainSceneButton, mExitGameButton
        , mDayEventCloseButton, mDayFightButton, mDayRunButton
        , mCafeteriaButton, mCancelCafeteriaPanelButton, mBedButton, mCancelBedPanelButton, mJournalButton, mCancelJournalPanelButton
        , mNightSteven, mNightCindy, mNightLisa
        , mStevenSkipNight, mStevenNightStart, mStevenNightClose, mCindySkipNight, mCindyNightStart, mCindyNightClose, mLisaSkipNight, mLisaNightStart, mLisaNightClose
        , mStevenNightOption01, mStevenNightOption02, mCindyNightOption01, mCindyNightOption02, mLisaNightOption01, mLisaNightOption02;
    [SerializeField]
    private GameObject mItemPanel, mSettingPanel, mDayEventPanel, mNightEventPanel, mCafeteriaPanel, mBedPanel , mJournalPanel
        ,mLoseGamePanel, mWinGamePanel
        ,mPickNightEventPanel;
    [SerializeField]
    private Text mTextTimeCounter, mTextDayCounter , mJournalDay, mJournalContext;
    [SerializeField]
    private Image mHPBarImage, mTimeNeedle;
    [SerializeField]
    private Text mTextHP, mTextNightEventTopic, mTextNightEventContent, mTextDayEventTopic, mTextDayEventContent;
    [SerializeField]
    private Text mTxtEventReport;
    private float mTimeCounter = StringHelper.DAY_TIME;
    private float mDayEventCounter = StringHelper.DAY_EVENT_COUNT_TIME;
    private float mSubmarineHP = StringHelper.SUBMARINE_HP;
    private int mDayTime = 1, mDayEventTime;
    private int mDayRandomNumMemory = 0;
    private int mRandEvent = 0;
    private bool mIsStevenOut = false, mIsCindyOut = false, mIsLisaOut = false;
    private bool mGotNews = true;
    private bool mMushroomEvent = false, mVisitorEvent = false;
    
    private void Start()
    {
        mDayEventTime = 2;
        mItemButton.onClick.AddListener(() => openPanelAction(mItemPanel));
        mCancelItemPanelButton.onClick.AddListener(() => closePanelAction(mItemPanel));
        mBedButton.onClick.AddListener(() => openPanelAction(mBedPanel));
        mCancelBedPanelButton.onClick.AddListener(() => closePanelAction(mBedPanel));
        mJournalButton.onClick.AddListener(() => openPanelAction(mJournalPanel));
        mCancelJournalPanelButton.onClick.AddListener(() => closePanelAction(mJournalPanel));
        mCafeteriaButton.onClick.AddListener(() => openPanelAction(mCafeteriaPanel));
        mCancelCafeteriaPanelButton.onClick.AddListener(() => closePanelAction(mCafeteriaPanel));
        mSettingButton.onClick.AddListener(openSettingOption);
        mCancelSettingPanelButton.onClick.AddListener(closeSettingPanelAction);
        mGotoMainSceneButton.onClick.AddListener(gotoMainScene);
        mExitGameButton.onClick.AddListener(quitGame);

        mDayEventCloseButton.onClick.AddListener(quitDayEvent);
        mDayFightButton.onClick.AddListener(fightOption);
        mDayRunButton.onClick.AddListener(runOption);
        mStevenSkipNight.onClick.AddListener(stevenSkip);
        mStevenNightStart.onClick.AddListener(() => whoNightEvent(mStevenSkipNight, mStevenNightStart, mIsStevenOut, mStevenNightClose
                                , mStevenNightOption01, mStevenNightOption02, StringHelper.STEVEN));
        mStevenNightClose.onClick.AddListener(openCindyNightEvent);
        mCindySkipNight.onClick.AddListener(cindySkip);
        mCindyNightStart.onClick.AddListener(() => whoNightEvent(mCindySkipNight, mCindyNightStart, mIsCindyOut, mCindyNightClose
                                , mCindyNightOption01, mCindyNightOption02, StringHelper.CINDY));
        mCindyNightClose.onClick.AddListener(openLisaNightEvent);
        mLisaSkipNight.onClick.AddListener(lisaSkip);
        mLisaNightStart.onClick.AddListener(() => whoNightEvent(mLisaSkipNight, mLisaNightStart, mIsLisaOut, mLisaNightClose
                                , mLisaNightOption01, mLisaNightOption02, StringHelper.LISA));
        mLisaNightClose.onClick.AddListener(quitNightEvent);
        mStevenNightOption01.onClick.AddListener(StevenSpecialEvent);
        mStevenNightOption02.onClick.AddListener(() => whoNothingHappens(mStevenNightOption01, mStevenNightOption02, mStevenNightClose));
        mCindyNightOption01.onClick.AddListener(CindySpecialEvent);
        mCindyNightOption02.onClick.AddListener(() => whoNothingHappens(mCindyNightOption01, mCindyNightOption02, mCindyNightClose));
        mLisaNightOption01.onClick.AddListener(LisaSpecialEvent);
        mLisaNightOption02.onClick.AddListener(() => whoNothingHappens(mLisaNightOption01, mLisaNightOption02, mLisaNightClose));
    }
    private void FixedUpdate()
    {
       showSubmarineHPBar();
       if(mTimeCounter > 0)
       {
            mTimeCounter -= Time.deltaTime;
            mDayEventCounter -= Time.deltaTime;
            if(Time.deltaTime > 0)
            {
                mTimeNeedle.transform.localRotation = Quaternion.Euler(0,0, mTimeCounter * 3 -90);
            }
       }
       else if(mTimeCounter <= 0 && mTimeCounter != -20f)
       {
            Time.timeScale = 0f;
            mPickNightEventPanel.SetActive(true);
            openStevenNightEvent();
            mTimeCounter = -20f;
        }
       if(mDayEventCounter <= 0 && mDayEventTime > 0)
       {
            Time.timeScale = 0;
            mTimeNeedle.transform.Rotate(0, 0, 0f);
            showDayEvent();
            mDayEventCounter = StringHelper.DAY_EVENT_COUNT_TIME;
            mDayEventTime--;
       }
        mTextTimeCounter.text = string.Format("{0}", mTimeCounter);
        mTextDayCounter.text = string.Format("Day - {0}", mDayTime.ToString("D2"));
        mJournalDay.text = string.Format("Day - {0}", (mDayTime-1).ToString("D2"));
        if (mSubmarineHP <= 0)
        {
            Time.timeScale = 0f;
            mLoseGamePanel.SetActive(true);
        }
    }
    private void openPanelAction(GameObject pPanelName)
    {
        pPanelName.SetActive(true);
    }
    private void closePanelAction(GameObject pPanelName)
    {
        pPanelName.SetActive(false);
    }
    private void openSettingOption()
    {
        Time.timeScale = 0f;
        mSettingPanel.SetActive(true);
    }
    private void closeSettingPanelAction()
    {
        mSettingPanel.SetActive(false);
        Time.timeScale = 1f;
    }
    private void gotoMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }
    private void quitGame()
    {
        Application.Quit();
    }
    private void showSubmarineHPBar()
    {
        mHPBarImage.fillAmount = mSubmarineHP / StringHelper.SUBMARINE_HP;
        mTextHP.text = string.Format("HP : {0}/100", mSubmarineHP);
    }
    public void fixSubmarine(int pValue)
    {
        if (mSubmarineHP + pValue <= StringHelper.SUBMARINE_HP)
        {
            mSubmarineHP += pValue;
        }
        else if (mSubmarineHP + pValue > StringHelper.SUBMARINE_HP)
        {
            mSubmarineHP = StringHelper.SUBMARINE_HP;
        }
    }
    public bool checkSubmarineHp()
    {
        if (mSubmarineHP >= 100)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void showDayEvent()
    {
        int randDayEvent = Random.Range(0,100);
        mDayRandomNumMemory = randDayEvent;
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        if(randDayEvent >=0 && randDayEvent < 20)
        {
            mDayEventPanel.SetActive(true);
            mDayEventCloseButton.gameObject.SetActive(false);
                mTextDayEventTopic.text = "Attacked";
                mTextDayEventContent.text = "Mutant Creature is Attacking our Ship!!!";
                if(controller.checkItemAmount(StringHelper.AMMO, 2))
                {
                    mDayFightButton.gameObject.SetActive(true);
                    mDayRunButton.gameObject.SetActive(true);
                }
                else if(!controller.checkItemAmount(StringHelper.AMMO, 2))
                {
                    mDayFightButton.gameObject.SetActive(true);
                    mDayRunButton.gameObject.SetActive(true);
                    mDayFightButton.interactable = false;
                }
            
        }
        else if(randDayEvent >= 20 && randDayEvent < 40)
        {
            mDayEventPanel.SetActive(true);
            mDayFightButton.gameObject.SetActive(false);
            mDayRunButton.gameObject.SetActive(false);
            if (randDayEvent >= 20 && randDayEvent < 36)
            {
                mTextDayEventTopic.text = "Repair Submarine";
                mTextDayEventContent.text = "Failed to repair submarine...";
                mJournalContext.text += "Failed to repair submarine...\n";
                mSubmarineHP -= 20;
            }
            if (randDayEvent >= 36  && randDayEvent < 40)
            {
                mTextDayEventTopic.text = "Repair Submarine";
                mTextDayEventContent.text = "Successfully repaired.";
                mJournalContext.text += "Successfully  repaired.\n";
            }
        }
        else 
        {
            Time.timeScale = 1f;
            return;
        }
    }
    private void fightOption()
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        if (mDayRandomNumMemory >= 0 && mDayRandomNumMemory < 16)
        {
            controller.useItem(StringHelper.AMMO, 1);
            mTextDayEventContent.text = "Attacked by Mutant Creature. \n but successfully defended.";
            mJournalContext.text += "Attacked by Mutant Creature. \n but successfully defended. \n";
        }
        else if (mDayRandomNumMemory >= 16 && mDayRandomNumMemory < 18)
        {
            controller.useItem(StringHelper.AMMO, 1);
            mSubmarineHP -= 30;
            mTextDayEventContent.text = "Attacked by Mutant Creature. \n Successfully defended, but Submarine got damage.";
            mJournalContext.text += "Attacked by Mutant Creature. \n Successfully defended, but Submarine got damage. \n";
        }
        else if(mDayRandomNumMemory >= 18 && mDayRandomNumMemory < 20)
        {
            controller.useItem(StringHelper.AMMO, 2);
            mSubmarineHP -= 50;
            mTextDayEventContent.text = "Attacked by Mutant Creature. \n Successfully defended, but Submarine got seriously damage.";
            mJournalContext.text += "Attacked by Mutant Creature. \n Successfully defended, but Submarine got seriously damage. \n";
        }
        mDayFightButton.gameObject.SetActive(false);
        mDayRunButton.gameObject.SetActive(false);
        mDayEventCloseButton.gameObject.SetActive(true);
    }
    private void runOption()
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        if (mDayRandomNumMemory >= 0 && mDayRandomNumMemory < 10)
        {
            controller.useItem(StringHelper.AMMO, 1);
            mTextDayEventContent.text = "Attacked by Mutant Creature. \n Successfully ran away.";
            mJournalContext.text += "Attacked by Mutant Creature. \n Successfully ran away.\n";
        }
        else if (mDayRandomNumMemory >= 10 && mDayRandomNumMemory < 15)
        {
            controller.useItem(StringHelper.AMMO, 1);
            mSubmarineHP -= 30;
            mTextDayEventContent.text = "Attacked by Mutant Creature. \n Successfully ran away, but Submarine got damage.";
            mJournalContext.text += "Attacked by Mutant Creature. \n Successfully ran away, but Submarine got damage.\n";
        }
        else if (mDayRandomNumMemory >= 15 && mDayRandomNumMemory < 20)
        {
            controller.useItem(StringHelper.AMMO, 2);
            mSubmarineHP -= 50;
            mTextDayEventContent.text = "Attacked by Mutant Creature. \n Successfully ran away, but Submarine got seriously damage.";
            mJournalContext.text += "Attacked by Mutant Creature. \n Successfully ran away, but Submarine got seriously damage. \n";
        }
        mDayFightButton.gameObject.SetActive(false);
        mDayRunButton.gameObject.SetActive(false);
        mDayEventCloseButton.gameObject.SetActive(true);
    }
    private void quitDayEvent()
    {
        mDayEventPanel.SetActive(false);
        Time.timeScale = 1f;
        mTimeNeedle.transform.Rotate(0, 0, -0.0137f);
    }
    private void openStevenNightEvent()
    {
        mTextNightEventTopic.text = "";
        mTextNightEventContent.text = "What will he do?";
        mStevenNightStart.gameObject.SetActive(true);
        mStevenSkipNight.gameObject.SetActive(true);
    }
    private void openCindyNightEvent()
    {
        mNightSteven.gameObject.SetActive(false);
        mStevenNightClose.gameObject.SetActive(false);
        mNightCindy.gameObject.SetActive(true);
        mCindySkipNight.gameObject.SetActive(true);
        mCindyNightStart.gameObject.SetActive(true);
        mCindyNightClose.gameObject.SetActive(false);
        mTextNightEventTopic.text = "";
        mTextNightEventContent.text = "What will she do?";
    }
    private void openLisaNightEvent()
    {
        mNightCindy.gameObject.SetActive(false);
        mCindyNightClose.gameObject.SetActive(false);
        mNightLisa.gameObject.SetActive(true);
        mLisaSkipNight.gameObject.SetActive(true);
        mLisaNightStart.gameObject.SetActive(true);
        mLisaNightClose.gameObject.SetActive(false);
        mTextNightEventTopic.text = "";
        mTextNightEventContent.text = "What will she do?";
    }
    private void stevenSkip()
    {
        mTextNightEventContent.text = "Steven skipped the Night!";
        mStevenSkipNight.gameObject.SetActive(false);
        mStevenNightStart.gameObject.SetActive(false);
        mStevenNightClose.gameObject.SetActive(true);
    }
    private void cindySkip()
    {
        mTextNightEventContent.text = "Cindy skipped the Night!";
        mCindySkipNight.gameObject.SetActive(false);
        mCindyNightStart.gameObject.SetActive(false);
        mCindyNightClose.gameObject.SetActive(true);
    }
    private void lisaSkip()
    {
        mTextNightEventContent.text = "Lisa skipped the Night!";
        mLisaSkipNight.gameObject.SetActive(false);
        mLisaNightStart.gameObject.SetActive(false);
        mLisaNightClose.gameObject.SetActive(true);
    }

    private void whoNightEvent(Button pWhoSkipNight, Button pWhoNightStart, bool pIsWhoOut, Button pWhoNightClose
                                , Button pWhoNightOption01, Button pWhoNightOption02, int pWho)
    {
        pWhoSkipNight.gameObject.SetActive(false);
        pWhoNightStart.gameObject.SetActive(false);
        pIsWhoOut = true;
        int randNightEvent = Random.Range(0, 100);
        mRandEvent = randNightEvent;
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Items controller = controllObj.GetComponent<Items>();
        Characters controllerChar = controllObj.GetComponent<Characters>();
        if (randNightEvent >= 0 && randNightEvent < 10)
        {
            mTextNightEventTopic.text = "Tuna Can";
            if (randNightEvent >= 0 && randNightEvent < 6)
            {
                controller.getItem(StringHelper.FOOD, 1);
                mTextNightEventContent.text = "Found Tuna Can.";
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 6 && randNightEvent < 9)
            {
                controller.getItem(StringHelper.FOOD, 2);
                mTextNightEventContent.text = "Found Tuna Can.";
                pWhoNightClose.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 10 && randNightEvent < 20)
        {
            mTextNightEventTopic.text = "Regular Mushroom";
            controller.getItem(StringHelper.FOOD, 1);
            mTextNightEventContent.text = "Found Regular Mushroom.";
            pWhoNightClose.gameObject.SetActive(true);
        }
        if (randNightEvent >= 20 && randNightEvent < 30)
        {
            mTextNightEventTopic.text = "Water Bottle";
            if (randNightEvent >= 20 && randNightEvent < 27)
            {
                controller.getItem(StringHelper.FOOD, 1);
                mTextNightEventContent.text = "Found Water.";
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 27 && randNightEvent < 28)
            {
                controller.getItem(StringHelper.FOOD, 2);
                mTextNightEventContent.text = "Found Water.";
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 28 && randNightEvent < 29)
            {
                mTextNightEventContent.text = "Empty Bottle...";
                pWhoNightClose.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 30 && randNightEvent < 40)
        {
            mTextNightEventTopic.text = "Ammo";
            if (randNightEvent >= 30 && randNightEvent < 37)
            {
                mTextNightEventContent.text = "Found Ammo.";
                controller.getItem(StringHelper.AMMO, 1);
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 37 && randNightEvent < 38)
            {
                mTextNightEventContent.text = "Found Ammo.";
                controller.getItem(StringHelper.AMMO, 2);
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 38 && randNightEvent < 39)
            {
                mTextNightEventContent.text = "We Can't Use This...";
                pWhoNightClose.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 40 && randNightEvent < 50)
        {
            mTextNightEventTopic.text = "Medic Kit";
            if (randNightEvent >= 40 && randNightEvent < 48)
            {
                mTextNightEventContent.text = "Found Medic Kit";
                controller.getItem(StringHelper.MEDIC_KITS, 1);
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 48 && randNightEvent < 50)
            {
                mTextNightEventContent.text = "Found Medic Kits";
                controller.getItem(StringHelper.MEDIC_KITS, 2);
                pWhoNightClose.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 50 && randNightEvent < 60)
        {
            mTextNightEventTopic.text = "Items";
            if (randNightEvent >= 50 && randNightEvent < 52)
            {
                mTextNightEventContent.text = "Found Some Wood";
                controller.getItem(StringHelper.WOODS, 2);
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 52 && randNightEvent < 54)
            {
                mTextNightEventContent.text = "Found Some Iron";
                controller.getItem(StringHelper.IRONS, 2);
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 54 && randNightEvent < 56)
            {
                mTextNightEventContent.text = "Found Some Gun Powder";
                controller.getItem(StringHelper.GUN_POWDER, 2);
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 56 && randNightEvent < 58)
            {
                mTextNightEventContent.text = "Found Some Nails";
                controller.getItem(StringHelper.NAILS, 2);
                pWhoNightClose.gameObject.SetActive(true);
            }
            if (randNightEvent >= 58 && randNightEvent < 60)
            {
                mTextNightEventContent.text = "Found Some Medic Kits";
                controller.getItem(StringHelper.MEDIC_KITS, 2);
                pWhoNightClose.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 60 && randNightEvent < 70)
        {
            if (mDayTime >= 5)
            {
                mTextNightEventTopic.text = "Food Journal";
                mTextNightEventContent.text = "We Found Food Journal";
                controller.getItem(StringHelper.FOOD_JOURNAL, 1);
                pWhoNightClose.gameObject.SetActive(true);
            }
            else
            {
                mTextNightEventContent.text = "We Found Nothing...";
                pWhoNightClose.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 70 && randNightEvent < 80)
        {
            if (mDayTime >= 5)
            {
                mTextNightEventTopic.text = "News";
                mTextNightEventContent.text = "It's A Good News!!!";
                mGotNews = true;
                pWhoNightClose.gameObject.SetActive(true);
            }
            else
            {
                mTextNightEventContent.text = "We Found Nothing...";
                pWhoNightClose.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 80 && randNightEvent < 90)
        {
            mMushroomEvent = true;
            mTextNightEventTopic.text = "Unusual Mushroom";
            if (controller.checkItemAmount(StringHelper.FOOD_JOURNAL, 1))
            {
                mTextNightEventContent.text = "We Found It's Safe Mushroom Through the Journal";
                controller.useItem(StringHelper.FOOD_JOURNAL, 1);
                controllerChar.eat(pWho, 30);
                pWhoNightClose.gameObject.SetActive(true);
            }
            else if (!controller.checkItemAmount(StringHelper.FOOD_JOURNAL, 1))
            {
                mTextNightEventContent.text = "What Will You Do?";
                pWhoNightOption01.gameObject.SetActive(true);
                pWhoNightOption02.gameObject.SetActive(true);
            }
        }
        if (randNightEvent >= 90 && randNightEvent < 100)
        {
            mTextNightEventTopic.text = "Visitor";
            pWhoNightOption01.gameObject.SetActive(true);
            pWhoNightOption02.gameObject.SetActive(true);
        }
    }
    private void StevenSpecialEvent()
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Characters controllerChar = controllObj.GetComponent<Characters>();
        if (mMushroomEvent == true)
        {
            if (mRandEvent >= 80 && mRandEvent < 84)
            {
                mTextNightEventContent.text = "Eatable But Raw...";
                controllerChar.eat(StringHelper.STEVEN, 30);
                controllerChar.getStatus(StringHelper.STEVEN, StringHelper.DISEASE);
            }
            if (mRandEvent >= 84 && mRandEvent < 88)
            {
                mTextNightEventContent.text = "He Shouldn't Eat that...";
                controllerChar.eat(StringHelper.STEVEN, -40);
                controllerChar.getStatus(StringHelper.STEVEN, StringHelper.INFECTION);
            }
            if (mRandEvent >= 88 && mRandEvent < 90)
            {
                mTextNightEventContent.text = "It was Delicious!";
                controllerChar.eat(StringHelper.STEVEN, 30);
            }
            
        }
        else if (mVisitorEvent == true)
        {
            if (mRandEvent >= 90 && mRandEvent < 95)
            {
                mTextNightEventContent.text = "Visitor Attacked Our Ship!!!";
                mSubmarineHP -= 10;
            }
            if (mRandEvent >= 95 && mRandEvent < 98)
            {
                mWinGamePanel.SetActive(true);
            }
            if (mRandEvent >= 98 && mRandEvent < 100)
            {
                mTextNightEventContent.text = "Visitor Attacked But Safe!";
            }
        }
        mStevenNightOption01.gameObject.SetActive(false);
        mStevenNightOption02.gameObject.SetActive(false);
        mStevenNightClose.gameObject.SetActive(true);
    }
    private void CindySpecialEvent()
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Characters controllerChar = controllObj.GetComponent<Characters>();
        if (mMushroomEvent == true)
        {
            if (mRandEvent >= 80 && mRandEvent < 84)
            {
                mTextNightEventContent.text = "Eatable But Raw...";
                controllerChar.eat(StringHelper.CINDY, 30);
                controllerChar.getStatus(StringHelper.CINDY, StringHelper.DISEASE);
            }
            if (mRandEvent >= 84 && mRandEvent < 88)
            {
                mTextNightEventContent.text = "She Shouldn't Eat that...";
                controllerChar.eat(StringHelper.CINDY, -40);
                controllerChar.getStatus(StringHelper.CINDY, StringHelper.INFECTION);
            }
            if (mRandEvent >= 88 && mRandEvent < 90)
            {
                mTextNightEventContent.text = "It was Delicious!";
                controllerChar.eat(StringHelper.CINDY, 30);
            }

        }
        else if (mVisitorEvent == true)
        {
            if (mRandEvent >= 90 && mRandEvent < 95)
            {
                mTextNightEventContent.text = "Visitor Attacked Our Ship!!!";
                mSubmarineHP -= 10;
            }
            if (mRandEvent >= 95 && mRandEvent < 98)
            {
                mWinGamePanel.SetActive(true);
            }
            if (mRandEvent >= 98 && mRandEvent < 100)
            {
                mTextNightEventContent.text = "Visitor Attacked But Safe!";
            }
        }
        mCindyNightOption01.gameObject.SetActive(false);
        mCindyNightOption02.gameObject.SetActive(false);
        mCindyNightClose.gameObject.SetActive(true);
    }
    private void LisaSpecialEvent()
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Characters controllerChar = controllObj.GetComponent<Characters>();
        if (mMushroomEvent == true)
        {
            if (mRandEvent >= 80 && mRandEvent < 84)
            {
                mTextNightEventContent.text = "Eatable But Raw...";
                controllerChar.eat(StringHelper.LISA, 30);
                controllerChar.getStatus(StringHelper.LISA, StringHelper.DISEASE);
            }
            if (mRandEvent >= 84 && mRandEvent < 88)
            {
                mTextNightEventContent.text = "She Shouldn't Eat that...";
                controllerChar.eat(StringHelper.LISA, -40);
                controllerChar.getStatus(StringHelper.LISA, StringHelper.INFECTION);
            }
            if (mRandEvent >= 88 && mRandEvent < 90)
            {
                mTextNightEventContent.text = "It was Delicious!";
                controllerChar.eat(StringHelper.LISA, 30);
            }

        }
        else if (mVisitorEvent == true)
        {
            if (mRandEvent >= 90 && mRandEvent < 95)
            {
                mTextNightEventContent.text = "Visitor Attacked Our Ship!!!";
                mSubmarineHP -= 10;
            }
            if (mRandEvent >= 95 && mRandEvent < 98)
            {
                mWinGamePanel.SetActive(true);
            }
            if (mRandEvent >= 98 && mRandEvent < 100)
            {
                mTextNightEventContent.text = "Visitor Attacked But Safe!";
            }
        }
        mLisaNightOption01.gameObject.SetActive(false);
        mLisaNightOption02.gameObject.SetActive(false);
        mLisaNightClose.gameObject.SetActive(true);
    }

    private void whoNothingHappens(Button pWhoNightOption01, Button pWhoNightOption02, Button pWhoNightClose)
    {
        if (mMushroomEvent == true)
        {
            mTextNightEventContent.text = "Ignored!";
            pWhoNightOption01.gameObject.SetActive(false);
            pWhoNightOption02.gameObject.SetActive(false);
            pWhoNightClose.gameObject.SetActive(true);
        }
        else if (mVisitorEvent == true)
        {
            mTextNightEventContent.text = "Ignored!";
            pWhoNightOption01.gameObject.SetActive(false);
            pWhoNightOption02.gameObject.SetActive(false);
            pWhoNightClose.gameObject.SetActive(true);
        }
    }
    private void quitNightEvent()
    {
        mLisaNightClose.gameObject.SetActive(false);
        mNightLisa.gameObject.SetActive(false);
        mNightSteven.gameObject.SetActive(true);

        mTimeCounter = StringHelper.DAY_TIME;
        mTimeNeedle.transform.rotation = Quaternion.Euler(0, 0, 90);
        mNightEventPanel.SetActive(false);
        Time.timeScale = 1;
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        Characters controllerChar = controllObj.GetComponent<Characters>();
        if (mIsStevenOut == true)
        {
            controllerChar.outNight(StringHelper.STEVEN, 40, 20);
        }
        if (mIsCindyOut == true)
        {
            controllerChar.outNight(StringHelper.CINDY, 40, 20);
        }
        if (mIsLisaOut == true)
        {
            controllerChar.outNight(StringHelper.LISA, 40, 20);
        }
        mIsStevenOut = mIsCindyOut = mIsLisaOut = false;
        mMushroomEvent = mVisitorEvent = false;
        controllerChar.wakeSteven();
        controllerChar.wakeCindy();
        controllerChar.wakeLisa();
        mDayEventTime = 2;
        mTxtEventReport.text = string.Format("Day - {0}", (mDayTime - 1).ToString("D2"));
        mJournalContext.text = "";
        mDayTime++;
    }
}
