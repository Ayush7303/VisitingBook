using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using VisitingBook.Models;


namespace VisitingBook.Services
{
    public class Common
    {
        HttpRequest? request;
        HttpResponse? response;
        // EduSprintCMSDataContext _DataContext;
        // public EduSprintCMSDataContext oDataContext
        // {
        //     get
        //     {
        //         if (_DataContext == null)
        //         {
        //             string ConnectionString = WebConfigurationManager.AppSettings["ToolConnectionString"];
        //             _DataContext = new EduSprintCMSDataContext(ConnectionString);
        //         }
        //         return _DataContext;
        //     }
        // }
        protected Dictionary<string, string> GetDBSessionDictionary()
        {
            if (request != null && request.Cookies["VbCookie"] != null)
            {
                var oVbCookie = request.Cookies["VbCookie"];
                if (oVbCookie == null) return new Dictionary<string, string>();
                string sqlquery = "SELECT * FROM SessionTB";
                Console.WriteLine(oVbCookie);
                var oVbSession = DapperORM<SessionTB>.ReturnList<SessionTB>(sqlquery).FirstOrDefault(s => s.SessionID == Guid.Parse(oVbCookie));
                
                if (oVbSession == null) return new Dictionary<string, string>();
                else return new Dictionary<string, string>{
                    {"SessionID",oVbSession.SessionID.ToString()},
                    {"SessionData",oVbSession.SessionValue}
                };
            }
            else return new Dictionary<string, string>();
        }
        protected void SetDBSessionDictionary(Dictionary<string, string> pDtn)
        {
            if (request != null)
            {
                var oGuid = Guid.NewGuid();
                var oVbCookie = request.Cookies["VbCookie"];
                if (oVbCookie != null) oGuid = Guid.Parse(oVbCookie);
               
                string getquery = "SELECT * FROM SessionTB";
                SessionTB oVbSession = DapperORM<SessionTB>.ReturnList<SessionTB>(getquery).FirstOrDefault(s => s.SessionID == Guid.Parse(oVbCookie));

                // Check if 'EmailID' exists in the dictionary
                if (pDtn.ContainsKey("EmailID"))
                {
                    if (oVbSession == null)
                    {
                        // Insert new session record
                        oVbSession = new SessionTB();
                        oVbSession.SessionKey = "EmailID";
                        oVbSession.SessionID = oGuid;
                        oVbSession.SessionValue = pDtn["EmailID"].ToString();

                        string insertquery = "INSERT INTO SessionTB([SessionID],[SessionKey],[SessionValue]) VALUES (@SessionID,@SessionKey,@SessionValue)";
                        var result = DapperORM<SessionTB>.AddOrUpdate(insertquery, oVbSession, null);
                        response.Cookies.Append("VbCookie", oGuid.ToString());
                    }
                    else
                    {
                        // Update existing session record
                        oVbSession.SessionValue = pDtn["EmailID"].ToString();
                        // string updatequery = "UPDATE SessionTB SET [SessionValue]= @SessionValue";
                        // var result = DapperORM<SessionTB>.AddOrUpdate(updatequery, oVbSession, null);
                    }
                }
                else
                {
                    throw new KeyNotFoundException("The 'EmailID' key is missing from the session data.");
                }
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
            if (!oDtn.Keys.Contains(pKey)) oDtn.Add(pKey, pValue);
            else oDtn[pKey] = pValue;
            SetDBSessionDictionary(oDtn);
        }
        // public void RemoveSession()
        // {
        //     if (HttpContext.Current != null && HttpContext.Current.Request.Cookies["VbCookie"] != null)
        //     {
        //         var oGuid = Guid.NewGuid();
        //         var oVbCookie = HttpContext.Current.Request.Cookies["VbCookie"];
        //         if (oVbCookie != null)
        //         {
        //             oGuid = new Guid(oVbCookie.Value);
        //             HttpContext.Current.Response.Cookies.Remove("VbCookie");
        //             oVbCookie.Expires = DateTime.Now.AddDays(-1);
        //             HttpContext.Current.Response.Cookies.Add(oVbCookie);
        //         }
        //         var oVbSession = oDataContext.CmsSessions.FirstOrDefault(s => s.CmsSessionID == oGuid);
        //         if (oVbSession != null)
        //         {
        //             oDataContext.CmsSessions.DeleteOnSubmit(oVbSession);
        //             oDataContext.SubmitChanges();
        //         }
        //     }
        // }
        // public string GetRoles(int pUserID)
        // {
        //     List<int> UserRoleList = new List<int>();
        //     var lstRole = oDataContext.UserVsRoles.Where(s => s.UserID == pUserID).ToList();
        //     foreach (var item in lstRole)
        //     {
        //         UserRoleList.Add(item.RoleID);
        //         if (item.Role.RoleName.ToLower() == "admin")
        //         {
        //             SetSession("RoleName", "admin");
        //         }
        //         else { SetSession("RoleName", ""); }
        //     }
        //     string Roles = String.Join(",", UserRoleList);
        //     return Roles;
        // }
        // public string GetPermissions(string pRoleIDs)
        // {
        //     List<string> PermissionList = new List<string>();
        //     string RoleStr = pRoleIDs;
        //     string[] role = RoleStr.Split(',');
        //     foreach (var item in role)
        //     {
        //         var oList = oDataContext.RoleVsPermissions.Where(s => s.RoleID.ToString() == item).ToList();
        //         foreach (var i in oList)
        //         {
        //             PermissionList.Add(i.PermissionMaster.PermissionName);
        //         }
        //     }
        //     string oData = String.Join(",", PermissionList);
        //     return oData;
        // }
        // public bool GetAuthorised(string pPermissionName)
        // {
        //     string ListPermission = GetPermissions(EduSprintCMS.Models.Common.NewObj().GetSession("RoleID").ToString());
        //     if (ListPermission.Contains(pPermissionName)) { return true; } else { return false; }
        // }
        // public string Encrypt(string clearText)
        // {
        //     string EncryptionKey = "Met24Vb";
        //     byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //     using (Aes encryptor = Aes.Create())
        //     {
        //         Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //         encryptor.Key = pdb.GetBytes(32);
        //         encryptor.IV = pdb.GetBytes(16);
        //         using (MemoryStream ms = new MemoryStream())
        //         {
        //             using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //             {
        //                 cs.Write(clearBytes, 0, clearBytes.Length);
        //                 cs.Close();
        //             }
        //             clearText = Convert.ToBase64String(ms.ToArray());
        //         }
        //     }
        //     return clearText;
        // }
        // public string Decrypt(string cipherText)
        // {
        //     string EncryptionKey = "Met24Vb";
        //     byte[] cipherBytes = Convert.FromBase64String(cipherText);
        //     using (Aes encryptor = Aes.Create())
        //     {
        //         Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        //         encryptor.Key = pdb.GetBytes(32);
        //         encryptor.IV = pdb.GetBytes(16);
        //         using (MemoryStream ms = new MemoryStream())
        //         {
        //             using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
        //             {
        //                 cs.Write(cipherBytes, 0, cipherBytes.Length);
        //                 cs.Close();
        //             }
        //             cipherText = Encoding.Unicode.GetString(ms.ToArray());
        //         }
        //     }
        //     return cipherText;
        // }

    }
}