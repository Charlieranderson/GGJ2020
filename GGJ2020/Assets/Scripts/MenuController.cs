using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LevelController.instance.SubscribeToTaskUpdate(UpdateTaskUI);
    }



    void UpdateTaskUI(Task task)
    {
        Debug.Log("Updating Task UI: " + task.name);
    }
}
