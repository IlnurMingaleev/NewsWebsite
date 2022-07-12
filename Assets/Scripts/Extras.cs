using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extras : MonoBehaviour
{
    //Tried to download image using asynchronous functions. Status: Failed



    /* public static async Task<Texture2D> GetRemoteTexture(string url)
      {
          using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
          {
              // begin request:
              var asyncOp = www.SendWebRequest();

              // await until it's done: 
              while (asyncOp.isDone == false)
                  await Task.Delay(60000);//30 hertz

              // read results:
              if (www.isNetworkError || www.isHttpError)
              // if( www.result!=UnityWebRequest.Result.Success )// for Unity >= 2020.1
              {
                  // log error:

                  Debug.Log($"{www.error}, URL:{www.url}");

                  // nothing to return on error:
                  return null;
              }
              else
              {
                  // return valid results:
                  return DownloadHandlerTexture.GetContent(www);
              }
          }
      }*/
    /*    async void DownloadImages() 
        {
            for (int i = 1; i < 3; i++)
            {
                if (articles[i].image_url != null)
                {
                    textureList.Add(await GetRemoteTexture(articles[i].image_url));
                    Texture2D tex = textureList[i - 1];
                    spriteList.Add(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2)));
                    ui.ImageList[i - 1].GetComponent<Image>().overrideSprite = spriteList[i - 1];
                }
            }

            textureList.Add(await GetRemoteTexture(articles[2].image_url));
            tex = textureList[1];
            spriteList.Add(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2)));
            ui.MidImage.GetComponent<Image>().overrideSprite = spriteList[1];

            textureList.Add(await GetRemoteTexture(articles[3].image_url));
            tex = textureList[2];
            spriteList.Add(Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width / 2, tex.height / 2)));
            ui.MidImage.GetComponent<Image>().overrideSprite = spriteList[2];

        }
    */
}
