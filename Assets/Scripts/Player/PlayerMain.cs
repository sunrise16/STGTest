#region USING
using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;
#endregion

public class PlayerSecondary
{
    #region VARIABLE
    private GameObject pSecondaryObject;
    private PlayerBase pPlayerBase;
    private Vector3 vRefVector;
    private int iNumber;
    #endregion

    #region CONSTRUCTOR
    public PlayerSecondary(PlayerBase pPlayerBase, int iNumber)
    {
        pSecondaryObject = pPlayerBase.GetChildTransform(2).GetChild(iNumber).gameObject;
        this.pPlayerBase = pPlayerBase;
        vRefVector = Vector3.one;
        this.iNumber = iNumber;
    }
    #endregion

    #region GET METHOD
    public GameObject GetSecondaryObject() { return pSecondaryObject; }
    #endregion

    #region COMMON METHOD
    public void MoveSecondary(bool bKeyUp)
    {
        if (pPlayerBase.GetChildTransform(2).GetChild(iNumber).gameObject.activeSelf.Equals(false))
            return;

        if ((int)pPlayerBase.GetPlayerPower() == 1 || (int)pPlayerBase.GetPlayerPower() == 3)
        {
            if (((int)pPlayerBase.GetPlayerPower() == 1 && iNumber.Equals(0)) || ((int)pPlayerBase.GetPlayerPower() == 3 && iNumber.Equals(2)))
            {
                pPlayerBase.GetChildTransform(2).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(2).GetChild(iNumber).position,
                    pPlayerBase.GetChildTransform(2).GetChild(bKeyUp.Equals(true) ? 15 : 14).position, ref vRefVector, 0.05f);
            }
            else
            {
                pPlayerBase.GetChildTransform(2).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(2).GetChild(iNumber).position,
                    pPlayerBase.GetChildTransform(2).GetChild(bKeyUp.Equals(true) ? 10 + iNumber : 6 + iNumber).position, ref vRefVector, 0.05f);
            }
        }
        else
        {
            pPlayerBase.GetChildTransform(2).GetChild(iNumber).position = Vector3.SmoothDamp(pPlayerBase.GetChildTransform(2).GetChild(iNumber).position,
                pPlayerBase.GetChildTransform(2).GetChild(bKeyUp.Equals(true) ? 10 + iNumber : 6 + iNumber).position, ref vRefVector, 0.05f);
        }
    }
    #endregion
}

public class PlayerMain : MonoBehaviour
{
    #region VARIABLE
    private SpriteRenderer pPlayerSprite;
    private SpriteRenderer pHitPointSprite;
    private List<PlayerSecondary> pSecondaryList;
    private PlayerBase pPlayerBase;
    private Timer pShotTimer;
    private Timer pSpellTimer;
    private Vector2 vMoveSpeedVector;
    private Vector2 vMargin;
    private float fPlayerAlpha;
    private float fHitPointAlpha;
    #endregion

    #region GET METHOD
    public PlayerSecondary GetSecondary(int iIndex) { return pSecondaryList[iIndex]; }
    public PlayerBase GetPlayerBase() { return pPlayerBase; }
    #endregion

    #region UNITY LIFE CYCLE
    public void FixedUpdate()
    {
        if (pPlayerBase.GetDeath().Equals(false))
        {
            PlayerMove();
            ShotBullet();
            UseSpell();
            MoveInScreen();
        }
        ControlHitPoint();

        // PLAYER SPRITE
        pPlayerSprite.color = new Color(1.0f, 1.0f, 1.0f, fPlayerAlpha);

        // PLAYER SECONDARY
        for (int i = 0; i < 4; i++)
        {
            pSecondaryList[i].MoveSecondary(pPlayerBase.GetSlowMode().Equals(true) ? true : false);
        }

        // PLAYER SPELL TIMER
        if (pPlayerBase.GetUsingSpell().Equals(true))
        {
            pSpellTimer.RunTimer();
            if (pSpellTimer.GetTrigger().Equals(true))
            {
                pPlayerBase.SetUsingSpell(false);
                pSpellTimer.ResetTimer(pSpellTimer.GetResetTime());
            }
        }
    }
    #endregion

