using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{

    public PlayerInputs input;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
