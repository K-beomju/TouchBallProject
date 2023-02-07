using UnityEngine;

abstract public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public bool dontDestroyOnLoad = false;

    static readonly string objName = $"_{typeof(T).ToString()}";

    protected void CheckDuplicate()
    {
        T[] objs = FindObjectsOfType<T>();
        if (objs.Length <= 1)
            return;

        for (int i = 0; i < objs.Length; ++i)
        {
            if (objs[i].gameObject.name != objName)
                Destroy(objs[i].gameObject);
        }
    }

    protected virtual void Start()
    {
        CheckDuplicate();

        this.gameObject.name = objName;

        if (dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
            {
                T[] objs = FindObjectsOfType<T>();

                if (objs.Length > 1)
                {
                    for (int i = 0; i < objs.Length; ++i)
                    {
                        if (objs[i].gameObject.name != objName)
                            Destroy(objs[i].gameObject);
                        else
                            m_instance = objs[i];
                    }
                }
                else if (objs.Length == 1)
                {
                    m_instance = objs[0];
                }

                if (m_instance == null)
                {
                    Debug.LogWarning($"Cannot find {typeof(T)}, instantiating new object");
                    m_instance = new GameObject(objName).AddComponent<T>();
                }
            }

            return m_instance;
        }
    }
}