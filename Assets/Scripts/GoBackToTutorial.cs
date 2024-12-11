using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToTutorial : MonoBehaviour
{
   public void NextScene(string SceneName)
   {
      SceneManager.LoadScene(SceneName);
   }
}
