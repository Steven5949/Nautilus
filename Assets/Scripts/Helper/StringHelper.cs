using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringHelper : MonoBehaviour
{
    #region BackGround
    public readonly static float BACKGROUND_SIZE = -16.33f;
    public readonly static float SUBMARINE_MOVING_SPEED = .2f;
    public readonly static float SUBMARINE_STOPPING_TIME = 1.5f;
    #endregion
    #region Sound
    public readonly static float SOUND_ON = 0.06f;
    public readonly static float SOUND_OFF = 0f;
    #endregion
    #region Items
    public readonly static int WOODS = 0;
    public readonly static int IRONS = 1;
    public readonly static int GUN_POWDER = 2;
    public readonly static int NAILS = 3;
    public readonly static int MEDIC_KITS = 4;
    public readonly static int REPAIR_KIT_1 = 5;
    public readonly static int REPAIR_KIT_2 = 6;
    public readonly static int REPAIR_KIT_3 = 7;
    public readonly static int LOCK_PICKS = 8;
    public readonly static int FOOD_JOURNAL = 9;
    public readonly static int AMMO = 10;
    public readonly static int FOOD = 11;

    #endregion
    #region Settings
    public readonly static float DAY_TIME = 60f;
    public readonly static float SUBMARINE_HP = 100f;
    public readonly static float DAY_EVENT_COUNT_TIME = 20f;
    #endregion
    #region Characters
    public readonly static int HEALTH = 0;
    public readonly static int MAX_HEALTH = 1;
    public readonly static int HUNGER = 2;
    public readonly static int MAX_HUNGER = 3;
    public readonly static int STATUS = 4;
    public readonly static int STEVEN = 0;
    public readonly static int CINDY = 1;
    public readonly static int LISA = 2;
    public readonly static int NORMAL = 0;
    public readonly static int DISEASE = 1;
    public readonly static int INFECTION = 3;
    public readonly static int DRAINED = 4;
    public readonly static int HUNGRY = 5;

    #endregion
}
