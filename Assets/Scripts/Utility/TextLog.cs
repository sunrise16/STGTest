using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class TextLog : MonoBehaviour
{
	[HideInInspector] public Text pText;
	public GameObject pGameObject;
	[HideInInspector] public Transform pTransform;
	public int iIndex;

	void Start ()
	{
		pText = GetComponent<Transform>().GetComponent<Text>();
		pTransform = pGameObject.GetComponent<Transform>();

		Timing.RunCoroutine(Update());
	}
	
	public IEnumerator<float> Update()
	{
		while (true)
        {
			yield return Timing.WaitForOneFrame;

			if (iIndex > 0 && iIndex <= 1)
            {
				pText.text = BulletManager.Instance.GetBulletPool().GetBulletListCount(iIndex).ToString();
			}
			else
            {
				pText.text = pTransform.childCount.ToString();
            }
		}
	}
}
