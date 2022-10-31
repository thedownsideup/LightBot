using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandButtonHolder : MonoBehaviour
{
    [SerializeField] private List<GameObject> buttonContainers = new List<GameObject>();

    public void SetButtons(int numberOfButtons)
    {
        for (int i = 0; i < numberOfButtons; i++)
        {
            buttonContainers[i].gameObject.SetActive(true);
        }

        for (int i = numberOfButtons; i < buttonContainers.Count; i++)
        {
            buttonContainers[i].gameObject.SetActive(false);
        }
    }
}
