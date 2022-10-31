using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CommandController : MonoBehaviour
{
    [FormerlySerializedAs("command")] public CommandData commandData;

    public void OnClick()
    {
        Remove();
    }

    private void Remove()
    {
        Command_Manager.Instance.RemoveCommand(commandData);
        Destroy(gameObject);
    }
}
