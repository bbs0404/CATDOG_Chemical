using UnityEngine;
using System.Collections;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {

    private static T s_Inst = null;

    public static T Inst()
    {
        if (s_Inst == null)
        {
            s_Inst = (T)FindObjectOfType(typeof(T));

            if (FindObjectsOfType(typeof(T)).Length > 1)
            {
                Debug.LogError("Multiple Singleton Exist");
                var list = FindObjectsOfType(typeof(T));
                foreach (var item in list)
                    Debug.LogError("SingleTon : " + item.name);
                return null;
            }
        }

        if (s_Inst == null)
        {
            GameObject singleton = new GameObject();
            s_Inst = singleton.AddComponent<T>();
            singleton.name = typeof(T).ToString();
            Debug.Log("Create new singleton : " + s_Inst.gameObject.name);
        }
        else
        {
            Debug.Log("Referencing existing singleton : " + s_Inst.gameObject.name);
        }
        return s_Inst;
    }

	protected void SetStatic()
    {
        var tmp = Inst().gameObject.transform;

        do
        {
            Debug.Log("GameObjecdt : " + tmp.name + " will not be destroy on level cahnge");
            DontDestroyOnLoad(tmp.gameObject);
            tmp = tmp.transform.parent;
        } while (tmp != null);
    }
}
