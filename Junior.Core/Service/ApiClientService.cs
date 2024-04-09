using Junior.Core.Extension;
using RestSharp;

namespace Junior.Core.Service
{
    public class ApiClientService
    {
        /// <summary>
        /// 发起API请求
        /// </summary>
        /// <param name="reqMethod"></param>
        /// <param name="strBaseUrl"></param>
        /// <param name="strRouteUrl"></param>
        /// <param name="queryParam"></param>
        /// <param name="strBody"></param>
        /// <param name="headerList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public static ApiResponse Request(Method reqMethod, string strBaseUrl, string strRouteUrl = null, Dictionary<string, string> queryParam = null, string strBody = null, Dictionary<string, string> headerList = null, int timeOut = 3000)
        {
            ApiResponse resp = new ApiResponse()
            {
                StatusCode = -1,
                Content = string.Empty
            };
            RestClient client = new RestClient(strBaseUrl);
            if (queryParam != null)
            {
                if (queryParam.Count > 0)
                {
                    strRouteUrl += "?";
                }
                int index = 0;
                foreach (KeyValuePair<string, string> kvp in queryParam)
                {
                    strRouteUrl += $"{System.Web.HttpUtility.UrlEncode(kvp.Key)}={System.Web.HttpUtility.UrlEncode(kvp.Value)}";
                    if (index < queryParam.Count - 1)
                    {
                        strRouteUrl += "&";
                    }
                    index++;
                }
            }
            RestRequest request = new RestRequest(strRouteUrl, reqMethod);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            if (headerList != null)
            {
                foreach (KeyValuePair<string, string> kvp in headerList)
                {
                    request.AddHeader(kvp.Key, kvp.Value);
                }
            }
            request.Timeout = timeOut;
            if (!strBody.IsNull())
                request.AddParameter("application/json; charset=utf-8", strBody, ParameterType.RequestBody);
            string strResp = string.Empty;
            try
            {
                RestResponse response = client.Execute(request);
                if (response != null)
                {
                    resp.StatusCode = (int)response.StatusCode;
                    resp.Content = response.Content;
                }
            }
            catch (Exception ex)
            {
                resp.Content = ex.Message;
            }
            return resp;
        }
    }
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Content { get; set; }
    }
}
