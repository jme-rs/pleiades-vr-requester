using System.Collections;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class Requester : MonoBehaviour
{
    private Client client;
    private string lambdaId;
    private Queue3 outputQ;

    [System.Serializable]
    public class CameraData
    {
        public Vector3 position;
        public Vector3 rotation;
    }

    IEnumerator Start()
    {

        // this.client = new("http://pleiades.local:8332/api/v0.5");
        this.client = new("http://192.168.168.127:8332/api/v0.5");
        this.outputQ = GetComponent<Queue3>();

        // create lambda
        var lambda = client.CreateLambda("vr", "1");
        yield return lambda;
        this.lambdaId = lambda.Current.ToString();

        Debug.Log("あ");
        // start
        StartCoroutine(CreateJob());
    }

    void Update()
    {

    }

    IEnumerator CreateJob()
    {
        while (true)
        {
            // sleep
            yield return new WaitForSeconds(0.2f);

            // カメラの位置と向きを取得
            Vector3 cameraPosition = transform.position;
            Quaternion cameraRotation = transform.rotation;

            // データをJSON形式で準備
            CameraData cameraData = new()
            {
                position = cameraPosition,
                rotation = cameraRotation.eulerAngles
            };

            string jsonData = JsonUtility.ToJson(cameraData);

            // input
            var upload = this.client.Upload(Encoding.UTF8.GetBytes(jsonData));
            yield return StartCoroutine(upload);
            var inputId = upload.Current.ToString();

            // create job
            var job = this.client.CreateJob(this.lambdaId, inputId);
            yield return StartCoroutine(job);
            var jobId = job.Current.ToString();

            // wait finis
            var jobInfo = this.client.JobInfo(jobId, "Finished");
            yield return StartCoroutine(jobInfo);

            string outputId;
            try
            {
                outputId = (jobInfo.Current as JObject)["output"]["id"].ToString();
            }
            catch
            {
                Debug.Log("failed to get output");
                continue;
            }

            // download
            var download = this.client.Download(outputId);
            yield return StartCoroutine(download);
            var frame = download.Current as byte[];

            // output
            this.outputQ.Enqueue(frame);
            //Debug.Log(Encoding.UTF8.GetString(frame));
        }
    }
}
