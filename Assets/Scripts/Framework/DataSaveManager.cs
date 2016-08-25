using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

/// ※ 사용하기 전 필독을 권장합니다 ※
/// 
/// SaveHelper<T>는 T를 시스템에 저장하고 불러올 수 있도록 하는 헬퍼 클래스입니다.
/// T는 System.Serializable attribute를 반드시 가지고 있어야 합니다.
/// 즉 T는 직렬화(Serialize)가 가능한 데이터이어야 한다는 이야기이고, 이는 T의 멤버 
/// 변수들도 모두 직렬화할 수 있어야 한다는 이야기입니다.
/// 
/// ※ 직렬화(Serialize)란?
///     원래 파일은 일렬로 늘어진 바이트, 즉 바이트 배열로 이루어져 있습니다. 
///     C#등의 언어에서 사용하는 객체의 경우 정수나 문자열로만 이루어진 경우(모든 
///     데이터가 스택에 있을 경우) 데이터가 메모리상에서 일렬로 늘어져있기 때문에 
///     손쉽게 파일로 바꿀 수 있습니다. (스택이 뭔지 안다면 쉽게 이해가 되겠...죠?)
///     그러나 그 외의 레퍼런스 변수(힙에 존재하는 데이터)들은 메모리 주소 참조를 통해 
///     연결이 되어있기 때문에 이를 저장하기 위해선 추가적인 작업이 필요합니다.
///     많은 언어나 프레임워크의 경우 파일을 저장하기 위해 필요한 이런 일련의 작업들을
///     자동으로 처리해주는 기능이나 함수를 제공하고 있습니다. 이를 이용하면 사용자가 
///     직접 재귀적으로 레퍼런스를 찾아가며 직접 직렬화를 할 수고를 덜을 수 있습니다.
///     직렬화는 객체의 파일 저장과 네트워크 통신 등 여러군데에서 쓰이므로 개념을 잘 
///     알아두시는 것이 좋습니다.
/// 
/// Unity에서 직렬화가 가능한 데이터는 다음과 같습니다.
///     - C#의 기본 타입 (int, string, float, bool 등)
///     - 몇몇 built-in struct 타입 (Vector2, Vector3, Vector4, Quaternion,
///       Matrix4x4, Color, Rect, LayerMask 등)
///     - UnityEngine.Object로부터 상속된 클래스 (GameObject, Component, 
///       MonoBehavior, Texture2D, AnimationClip 등)
///     - 위의 자료형 변수만 가지고 있으며, System.Serializable attribute를
///       가지고 있는 클래스
/// 
/// 참고로 private 변수는 저장되지 않습니다. private 변수를 저장하기 위해선 
/// SerializeField attribute를 붙여주셔야 합니다.
/// 
/// 사용 예:
///     // 정의할 때
///     [System.Serializable]
///     class Data 
///     {
///         public Vector3 var1;  // 저장됨
///         
///         private int var2;     // 저장 안됨
///         
///         [SerializeField]
///         private float var3;   // 저장됨
///     }
///     
///     // 사용할 때
///     Data data = new Data();
///     ...   // data로 여러가지 함
///     SaveHelper(data, "/gamedata/playerlevel");  // 저장
///     Data data2 = LoadHelper<Data>("/gamedata/playerlevel");
///     
/// class와 struct는 서로 저장하는 방법이 다르니 코드를 참조하세요.
/// 
/// ※ 제대로 테스트해보지 않았습니다. 정상작동하지 않을 경우 이야기해주세요.
/// 
public static class SaveHelper
{
    // class의 경우 ref를 붙이지 않아도 됩니다.
    public static void Save<T>(T data, string path) where T: class {
        Save(ref data, path);
    }

    // struct의 경우 쓸데없는 복사를 막기 위해 ref를 붙여줘야 합니다. 
    public static void Save<T>(ref T data, string path) {
        try
        {
            FileInfo finfo = new FileInfo(Application.persistentDataPath + path);
            if (!finfo.Directory.Exists)
            {
                Debug.Log("Create new directory: " + finfo.Directory.FullName);
                Directory.CreateDirectory(finfo.Directory.FullName);
            }
            var fstream = File.Open(Application.persistentDataPath + path, FileMode.Create);
            var bf = new BinaryFormatter();
            bf.Serialize(fstream, data);
            fstream.Close();
        }
        catch (Exception e) {
            Debug.LogError(string.Format("Error while save {0}: {1}", path, e.Message));
            if (!Attribute.IsDefined(typeof(T), typeof(System.SerializableAttribute)))
                Debug.Log(typeof(T).Name + " does not have Serializable attribute.");
        }
    }
    
    // class의 경우 placeHolder를 지정하지 않으면 파일이 없을 때 null을 반환합니다.
    public static T Load<T>(string path) where T: class {
        try {
            if (!File.Exists(Application.persistentDataPath + path)) 
                throw new FileNotFoundException("not exists", Application.persistentDataPath + path);
            var bf = new BinaryFormatter();
            var fstream = File.Open(Application.persistentDataPath + path, FileMode.Open);
            var ret = bf.Deserialize(fstream) as T;
            fstream.Close();
            return ret;
        }
        catch (Exception e) {
            Debug.LogError(string.Format("Error while load {0}: {1}", path, e.Message));
        }
        return null;
    }

    // struct의 경우 변수에 null을 저장할 수 없기 때문에 placeHolder가 필요합니다.
    public static T Load<T>(string path, T placeHolder) {
        try {
            if (!File.Exists(Application.persistentDataPath + path))
                throw new FileNotFoundException("not exists", Application.persistentDataPath + path);
            var bf = new BinaryFormatter();
            var fstream = File.Open(Application.persistentDataPath + path, FileMode.Open);
            var ret = (T)bf.Deserialize(fstream);
            fstream.Close();
            return ret;
        }
        catch (Exception e) {
            Debug.LogError(string.Format("Error while load {0}: {1}", path, e.Message));
        }
        return placeHolder;
    }

    public static bool Delete(string path) {
        if (File.Exists(Application.persistentDataPath + path)) {
            File.Delete(Application.persistentDataPath + path);
            return true;
        }
        return false;
    }
}
