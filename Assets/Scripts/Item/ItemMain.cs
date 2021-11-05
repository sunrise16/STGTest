#region USING
using System;
using UnityEngine;
using MEC;
#endregion

public class ItemMain : MonoBehaviour
{
    #region VARIABLE
    public event DelegateItemBase pEvent;
    public DelegateCommon pCommonDelegate;

    private ItemBase pItemBase;
    private PlayerMain pPlayerMain;
    private PlayerBase pPlayerBase;
    private GameObject pPlayerObject;
    private Transform pPlayerTransform;
    private Transform pTempTransform;
    private Timer pTimer;
    private CoroutineHandle cHomingHandle;
    private float fDistance;
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

        // COLLIDER CHECK
        // pTempTransform = pPlayerTransform;
        // fDistance = Vector3.Distance(pItemBase.GetTransform().position, pTempTransform.position);
        // ColliderCheck(fDistance);

        // AUTO COLLECT
        if (pPlayerBase.GetPositionY() >= 2.0f)
        {
            pEvent(pItemBase);
        }
        if (pItemBase.GetAutoCollect().Equals(true))
        {
            pItemBase.SetPosition(Vector3.MoveTowards(pItemBase.GetPosition(), pPlayerBase.GetPosition(), 0.1f));
        }

        // MAIN TIMER RUN
        if (pItemBase.GetSpriteRotate().Equals(true) && pTimer.GetSwitch().Equals(true))
        {
            pTimer.RunTimer();

            if (pTimer.GetTrigger().Equals(true))
            {
                pItemBase.GetRigidbody().velocity = Vector2.zero;
                pItemBase.GetRigidbody().gravityScale = 0.1f;

                pTimer.ResetTimer(pTimer.GetResetTime());
                pTimer.SetSwitch(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D pCollider)
    {
        if (pCollider.name.Equals("Body"))
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
            pPlayerObject = GameManager.Instance.pPlayer;
            pPlayerMain = pPlayerObject.GetComponent<PlayerMain>();
            pPlayerBase = pPlayerMain.GetPlayerBase();
            pPlayerTransform = pPlayerObject.GetComponent<Transform>();
            pTimer = new Timer();
            pItemBase.SetSpriteRenderer(pItemBase.GetChildTransform(0).GetComponent<SpriteRenderer>());
            pItemBase.SetAnimator(pItemBase.GetChildTransform(0).GetComponent<Animator>());
            pItemBase.SetRigidbody(GetComponent<Rigidbody2D>());
            pItemBase.SetCircleCollider(GetComponent<CircleCollider2D>());
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
        fDistance = 0.0f;

        pItemBase.GetSpriteRenderer().sprite = GameManager.Instance.pItemSprite[Convert.ToInt32(enItemType)];
        pItemBase.GetRigidbody().gravityScale = 0.1f;
        if (bSpriteRotate.Equals(false))
        {
            pItemBase.GetRigidbody().AddForce(Vector2.up * 75.0f);
        }
        switch (enItemType)
        {
            case EItemType.enType_PowerS:
            case EItemType.enType_ScoreS:
            case EItemType.enType_SpecialScoreS:
                pItemBase.GetCircleCollider().radius = 0.05f;
                break;
            default:
                break;
        }
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
    public void ColliderCheck(float fDistance)
    {
        if (fDistance <= 0.1f)
        {
            pItemBase.GetCircleCollider().enabled = true;
        }
        else
        {
            pItemBase.GetCircleCollider().enabled = false;
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
        for (int i = 0; i < (int)pPlayerBase.GetPlayerPower(); i++)
        {
            if (pPlayerMain.GetSecondary(i).GetSecondaryObject().activeSelf.Equals(false))
            {
                pPlayerMain.GetSecondary(i).GetSecondaryObject().SetActive(true);
            }
        }
    }
    public void SetGameScore(EItemType enItemType)
    {
        switch (enItemType)
        {
            case EItemType.enType_PowerS:
            case EItemType.enType_PowerM:
            case EItemType.enType_PowerL:
                pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 100);
                break;
            case EItemType.enType_ScoreS:
                switch (GlobalData.enGameDifficulty)
                {
                    case EGameDifficulty.enDifficulty_Easy:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 100000);
                        break;
                    case EGameDifficulty.enDifficulty_Normal:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 150000);
                        break;
                    case EGameDifficulty.enDifficulty_Hard:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 200000);
                        break;
                    case EGameDifficulty.enDifficulty_Lunatic:
                    case EGameDifficulty.enDifficulty_Extra:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 300000);
                        break;
                    default:
                        break;
                }
                break;
            case EItemType.enType_ScoreM:
                switch (GlobalData.enGameDifficulty)
                {
                    case EGameDifficulty.enDifficulty_Easy:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 200000);
                        break;
                    case EGameDifficulty.enDifficulty_Normal:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 300000);
                        break;
                    case EGameDifficulty.enDifficulty_Hard:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 400000);
                        break;
                    case EGameDifficulty.enDifficulty_Lunatic:
                    case EGameDifficulty.enDifficulty_Extra:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 600000);
                        break;
                    default:
                        break;
                }
                break;
            case EItemType.enType_SpecialScoreS:
                switch (GlobalData.enGameDifficulty)
                {
                    case EGameDifficulty.enDifficulty_Easy:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (10 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Normal:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (20 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Hard:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (30 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Lunatic:
                    case EGameDifficulty.enDifficulty_Extra:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (50 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    default:
                        break;
                }
                break;
            case EItemType.enType_SpecialScoreM:
                switch (GlobalData.enGameDifficulty)
                {
                    case EGameDifficulty.enDifficulty_Easy:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (20 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Normal:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (30 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Hard:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (50 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Lunatic:
                    case EGameDifficulty.enDifficulty_Extra:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (70 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    default:
                        break;
                }
                break;
            case EItemType.enType_SpecialScoreL:
                switch (GlobalData.enGameDifficulty)
                {
                    case EGameDifficulty.enDifficulty_Easy:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (30 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Normal:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (50 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Hard:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (70 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    case EGameDifficulty.enDifficulty_Lunatic:
                    case EGameDifficulty.enDifficulty_Extra:
                        pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + (100 * (pPlayerBase.GetPlayerScoreItem() + 1)));
                        break;
                    default:
                        break;
                }
                break;
            case EItemType.enType_LifeFragmentS:
            case EItemType.enType_SpellFragmentS:
                pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 1000);
                break;
            case EItemType.enType_LifeFragmentL:
            case EItemType.enType_SpellFragmentL:
                pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 5000);
                break;
            case EItemType.enType_LifeS:
            case EItemType.enType_SpellS:
                pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 10000);
                break;
            case EItemType.enType_LifeL:
            case EItemType.enType_SpellL:
                pPlayerBase.SetPlayerCurrentScore(pPlayerBase.GetPlayerCurrentScore() + 50000);
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
}
