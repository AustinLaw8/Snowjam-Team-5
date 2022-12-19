using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonVarMAnager : MonoBehaviour
{
    public static SingletonVarMAnager instance;
    public static float volume = 1f;
    public static float music = 1f;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } 
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
