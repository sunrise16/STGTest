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

			switch (iIndex)
            {
				case 0:
					pText.text = BulletManager.Instance.GetBulletPool().GetBulletListCount(iIndex).ToString();
					break;
				case 1:
					pText.text = pTransform.childCount.ToString();
					break;
				case 2:
					pText.text = GameManager.Instance.pPlayerBase.GetPlayerGrazeCount().ToString();
					break;
				case 3:
					pText.text = GameManager.Instance.pPlayerBase.GetPlayerMissCount().ToString();
					break;
				default:
					break;
            }
		}
	}
}
