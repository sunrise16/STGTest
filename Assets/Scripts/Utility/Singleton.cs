#region USING
using UnityEngine;
#endregion

public class Singleton<T> where T : class, new()
{
    #region PROPERTY
    public static T Instance
    {
        get;
        private set;
    }
    #endregion

    #region CONSTRUCTOR
    static Singleton()
    {
        if (Instance == null)
        {
            Instance = new T();
        }
    }
    #endregion

    #region VIRTUAL METHOD
    public virtual void InitInstance()
    {

    }
    protected virtual void ExitInstance()
    {

    }
    #endregion

    #region COMMON METHOD
    public virtual void Clear()
    {
        Instance = null;
        Instance = new T();
    }
    #endregion
}

public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region VARIABLE
    private static T sInstance;
    private static object oLock = new object();
    private static bool bQuit = false;
    #endregion

    #region PROPERTY
    public static T Instance
    {
        get
        {
            if (bQuit)
            {
                return null;
            }

            lock (oLock)
            {
                if (sInstance == null)
                {
                    sInstance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        return sInstance;
                    }

                    if (sInstance == null)
                    {
                        GameObject singleton = new GameObject();
                        sInstance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).Name;

                        DontDestroyOnLoad(singleton);
                    }
                }
                return sInstance;
            }
        }
    }
    #endregion

    #region UNITY LIFE CYCLE
    protected virtual void OnDestroy()
    {
        bQuit = true;
    }
    #endregion
}