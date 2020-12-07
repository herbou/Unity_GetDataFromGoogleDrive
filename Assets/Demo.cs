using UnityEngine ;
using UnityEngine.Networking ;
using UnityEngine.UI ;
using System.Collections ;

// Json data format
/*
      {
        "Name"     : "..." ,
        "ImageURL" : "..."
      }
*/
public struct Data {
   public string Name ;
   public string ImageURL ;
}

public class Demo : MonoBehaviour {
   [SerializeField] Text uiNameText ;
   [SerializeField] RawImage uiRawImage ;

   string jsonURL = "https://drive.google.com/uc?export=download&id=1Rg_QKGNh5S8t6ziMnkwT4GKhEqtFpK2Q" ;

   void Start () {
      StartCoroutine (GetData (jsonURL)) ;
   }

   IEnumerator GetData (string url) {
      UnityWebRequest request = UnityWebRequest.Get (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         // error ...

      } else {
         // success...
         Data data = JsonUtility.FromJson<Data> (request.downloadHandler.text) ;

         // print data in UI
         uiNameText.text = data.Name ;

         // Load image:
         StartCoroutine (GetImage (data.ImageURL)) ;
      }
      
      // Clean up any resources it is using.
      request.Dispose () ;
   }

   IEnumerator GetImage (string url) {
      UnityWebRequest request = UnityWebRequest.GetTexture (url) ;

      yield return request.SendWebRequest() ;

      if (request.isNetworkError || request.isHttpError) {
         // error ...

      } else {
         //success...
         uiRawImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture ;
      }

      // Clean up any resources it is using.
      request.Dispose () ;
   }

}
