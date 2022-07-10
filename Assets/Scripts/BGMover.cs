using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour
{
    [SerializeField]
    private float mSpeed;
    private Rigidbody2D mRB;
    private Vector2 mMoveDistance;
    void Start()
    {
        mRB = GetComponent <Rigidbody2D>();
        mRB.velocity = Vector2.right * mSpeed;
        mMoveDistance = new Vector2(StringHelper.BACKGROUND_SIZE, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bumper"))
        {
            transform.position = mMoveDistance;
        }
    }
}