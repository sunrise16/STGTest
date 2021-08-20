#region USING
using System.Collections;
using UnityEngine;
#endregion

public class Timer
{
    #region VARIABLE
    private int iFlag;
    private int iRepeatCount;
    private int iRepeatLimit;
    private float fTime;
    private float fResetTime;
    private float fLimitTime;
    private float fDelayTime;
    private bool bDelay;
    private bool bSwitch;
    private bool bTrigger;
    #endregion

    #region PROPERTY
    public static float DeltaTime
    {
        get
        {
            return Time.deltaTime;
        }
    }
    #endregion

    #region CONSTRUCTOR
    public Timer(int iFlag = 0, float fDelayTime = 0.0f, bool bSwitch = true)
    {
        InitTimer(iFlag, fDelayTime, bSwitch);
    }
    public Timer(float fLimitTime, int iFlag = 0, float fDelayTime = 0.0f, bool bSwitch = true)
    {
        InitTimer(fLimitTime, iFlag, fDelayTime, bSwitch);
    }
    public Timer(int iRepeatLimit, float fLimitTime, int iFlag = 0, float fStartTime = 0.0f, float fResetTime = 0.0f, float fDelayTime = 0.0f, bool bSwitch = true)
    {
        InitTimer(iRepeatLimit, fLimitTime, iFlag, fStartTime, fResetTime, fDelayTime, bSwitch);
    }
    #endregion

    #region GET METHOD
    public int GetFlag() { return iFlag; }
    public int GetRepeatCount() { return iRepeatCount; }
    public int GetRepeatLimit() { return iRepeatLimit; }
    public float GetTime() { return fTime; }
    public float GetResetTime() { return fResetTime; }
    public float GetLimitTime() { return fLimitTime; }
    public float GetDelayTime() { return fDelayTime; }
    public bool GetDelay() { return bDelay; }
    public bool GetSwitch() { return bSwitch; }
    public bool GetTrigger() { return bTrigger; }
    #endregion

    #region SET METHOD
    public void SetFlag(int iFlag) { this.iFlag = iFlag; }
    public void SetRepeatCount(int iRepeatCount) { this.iRepeatCount = iRepeatCount; }
    public void SetRepeatLimit(int iRepeatLimit) { this.iRepeatLimit = iRepeatLimit; }
    public void SetTime(float fTime) { this.fTime = fTime; }
    public void SetResetTime(float fResetTime) { this.fResetTime = fResetTime; }
    public void SetLimitTime(float fLimitTime) { this.fLimitTime = fLimitTime; }
    public void SetDelayTime(float fDelayTime) { this.fDelayTime = fDelayTime; }
    public void SetDelay(bool bDelay) { this.bDelay = bDelay; }
    public void SetSwitch(bool bSwitch) { this.bSwitch = bSwitch; }
    public void SetTrigger(bool bTrigger) { this.bTrigger = bTrigger; }
    #endregion

    #region COMMON METHOD
    public void InitTimer(int iFlag = 0, float fDelayTime = 0.0f, bool bSwitch = true)
    {
        this.iFlag = iFlag;
        iRepeatCount = 0;
        iRepeatLimit = 1;
        fTime = 0.0f;
        fResetTime = 0.0f;
        fLimitTime = 0.1f;
        this.fDelayTime = fDelayTime;
        bDelay = true;
        this.bSwitch = bSwitch;
        bTrigger = false;
    }
    public void InitTimer(float fLimitTime, int iFlag = 0, float fDelayTime = 0.0f, bool bSwitch = true)
    {
        this.iFlag = iFlag;
        iRepeatCount = 0;
        iRepeatLimit = 1;
        fTime = 0.0f;
        fResetTime = 0.0f;
        this.fLimitTime = fLimitTime;
        if (fLimitTime <= 0.0f)
        {
            this.fLimitTime = 0.1f;
        }
        this.fDelayTime = fDelayTime;
        bDelay = true;
        this.bSwitch = bSwitch;
        bTrigger = false;
    }
    public void InitTimer(int iRepeatLimit, float fLimitTime, int iFlag = 0, float fStartTime = 0.0f, float fResetTime = 0.0f, float fDelayTime = 0.0f, bool bSwitch = true)
    {
        this.iFlag = iFlag;
        iRepeatCount = 0;
        this.iRepeatLimit = iRepeatLimit;
        if (iRepeatLimit < 0)
        {
            this.iRepeatLimit = 0;
        }
        fTime = fStartTime;
        this.fResetTime = fResetTime;
        this.fLimitTime = fLimitTime;
        if (fLimitTime <= 0.0f)
        {
            this.fLimitTime = 0.1f;
        }
        this.fDelayTime = fDelayTime;
        bDelay = true;
        this.bSwitch = bSwitch;
        bTrigger = false;
    }
    public void RunTimer()
    {
        if (bSwitch.Equals(false))
        {
            return;
        }

        if (bDelay.Equals(true))
        {
            if (fTime < fDelayTime)
            {
                fTime += DeltaTime;
            }
            else
            {
                bDelay = false;
            }
        }
        else
        {
            if (fTime < fLimitTime)
            {
                fTime += DeltaTime;
            }
            else
            {
                iRepeatCount++;
                bTrigger = true;
                if (iRepeatCount.Equals(iRepeatLimit))
                {
                    bSwitch = false;
                }
            }
        }
    }
    public void ResetTimer(float fResetTime)
    {
        fTime = fResetTime;
        bTrigger = false;
    }
    #endregion
}