    #region COMMON METHOD
    public void Init()
    {
        GameObject pPlayerObject = this.gameObject;
        Transform pTransform = pPlayerObject.GetComponent<Transform>();
        Vector3 vSpawnPosition = new Vector3(0.0f, -3.0f);
        Vector3 vScale = Vector3.one;

        if (pPlayerBase == null)
        {
            pPlayerBase = new PlayerBase(pPlayerObject, pTransform, vSpawnPosition, vScale);
            pShotTimer = new Timer(0, 0.03f, 0);
            pSpellTimer = new Timer(0, 3.0f, 0);
            pPlayerBase.SetCamera(Camera.main);
            pPlayerBase.SetAnimator(pTransform.GetChild(0).GetComponent<Animator>());
            pPlayerBase.SetSpriteRenderer(pTransform.GetChild(1).GetComponent<SpriteRenderer>());
            pPlayerBase.SetAudioSource(GetComponent<AudioSource>());
            pPlayerBase.SetRigidbody2D(GetComponent<Rigidbody2D>());
            pPlayerBase.SetAction((string szTrigger1, string szTrigger2, string szTrigger3) =>
            {
                pPlayerBase.GetAnimator().SetTrigger(szTrigger1);
                pPlayerBase.GetAnimator().ResetTrigger(szTrigger2);
                pPlayerBase.GetAnimator().ResetTrigger(szTrigger3);
            });
            pPlayerSprite = pPlayerBase.GetChildTransform(0).GetComponent<SpriteRenderer>();
            pHitPointSprite = pPlayerBase.GetChildTransform(1).GetComponent<SpriteRenderer>();

            pSecondaryList = new List<PlayerSecondary>();
            for (int i = 0; i < 4; i++)
            {
                pSecondaryList.Add(new PlayerSecondary(pPlayerBase, i));
            }
        }
        else
        {
            pPlayerBase.Init(pPlayerObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Player);
            pShotTimer.InitTimer(0, 0.03f, 0);
            pSpellTimer.InitTimer(0, 3.0f, 0);
        }
        pPlayerBase.SetColor(new Color(1, 1, 1, 0));
        pPlayerBase.SetSlowMode(false);
        pPlayerBase.SetDeath(false);
        pPlayerBase.SetInvincible(false);
        pPlayerBase.SetUsingSpell(false);
        vMoveSpeedVector = Vector2.zero;
        vMargin = new Vector2(0.03f, 0.03f);
        fPlayerAlpha = 1.0f;
        fHitPointAlpha = 0.0f;
    }
    public void PlayerMove()
    {
        // SLOW MODE CHECK
        pPlayerBase.SetSlowMode(Input.GetKey(KeyCode.LeftShift) ? true : false);

        // PLAYER MOVING
        pPlayerBase.GetRigidbody2D().velocity = vMoveSpeedVector;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                vMoveSpeedVector.x = -pPlayerBase.GetPlayerMoveSpeed() + 0.3f;
            }
            else
            {
                vMoveSpeedVector.x = -pPlayerBase.GetPlayerMoveSpeed();
            }
            pPlayerBase.GetAction()("isLeftMove", "isIdle", "isRightMove");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
            {
                vMoveSpeedVector.x = pPlayerBase.GetPlayerMoveSpeed() - 0.3f;
            }
            else
            {
                vMoveSpeedVector.x = pPlayerBase.GetPlayerMoveSpeed();
            }
            pPlayerBase.GetAction()("isRightMove", "isIdle", "isLeftMove");
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                vMoveSpeedVector.y = pPlayerBase.GetPlayerMoveSpeed() - 0.3f;
            }
            else
            {
                vMoveSpeedVector.y = pPlayerBase.GetPlayerMoveSpeed();
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                vMoveSpeedVector.y = -pPlayerBase.GetPlayerMoveSpeed() + 0.3f;
            }
            else
            {
                vMoveSpeedVector.y = -pPlayerBase.GetPlayerMoveSpeed();
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            vMoveSpeedVector.x = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            vMoveSpeedVector.y = 0.0f;
        }

        // PLAYER SPRITE CHECK
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                pPlayerBase.GetAction()("isRightMove", "isIdle", "isLeftMove");
            }
            else
            {
                pPlayerBase.GetAction()("isIdle", "isLeftMove", "isRightMove");
            }
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                pPlayerBase.GetAction()("isLeftMove", "isIdle", "isRightMove");
            }
            else
            {
                pPlayerBase.GetAction()("isIdle", "isLeftMove", "isRightMove");
            }
        }
    }
    public void ShotBullet()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            if (pShotTimer.GetRepeatCount().Equals(0))
            {
                ExtractBullet(pPlayerBase.GetPlayerType(), pPlayerBase.GetPlayerWeaponType());
                pShotTimer.SetRepeatCount(pShotTimer.GetRepeatCount() + 1);
                SoundManager.Instance.PlaySE(ESE.enSE_PlSt00, 1.0f);
                pShotTimer.ResetTimer(pShotTimer.GetResetTime());
            }
            else
            {
                pShotTimer.RunTimer();
                if (pShotTimer.GetTrigger().Equals(true))
                {
                    ExtractBullet(pPlayerBase.GetPlayerType(), pPlayerBase.GetPlayerWeaponType());
                    pShotTimer.SetRepeatCount(pShotTimer.GetRepeatCount() + 1);
                    SoundManager.Instance.PlaySE(ESE.enSE_PlSt00, 1.0f);
                    pShotTimer.ResetTimer(pShotTimer.GetResetTime());
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            pShotTimer.SetRepeatCount(0);
            pShotTimer.ResetTimer(pShotTimer.GetResetTime());
        }
    }
    public void ExtractBullet(EPlayerType enPlayerType, EPlayerWeaponType enPlayerWeaponType)
    {
        Vector3 vPosition = pPlayerBase.GetPosition();
        Vector3 vSubPosition = Vector3.one;

        if (enPlayerType.Equals(EPlayerType.enType_Reimu))
        {
            // PRIMARY SHOT
            for (int i = 0; i < 2; i++)
            {
                GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                    (pPlayerBase.GetChildTransform(2).GetChild(i + 4).position, Vector3.one, EBulletType.enType_Box, EBulletShooter.enShooter_Player,
                    EEnemyBulletType.None, EPlayerBulletType.enType_ReimuPrimary, 0.6f, 10.0f);
                BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                BulletBase pBulletBase = pBulletMain.GetBulletBase();

                pBulletBase.SetChildRotationZ(0, 90.0f);
                pBulletBase.SetUniqueNumber(0);
                pBulletMain.GetPatternTimer().InitTimer();
                pBulletMain.GetRotateTimer().InitTimer();
                pBulletBase.SetBulletSpeed(18.0f);
                pBulletBase.SetBulletRotate(0.0f);
                pBulletBase.SetBulletOption();
                pBulletBase.SetCollisionDestroy(true);
                pBulletBase.SetColliderTrigger(true);
                pBulletBase.SetHoming(false);
                pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSprite[Convert.ToInt32(EPlayerBulletType.enType_ReimuPrimary)];
            }

            // SECONDARY SHOT
            if (pPlayerBase.GetPlayerPower() >= 1.0f)
            {
                for (int i = 0; i < (int)pPlayerBase.GetPlayerPower(); i++)
                {
                    if (pPlayerBase.GetSlowMode().Equals(true))
                    {
                        GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pPlayerBase.GetChildTransform(2).GetChild(i).position, Vector3.one, EBulletType.enType_Capsule, EBulletShooter.enShooter_Player,
                            EEnemyBulletType.None, EPlayerBulletType.enType_ReimuSecondary_Niddle, 0.6f, 10.0f);
                        BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                        BulletBase pBulletBase = pBulletMain.GetBulletBase();

                        pBulletBase.SetChildRotationZ(0, 90.0f);
                        pBulletBase.SetUniqueNumber(0);
                        pBulletMain.GetPatternTimer().InitTimer();
                        pBulletMain.GetRotateTimer().InitTimer();
                        pBulletBase.SetBulletSpeed(18.0f);
                        pBulletBase.SetBulletRotate(0.0f);
                        pBulletBase.SetBulletOption();
                        pBulletBase.SetCollisionDestroy(true);
                        pBulletBase.SetColliderTrigger(true);
                        pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSprite[Convert.ToInt32(EPlayerBulletType.enType_ReimuSecondary_Niddle)];
                    }
                    else
                    {
                        if ((pShotTimer.GetRepeatCount() % 3).Equals(0))
                        {
                            GameObject pBulletObject = BulletManager.Instance.GetBulletPool().ExtractBullet
                            (pPlayerBase.GetChildTransform(2).GetChild(i).position, Vector3.one, EBulletType.enType_Box, EBulletShooter.enShooter_Player,
                            EEnemyBulletType.None, EPlayerBulletType.enType_ReimuSecondary_Homing, 0.6f, 20.0f, true);
                            BulletMain pBulletMain = pBulletObject.GetComponent<BulletMain>();
                            BulletBase pBulletBase = pBulletMain.GetBulletBase();

                            pBulletBase.SetChildRotationZ(0, 90.0f);
                            pBulletBase.SetUniqueNumber(0);
                            pBulletMain.GetPatternTimer().InitTimer();
                            pBulletMain.GetRotateTimer().InitTimer();
                            pBulletBase.SetBulletSpeed(8.0f);
                            switch ((int)pPlayerBase.GetPlayerPower())
                            {
                                case 1:
                                    pBulletBase.SetBulletRotate(0.0f);
                                    break;
                                case 2:
                                    pBulletBase.SetBulletRotate(-22.5f + (45.0f * i));
                                    break;
                                case 3:
                                    pBulletBase.SetBulletRotate(i.Equals(2) ? 0.0f : -22.5f + (45.0f * i));
                                    break;
                                case 4:
                                    pBulletBase.SetBulletRotate(((i < 2) ? 22.5f : 45.0f) - ((i < 2) ? 45.0f * (i % 2) : 90.0f * (i % 2)));
                                    break;
                            }
                            pBulletBase.SetBulletOption();
                            pBulletBase.SetCollisionDestroy(true);
                            pBulletBase.SetColliderTrigger(true);
                            pBulletBase.GetSpriteRenderer().sprite = GameManager.Instance.pPlayerSprite[Convert.ToInt32(EPlayerBulletType.enType_ReimuSecondary_Homing)];
                        }
                    }
                }
            }
        }
        else if (enPlayerType.Equals(EPlayerType.enType_Marisa))
        {
            // UNDER CONSTRUCTION
        }
    }
    public void UseSpell()
    {
        if (Input.GetKeyDown(KeyCode.X) && pPlayerBase.GetUsingSpell().Equals(false) && pPlayerBase.GetPlayerSpell() > 0)
        {
            pPlayerBase.SetUsingSpell(true);
            pPlayerBase.SetPlayerSpell(pPlayerBase.GetPlayerSpell() - 1);
            Timing.RunCoroutine(PlayerInvincible(3.0f));
            Transform pTransform = GameObject.Find("ActiveBullets").transform;

            for (int i = 0; i < pTransform.childCount; i++)
            {
                ItemManager.Instance.GetItemPool().ExtractItem(pTransform.GetChild(i).position, Vector3.one, Color.white, EItemType.enType_SpecialScoreS, 10.0f);
            }
            ItemManager.Instance.ActiveAutoCollectAll(true);
            for (int i = 0; i < pTransform.childCount; i++)
            {
                BulletManager.Instance.GetBulletPool().ReturnPool(pTransform.GetChild(i).gameObject);
                i--;
            }
        }
    }
    public void ControlHitPoint()
    {
        if (pPlayerBase.GetSlowMode().Equals(true))
        {
            fHitPointAlpha += 0.1f;
            if (fHitPointAlpha > 1.0f)
            {
                fHitPointAlpha = 1.0f;
            }
        }
        else
        {
            fHitPointAlpha -= 0.1f;
            if (fHitPointAlpha < 0.0f)
            {
                fHitPointAlpha = 0.0f;
            }
        }

        if (pPlayerBase.GetDeath().Equals(true))
        {
            fHitPointAlpha = 0.0f;
        }
        pHitPointSprite.color = new Color(1.0f, 1.0f, 1.0f, fHitPointAlpha);
    }
    public void MoveInScreen()
    {
        Vector3 vPosition = pPlayerBase.GetCamera().WorldToViewportPoint(pPlayerBase.GetPosition());
        vPosition.x = Mathf.Clamp(vPosition.x, 0.0f + vMargin.x, 1.0f - vMargin.x);
        vPosition.y = Mathf.Clamp(vPosition.y, 0.0f + vMargin.y, 1.0f - vMargin.y);

        pPlayerBase.SetPosition(pPlayerBase.GetCamera().ViewportToWorldPoint(vPosition));
    }
    public void SetPlayerPower()
    {
        pPlayerBase.SetPlayerPower(pPlayerBase.GetPlayerPower() - 0.5f);
        if (pPlayerBase.GetPlayerPower() < 0.0f)
        {
            pPlayerBase.SetPlayerPower(0.0f);
        }
        foreach (PlayerSecondary pSecondary in pSecondaryList)
        {
            pSecondary.GetSecondaryObject().SetActive(false);
        }
        for (int i = 0; i < (int)pPlayerBase.GetPlayerPower(); i++)
        {
            if (GetSecondary(i).GetSecondaryObject().activeSelf.Equals(false))
            {
                GetSecondary(i).GetSecondaryObject().SetActive(true);
            }
        }
    }
    #endregion

    #region IENUMERATOR
    public IEnumerator<float> PlayerDeath()
    {
        pPlayerBase.SetDeath(true);
        pPlayerBase.SetSlowMode(false);
        pPlayerBase.SetPlayerMissCount(pPlayerBase.GetPlayerMissCount() + 1);
        SetPlayerPower();

        fPlayerAlpha = 0.0f;
        pPlayerBase.GetRigidbody2D().velocity = Vector2.zero;
        vMoveSpeedVector = Vector2.zero;
        pPlayerBase.GetAction()("isIdle", "isLeftMove", "isRightMove");
        pPlayerBase.GetChildGameObject(2).SetActive(false);
        SoundManager.Instance.PlaySE(ESE.enSE_PlDead00, 0.5f);

        // CREATE EFFECT HERE

        yield return Timing.WaitForSeconds(1.0f);

        fPlayerAlpha = 0.8f;
        pPlayerBase.SetPosition(new Vector3(0.0f, -4.5f, 0.0f));
        pPlayerBase.GetChildGameObject(2).SetActive(true);
        if (pPlayerBase.GetPlayerLife() <= 0)
        {
            // 임시 게임 종료
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            pPlayerBase.SetPlayerLife(pPlayerBase.GetPlayerLife() - 1);
            pPlayerBase.SetPlayerSpell(GlobalData.iStartSpell);
        }
        iTween.MoveTo(pPlayerBase.GetGameObject(), iTween.Hash("position", new Vector3(0.0f, -3.25f, 0.0f), "easetype", iTween.EaseType.linear, "time", 1.0f));

        yield return Timing.WaitForSeconds(1.0f);

        pPlayerBase.SetDeath(false);
        Timing.RunCoroutine(PlayerInvincible(3.0f));

        yield break;
    }
    public IEnumerator<float> PlayerInvincible(float fTime)
    {
        float fTempTime = 0.0f;

        pPlayerBase.SetInvincible(true);

        while (fTempTime <= fTime)
        {
            fPlayerAlpha = fPlayerAlpha.Equals(0.3f) ? 0.8f : 0.3f;

            yield return Timing.WaitForSeconds(0.03f);
            fTempTime += 0.03f;
        }
        fPlayerAlpha = 1.0f;
        pPlayerBase.SetInvincible(false);

        yield break;
    }
    #endregion
}