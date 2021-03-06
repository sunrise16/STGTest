#region USING
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
#endregion

public class TextLog : MonoBehaviour
{
    #region VARIABLE
	public GameObject pGameObject;
	[HideInInspector] public Transform pTransform;
	[HideInInspector] public Text pText;
	public int iIndex;
    #endregion

    #region UNITY LIFE CYCLE
    void Start ()
	{
		pText = GetComponent<Transform>().GetComponent<Text>();
		pTransform = pGameObject.GetComponent<Transform>();

		Timing.RunCoroutine(Update());
	}
    #endregion

    #region IENUMERATOR
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
				case 4:
					pText.text = GameManager.Instance.pPlayerBase.GetPlayerCurrentScore().ToString();
					break;
				case 5:
					pText.text = GameManager.Instance.pPlayerBase.GetPlayerPower().ToString();
					break;
				default:
					break;
            }
		}
	}
	#endregion
}