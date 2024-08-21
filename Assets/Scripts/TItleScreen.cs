using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TItleScreen : MonoBehaviour
{
    

    // Update is called once per frame
    public void Update()
    {
        

        if(Input.GetKeyDown(KeyCode.Space))
        {
             Debug.Log("START");
            gameObject.SetActive(false);
           
        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameObject.activeSelf == true)
        {Quit();}
    }

    public void Quit() {
    #if UNITY_STANDALONE
        Application.Quit();
    #endif
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #endif
}
}
