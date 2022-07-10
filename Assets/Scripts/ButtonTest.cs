using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTest : MonoBehaviour
    , IPointerEnterHandler
{
    [SerializeField]
    private Button mStartButton;
    //private Animator mAckEnterAnim;
    private void Awake()
    {
        //mAckEnterAnim.GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
       // mAckEnterAnim.SetBool("IsEntered",true);
    }
}
