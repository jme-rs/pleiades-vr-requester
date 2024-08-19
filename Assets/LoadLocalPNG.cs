using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;

public class LoadLocalPNG : MonoBehaviour
{
    GameObject queue;
    //public Camera queue;
    Queue3 Q3;

    // ���[�J���p�X�i��: "Assets/Resources/Images/"�j
    //public string localPath = "Assets/Images/";

    // �ǂݍ��݂����t�@�C���̖��O�i��: "example.png"�j
    //public string fileName = "frame.png";

    // Image�R���|�[�l���g
    public Image targetImage;

    void Start()
    {

        targetImage = GameObject.Find("Image").GetComponent<Image>();
        queue = transform.parent.parent.parent.gameObject;
        Q3 = queue.GetComponent<Queue3>();
        // �R���[�`�����J�n����1�b������LoadImage�����s
        StartCoroutine(LoadImageCoroutine());
    }

    IEnumerator LoadImageCoroutine()
    {
        while (true)
        {
            var image = Q3.Dequeue();
            

            if(image == null)
            {
                //Debug.Log("�߂�");
                // 1�b�҂�
                yield return new WaitForSeconds(0.05f);
                continue;
            }
            
            Texture2D texture = new Texture2D(2, 2);
            //Texture2D texture = new Texture2D(2, 2, TextureFormat.RGB24, false);

            //if (texture.LoadImage(image))
            //{
            Debug.Log("�ł���");
            // �X�v���C�g���쐬
           texture.LoadImage(image);

            Sprite newSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            // Image�R���|�[�l���g�ɃX�v���C�g���Z�b�g
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
        // PNG�t�@�C����ǂݍ��݁AImage�R���|�[�l���g�ɃZ�b�g
        //LoadImage(localPath, fileName);
        
    }
}
