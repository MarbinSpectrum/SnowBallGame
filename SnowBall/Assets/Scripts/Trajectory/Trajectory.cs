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

			if(notGrond && i >= 1)
			{ 
				RaycastHit2D isGround = Physics2D.Linecast(
					dotsList[i - 1].transform.position, pos,
					LayerMask.GetMask("Ground"));
				notGrond &= !isGround;
				dotsList[i].gameObject.SetActive(true);
				if (isGround)
					dotsList[i].position = isGround.point;
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
