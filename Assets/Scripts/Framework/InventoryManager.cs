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
    [SerializeField]
    private int proton = 0, neutron = 0, electron = 0;

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
