using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Items : MonoBehaviour
{
    private int[] mArrItem = new int[12];
    [SerializeField]
    private Text txtWoods, txtIrons, txtGunPowders, txtNails, txtMedicKits
        , txtRepairKits1, txtRepairKits2, txtRepairKits3, txtLockPicks, txtFoodJournals, txtAmmo, txtFood;
    [SerializeField]
    private Button mFixSubmarineButton, mCloseFixSubmarineButton, mRK01, mRK02, mRK03
        , mAmmoCraft, mRK01Craft, mRK02Craft, mRK03Craft, mLockPickCraft;
    [SerializeField]
    private GameObject mFixSubmarinePanel;
    private void Awake()
    {
        for(int i=0; i < mArrItem.Length; i++)
        {
            mArrItem[i] = 0; 
        }
        getItem(StringHelper.MEDIC_KITS, 1);
        getItem(StringHelper.AMMO, 2);
        getItem(StringHelper.FOOD, 3);
        getItem(StringHelper.REPAIR_KIT_1, 1);
        getItem(StringHelper.REPAIR_KIT_2, 1);
        getItem(StringHelper.REPAIR_KIT_3, 1);
        getItem(StringHelper.WOODS, 1);
        getItem(StringHelper.IRONS, 1);
        getItem(StringHelper.FOOD_JOURNAL, 1);
    }
    private void Start()
    {
        mFixSubmarineButton.onClick.AddListener(openfixSubmarinePanel);
        mCloseFixSubmarineButton.onClick.AddListener(closefixSubmarinePanel);
        mRK01.onClick.AddListener(()=>fixSubmarine(StringHelper.REPAIR_KIT_1, 10));
        mRK02.onClick.AddListener(()=>fixSubmarine(StringHelper.REPAIR_KIT_2, 30));
        mRK03.onClick.AddListener(() => fixSubmarine(StringHelper.REPAIR_KIT_3, 50));
    }
    private void FixedUpdate()
    {
        txtWoods.text = string.Format("{0}: {1}", "Wood", mArrItem[StringHelper.WOODS]);
        txtIrons.text = string.Format("{0}: {1}", "Iron", mArrItem[StringHelper.IRONS]);
        txtGunPowders.text = string.Format("{0}: {1}", "Gun Powder", mArrItem[StringHelper.GUN_POWDER]);
        txtNails.text = string.Format("{0}: {1}", "Nails", mArrItem[StringHelper.NAILS]);
        txtMedicKits.text = string.Format("{0}: {1}", "Medic Kits", mArrItem[StringHelper.MEDIC_KITS]);
        txtRepairKits1.text = string.Format("{0}: {1}", "Repair Kits(1)", mArrItem[StringHelper.REPAIR_KIT_1]);
        txtRepairKits2.text = string.Format("{0}: {1}", "Repair Kits(2)", mArrItem[StringHelper.REPAIR_KIT_2]);
        txtRepairKits3.text = string.Format("{0}: {1}", "Repair Kits(3)", mArrItem[StringHelper.REPAIR_KIT_3]);
        txtLockPicks.text = string.Format("{0}: {1}", "Lock Picks", mArrItem[StringHelper.LOCK_PICKS]);
        txtFoodJournals.text = string.Format("{0}: {1}", "Food Journal", mArrItem[StringHelper.FOOD_JOURNAL]);
        txtAmmo.text = "X " + mArrItem[StringHelper.AMMO].ToString("D2");
        txtFood.text = "X " + mArrItem[StringHelper.FOOD].ToString("D2");
        checkRepairKitUsable(mRK01, StringHelper.REPAIR_KIT_1);
        checkRepairKitUsable(mRK02, StringHelper.REPAIR_KIT_2);
        checkRepairKitUsable(mRK03, StringHelper.REPAIR_KIT_3);
        checkItemCraftable(mAmmoCraft, mArrItem[StringHelper.GUN_POWDER], 4, mArrItem[StringHelper.IRONS], 2);
        checkItemCraftable(mRK01Craft, mArrItem[StringHelper.WOODS], 4, mArrItem[StringHelper.NAILS], 2);
        checkItemCraftable(mRK02Craft, mArrItem[StringHelper.IRONS], 2, mArrItem[StringHelper.NAILS], 2);
        checkItemCraftable(mRK03Craft, mArrItem[StringHelper.GUN_POWDER], 4, mArrItem[StringHelper.NAILS], 2);
        checkItemCraftable(mLockPickCraft, mArrItem[StringHelper.GUN_POWDER], 1, mArrItem[StringHelper.IRONS], 1);
    }
    public void getItem(int pItemNum, int pValue)
    {
        mArrItem[pItemNum] += pValue;
    }
    public void useItem(int pItemNum, int pValue)
    {
        if(mArrItem[pItemNum] >= pValue)
        {
            mArrItem[pItemNum] -= pValue;
        }
    }
    public bool checkItemAmount(int pItemNum, int pAmount)
    {
        if (mArrItem[pItemNum] >= pAmount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void checkRepairKitUsable(Button pButton, int pItem)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        GameController controller = controllObj.GetComponent<GameController>();
        if (!checkItemAmount(pItem, 1) || controller.checkSubmarineHp())
        {
            pButton.interactable = false;
        }
        else if (checkItemAmount(pItem, 1) || !controller.checkSubmarineHp())
        {
            pButton.interactable = true;
        }
    }
    private void checkItemCraftable(Button pButton, int pItem01, int pAmount01, int pItem02, int pAmount02)
    {
        if(mArrItem[pItem01] < pAmount01 || mArrItem[pItem02] < pAmount02)
        {
            pButton.interactable = false;
        }
    }
    private void openfixSubmarinePanel()
    {
        mFixSubmarinePanel.SetActive(true);        
    }
    private void closefixSubmarinePanel()
    {
        mFixSubmarinePanel.SetActive(false);
    }
    private void fixSubmarine(int pKitNum, int pFixNum)
    {
        GameObject controllObj = GameObject.FindGameObjectWithTag("GameController");
        GameController controller = controllObj.GetComponent<GameController>();
        useItem(pKitNum, 1);
        controller.fixSubmarine(pFixNum);
    }
}
