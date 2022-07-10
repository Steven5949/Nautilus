using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmarineMover : MonoBehaviour
{
	private Rigidbody2D mRB;
	void Awake()
	{
		mRB = GetComponent<Rigidbody2D>();
	}
    private void OnEnable()
    {
		StartCoroutine(moveSubmarine());
    }
    private IEnumerator moveSubmarine()
	{
		while (true)
		{
			yield return new WaitForSeconds(StringHelper.SUBMARINE_STOPPING_TIME);
			if (transform.position.y < 0)
			{
				mRB.velocity += Vector2.up * StringHelper.SUBMARINE_MOVING_SPEED;
			}
			else
			{
				mRB.velocity += Vector2.down * StringHelper.SUBMARINE_MOVING_SPEED;
			}
			yield return new WaitForSeconds(StringHelper.SUBMARINE_STOPPING_TIME);
			Vector2 oriVel = mRB.velocity;
			oriVel.y = 0;
			mRB.velocity = oriVel;
		}
	}
}
