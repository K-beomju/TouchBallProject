using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void SceneManagerLoadScene(string _name)
    {
        SceneManager.LoadScene(_name);
    }
}
