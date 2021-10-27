using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities.LoadingSystem
{
    public class Loader : MonoBehaviour
    {
       
        public void LoadScene()
        {
            SceneManager.LoadScene(0);
        }
    }
}