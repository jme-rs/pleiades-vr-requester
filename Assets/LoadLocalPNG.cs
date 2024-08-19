using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadLocalPNG : MonoBehaviour
{
    GameObject queue;
    //public Camera queue;
    Queue3 Q3;

    // ローカルパス（例: "Assets/Resources/Images/"）
    //public string localPath = "Assets/Images/";

    // 読み込みたいファイルの名前（例: "example.png"）
    //public string fileName = "frame.png";

    // Imageコンポーネント
    public Image targetImage;

    void Start()
    {

        targetImage = GameObject.Find("Image").GetComponent<Image>();
        queue = transform.parent.parent.parent.gameObject;
        Q3 = queue.GetComponent<Queue3>();
        // コルーチンを開始して1秒おきにLoadImageを実行
        StartCoroutine(LoadImageCoroutine());
    }

    IEnumerator LoadImageCoroutine()
    {
        while (true)
        {
            var image = Q3.Dequeue();
            

            if(image == null)
            {
                //Debug.Log("戻る");
                // 1秒待つ
                yield return new WaitForSeconds(0.05f);
                continue;
            }
            
            Texture2D texture = new Texture2D(2, 2);
            //Texture2D texture = new Texture2D(2, 2, TextureFormat.RGB24, false);

            //if (texture.LoadImage(image))
            //{
            Debug.Log("できゅ");
            // スプライトを作成
           texture.LoadImage(image);

            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Imageコンポーネントにスプライトをセット
            targetImage.sprite = newSprite;

            Debug.Log("Image successfully loaded and set to the target Image component.");
            /*
            }
            else
            {
                Debug.LogError("Failed to load texture from file data.");
            }
            */
        }
        // PNGファイルを読み込み、Imageコンポーネントにセット
        //LoadImage(localPath, fileName);
        
    }
}
