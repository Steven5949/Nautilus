using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField]
    private Button mVolumeButton;
    [SerializeField]
    private AudioSource mBGM;
    [SerializeField]
    private GameObject mMuteImage;
    void Start()
    {
        mVolumeButton.onClick.AddListener(musicAction);
    }
    public void musicAction()
    {
        if (mBGM.volume > 0)
        {
            mBGM.volume = StringHelper.SOUND_OFF;
            mMuteImage.SetActive(true);
        }
        else if(mBGM.volume <= 0)
        {
            mBGM.volume = StringHelper.SOUND_ON;
            mMuteImage.SetActive(false);
        }
    }
}
