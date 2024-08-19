using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;

public class RecievePNGFile : MonoBehaviour
{
    // �T�[�o�[URL
    public string serverUrl = "http://172.21.39.32:8332/api/v0.5/ping";

    // �ۑ����郍�[�J���t�@�C�����i��: "image.png"�j
    public string fileName = "image.png";

    void Start()
    {
        // �t�@�C�����_�E�����[�h���ĕۑ�����R���[�`�����J�n
        StartCoroutine(DownloadPNGCoroutine(serverUrl, fileName));
    }

    public IEnumerator DownloadPNGCoroutine(string serverUrl, string fileName)
    {
        while (true)
        {
    // HTTP GET���N�G�X�g���쐬
            UnityWebRequest www = UnityWebRequest.Get(serverUrl);

            // ���N�G�X�g�𑗐M���A���X�|���X��҂�
            yield return www.SendWebRequest();

            // �G���[�`�F�b�N
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
                yield return new WaitForSeconds(2f);
            }
            else
            {
                // �o�C�g�z��Ƃ��ă��X�|���X�f�[�^���擾
                byte[] fileData = www.downloadHandler.data;

                // �ۑ���̃p�X���쐬
                string directoryPath = Path.Combine(Application.dataPath, "Images");
                string fullPath = Path.Combine(directoryPath, fileName);

                // �f�B���N�g�������݂��Ȃ��ꍇ�͍쐬
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // �f�[�^�����[�J���t�@�C���ɏ�������
                File.WriteAllBytes(fullPath, fileData);

                Debug.Log("File successfully downloaded and saved to: " + fullPath);
                break;
            }
        }
        
    }
}
