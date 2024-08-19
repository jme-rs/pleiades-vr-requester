using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class save_img : MonoBehaviour
{
    private Queue3 Q;
    string filename = "frame.png";
    // Start is called before the first frame update
    void Start()
    {
        Q = GetComponent<Queue3>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Q == null) Q = GetComponent<Queue3>();

        var img = Q.Dequeue();
        if(img != null)
        {
            //img = Q.Dequeue();

            // 保存先のパスを作成
            string directoryPath = Path.Combine(Application.dataPath, "Images");
            string fullPath = Path.Combine(directoryPath, filename);

            // ディレクトリが存在しない場合は作成
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // データをローカルファイルに書き込む
            File.WriteAllBytes(fullPath, img);
        }
    }
}
