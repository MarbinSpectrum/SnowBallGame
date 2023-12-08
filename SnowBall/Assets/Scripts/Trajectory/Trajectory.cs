using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
	[SerializeField] 
	private int			dotsNumber;

	[SerializeField] 
	private GameObject	dotsParent;

	[SerializeField] 
	private GameObject	dotPrefab;

	[SerializeField] 
	private float		dotSpacing;

	[SerializeField, Range(0.01f, 1f)]
	private float		dotMinScale;

	[SerializeField, Range(1f, 2f)]
	private float		dotMaxScale;

	private Transform[] dotsList;

	private Vector2 pos;

	//dot pos
	private float timeStamp;

	//--------------------------------
	private void Start()
	{
		//hide trajectory in the start
		Hide();

		//prepare dots
		PrepareDots();
	}

	private void PrepareDots()
	{
		dotsList = new Transform[dotsNumber];
		dotPrefab.transform.localScale = Vector3.one * dotMaxScale;

		float scale = dotMaxScale;
		float scaleFactor = scale / dotsNumber;

		for (int i = 0; i < dotsNumber; i++)
		{
			dotsList[i] = Instantiate(dotPrefab, null).transform;
			dotsList[i].parent = dotsParent.transform;

			dotsList[i].localScale = Vector3.one * scale;
			if (scale > dotMinScale)
				scale -= scaleFactor;
		}
	}

	public void UpdateDots(Vector3 ballPos, Vector2 forceApplied)
	{
		timeStamp = dotSpacing;
		bool notGrond = true;
		for (int i = 0; i < dotsNumber; i++)
		{
			pos.x = (ballPos.x + forceApplied.x * timeStamp);
			pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics2D.gravity.magnitude * timeStamp * timeStamp) / 2f;



			if (notGrond && i >= 1)
			{
				Ball ball = ControlMng.ball;
				Vector2 dotPos = dotsList[i - 1].transform.position;
				RaycastHit2D isGround0 = Physics2D.Linecast(
					dotPos, pos,
					LayerMask.GetMask("Ground"));
				RaycastHit2D isGround1 = Physics2D.CircleCast(
					dotPos + ball.col.offset * ball.transform.localScale.x,
						ball.col.radius * transform.localScale.x, Vector2.zero, 0,
					LayerMask.GetMask("Ground"));
				notGrond &= !isGround0;
				notGrond &= !isGround1;

				dotsList[i].gameObject.SetActive(true);

				if (isGround0)
					dotsList[i].position = isGround0.point;
				else
					dotsList[i].position = pos;
			}
			else
            {
				dotsList[i].gameObject.SetActive(notGrond);
				dotsList[i].position = pos;
			}
			timeStamp += dotSpacing;
		}
	}

	public void Show()
	{
		dotsParent.SetActive(true);
	}

	public void Hide()
	{
		dotsParent.SetActive(false);
	}
}
