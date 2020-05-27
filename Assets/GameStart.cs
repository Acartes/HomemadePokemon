using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public InteractableItems interactableItems;
    public GridRules gridRules;

    // Start is called before the first frame update
    void Start()
    {
        foreach (InGameEntity item in interactableItems.interactableEntities)
        {
            gridRules.blockPosition((int)item.transform.position.x, (int)item.transform.position.y);
        }
    }
}
