using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int mPlayerHp = 100;
    private int mPlayerFood = 100;
    private int mPlayerHunger = 20;
    public Player(int pPlayerHp, int pPlayerFood, int pPlayerHunger)
    {
        mPlayerHp = pPlayerHp;
        mPlayerFood = pPlayerFood;
        mPlayerHunger = pPlayerHunger;
    }

    public void afterDayEnd(int pHunger)
    {
        mPlayerFood -= pHunger;
    }
    public void getDamage(int pValue)
    {
        mPlayerHp -= pValue;
    }
}
