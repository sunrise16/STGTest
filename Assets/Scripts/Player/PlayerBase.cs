#region USING
using System;
using UnityEngine;
#endregion

public class PlayerBase : GameObjectBase, IObjectBase
{
    #region VARIABLE
    private Action<string, string, string> pAction;
    private Camera pCamera;
    private SpriteRenderer pSpriteRenderer;
    private Animator pAnimator;
    private Rigidbody2D pRigidbody2D;
    private Color pColor;
    private EPlayerType enPlayerType;
    private EPlayerWeaponType enPlayerWeaponType;
    private int iPlayerLife;
    private int iPlayerLifeFragment;
    private int iPlayerSpell;
    private int iPlayerSpellFragment;
    private int iPlayerScoreItem;
    private int iPlayerGrazeCount;
    private float fPlayerPower;
    private float fPlayerAttackDamage;
    private float fPlayerMoveSpeedFast;
    private float fPlayerMoveSpeedSlow;
    private bool bSlowMode;
    private bool bDeath;
    private bool bRevive;
    #endregion

    #region CONSTRUCTOR
    public PlayerBase(GameObject pPlayerObject, Transform pTransform, Vector3 vSpawnPosition, Vector3 vScale)
    {
        base.Init(pPlayerObject, pTransform, vSpawnPosition, vScale, EGameObjectType.enType_Player);

        enPlayerType = GlobalData.enSelectPlayerType;
        enPlayerWeaponType = GlobalData.enSelectPlayerWeaponType;
        switch (enPlayerType)
        {
            case EPlayerType.enType_Reimu:
                fPlayerAttackDamage = (enPlayerWeaponType.Equals(EPlayerWeaponType.enType_A)) ? 1.2f : 1.3f;
                fPlayerMoveSpeedFast = (enPlayerWeaponType.Equals(EPlayerWeaponType.enType_A)) ? 3.5f : 3.8f;
                fPlayerMoveSpeedSlow = (enPlayerWeaponType.Equals(EPlayerWeaponType.enType_A)) ? 1.2f : 1.4f;
                break;
            case EPlayerType.enType_Marisa:
                fPlayerAttackDamage = (enPlayerWeaponType.Equals(EPlayerWeaponType.enType_A)) ? 1.4f : 1.2f;
                fPlayerMoveSpeedFast = (enPlayerWeaponType.Equals(EPlayerWeaponType.enType_A)) ? 4.2f : 4.0f;
                fPlayerMoveSpeedSlow = (enPlayerWeaponType.Equals(EPlayerWeaponType.enType_A)) ? 1.8f : 1.6f;
                break;
            default:
                break;
        }
        bSlowMode = false;
        bDeath = false;
        bRevive = false;
    }
    #endregion

    #region GET METHOD
    public Action<string, string, string> GetAction() { return pAction; }
    public Camera GetCamera() { return pCamera; }
    public SpriteRenderer GetSpriteRenderer() { return pSpriteRenderer; }
    public Animator GetAnimator() { return pAnimator; }
    public Rigidbody2D GetRigidbody2D() { return pRigidbody2D; }
    public Color GetColor() { return pColor; }
    public EPlayerType GetPlayerType() { return enPlayerType; }
    public EPlayerWeaponType GetPlayerWeaponType() { return enPlayerWeaponType; }
    public int GetPlayerLife() { return iPlayerLife; }
    public int GetPlayerLifeFragment() { return iPlayerLifeFragment; }
    public int GetPlayerSpell() { return iPlayerSpell; }
    public int GetPlayerSpellFragment() { return iPlayerSpellFragment; }
    public int GetPlayerScoreItem() { return iPlayerScoreItem; }
    public int GetPlayerGrazeCount() { return iPlayerGrazeCount; }
    public float GetPlayerPower() { return fPlayerPower; }
    public float GetPlayerAttackDamage() { return fPlayerAttackDamage; }
    public float GetPlayerMoveSpeed() { return !bSlowMode ? fPlayerMoveSpeedFast : fPlayerMoveSpeedSlow; }
    public bool GetSlowMode() { return bSlowMode; }
    public bool GetDeath() { return bDeath; }
    public bool GetRevive() { return bRevive; }
    #endregion

    #region SET METHOD
    public void SetAction(Action<string, string, string> pAction) { this.pAction = pAction; }
    public void SetCamera(Camera pCamera) { this.pCamera = pCamera; }
    public void SetSpriteRenderer(SpriteRenderer pSpriteRenderer) { this.pSpriteRenderer = pSpriteRenderer; }
    public void SetAnimator(Animator pAnimator) { this.pAnimator = pAnimator; }
    public void SetRigidbody2D(Rigidbody2D pRigidbody2D) { this.pRigidbody2D = pRigidbody2D; }
    public void SetColor(Color pColor) { this.pColor = pColor; }
    public void SetPlayerType(EPlayerType enPlayerType) { this.enPlayerType = enPlayerType; }
    public void SetPlayerWeaponType(EPlayerWeaponType enPlayerWeaponType) { this.enPlayerWeaponType = enPlayerWeaponType; }
    public void SetPlayerLife(int iPlayerLife) { this.iPlayerLife = iPlayerLife; }
    public void SetPlayerLifeFragment(int iPlayerLifeFragment) { this.iPlayerLifeFragment = iPlayerLifeFragment; }
    public void SetPlayerSpell(int iPlayerSpell) { this.iPlayerSpell = iPlayerSpell; }
    public void SetPlayerSpellFragment(int iPlayerSpellFragment) { this.iPlayerSpellFragment = iPlayerSpellFragment; }
    public void SetPlayerScoreItem(int iPlayerScoreItem) { this.iPlayerScoreItem = iPlayerScoreItem; }
    public void SetPlayerGrazeCount(int iPlayerGrazeCount) { this.iPlayerGrazeCount = iPlayerGrazeCount; }
    public void SetPlayerPower(float fPlayerPower) { this.fPlayerPower = fPlayerPower; }
    public void SetPlayerAttackDamage(float fPlayerAttackDamage) { this.fPlayerAttackDamage = fPlayerAttackDamage; }
    public void SetPlayerMoveSpeed(float fPlayerMoveSpeedFast, float fPlayerMoveSpeedSlow)
    {
        this.fPlayerMoveSpeedFast = fPlayerMoveSpeedFast;
        this.fPlayerMoveSpeedSlow = fPlayerMoveSpeedSlow;
    }
    public void SetSlowMode(bool bSlowMode) { this.bSlowMode = bSlowMode; }
    public void SetDeath(bool bDeath) { this.bDeath = bDeath; }
    public void SetRevive(bool bRevive) { this.bRevive = bRevive; }
    #endregion

    #region COMMON METHOD
    public override void AllReset()
    {
        SetColor(Color.white);
        SetPlayerType(EPlayerType.None);
        SetPlayerWeaponType(EPlayerWeaponType.None);
        SetPlayerLife(0);
        SetPlayerLifeFragment(0);
        SetPlayerSpell(0);
        SetPlayerSpellFragment(0);
        SetPlayerScoreItem(0);
        SetPlayerGrazeCount(0);
        SetPlayerPower(0.0f);
        SetPlayerAttackDamage(0.0f);
        SetPlayerMoveSpeed(0.0f, 0.0f);
        SetSlowMode(false);
        SetDeath(false);
        SetRevive(false);

        base.AllReset();
    }
    #endregion
}