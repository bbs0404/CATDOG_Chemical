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

[System.Serializable]
public class InventoryManager : SingletonBehaviour<InventoryManager>{
    public static int proton = 0, neutron = 0, electron = 0;

    public static bool[] Element = new bool[20] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
    public static bool[] Raw = new bool[4] { true, false, false, false };

    public int getProton()
    {
        return proton;
    }
    public int getNeutron()
    {
        return neutron;
    }
    public int getElectron(){
        return electron;
    }
    public void setProton(int num)
    {
        proton = num;
    }
    public void setNeutron(int num)
    {
        neutron = num;
    }
    public void setElectron(int num)
    {
        electron = num;
    }
}
