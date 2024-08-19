using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GetPosition : MonoBehaviour
{

    //���N�G�X�g�𑗐M����url
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
            // �J�����̈ʒu�ƌ������擾
            Vector3 cameraPosition = transform.position;
            Quaternion cameraRotation = transform.rotation;

            // �f�[�^��JSON�`���ŏ���
            CameraData cameraData = new CameraData()
            {
                position = cameraPosition,
                rotation = cameraRotation.eulerAngles
            };

            string jsonData = JsonUtility.ToJson(cameraData);

            // HTTP POST���N�G�X�g���쐬
            UnityWebRequest www = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();
            www.SetRequestHeader("Content-Type", "application/json");

            // ���N�G�X�g�𑗐M���A���X�|���X��҂�
            yield return www.SendWebRequest();

            // �G���[�`�F�b�N
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

            // ���̑��M�܂őҋ@
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
