using UnityEngine;
using System.Collections;
using System.Collections.Generic;

class CollectedElements
{
    int num; // 원소번호
    int period; //주기

    CollectedElements(int num, int period)
    {
        this.num = num;
        this.period = period;
    }


}

public class InventoryManager : SingletonBehaviour<InventoryManager>{
    public static int _proton = 0, _neutron = 0, _electron = 0;
    public static int proton {
        get { return _proton; }
        set {
            if (_proton != value) {
                _proton = value;
                Save1();
            }
        }
    }
    public static int neutron {
        get { return _neutron; }
        set {
            if (_neutron != value) {
                _neutron = value;
                Save1();
            }
        }
    }
    public static int electron {
        get { return _electron; }
        set {
            if (_electron != value) {
                _electron = value;
                Save1();
            }
        }
    }

    public class _Element
    {
        public bool[] data = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        public bool this[int index] {
            get { return data[index]; }
            set {
                if (data[index] != value) {
                    data[index] = value;
                    Save2();
                }
            }
        }
    }
    public static readonly _Element Element = new _Element();
    public class _Raw
    {
        public bool[] data = new bool[4] {true, false, false, false};
        public bool this[int index] {
            get { return data[index]; }
            set {
                if (data[index] != value) {
                    data[index] = value;
                    Save2();
                }
            }
        }
    }
    public static readonly _Raw Raw = new _Raw();

    void Awake() {
        Load1();
        Load2();
    }

    static void Save1() {
        Debug.Log("InventoryManager.Save1 called");
        var data = new InventoryManagerData1();
        data.proton = proton;
        data.neutron = neutron;
        data.electron = electron;
        SaveHelper.Save(data, "/inventory_manager_data_1");
    }

    public static void Load1() {
        Debug.Log("InventoryManager.Load1 called");
        var data = SaveHelper.Load<InventoryManagerData1>("/inventory_manager_data_1");
        if (data != null) {
            proton = data.proton;
            neutron = data.neutron;
            electron = data.electron;
        }
    }

    static void Save2() {
        Debug.Log("InventoryManager.Save2 called");
        var data = new InventoryManagerData2();
        data.Element = Element.data;
        data.Raw = Raw.data;
        SaveHelper.Save(data, "/inventory_manager_data_2");
    }

    public static void Load2() {
        Debug.Log("InventoryManager.Load2 called");
        var data = SaveHelper.Load<InventoryManagerData2>("/inventory_manager_data_2");
        if (data != null) {
            Element.data = data.Element;
            Raw.data = data.Raw;
        }
    }
}


[System.Serializable]
class InventoryManagerData1
{
    public int proton, neutron, electron;
}

[System.Serializable]
class InventoryManagerData2
{
    public bool[] Element;
    public bool[] Raw;
}