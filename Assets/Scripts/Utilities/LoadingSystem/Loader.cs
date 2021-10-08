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
        
        //IEnumerator LoadYourAsyncScene(int buildIndex)
        //{
        //    yield return null;
        //    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
        //    _isLoading = true;
          
        //    asyncOperation.allowSceneActivation = false;
          
        //    while (!asyncOperation.isDone)
        //    {
        //        if (asyncOperation.progress >= 0.9f)
        //        {
        //            asyncOperation.allowSceneActivation = true;
           
        //        }
                
        //        yield return null;
        //    }
            
        //    _isLoading = false;
           
        //}
    }
}