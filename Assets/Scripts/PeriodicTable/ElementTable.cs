using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ElementTable : SingletonBehaviour<ElementTable> {
    public GameObject[] ElementsObect = new GameObject[20];

    void Start()
    {
        ElementTableUpdate();
    }
    public void ElementTableUpdate()
    {
        if (SceneManager.GetActiveScene().name == "CompoundElement")
        {
            if (PlayerManager.villageProgress > 0)
            {
                for (int i = 2; i < 10; ++i)
                {
                    if (!InventoryManager.Element[i])
                        ElementsObect[i].SetActive(true);
                    if (i == 2 && !InventoryManager.Element[0] || i == 9 && !InventoryManager.Element[1])
                        ElementsObect[i].SetActive(false);
                }
            }
            if (PlayerManager.villageProgress > 1)
            {
                for (int i = 10; i < 18; ++i)
                {
                    if (!InventoryManager.Element[i] && InventoryManager.Element[i - 8])
                        ElementsObect[i].SetActive(true);
                }
            }
            if (PlayerManager.villageProgress > 2)
            {
                for (int i = 18; i < 20; ++i)
                {
                    if (!InventoryManager.Element[i] && InventoryManager.Element[i - 8])
                        ElementsObect[i].SetActive(true);
                }
            }
            for (int i = 0; i < 20; ++i)
            {
                if (InventoryManager.Element[i])
                    ElementsObect[i].GetComponent<Button>().enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < 20; ++i)
            {
                if (InventoryManager.Element[i])
                    ElementsObect[i].gameObject.SetActive(true);
                else
                    ElementsObect[i].gameObject.SetActive(false);
            }
        }
    }

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
