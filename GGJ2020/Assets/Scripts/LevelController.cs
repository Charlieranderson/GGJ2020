using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public PlayerInputs input;
    [SerializeField] List<Task> tasks;
    int tasksIndex = 0;

    public delegate void TaskUpdated(Task task);
    TaskUpdated taskUpdated;

    #region Singleton
    public static LevelController instance;

    private void Awake()
    {
        if(instance == null) {
            instance = this;
            InitializeLevelController();
            return;
        }
        else
        {
            Destroy(this.gameObject);
            return;

        }
    }

    void InitializeLevelController()
    {
        input = new PlayerInputs();
    }

    #endregion


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CompletedTask();
        }
    }


    public void SubscribeToTaskUpdate(TaskUpdated func)
    {
        taskUpdated += func;
    }

    /// <summary>
    /// Completes the next task in tasks
    /// </summary>
    public void CompletedTask()
    {
        if(tasksIndex < tasks.Count)
        {
            Task task = tasks[tasksIndex];
            task.isDone = true;

            InvokeTaskUpdated(task);
            tasksIndex++;
            Debug.Log("Completed task!");
        }


    }


    private void InvokeTaskUpdated(Task task)
    {
        if(taskUpdated != null)
        {
            taskUpdated.Invoke(task);
        }
    }
}
