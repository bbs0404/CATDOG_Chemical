using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class ElementTable : SingletonBehaviour<ElementTable> {
    public GameObject[] ElementsObect = new GameObject[20];
    private Button[] ElementsButton = new Button[20];

    void Start()
    {
        for (int i = 0; i < 20; ++i)
            ElementsButton[i] = ElementsObect[i].GetComponent<Button>();
        ElementTableUpdate();
    }
    public void ElementTableUpdate()
    {
        if (SceneManager.GetActiveScene().name == "CompoundElement")
        {
            for (int i = 0; i < 20; ++i)
                ElementsButton[i].interactable = false;
            ElementsButton[0].interactable = true;
            ElementsButton[1].interactable = true;
            if (PlayerManager.villageProgress > 0)
            {
                if (InventoryManager.Element[0])
                    ElementsButton[2].interactable = true;
                if (InventoryManager.Element[1])
                    ElementsButton[9].interactable = true;
                for (int i = 3; i < 9; ++i)
                {
                    ElementsButton[i].interactable = true;
                }
            }
            if (PlayerManager.villageProgress > 1)
            {
                for (int i = 10; i < 18; ++i)
                {
                    if (InventoryManager.Element[i - 8])
                        ElementsButton[i].interactable = true;
                }
            }
            if (PlayerManager.villageProgress > 2)
            {
                for (int i = 18; i < 20; ++i)
                {
                    if (InventoryManager.Element[i - 8])
                        ElementsButton[i].interactable = true;
                }
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
