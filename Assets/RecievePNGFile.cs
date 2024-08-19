using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class RecievePNGFile : MonoBehaviour
{
    // サーバーURL
    public string serverUrl = "http://172.21.39.32:8332/api/v0.5/ping";

    // 保存するローカルファイル名（例: "image.png"）
    public string fileName = "image.png";

    void Start()
    {
        // ファイルをダウンロードして保存するコルーチンを開始
        StartCoroutine(DownloadPNGCoroutine(serverUrl, fileName));
    }

    public IEnumerator DownloadPNGCoroutine(string serverUrl, string fileName)
    {
        while (true)
        {
    // HTTP GETリクエストを作成
            UnityWebRequest www = UnityWebRequest.Get(serverUrl);

            // リクエストを送信し、レスポンスを待つ
            yield return www.SendWebRequest();

            // エラーチェック
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                // バイト配列としてレスポンスデータを取得
                byte[] fileData = www.downloadHandler.data;

                // 保存先のパスを作成
                string directoryPath = Path.Combine(Application.dataPath, "Images");
                string fullPath = Path.Combine(directoryPath, fileName);

                // ディレクトリが存在しない場合は作成
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // データをローカルファイルに書き込む
                File.WriteAllBytes(fullPath, fileData);

                Debug.Log("File successfully downloaded and saved to: " + fullPath);
                break;
            }
        }
        
    }
}
