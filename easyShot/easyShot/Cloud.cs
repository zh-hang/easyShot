using Aliyun.OSS;
using Aliyun.OSS.Common;
using System.Collections.Generic;
using System.Numerics;

namespace easyShot
{
    enum CloudError{
        ErrorNo,
        ErrorTimeOut,
        ErrorInvalidLink,
        ErrorUserDoesNotExist,
        ErrorIncorrectPassword
    }

    public class Cloud
    {
        private easyShot.ConfigManager config;
        private string endpoint;
        private string accessKeyId;
        private string accessKeySecret;
        private string bucketName;
        private OssClient client;
        private Bucket bucket;
        private List<System.String> cloudFileList;
        private List<System.String> localFileList;
        public Cloud()
        {
            config = new easyShot.ConfigManager();
            cloudFileList = new List<string>();
            loadConfig();
            setUpConnect();
            updateCloudFileList();
            updateLocalFileList();
        }
        public void loadConfig()
        {
            this.endpoint = "oss-cn-beijing.aliyuncs.com";
            this.accessKeyId = "LTAI4GL1UkZQoveLGWA49gmo";
            this.accessKeySecret = "NWByphOyb5oj9U8q49ZDpe3H6PsU6K";
            this.bucketName = "easyshot";
        }
        //从config获取云端设置
        public void setUpConnect()
        {
            // 创建OSSClient实例。
            this.client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                if (!this.client.DoesBucketExist(this.bucketName))
                {
                    // 创建存储空间。
                    this.bucket = client.CreateBucket(bucketName);
                    System.Console.WriteLine("Create bucket succeeded, {0} ", bucket.Name);
                }
            }catch (System.Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw ex;
            }
        }
        public void upLoad(System.String localFileName)
        {
            //这里需要网络异常处理机制
            System.String objectName = localFileName;
            localFileName = config.getshotfilepath()+@"\"+localFileName;
            try
            {
                // 上传文件
                client.PutObject(bucketName, objectName, localFileName);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("fail to uoload the file!");
            }
        }
        public void downLoad(System.String filename)
        {

            //这里需要网络异常处理机制
            try
            {
                // 下载文件到流。OssObject 包含了文件的各种信息，如文件所在的存储空间、文件名、元信息以及一个输入流。
                System.String downloadFilename = this.config.getshotfilepath()+@"\"+filename;
                OssObject obj = client.GetObject(bucketName, filename);
                using (System.IO.Stream requestStream = obj.Content)
                {
                    byte[] buf = new byte[1024];
                    var fs = System.IO.File.Open(downloadFilename, System.IO.FileMode.OpenOrCreate);
                    var len = 0;
                    // 通过输入流将文件的内容读取到文件或者内存中。
                    while ((len = requestStream.Read(buf, 0, 1024)) != 0)
                    {
                        fs.Write(buf, 0, len);
                    }
                    fs.Close();
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("fail to download the file!");
            }
        }
        public void delete(System.String filename)
        {
            try
            {
                // 删除文件
                client.DeleteObject(bucketName, filename);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("fail to delete the file!");
            }
        }

        public void updateCloudFileList()
        {
            try
            {
                ListObjectsRequest listObjectsRequest = new ListObjectsRequest(bucketName) { MaxKeys = 100, };
                ObjectListing result = client.ListObjects(listObjectsRequest);
                foreach (OssObjectSummary summary in result.ObjectSummaries)
                {
                    this.cloudFileList.Add(summary.Key);
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception("fail to get the file list!");
            }
        }

        public void updateLocalFileList()
        {
            System.String directorypath = config.getshotfilepath();
            //List<string> dirs = new List<string>(System.IO.Directory.GetDirectories(directorypath, "*", System.IO.SearchOption.AllDirectories));
            List<string> dirs = new List<string>();
            System.IO.DirectoryInfo folder = new System.IO.DirectoryInfo(directorypath);
            foreach (System.IO.FileInfo file in folder.GetFiles("*"))
            {
                dirs.Add(file.Name);
            }
            this.localFileList = dirs;
        }
        public void upLoadNewLocalFile()
        {
            foreach(var filename in localFileList)
            {
                if (cloudFileList.Find((System.String thisfilename)=> thisfilename.Equals(filename)) == null)
                {
                    upLoad(filename);
                }
            }
        }
        public void downLoadNewCloudFile()
        {
            foreach (var filename in cloudFileList)
            {
                if (localFileList.Find((System.String thisfilename) => thisfilename.Equals(filename)) == null)
                {
                    downLoad(filename);
                }
            }
        }
        public void deleteCloudFile()
        {
            foreach (var filename in cloudFileList)
            {   
                if (localFileList.Find((System.String thisfilename) => thisfilename.Equals(filename)) == null)
                {
                    delete(filename);
                }
            }
        }

    }
}
