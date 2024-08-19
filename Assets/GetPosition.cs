using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetPosition : MonoBehaviour
{

    //リクエストを送信するurl
    //private string url = "https://example.com/";
    private string url = "http://172.21.39.32:8332/api/v0.5/ping";
    private string position;
    RecievePNGFile Recieve;
    public GameObject Image;
    LoadLocalPNG llp;

    private Camera HMDcamera;
    // Start is called before the first frame update
    void Start()
    {
        HMDcamera = GetComponent<Camera>();
        StartCoroutine(SendPosition(url));
        Recieve = GetComponent<RecievePNGFile>();
        Image = GetComponent<GameObject>();
        llp = Image.GetComponent<LoadLocalPNG>();
    }

    IEnumerator SendPosition(string url)
    {
        while (true)
        {
            // カメラの位置と向きを取得
            Vector3 cameraPosition = transform.position;
            Quaternion cameraRotation = transform.rotation;

            // データをJSON形式で準備
            CameraData cameraData = new CameraData()
            {
                position = cameraPosition,
                rotation = cameraRotation.eulerAngles
            };

            string jsonData = JsonUtility.ToJson(cameraData);

            // HTTP POSTリクエストを作成
            UnityWebRequest www = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // リクエストを送信し、レスポンスを待つ
            yield return www.SendWebRequest();

            // エラーチェック
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                Debug.Log("Camera data successfully uploaded!");
            }
            Recieve.DownloadPNGCoroutine(url, "image.png");
            //llp.LoadImage("Assets/Images/", "image.png");

            // 次の送信まで待機
            yield return new WaitForSeconds(2f);
        }
    }
    [System.Serializable]
    public class CameraData
    {
        public Vector3 position;
        public Vector3 rotation;
    }
}
