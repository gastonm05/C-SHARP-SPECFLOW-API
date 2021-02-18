using CCC_API.Data.Responses.Streams;
using CCC_API.Data.TestDataObjects;
using RestSharp;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CCC_API.Services.FileUpload
{
    public class FileUploadService : AuthApiService
    {
        public static string FileEndPoint = "distribution/file";

        public FileUploadService(string sessionKey) : base(sessionKey) { }


        public IRestResponse Upload(int chunks, string filePath)
        {
            IRestResponse result = null;

            string token = string.Empty;

            Parameter param = new Parameter();
            param.Name = "name";
            param.Type = ParameterType.GetOrPost;
            param.ContentType = "application/json";
            param.Value = (new FileInfo(filePath)).Name;
            IRestResponse initialLoadResponse = Post<StreamResponse>(FileEndPoint + "/" + chunks, param);
            UploadingFile uploadingFile = new UploadingFile();
            using (MemoryStream memStream = new MemoryStream(Encoding.UTF8.GetBytes(initialLoadResponse.Content)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(UploadingFile));
                XmlReader respContentReader = XmlReader.Create(memStream);
                uploadingFile = (UploadingFile)serializer.Deserialize(respContentReader);
            }
            result = PutFile<StreamResponse>(FileEndPoint + "/" + uploadingFile.UploadToken,
                 filePath, uploadingFile.Size);


            return result;
        }
    }
}
