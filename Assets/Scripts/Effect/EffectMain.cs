#region USING
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class EffectMain : MonoBehaviour
{
    #region VARIABLE
    public DelegateCommon pStartDelegate;
    public DelegateCommon pCommonDelegate;
    public DelegateCommon pConditionDelegate;

    private EffectBase pEffectBase;
    private Timer pTimer;
    private Timer pLaserDelayTimer;
    private Timer pLaserActiveTimer;
    private CoroutineHandle cUpdate;
    private float fAlpha;
    private float fScale;
    #endregion

    #region GET METHOD
    public EffectBase GetEffectBase() { return pEffectBase; }
    public Timer GetTimer() { return pTimer; }
    public Timer GetLaserDelayTimer() { return pLaserDelayTimer; }
    public Timer GetLaserActiveTimer() { return pLaserActiveTimer; }
    public CoroutineHandle GetUpdate() { return cUpdate; }
    public float GetAlpha() { return fAlpha; }
    public float GetScale() { return fScale; }
    #endregion

    #region UNITY LIFE CYCLE
    public void FixedUpdate()
    {
        if (pEffectBase == null)
        {
            return;
        }

        // START EFFECT CHECK
        if (pStartDelegate != null)
        {
            pStartDelegate();
            pStartDelegate = null;
        }

        // LASER TIMER ON, OFF
        if (pEffectBase.GetEffectAnimationType().Equals(EEffectAnimationType.enType_HoldingLaserShot) ||
            pEffectBase.GetEffectAnimationType().Equals(EEffectAnimationType.enType_MovingLaserShot))
        {
            // SET LASER SCALE
            if (pLaserActiveTimer.GetSwitch().Equals(true))
            {
                fScale += 0.05f;
                if (fScale >= 1.0f)
                {
                    fScale = 1.0f;
                }
            }
            else
            {
                fScale -= 0.05f;
                if (pLaserDelayTimer.GetSwitch().Equals(false) && pLaserActiveTimer.GetSwitch().Equals(false))
                {
                    if (fScale <= 0.0f)
                    {
                        DestroyEffect();
                    }
                }
                else
                {
                    if (fScale <= 0.1f)
                    {
                        fScale = 0.1f;
                    }
                }
            }

            // PARENT ROTATE
            ParentRotate();

            // SWITCH ON, OFF
            if (pLaserDelayTimer.GetSwitch().Equals(true))
            {
                pLaserDelayTimer.RunTimer();
                if (pLaserDelayTimer.GetTrigger().Equals(true))
                {
                    pLaserDelayTimer.SetSwitch(false);
                    pLaserActiveTimer.SetSwitch(true);
                }
            }
            else
            {
                pLaserActiveTimer.RunTimer();
                if (pLaserActiveTimer.GetTrigger().Equals(true))
                {
                    pLaserActiveTimer.SetSwitch(false);
                }
            }

            // LASER SCALE, COLLIDER TRIGGER CHECK
            if (pEffectBase.GetTransform().childCount > 1)
            {
                for (int i = 1; i < pEffectBase.GetTransform().childCount; i++)
                {
                    BulletBase pBulletBase = pEffectBase.GetChildGameObject(i).GetComponent<BulletMain>().GetBulletBase();
                    pBulletBase.SetColliderTrigger((pLaserActiveTimer.GetSwitch().Equals(true) && fScale >= 1.0f) ? true : false);
                    pBulletBase.SetScaleX(fScale);
                }
            }
        }

        // MAIN TIMER RUN
        if (pTimer.GetSwitch().Equals(true))
        {
            pTimer.RunTimer();

            // TYPE CHECK
            switch (pEffectBase.GetEffectAnimationType())
            {
                case EEffectAnimationType.enType_Common:
                    break;
                case EEffectAnimationType.enType_BulletShot:
                    ScaleDown();
                    AlphaUp();
                    break;
                case EEffectAnimationType.enType_HoldingLaserShot:
                case EEffectAnimationType.enType_MovingLaserShot:
                case EEffectAnimationType.enType_MovingCurvedLaserShot:
                    SpriteRotate();
                    SpriteAlpha();
                    ScalePingpong();
                    break;
                case EEffectAnimationType.enType_Animation:
                    break;
                case EEffectAnimationType.enType_Explosion:
                    break;
                default:
                    break;
            }

            // CONDITION PATTERN
            if (pConditionDelegate != null)
            {
                pConditionDelegate();
            }

            // SWITCH OFF
            if (pTimer.GetTrigger().Equals(true))
            {
                if (pCommonDelegate != null)
                {
                    pCommonDelegate();
                }

                if (pTimer.GetRepeatCount().Equals(pTimer.GetRepeatLimit()))
                {
                    DestroyEffect();
                }
                else
                {
                    pTimer.ResetTimer(pTimer.GetResetTime());
                }
            }
        }

    }
    #endregion

    #region COMMON METHOD
    public void Init(GameObject pEffectObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale, Color pColor, EEffectType enEffectType, EEffectAnimationType enEffectAnimationType)
    {
        if (pEffectBase == null)
        {
            pEffectBase = new EffectBase(pEffectObject, pTransform, vSpawnPosition, vScale);
            pTimer = new Timer();
            pLaserDelayTimer = new Timer();
            pLaserActiveTimer = new Timer();
            pEffectBase.SetSpriteRenderer(pEffectBase.GetChildTransform(0).GetComponent<SpriteRenderer>());
            pEffectBase.SetAnimator(pEffectBase.GetChildTransform(0).GetComponent<Animator>());
        }
        else
        {
            pEffectBase.Init(pEffectObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Effect);
            pTimer.InitTimer();
            pLaserDelayTimer.InitTimer();
            pLaserActiveTimer.InitTimer();
        }
        pEffectBase.SetScale(Vector3.one);
        pEffectBase.SetChildScale(0, vScale);
        pEffectBase.SetEffectScalePingpongFlag(false);
        pEffectBase.SetColor(pColor);
        pEffectBase.SetEffectType(enEffectType);
        pEffectBase.SetEffectAnimationType(enEffectAnimationType);
        fAlpha = pEffectBase.GetColor().a;

        // cUpdate = Timing.RunCoroutine(Update());
    }
    public void ScaleUp()
    {
        pEffectBase.SetChildScaleX(0, pEffectBase.GetChildScale(0).x + pEffectBase.GetEffectScaleUpSpeed());
        pEffectBase.SetChildScaleY(0, pEffectBase.GetChildScale(0).y + pEffectBase.GetEffectScaleUpSpeed());
    }
    public void ScaleDown()
    {
        pEffectBase.SetChildScaleX(0, pEffectBase.GetChildScale(0).x - pEffectBase.GetEffectScaleDownSpeed());
        pEffectBase.SetChildScaleY(0, pEffectBase.GetChildScale(0).y - pEffectBase.GetEffectScaleDownSpeed());
    }
    public void ScalePingpong()
    {
        if (pEffectBase.GetChildScaleX(0) <= pEffectBase.GetEffectScalePingpongLimit() || pEffectBase.GetChildScaleY(0) <= pEffectBase.GetEffectScalePingpongLimit())
        {
            pEffectBase.SetEffectScalePingpongFlag(true);
        }    
        else if (pEffectBase.GetChildScaleX(0) >= pEffectBase.GetTempScaleX() || pEffectBase.GetChildScaleY(0) >= pEffectBase.GetTempScaleY())
        {
            pEffectBase.SetEffectScalePingpongFlag(false);
        }

        if (pEffectBase.GetEffectScalePingpongFlag().Equals(true))
        {
            ScaleUp();
        }
        else
        {
            ScaleDown();
        }
    }
    public void AlphaUp()
    {
        fAlpha += pEffectBase.GetEffectAlphaUpSpeed();
        if (fAlpha > 1.0f)
        {
            fAlpha = 1.0f;
        }
        pEffectBase.GetSpriteRenderer().color = new Color(pEffectBase.GetSpriteRenderer().color.r, pEffectBase.GetSpriteRenderer().color.g, pEffectBase.GetSpriteRenderer().color.b, fAlpha);
    }
    public void AlphaDown()
    {
        fAlpha -= pEffectBase.GetEffectAlphaDownSpeed();
        if (fAlpha < 1.0f)
        {
            fAlpha = 0.0f;
        }
        pEffectBase.GetSpriteRenderer().color = new Color(pEffectBase.GetSpriteRenderer().color.r, pEffectBase.GetSpriteRenderer().color.g, pEffectBase.GetSpriteRenderer().color.b, fAlpha);
    }
    public void ParentRotate()
    {
        transform.Rotate(new Vector3(0, 0, pEffectBase.GetEffectParentRotateSpeed() * Time.deltaTime));
        if (pEffectBase.GetTransform().childCount > 1)
        {
            for (int i = 1; i < pEffectBase.GetTransform().childCount; i++)
            {
                BulletBase pBulletBase = pEffectBase.GetChildGameObject(i).GetComponent<BulletMain>().GetBulletBase();
                pBulletBase.SetLocalRotationZ(transform.position.z);
            }
        }
    }
    public void SpriteRotate()
    {
        pEffectBase.GetChildTransform(0).Rotate(0.0f, 0.0f, pEffectBase.GetEffectSpriteRotateSpeed() * Time.deltaTime);
    }
    public void SpriteAlpha()
    {
        pEffectBase.GetSpriteRenderer().color = new Color(pEffectBase.GetSpriteRenderer().color.r, pEffectBase.GetSpriteRenderer().color.g, pEffectBase.GetSpriteRenderer().color.b, fAlpha);
    }
    public void DestroyEffect()
    {
        pTimer.InitTimer(0, 0.0f, false);
        pLaserDelayTimer.InitTimer(0, 0.0f, false);
        pLaserActiveTimer.InitTimer(0, 0.0f, false);

        // Timing.Instance.KillCoroutinesOnInstance(cUpdate);
        EffectManager.Instance.GetEffectPool().ReturnPool(pEffectBase.GetGameObject());
    }
    #endregion

    #region IENUMERATOR
    // public IEnumerator<float> Update()
    // {
    //     while (true)
    //     {
    //         yield return Timing.WaitForOneFrame;
    // 
    //         if (pEffectBase == null)
    //         {
    //             continue;
    //         }
    // 
    //         // TIMER CHECK
    //         if (pTimer.GetSwitch().Equals(true))
    //         {
    //             pTimer.RunTimer();
    // 
    //             // TYPE CHECK
    //             switch (pEffectBase.GetEffectAnimationType())
    //             {
    //                 case EEffectAnimationType.enType_Common:
    //                     break;
    //                 case EEffectAnimationType.enType_BulletShot:
    //                     ScaleDown();
    //                     AlphaUp();
    //                     break;
    //                 case EEffectAnimationType.enType_Animation:
    //                     break;
    //                 case EEffectAnimationType.enType_Explosion:
    //                     break;
    //                 default:
    //                     break;
    //             }
    // 
    //             // SWITCH OFF
    //             if (pTimer.GetFlag().Equals(true))
    //             {
    //                 if (pCommonDelegate != null)
    //                 {
    //                     pCommonDelegate();
    //                 }
    //                 pTimer.SetSwitch(false);
    //                 pTimer.InitTimer();
    //                 // Timing.Instance.KillCoroutinesOnInstance(cUpdate);
    //                 EffectManager.Instance.GetEffectPool().ReturnPool(pEffectBase.GetGameObject());
    //             }
    //         }
    //     }
    // }
    #endregion
}