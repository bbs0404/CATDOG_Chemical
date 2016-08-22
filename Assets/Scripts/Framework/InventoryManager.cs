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
    int proton; //양성자
    int neutron; //중성자
    int electron; // 전자
}
