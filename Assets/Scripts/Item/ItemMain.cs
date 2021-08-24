#region USING
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class ItemMain : MonoBehaviour
{
    #region VARIABLE
    public event DelegateObject pEvent;
    public DelegateCommon pCommonDelegate;

    private ItemBase pItemBase;
    private PlayerMain pPlayerMain;
    private PlayerBase pPlayerBase;
    private Timer pTimer;
    private Vector3 vRefVector;
    private CoroutineHandle cHomingHandle;
    #endregion

    #region GET METHOD
    public ItemBase GetItemBase() { return pItemBase; }
    public Timer GetTimer() { return pTimer; }
    #endregion

    #region UNITY LIFE CYCLE
    private void FixedUpdate()
    {
        if (pItemBase == null)
        {
            return;
        }

        // SCREEN CHECK
        OutScreenCheck(pItemBase.GetTransform(), pItemBase.GetPadding());

        // AUTO COLLECT
        if (pPlayerBase.GetPositionY() >= 2.0f)
        {
            pEvent(pItemBase.GetGameObject());
        }
        if (pItemBase.GetAutoCollect().Equals(true))
        {
            pItemBase.SetPosition(Vector3.SmoothDamp(pItemBase.GetPosition(), pPlayerBase.GetPosition(), ref vRefVector, 0.2f));
        }

        // MAIN TIMER RUN
        if (pItemBase.GetSpriteRotate().Equals(true) && pTimer.GetSwitch().Equals(true))
        {
            pTimer.RunTimer();

            if (pTimer.GetTrigger().Equals(true))
            {
                pItemBase.GetRigidbody().velocity = Vector2.zero;
                pItemBase.GetRigidbody().gravityScale = 3.0f;

                pTimer.ResetTimer(pTimer.GetResetTime());
                pTimer.SetSwitch(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D pCollider)
    {
        if (pCollider.name.Equals("HitPoint"))
        {
            switch (pItemBase.GetItemType())
            {
                case EItemType.enType_PowerS:
                case EItemType.enType_PowerM:
                    SetPlayerPower(pItemBase.GetItemType());
                    SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    break;
                case EItemType.enType_PowerL:
                    SetPlayerPower(pItemBase.GetItemType());
                    SoundManager.Instance.PlaySE(ESE.enSE_Item01);
                    break;
                case EItemType.enType_ScoreS:
                    pPlayerBase.SetPlayerScoreItem(pPlayerBase.GetPlayerScoreItem() + 1);
                    SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    break;
                case EItemType.enType_ScoreM:
                    pPlayerBase.SetPlayerScoreItem(pPlayerBase.GetPlayerScoreItem() + 5);
                    SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    break;
                case EItemType.enType_LifeS:
                case EItemType.enType_LifeL:
                    if (pPlayerBase.GetPlayerLife() < 8)
                    {
                        pPlayerBase.SetPlayerLife(pPlayerBase.GetPlayerLife() + 1);
                        SoundManager.Instance.PlaySE(ESE.enSE_Extend);
                    }
                    else
                    {
                        SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    }
                    break;
                case EItemType.enType_LifeFragmentS:
                case EItemType.enType_LifeFragmentL:
                    if (pPlayerBase.GetPlayerLifeFragment() < 4)
                    {
                        pPlayerBase.SetPlayerLifeFragment(pPlayerBase.GetPlayerLifeFragment() + 1);
                        SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    }
                    else
                    {
                        pPlayerBase.SetPlayerLifeFragment(0);
                        pPlayerBase.SetPlayerLife(pPlayerBase.GetPlayerLife() + 1);
                        SoundManager.Instance.PlaySE(ESE.enSE_Extend);
                    }
                    break;
                case EItemType.enType_SpellS:
                case EItemType.enType_SpellL:
                    if (pPlayerBase.GetPlayerSpell() < 8)
                    {
                        pPlayerBase.SetPlayerSpell(pPlayerBase.GetPlayerSpell() + 1);
                        SoundManager.Instance.PlaySE(ESE.enSE_CardGet);
                    }
                    else
                    {
                        SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    }
                    break;
                case EItemType.enType_SpellFragmentS:
                case EItemType.enType_SpellFragmentL:
                    if (pPlayerBase.GetPlayerSpellFragment() < 4)
                    {
                        pPlayerBase.SetPlayerSpellFragment(pPlayerBase.GetPlayerSpellFragment() + 1);
                        SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    }
                    else
                    {
                        pPlayerBase.SetPlayerSpellFragment(0);
                        pPlayerBase.SetPlayerSpell(pPlayerBase.GetPlayerSpell() + 1);
                        SoundManager.Instance.PlaySE(ESE.enSE_CardGet);
                    }
                    break;
                case EItemType.enType_FullPowerM:
                case EItemType.enType_FullPowerL:
                    SetPlayerPower(pItemBase.GetItemType());
                    SoundManager.Instance.PlaySE(ESE.enSE_PowerUp);
                    break;
                case EItemType.enType_SpecialScoreS:
                case EItemType.enType_SpecialScoreM:
                case EItemType.enType_SpecialScoreL:
                    SoundManager.Instance.PlaySE(ESE.enSE_Item00);
                    break;
                default:
                    break;
            }
            SetGameScore(pItemBase.GetItemType());
            DestroyItem();
        }
    }
    private void OnTriggerStay2D(Collider2D pCollider)
    {
        if (pCollider.name.Equals("ItemPoint") && pPlayerBase.GetSlowMode().Equals(true) && pItemBase.GetAutoCollect().Equals(false))
        {
            pItemBase.GetRigidbody().velocity = Vector2.zero;
            pItemBase.GetRigidbody().gravityScale = 0.0f;
            pItemBase.SetAutoCollect(true);
        }
    }
    #endregion

    #region COMMON METHOD
    public void Init(GameObject pItemObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale, Color pColor, EItemType enItemType, float fPadding, bool bSpriteRotate = false)
    {
        if (pItemBase == null)
        {
            pItemBase = new ItemBase(pItemObject, pTransform, vSpawnPosition, vScale);
            pPlayerMain = GameManager.Instance.pPlayer.GetComponent<PlayerMain>();
            pPlayerBase = pPlayerMain.GetPlayerBase();
            pTimer = new Timer();
            pItemBase.SetSpriteRenderer(pItemBase.GetChildTransform(0).GetComponent<SpriteRenderer>());
            pItemBase.SetAnimator(pItemBase.GetChildTransform(0).GetComponent<Animator>());
        }
        else
        {
            pItemBase.Init(pItemObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Item);
            pTimer.InitTimer();
        }
        pItemBase.SetScale(vScale);
        pItemBase.SetColor(pColor);
        pItemBase.SetItemType(enItemType);
        pItemBase.SetPadding(fPadding);
        pItemBase.SetAutoCollect(false);
        pItemBase.SetSpriteRotate(bSpriteRotate);
        vRefVector = Vector3.zero;

        pItemBase.GetRigidbody().gravityScale = 3.0f;
    }
    public void OutScreenCheck(Transform pTransform, float fPadding)
    {
        Vector3 vTempPosition = GameManager.Instance.pMainCamera.WorldToScreenPoint(pTransform.position);

        if (vTempPosition.x < 0 - fPadding || vTempPosition.x > Screen.width + fPadding ||
            vTempPosition.y < 0 - fPadding || vTempPosition.y > Screen.height + fPadding)
        {
            DestroyItem();
        }
    }
    public void SetPlayerPower(EItemType enItemType)
    {
        switch (enItemType)
        {
            case EItemType.enType_PowerS:
                if ((pPlayerBase.GetPlayerPower() < 1.0f && pPlayerBase.GetPlayerPower() + 0.01f >= 1.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 1.0f && pPlayerBase.GetPlayerPower() < 2.0f) && pPlayerBase.GetPlayerPower() + 0.01f >= 2.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 2.0f && pPlayerBase.GetPlayerPower() < 3.0f) && pPlayerBase.GetPlayerPower() + 0.01f >= 3.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 3.0f && pPlayerBase.GetPlayerPower() < 4.0f) && pPlayerBase.GetPlayerPower() + 0.01f >= 4.0f))
                {
                    SoundManager.Instance.PlaySE(ESE.enSE_PowerUp);
                }
                pPlayerBase.SetPlayerPower(pPlayerBase.GetPlayerPower() + 0.01f);
                break;
            case EItemType.enType_PowerM:
                if ((pPlayerBase.GetPlayerPower() < 1.0f && pPlayerBase.GetPlayerPower() + 0.05f >= 1.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 1.0f && pPlayerBase.GetPlayerPower() < 2.0f) && pPlayerBase.GetPlayerPower() + 0.05f >= 2.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 2.0f && pPlayerBase.GetPlayerPower() < 3.0f) && pPlayerBase.GetPlayerPower() + 0.05f >= 3.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 3.0f && pPlayerBase.GetPlayerPower() < 4.0f) && pPlayerBase.GetPlayerPower() + 0.05f >= 4.0f))
                {
                    SoundManager.Instance.PlaySE(ESE.enSE_PowerUp);
                }
                pPlayerBase.SetPlayerPower(pPlayerBase.GetPlayerPower() + 0.05f);
                break;
            case EItemType.enType_PowerL:
                if ((pPlayerBase.GetPlayerPower() < 1.0f && pPlayerBase.GetPlayerPower() + 0.1f >= 1.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 1.0f && pPlayerBase.GetPlayerPower() < 2.0f) && pPlayerBase.GetPlayerPower() + 0.1f >= 2.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 2.0f && pPlayerBase.GetPlayerPower() < 3.0f) && pPlayerBase.GetPlayerPower() + 0.1f >= 3.0f)
                    || ((pPlayerBase.GetPlayerPower() >= 3.0f && pPlayerBase.GetPlayerPower() < 4.0f) && pPlayerBase.GetPlayerPower() + 0.1f >= 4.0f))
                {
                    SoundManager.Instance.PlaySE(ESE.enSE_PowerUp);
                }
                pPlayerBase.SetPlayerPower(pPlayerBase.GetPlayerPower() + 0.1f);
                break;
            case EItemType.enType_FullPowerM:
            case EItemType.enType_FullPowerL:
                pPlayerBase.SetPlayerPower(4.0f);
                break;
        }
        if (pPlayerBase.GetPlayerPower() > 4.0f)
        {
            pPlayerBase.SetPlayerPower(4.0f);
        }
    }
    public void SetGameScore(EItemType enItemType)
    {
        switch (enItemType)
        {
            case EItemType.enType_ScoreS:
                break;
            case EItemType.enType_ScoreM:
                break;
            case EItemType.enType_SpecialScoreS:
                break;
            case EItemType.enType_SpecialScoreM:
                break;
            case EItemType.enType_SpecialScoreL:
                break;
            default:
                break;
        }
    }
    public void DestroyItem()
    {
        pTimer.InitTimer(0, 0.0f, false);

        if (cHomingHandle != null)
        {
            Timing.Instance.KillCoroutinesOnInstance(cHomingHandle);
        }
        ItemManager.Instance.GetItemPool().ReturnPool(pItemBase.GetGameObject());
    }
    #endregion

    #region IENUMERATOR
    #endregion
}
