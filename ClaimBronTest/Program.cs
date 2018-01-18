using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ClaimBronTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string claim = "147453";
            string cancelClaimUri = "http://localhost:50419/api/claimcancel/" + claim;
            HttpWebRequest cancelClaim = (HttpWebRequest)WebRequest.Create(cancelClaimUri);
            cancelClaim.Method = "PUT";
            cancelClaim.ContentType = "application/json";
            MemoryStream rstrream = new MemoryStream();
            string rrq = "{Reason: 1}";
            UTF8Encoding enc = new UTF8Encoding();
            byte[] b = enc.GetBytes(rrq);
            cancelClaim.ContentLength = b.Length;
            Stream rqs = cancelClaim.GetRequestStream();
            rqs.Write(b, 0, b.Length);
            rqs.Close();
            HttpWebResponse resp = (HttpWebResponse)cancelClaim.GetResponse();
            Console.WriteLine(resp.StatusCode);
            return;
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(dynamic));
            //Debug.Assert(2 == 1, "JJJ");
            string catClaim = "0x0608E5932B0000000C00000017A8590000000004380000CD8A0000000800004AD90000279100000CDD000000040000000000000001A2D2AC420000000100000000A86100000000000800000000019915C800000002000000030000000200000001000000000000000A0000000000000000A8590000A85F0000";
            string uriGet = "http://localhost:50419/api/bronClaim?catclaim=" + catClaim + "&currency=2";
            //string uri = "http://localhost:50419/api/bronClaim?catclaim=0x062047c9bc0000000900000028a81d00000000031000007b2b000000070000471f00001950000000040000000400000000000000024a102f580000023100000000a8240000000000050000000001a65c380000000200000002000000020000000000000000000000000000000000000000a8200000a8230000&currency=2";
            string uriCalc = "http://localhost:50419/api/bronClaim?action=CALC";
            HttpWebRequest get = (HttpWebRequest)WebRequest.Create(uriGet);
            get.Method = "GET";
            HttpWebResponse getResponce = (HttpWebResponse)get.GetResponse();
            Stream bronClaimStream = getResponce.GetResponseStream();
            StreamReader read = new StreamReader(bronClaimStream);
            string brclaim = read.ReadToEnd();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriCalc);
            request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            MemoryStream requestStream = new MemoryStream();
            string rq = brclaim; //"HHH";// 
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] buffer = encoding.GetBytes(rq);
            request.ContentLength = buffer.Length;
            Stream rqstream = request.GetRequestStream();
            rqstream.Write(buffer, 0, buffer.Length);
            //StreamWriter writer = new StreamWriter(rqstream);
            //writer.AutoFlush = true;
            //writer.Write(rq);
            //writer.Close();
            rqstream.Close(); 
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Console.WriteLine(response.StatusDescription);
            Console.ReadLine();
            //Stream dataStream = response.GetResponseStream();
            // Open the stream using a StreamReader for easy access.
            //StreamReader reader = new StreamReader(dataStream);
            // Read the content.
            //dynamic o = ser.ReadObject(dataStream);
            //dataStream.Position = 0;
            //string responseFromServer = reader.ReadToEnd();
            // Display the content.
            //Console.WriteLine(responseFromServer);
            // Cleanup the streams and the response.
            //reader.Close();
            //dataStream.Close();
            response.Close();
            

        }
    }

    class LockResult
    {
        public int result { get; set; }
        public int user { get; set; }
        public DateTime? locktime { get; set; }
    }
}
