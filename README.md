# Junior.Core
A lightweight low-coupling .NET Core back-end framework.

## Supported Service
- Restful api client service based on <b>RestSharp</b>.
- Json serialization and deserialization based on <b>Newtonsoft.Json</b>.
- SQL(MySQL、SQL Server and SQLite) connection and query based on <b>System.Data.SqlClient</b>.
- Redis connection and operation based on <b>StackExchange.Redis</b>.
- JwtToken generation and verification.
- Convert extension for String、Int、Decimal、Long and DateTime.
- DateTime comparation、calculation and converting.
- File(contains WebSource)、directory IO and file zipping based on <b>DotNetZip</b>.
- Regex string matching.
- Config operation based on <b>System.Configuration.ConfigurationManager</b>.

## Usage Guide
### ApiClientService
Making a Restful request
```csharp
string strResp = string.Empty;
string strBaseUrl = "http://127.0.0.1";
string strRouteUrl = "/user/login";
//Optional QueryParam
Dictionary<string, string> queryParam = new Dictionary<string, string>();
queryParam.Add("TokenType","JwtToken");
//Optional Body Data
string strBody = postBody.ToJson();
//Optional Header
Dictionary<string, string> headerList = new Dictionary<string, string>();
headerList.Add("Authorization","token");
//Optional TimeOut(ms)
int timeOut = 3000;
ApiResponse resp = ApiResponse Request(Method.Post, strBaseUrl, strRouteUrl, queryParam, strBody, headerList, timeOut);
if(resp != null)
{
  if(resp.StatusCode == 200)
  {
    if(!resp.Content.IsNull())
    {
      strResp = resp.Content;
    }
  }
}
return strResp;
```
