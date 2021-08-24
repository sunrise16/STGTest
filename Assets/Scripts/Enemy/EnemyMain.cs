#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class EnemyMain : MonoBehaviour
{
	#region VARIABLE
	private EnemyBase pEnemyBase;
	private List<Timer> pPatternTimerList;
	private Dictionary<int, CoroutineHandle> pSinglePatternList;
	private List<CoroutineHandle> pRepeatPatternList;
	private List<int> pCounterPatternList;
	private int iFireCount;
	private bool bScreenOut;
    #endregion

    #region GET METHOD
	public EnemyBase GetEnemyBase() { return pEnemyBase; }
	public List<Timer> GetPatternTimerList() { return pPatternTimerList; }
	public Dictionary<int, CoroutineHandle> GetSinglePatternList() { return pSinglePatternList; }
	public List<CoroutineHandle> GetRepeatPatternList() { return pRepeatPatternList; }
	public List<int> GetCounterPatternList() { return pCounterPatternList; }
	public Timer GetPatternTimer(int iIndex) { return pPatternTimerList[iIndex]; }
	public CoroutineHandle GetSinglePattern(int iIndex) { return pSinglePatternList[iIndex]; }
	public CoroutineHandle GetRepeatPattern(int iIndex) { return pRepeatPatternList[iIndex]; }
	public int GetCounterPattern(int iIndex) { return pCounterPatternList[iIndex]; }
	public int GetFireCount() { return iFireCount; }
	#endregion

	#region UNITY LIFE CYCLE
	public void FixedUpdate()
    {
		if (pEnemyBase == null)
		{
			return;
		}

		// ENEMY MOVE
		if (pEnemyBase.GetEnemyMoveAccelerationSpeedX() != 0.0f || pEnemyBase.GetEnemyMoveAccelerationSpeedY() != 0.0f)
			EnemyMoveAcceleration();
		if (pEnemyBase.GetEnemyMoveDecelerationSpeedX() != 0.0f || pEnemyBase.GetEnemyMoveDecelerationSpeedY() != 0.0f)
			EnemyMoveDeceleration();
		if (pEnemyBase.GetEnemyMoveSpeedX() != 0.0f || pEnemyBase.GetEnemyMoveSpeedY() != 0.0f)
			EnemyMove();

		// SCREEN CHECK
		OutScreenCheck(pEnemyBase.GetTransform());
		if (bScreenOut.Equals(false) && TouchScreenCheck(pEnemyBase.GetTransform()).Equals(true))
		{
			bScreenOut = true;

			if (pEnemyBase.GetOutScreenShot().Equals(false))
            {
				for (int i = 0; i < pPatternTimerList.Count; i++)
				{
					if (pSinglePatternList.ContainsKey(pPatternTimerList[i].GetFlag()))
					{
						Timing.Instance.KillCoroutinesOnInstance(pSinglePatternList[pPatternTimerList[i].GetFlag()]);
					}
					pPatternTimerList[i].SetSwitch(false);
				}
				pPatternTimerList.Clear();
				pSinglePatternList.Clear();

				if (!pRepeatPatternList.Count.Equals(0))
				{
					for (int i = 0; i < pRepeatPatternList.Count; i++)
					{
						Timing.Instance.KillCoroutinesOnInstance(pRepeatPatternList[i]);
					}
					pRepeatPatternList.Clear();
				}
			}
		}

		// TIMER CHECK
		if (pPatternTimerList.Count > 0)
        {
			for (int i = 0; i < pPatternTimerList.Count; i++)
			{
				if (pPatternTimerList[i].GetSwitch().Equals(true))
				{
					pPatternTimerList[i].RunTimer();
					if (pPatternTimerList[i].GetDelay().Equals(false) && pPatternTimerList[i].GetRepeatCount().Equals(0))
                    {
						iFireCount++;
						pPatternTimerList[i].SetRepeatCount(pPatternTimerList[i].GetRepeatCount() + 1);
						if (pSinglePatternList.ContainsKey(pPatternTimerList[i].GetFlag()).Equals(false))
                        {
							pSinglePatternList.Add(pPatternTimerList[i].GetFlag(), GameManager.Instance.PatternCall(pEnemyBase.GetGameObject(), this,
								pPatternTimerList[i].GetFlag(), iFireCount));
						}
						else
                        {
							iFireCount--;
                        }
						pPatternTimerList[i].ResetTimer(pPatternTimerList[i].GetResetTime());
						if (pPatternTimerList[i].GetRepeatLimit().Equals(1))
						{
							pPatternTimerList[i].SetSwitch(false);
						}
					}
					else
                    {
						if (pPatternTimerList[i].GetTrigger().Equals(true))
						{
							iFireCount++;
							pPatternTimerList[i].SetRepeatCount(pPatternTimerList[i].GetRepeatCount() + 1);
							if (pSinglePatternList.ContainsKey(pPatternTimerList[i].GetFlag()).Equals(false))
							{
								pSinglePatternList.Add(pPatternTimerList[i].GetFlag(), GameManager.Instance.PatternCall(pEnemyBase.GetGameObject(), this,
									pPatternTimerList[i].GetFlag(), iFireCount));
							}
							else
							{
								iFireCount--;
							}
							pPatternTimerList[i].ResetTimer(pPatternTimerList[i].GetResetTime());
						}
					}
				}
			}
		}
	}
	#endregion

	#region COMMON METHOD
	public void Init(GameObject pEnemyObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale, EEnemyType enEnemyType, float fEnemyHP,
		bool bCounter, bool bOutScreenShot)
	{
		if (pEnemyBase == null)
		{
			pEnemyBase = new EnemyBase(pEnemyObject, pTransform, vSpawnPosition, vScale);
			pPatternTimerList = new List<Timer>();
			pSinglePatternList = new Dictionary<int, CoroutineHandle>();
			pRepeatPatternList = new List<CoroutineHandle>();
			pCounterPatternList = new List<int>();
			pEnemyBase.SetSpriteRenderer(pEnemyBase.GetTransform().GetChild(0).GetComponent<SpriteRenderer>());
			pEnemyBase.SetAnimator(pEnemyBase.GetTransform().GetChild(0).GetComponent<Animator>());
			pEnemyBase.SetCircleCollider(GetComponent<CircleCollider2D>());
		}
		else
		{
			pEnemyBase.Init(pEnemyObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Enemy);
			pPatternTimerList.Clear();
			pRepeatPatternList.Clear();
			pCounterPatternList.Clear();
		}
		pEnemyBase.SetEnemyType(enEnemyType);
		pEnemyBase.SetEnemyHP(fEnemyHP);
		pEnemyBase.SetCounter(bCounter);
		pEnemyBase.SetOutScreenShot(bOutScreenShot);
		iFireCount = 0;
		bScreenOut = false;

		SetSpriteBase(enEnemyType);
	}
	public void SetSpriteBase(EEnemyType enEnemyType)
    {
		pEnemyBase.GetSpriteRenderer().sprite = GameManager.Instance.pEnemySprite[Convert.ToInt32(enEnemyType)];
		pEnemyBase.GetCircleCollider().radius = 0.25f;

		#region ADD CONTROLLER
		switch (enEnemyType)
        {
			case EEnemyType.enType_TinyFairy_Type1:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[10];
				break;
			case EEnemyType.enType_TinyFairy_Type2:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[11];
				break;
			case EEnemyType.enType_TinyFairy_Type3:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[12];
				break;
			case EEnemyType.enType_TinyFairy_Type4:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[13];
				break;
			case EEnemyType.enType_TinyFairy_Type5:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[14];
				break;
			case EEnemyType.enType_TinyFairy_Type6:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[15];
				break;
			case EEnemyType.enType_TinyFairy_Type7:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[16];
				break;
			case EEnemyType.enType_TinyFairy_Type8:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[17];
				break;
			case EEnemyType.enType_TinyFairy_Type9:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[18];
				break;
			case EEnemyType.enType_TinyFairy_Type10:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[19];
				break;
			case EEnemyType.enType_TinyFairy_Type11:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[20];
				break;
			case EEnemyType.enType_TinyFairy_Type12:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[21];
				break;
			case EEnemyType.enType_TinyFairy_Type13:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[22];
				break;
			case EEnemyType.enType_TinyFairy_Type14:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[23];
				break;
			case EEnemyType.enType_TinyFairy_Type15:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[24];
				break;
			case EEnemyType.enType_TinyFairy_Type16:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[25];
				break;
			case EEnemyType.enType_TinyFairy_Type17:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[26];
				break;
			case EEnemyType.enType_TinyFairy_Type18:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[27];
				break;
			case EEnemyType.enType_TinyFairy_Type19:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[28];
				break;
			case EEnemyType.enType_TinyFairy_Type20:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[29];
				break;
			case EEnemyType.enType_TinyFairy_Type21:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[30];
				break;
			case EEnemyType.enType_TinyFairy_Type22:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[31];
				break;
			case EEnemyType.enType_HugeFairy_Type1:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[0];
				break;
			case EEnemyType.enType_HugeFairy_Type2:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[1];
				break;
			case EEnemyType.enType_HugeFairy_Type3:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[2];
				break;
			case EEnemyType.enType_HugeFairy_Type4:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[3];
				break;
			case EEnemyType.enType_HugeFairy_Type5:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[4];
				break;
			case EEnemyType.enType_HugeFairy_Type6:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[5];
				break;
			case EEnemyType.enType_HugeFairy_Type7:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[6];
				break;
			case EEnemyType.enType_HugeFairy_Type8:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[7];
				break;
			case EEnemyType.enType_HugeFairy_Type9:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[8];
				break;
			case EEnemyType.enType_HugeFairy_Type10:
				pEnemyBase.GetAnimator().runtimeAnimatorController = GameManager.Instance.pEnemyAnimator[9];
				break;
			default:
				break;
		}
        #endregion
    }
	public void EnemyMove()
	{
		pEnemyBase.GetTransform().Translate(new Vector2(pEnemyBase.GetEnemyMoveSpeedX() * Time.deltaTime, pEnemyBase.GetEnemyMoveSpeedY() * Time.deltaTime));
		// pEnemyBase.GetTransform().Translate(Vector2.right * pEnemyBase.GetEnemyMoveSpeedX() * Time.deltaTime);
		// pEnemyBase.GetTransform().Translate(Vector2.up * pEnemyBase.GetEnemyMoveSpeedY() * Time.deltaTime);
	}
	public void EnemyMoveAcceleration()
	{
		// ACCELERATION X
		pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveSpeedX() + pEnemyBase.GetEnemyMoveAccelerationSpeedX());
		if (pEnemyBase.GetEnemyMoveSpeedX() >= pEnemyBase.GetEnemyMoveAccelerationSpeedXMax())
		{
			pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveAccelerationSpeedXMax());
			pEnemyBase.SetEnemyMoveAccelerationSpeedX(0.0f);
			pEnemyBase.SetEnemyMoveAccelerationSpeedXMax(0.0f);
		}

		// ACCELERATION Y
		pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveSpeedY() + pEnemyBase.GetEnemyMoveAccelerationSpeedY());
		if (pEnemyBase.GetEnemyMoveSpeedY() >= pEnemyBase.GetEnemyMoveAccelerationSpeedYMax())
		{
			pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveAccelerationSpeedYMax());
			pEnemyBase.SetEnemyMoveAccelerationSpeedY(0.0f);
			pEnemyBase.SetEnemyMoveAccelerationSpeedYMax(0.0f);
		}
	}
	public void EnemyMoveDeceleration()
	{
		// DECELERTAION X
		pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveSpeedX() - pEnemyBase.GetEnemyMoveDecelerationSpeedX());
		if (pEnemyBase.GetEnemyMoveSpeedX() <= pEnemyBase.GetEnemyMoveDecelerationSpeedXMin())
		{
			pEnemyBase.SetEnemyMoveSpeedX(pEnemyBase.GetEnemyMoveDecelerationSpeedXMin());
			pEnemyBase.SetEnemyMoveDecelerationSpeedX(0.0f);
			pEnemyBase.SetEnemyMoveDecelerationSpeedXMin(0.0f);
		}

		// DECELERATION Y
		pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveSpeedY() - pEnemyBase.GetEnemyMoveDecelerationSpeedY());
		if (pEnemyBase.GetEnemyMoveSpeedY() <= pEnemyBase.GetEnemyMoveDecelerationSpeedYMin())
		{
			pEnemyBase.SetEnemyMoveSpeedY(pEnemyBase.GetEnemyMoveDecelerationSpeedYMin());
			pEnemyBase.SetEnemyMoveDecelerationSpeedY(0.0f);
			pEnemyBase.SetEnemyMoveDecelerationSpeedYMin(0.0f);
		}
	}
	public void OutScreenCheck(Transform pTransform)
	{
		Vector3 vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);
		float fPadding = GameManager.Instance.fDefaultPadding;

		if (vTempPosition.x < 0 - fPadding || vTempPosition.x > Screen.width + fPadding ||
			vTempPosition.y < 0 - fPadding || vTempPosition.y > Screen.height + fPadding)
		{
			DestroyEnemy();
		}
	}
	public bool TouchScreenCheck(Transform pTransform)
	{
		Vector3 vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);

		if (vTempPosition.x < 0 || vTempPosition.x > Screen.width || vTempPosition.y < 0 || vTempPosition.y > Screen.height)
		{
			return true;
		}
		return false;
	}
	public void DestroyEnemy()
    {
		// CREATE EFFECT HERE

		for (int i = 0; i < pPatternTimerList.Count; i++)
        {
			if (pSinglePatternList.ContainsKey(pPatternTimerList[i].GetFlag()))
            {
				Timing.Instance.KillCoroutinesOnInstance(pSinglePatternList[pPatternTimerList[i].GetFlag()]);
			}
			pPatternTimerList[i].InitTimer(0, 0, false);
        }
		pPatternTimerList.Clear();
		pSinglePatternList.Clear();

		for (int i = 0; i < pRepeatPatternList.Count; i++)
		{
			Timing.Instance.KillCoroutinesOnInstance(pRepeatPatternList[i]);
		}
		pRepeatPatternList.Clear();

		if (pEnemyBase.GetCounter().Equals(true))
		{
			for (int i = 0; i < pCounterPatternList.Count; i++)
			{
				GameManager.Instance.PatternCall(pEnemyBase.GetGameObject(), this, pCounterPatternList[i], iFireCount);
			}
		}
		pCounterPatternList.Clear();

		EnemyManager.Instance.GetEnemyPool().ReturnPool(pEnemyBase.GetGameObject());
	}
	#endregion
}