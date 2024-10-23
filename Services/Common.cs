using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using VisitingBook.Models;

namespace VisitingBook.Services
{
    public class Common
    {
        HttpRequest? request;
        HttpResponse? response;
        public string? _SessionID = null;

        public string? SessionID
        {
            get
            {
                if (_SessionID == null)
                {
                    if (request != null && request.Cookies["VbCookie"] != null)
                    {
                        _SessionID = request.Cookies["VbCookie"].ToString();
                    }
                    else if (request != null && response != null)
                    {
                        _SessionID = Guid.NewGuid().ToString();
                        response.Cookies.Append("VbCookie", _SessionID);
                    }
                    else
                    {
                        _SessionID = Guid.NewGuid().ToString();
                    }
                }
                return _SessionID;
            }
        }

        protected Dictionary<string, string> GetDBSessionDictionary()
        {
            if (string.IsNullOrEmpty(SessionID))
            {
                throw new ArgumentException("SessionID cannot be null or empty.");
            }

            string sqlquery = "SELECT * FROM SessionTB WHERE SessionID = @SessionID";
            DynamicParameters dynamic = new DynamicParameters();
            dynamic.Add("@SessionID", SessionID);

            Console.WriteLine($"SessionID: {SessionID}"); // Debugging output

            var oVbSession = DapperORM<SessionTB>.ReturnList(sqlquery, dynamic);

            return oVbSession?.ToDictionary(o => o.SessionKey, o => o.SessionValue) ?? new Dictionary<string, string>();
        }

       protected async Task SetDBSessionDictionaryAsync(Dictionary<string, string> pDtn)
{
    // Check if 'EmailID' exists in the dictionary
    foreach (KeyValuePair<string, string> selectedDicitionary in pDtn)
    {
        Console.WriteLine(selectedDicitionary);

        var dynamicParameters = new DynamicParameters();
        dynamicParameters.Add("SessionID", SessionID);
        dynamicParameters.Add("SessionKey", selectedDicitionary.Key);
        dynamicParameters.Add("SessionValue", selectedDicitionary.Value);

        string upsertQuery = "INSERT INTO SessionTB([SessionID], [SessionKey], [SessionValue]) " +
                             "VALUES (@SessionID, @SessionKey, @SessionValue) ";

        // Call the asynchronous method
        await DapperORM<SessionTB>.AddOrUpdateAsync(upsertQuery, null, dynamicParameters);
    }
}


        public static Common NewObj(HttpRequest pRequest, HttpResponse pResponse)
        {
            var obj = new Common();
            obj.request = pRequest;
            obj.response = pResponse;
            return obj;
        }

        public string GetSession(string pKey)
        {
            var oDtn = GetDBSessionDictionary();
            return oDtn.Keys.Contains(pKey) ? oDtn[pKey] : null;
        }

        public void SetSession(string pKey, string pValue)
        {
            var oDtn = GetDBSessionDictionary();
            if (!oDtn.Keys.Contains(pKey))
            {
                oDtn.Add(pKey, pValue);
                SetDBSessionDictionaryAsync(oDtn);
            }
            else
                oDtn[pKey] = pValue;    
        }
    }
}
