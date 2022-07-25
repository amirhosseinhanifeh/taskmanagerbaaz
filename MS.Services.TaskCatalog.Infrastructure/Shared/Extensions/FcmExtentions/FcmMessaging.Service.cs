using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.FcmExtentions;

public interface IFcmMessaging
{
    Task<bool> SendAsync(string DeviceToken, string title, string msg);
}
public class FcmMessaging : IFcmMessaging
{
    private string webAddr = "https://fcm.googleapis.com/fcm/send";
    private string SenderId = "872927249619";
    private string ServerKey = "AAAAyz6ExNM:APA91bFDqQztbspYriIRMqWXdiGMgAPTd4L0Hgu2DyJwIwJ6ucSlLHj-ClhcHiqx1dbPIfAAPSVWNWf7_xnks7PVg417xwj6z2ekcKIaLiOVCKoRuAj62i54-nODhTQ6YSHwKcEnKdsd";
    //private readonly IOptions<FcmConfigOptions> _fcmNotificationSetting;

    //public FcmMessaging(IOptions<FcmConfigOptions> settings)
    //{
    //    _fcmNotificationSetting = settings;
    //}
    public async Task<bool> SendAsync(string DeviceToken, string title, string msg)
    {
        try
        {

            WebRequest tRequest = WebRequest.Create(webAddr);
            tRequest.Method = "POST";
            tRequest.ContentType = "application/json";

            var data = new
            {
                to = DeviceToken,
                notification = new
                {
                    body = msg,
                    title = title,
                    icon = "myicon"
                }
            };

            var json = JsonConvert.SerializeObject(data);
            byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.Headers.Add(string.Format("Authorization: key={0}",ServerKey));
            tRequest.Headers.Add(string.Format("Sender: id={0}",SenderId));
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            string sResponseFromServer = tReader.ReadToEnd();
                            var response = JsonConvert.DeserializeObject<ResponseModel>(sResponseFromServer);
                            if (response != null)
                            {
                                if (response.IsSuccess)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {

            string str = ex.Message;

        }
        return false;

    }
    public class ResponseModel
    {
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
    }

}
public class FcmConfigOptions
{
    public string SenderId { get; set; }
    public string ServerKey { get; set; }
}
