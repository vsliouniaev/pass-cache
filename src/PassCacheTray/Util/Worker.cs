using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flurl.Http;
using Noesis.Javascript;

namespace PassCacheTray.Util
{
    public class Worker
    {
        public string ServerUri { get; set; }

        public Worker(string sUri)
        {
            ServerUri = sUri;
        }

        public Result EncryptClipboard(string data)
        {
            var uheprng = GetScript("PassCacheTray.Resources.uheprng.js");
            var sjcl = GetScript("PassCacheTray.Resources.sjcl.js");
            var code = GetScript("PassCacheTray.Resources.code.js");

            var jc = new JavascriptContext();

            jc.SetParameter("plaintext",data);
            jc.SetParameter("url", ServerUri + "/Get");
            jc.Run(uheprng);
            jc.Run(sjcl);
            jc.Run(code);

            var result = new Result
            {
                Id = jc.GetParameter("id") as string,
                Json = jc.GetParameter("result") as string,
                FullUrl = jc.GetParameter("fullUrl") as string
            };
            return result;
        }

        public async void PostToServer(Result result)
        {
            var responseString = await ServerUri
                .PostUrlEncodedAsync(new { id = result.Id, data = result.Json })
                .ReceiveString();
        }

        private string GetScript(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                return result;
            }
        }
    }
}
