using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMusicController : MonoBehaviour
{
    [SerializeField]
    private Button mVolumeButton;
    [SerializeField]
    private AudioSource mBGM;
    [SerializeField]
    private Text mMusicText;
    void Start()
    {
        mVolumeButton.onClick.AddListener(musicAction);
    }
    public void musicAction()
    {
        if (mBGM.volume > 0)
        {
            mBGM.volume = StringHelper.SOUND_OFF;
            mBGM.Pause();
            mMusicText.text = "Music On";
        }
        else
        {
            mBGM.volume = StringHelper.SOUND_ON;
            mBGM.Play();
            mMusicText.text = "Music Off";
        }
    }
}
