
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using RoboschienWeb.Encryption;
using RoboschienWeb.Helpers;
using RoboschienWeb.Models.Entities;
using RoboschienWeb.Models.Entities.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;



namespace RoboschienWeb.Controllers
{
    [ApiController]
    [Route("Roboschien")]
    public class EmailNotificationController : Controller
    {

        private readonly SickLeaveContext _context;
        private readonly string OcrMatchText;
        private readonly List<String> OcrMatchTextList = new List<string>();

        private const string APP_NAME = "Bosch SAM";



        public EmailNotificationController(SickLeaveContext context, IOptions<ApiSettings> apiSettings)
        {
            _context = context;

            OcrMatchText = apiSettings.Value.OcrMatchText;

            OcrMatchTextList.Add("Austertigung");
            OcrMatchTextList.Add("zur");
            OcrMatchTextList.Add("Vorlage");
            OcrMatchTextList.Add("beim");
            OcrMatchTextList.Add("Arbeitgetrer");

        }


        //[HttpGet]
        //[Route("GetSessionDetails")]
        //public async Task<IActionResult> GetSessionDetails()
        //{
        //    SessionDetails sessionDetails = new SessionDetails();
        //    List<SessionDetails> SessionsList = new List<SessionDetails>();
        //    SessionResponseDetails responseDetails = new SessionResponseDetails()
        //    {
        //        IsSuccess = false,
        //        Message = "",
        //        ErrorMessage = "",
        //        ResponseDataDetails = new List<SessionDetails>()
        //    };

        //    Console.WriteLine("GetSession-Details API  : Called... ");

        //    string milliseconds = Utils.GetInstance().getReferenceNumber(); //Session Key

        //    AESEncryption aes = new AESEncryption();

        //    try
        //    {
        //        byte[] key = aes.GenerateKey();
        //        string KeyStr = aes.ConvertByteArrayToString(key);


        //        sessionDetails.SecretKey = KeyStr;
        //        sessionDetails.SessionKey = milliseconds;
        //        SessionsList.Add(sessionDetails);


        //        responseDetails.IsSuccess = true;
        //        responseDetails.Message = Constants.SUCCESS;
        //        responseDetails.ResponseDataDetails = SessionsList;

        //        EmployeeLeaveInformation employeeLeaveInformation = new EmployeeLeaveInformation();
        //        employeeLeaveInformation.SecretKey = sessionDetails.SecretKey;
        //        employeeLeaveInformation.SessionKey = sessionDetails.SessionKey;
        //        employeeLeaveInformation.CreatedDate = DateTime.UtcNow;
        //        employeeLeaveInformation.ModifiedDate = DateTime.UtcNow;
        //        employeeLeaveInformation.Consent = false;

        //        _context.EmployeeLeaveInformations.Add(employeeLeaveInformation);
        //        await _context.SaveChangesAsync();

        //        Console.WriteLine("Session-key and Secret-Key are Saved into databse Successfully");

        //        return Ok(responseDetails);

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("GetSession-Details API Exception : " + ex.Message);

        //        if (ex.InnerException != null)
        //        {
        //            Console.WriteLine("GetSession-Details API InnerException : " + ex.InnerException);

        //        }

        //        responseDetails.Message = "Failure";
        //        responseDetails.ErrorMessage = Constants.COMMON_ERROR_MESSAGE;
        //        responseDetails.IsSuccess = Constants.FALSE;
        //        responseDetails.ResponseDataDetails = null;
        //        return Ok(responseDetails);

        //    }

        //}

        [Route("GetSessionDetails"), HttpPost]
        public async Task<IActionResult> GetSessionDetails(Common common)
        {
            SessionDetails sessionDetails = new SessionDetails();
            List<SessionDetails> SessionsList = new List<SessionDetails>();
            SessionResponseDetails responseDetails = new SessionResponseDetails()
            {
                IsSuccess = false,
                Message = "",
                ErrorMessage = "",
                ResponseDataDetails = new List<SessionDetails>()
            };

            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : GetSession-Details API  : Called... ");

            string milliseconds = Utils.GetInstance().getReferenceNumber(); //Session Key

            AESEncryption aes = new AESEncryption();

            try
            {
                byte[] key = aes.GenerateKey();
                string KeyStr = aes.ConvertByteArrayToString(key);


                sessionDetails.SecretKey = KeyStr;
                sessionDetails.SessionKey = milliseconds;
                SessionsList.Add(sessionDetails);


                responseDetails.IsSuccess = true;
                responseDetails.Message = Constants.SUCCESS;
                responseDetails.ResponseDataDetails = SessionsList;

                //if (common.CountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_DE))
                //{

                //  EmployeeLeaveInformation employeeLeaveInformation = new EmployeeLeaveInformation();
                //employeeLeaveInformation.SecretKey = sessionDetails.SecretKey;
                //employeeLeaveInformation.SessionKey = sessionDetails.SessionKey;
                //employeeLeaveInformation.CreatedDate = DateTime.UtcNow;
                //employeeLeaveInformation.ModifiedDate = DateTime.UtcNow;
                //employeeLeaveInformation.Consent = false;


                //    _context.EmployeeLeaveInformations.Add(employeeLeaveInformation);
                //}

                if (common.CountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_FR))
                {
                    EmployeeLeaveInformation_FR employeeLeaveInformationFR = new EmployeeLeaveInformation_FR(); //EmployeeLeaveInformations_FR
                    employeeLeaveInformationFR.SecretKey = sessionDetails.SecretKey;
                    employeeLeaveInformationFR.SessionKey = sessionDetails.SessionKey;
                    employeeLeaveInformationFR.CreatedDate = DateTime.UtcNow;
                    employeeLeaveInformationFR.ModifiedDate = DateTime.UtcNow;
                    employeeLeaveInformationFR.Consent = false;

                    _context.EmployeeLeaveInformation_FR.Add(employeeLeaveInformationFR);
                    await _context.SaveChangesAsync();
                }
                else if (common.CountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_PT) || common.CountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_PO))
                {
                    EmployeeLeaveInformation_PT employeeLeaveInformationPT = new EmployeeLeaveInformation_PT();
                    employeeLeaveInformationPT.SecretKey = sessionDetails.SecretKey;
                    employeeLeaveInformationPT.SessionKey = sessionDetails.SessionKey;
                    employeeLeaveInformationPT.CreatedDate = DateTime.UtcNow;
                    employeeLeaveInformationPT.ModifiedDate = DateTime.UtcNow;
                    employeeLeaveInformationPT.Consent = false;

                    _context.EmployeeLeaveInformation_PT.Add(employeeLeaveInformationPT);
                    await _context.SaveChangesAsync();
                }
                else if (common.CountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_ES))
                {
                    EmployeeLeaveInformation_ES employeeLeaveInformationSP = new EmployeeLeaveInformation_ES();
                    employeeLeaveInformationSP.SecretKey = sessionDetails.SecretKey;
                    employeeLeaveInformationSP.SessionKey = sessionDetails.SessionKey;
                    employeeLeaveInformationSP.CreatedDate = DateTime.UtcNow;
                    employeeLeaveInformationSP.ModifiedDate = DateTime.UtcNow;
                    employeeLeaveInformationSP.Consent = false;

                    _context.EmployeeLeaveInformation_ES.Add(employeeLeaveInformationSP);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    responseDetails.Message = Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED;
                    responseDetails.ErrorMessage = Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED;
                    responseDetails.IsSuccess = false;
                }



                Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : Session-key and Secret-Key are Saved into databse Successfully");

                return Ok(responseDetails);

            }
            catch (Exception ex)
            {
                Console.WriteLine("GetSession-Details API Exception : " + ex.Message);
                responseDetails.ErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    responseDetails.ErrorMessage = responseDetails.ErrorMessage + " " + ex.InnerException;
                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : GetSession-Details API InnerException : " + ex.InnerException);

                }

                responseDetails.Message = "Failure";
                //responseDetails.ErrorMessage = Constants.COMMON_ERROR_MESSAGE;
                responseDetails.ErrorMessage = ex.Message;
                responseDetails.IsSuccess = Constants.FALSE;
                responseDetails.ResponseDataDetails = null;
                return Ok(responseDetails);

            }

        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("SendFileEmailNotification")]
        public async Task<IActionResult> SendLeaveInformation()
        {

            ResponseDetails responseDetails = new ResponseDetails()
            {
                IsSuccess = false,
                Message = "",
                ErrorMessage = "",
                ResponseDataDetails = new List<ResponseData>()

            };

            Associate _associateDetails = null;

            string requestSource = "";
            string Locale = null;
            string SessionKey = "";
            string REQUEST_SOURCE = "RequestSource";
            bool Consent = false;

            var httpRequest = HttpContext.Request;
            EmployeeLeaveInformation employeeLeaveInfo = new EmployeeLeaveInformation();
            EmployeeLeaveInformation_FR employeeLeaveInfoFR = new EmployeeLeaveInformation_FR();
            EmployeeLeaveInformation_PT employeeLeaveInfoPT = new EmployeeLeaveInformation_PT();

            EmployeeLeaveInformation_ES employeeLeaveInfoSP = new EmployeeLeaveInformation_ES();
            try
            {
                if (httpRequest != null)
                {

                    requestSource = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(httpRequest.Form[REQUEST_SOURCE]);
                    _associateDetails = Newtonsoft.Json.JsonConvert.DeserializeObject<Associate>(httpRequest.Form[Constants.ASSOCIATE]);

                    SessionKey = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(httpRequest.Form[Constants.SESSION_KEY]);


                    if (!string.IsNullOrEmpty(httpRequest.Form[Constants.CONSENT]))
                    {
                        Consent = Newtonsoft.Json.JsonConvert.DeserializeObject<bool>(httpRequest.Form[Constants.CONSENT]);
                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : Request process Start : Consent : " +
                            employeeLeaveInfo.Consent);
                    }
                    else
                    {
                        Consent = false;

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : Request process Start : Consent is null for : ");
                    }

                }
                else
                {
                    responseDetails.IsSuccess = Constants.FALSE;
                    Console.WriteLine(Constants.HTTP_REQUEST_ERROR_MESSAGE);
                    responseDetails.ErrorMessage = Constants.HTTP_REQUEST_ERROR_MESSAGE;
                    return Ok(responseDetails);

                }

                if (!string.IsNullOrEmpty(SessionKey) && _associateDetails != null)
                {

                    if (requestSource.Equals("Web"))
                    {
                        Locale = httpRequest.Form[Constants.LOCALE].ToString();

                        //Locale
                        // employeeLeaveInfo.Locale = Locale != null ? Locale : "EN";
                    }
                    else
                    {
                        // APP Locale
                        if (!string.IsNullOrEmpty(httpRequest.Form["Locale"].ToString()))
                        {

                            Locale = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(httpRequest.Form[Constants.LOCALE]);

                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : IF:  Actual Locale  : " + httpRequest.Form[Constants.LOCALE].ToString());
                        }
                        else
                        {
                            Locale = "EN";

                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : ELSE: Actual Locale  : " + httpRequest.Form[Constants.LOCALE].ToString());

                        }
                    }

                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Actual Locale  : " + httpRequest.Form[Constants.LOCALE].ToString());


                    CertificateDetails certificateDetails = null;




                    // If country code is null returning error message
                    if (_associateDetails.SelectedCountryCode == null || !CheckFileExtension(Request.Form.Files))
                    {
                        responseDetails.IsSuccess = Constants.FALSE;
                        Console.WriteLine(Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED);
                        responseDetails.ErrorMessage = Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED;
                        return Ok(responseDetails);
                    }

                    //if (_associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_DE))
                    //{

                    //    employeeLeaveInfo = ReadRequestDetails(_associateDetails);
                    //    employeeLeaveInfo.RequestSource = requestSource;
                    //    employeeLeaveInfo.Consent = Consent;
                    //    employeeLeaveInfo.Locale = Locale;

                    //   var EmpLeaveInfo = _context.EmployeeLeaveInformations.Where(x => x.SessionKey == SessionKey).SingleOrDefault();
                    //    if (EmpLeaveInfo!=null) {

                    //        string keyStr = EmpLeaveInfo.SecretKey;
                    //        certificateDetails = ProcessCertificate(Request.Form.Files, employeeLeaveInfo.RequestSource,
                    //        employeeLeaveInfo.EmployeeId, SessionKey, keyStr);
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine(DateTime.UtcNow.ToString()+" : Session Key Not Found...For : "+_associateDetails.SelectedCountryCode);

                    //    }
                    //}

                    if (_associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_FR))
                    {

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Country Code : " + _associateDetails.SelectedCountryCode);

                        employeeLeaveInfoFR = ReadFRRequestDetails(_associateDetails);
                        employeeLeaveInfoFR.RequestSource = requestSource;
                        employeeLeaveInfoFR.Consent = Consent;
                        employeeLeaveInfoFR.Locale = Locale;

                        var EmpLeaveInfo = _context.EmployeeLeaveInformation_FR.Where(x => x.SessionKey == SessionKey).SingleOrDefault();

                        if (EmpLeaveInfo != null)
                        {
                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + "Session Key Found...For : " + _associateDetails.AssociateNumber);
                            string keyStr = EmpLeaveInfo.SecretKey;
                            certificateDetails = ProcessCertificate(Request.Form.Files, employeeLeaveInfoFR.RequestSource,
                            employeeLeaveInfoFR.EmployeeId, SessionKey, keyStr);
                        }
                        else
                        {
                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + "Session Key Not Found...For : " + _associateDetails.AssociateNumber);

                        }
                    }
                    else if (_associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_PT) ||
                        _associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_PO))
                    {

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Country Code : " + _associateDetails.SelectedCountryCode);
                        employeeLeaveInfoPT = ReadPTRequestDetails(_associateDetails);
                        employeeLeaveInfoPT.RequestSource = requestSource;
                        employeeLeaveInfoPT.Consent = Consent;
                        employeeLeaveInfoPT.Locale = Locale;
                        var EmpLeaveInfo = _context.EmployeeLeaveInformation_PT.Where(x => x.SessionKey == SessionKey).SingleOrDefault();

                        if (EmpLeaveInfo != null)
                        {

                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Session Key Found...For : " + _associateDetails.AssociateNumber);
                            string keyStr = EmpLeaveInfo.SecretKey;
                            certificateDetails = ProcessCertificate(Request.Form.Files, employeeLeaveInfoPT.RequestSource,
                            employeeLeaveInfoPT.EmployeeId, SessionKey, keyStr);
                        }
                        else
                        {
                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Session Key Not Found...For : " + _associateDetails.SelectedCountryCode);

                        }


                    }
                    //Spain
                    else if (_associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_ES))
                    {

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Country Code : " + _associateDetails.SelectedCountryCode);
                        employeeLeaveInfoSP = ReadSPRequestDetails(_associateDetails);
                        employeeLeaveInfoSP.RequestSource = requestSource;
                        employeeLeaveInfoSP.Consent = Consent;
                        employeeLeaveInfoSP.Locale = Locale;
                        var EmpLeaveInfo = _context.EmployeeLeaveInformation_ES.Where(x => x.SessionKey == SessionKey).SingleOrDefault();

                        if (EmpLeaveInfo != null)
                        {

                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Session Key Found...For : " + _associateDetails.AssociateNumber);
                            string keyStr = EmpLeaveInfo.SecretKey;
                            certificateDetails = ProcessCertificate(Request.Form.Files, employeeLeaveInfoSP.RequestSource, employeeLeaveInfoSP.EmployeeId, SessionKey, keyStr);
                        }
                        else
                        {
                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Session Key Not Found...For : " + _associateDetails.SelectedCountryCode);

                        }


                    }

                    else
                    {
                        //Country code not matched
                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Country Code Not Matched...For : Country-Code :  " + _associateDetails.SelectedCountryCode);
                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Country Code Not Matched...For : Employee Number :  " + _associateDetails.AssociateNumber);
                        responseDetails.IsSuccess = false;
                        responseDetails.Message = Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED;
                        responseDetails.ErrorMessage = Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED;
                        return Ok(responseDetails);
                    }



                    if (null != certificateDetails && certificateDetails.BlobId != null && certificateDetails.CertificateData != null)
                    {
                        string referene = null;

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + "Certificate Details are valid...For : Employee Number : " + _associateDetails.AssociateNumber);
                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Certificate Details are valid...For : Country-Code " + _associateDetails.SelectedCountryCode);
                        if (_associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_FR))
                        {
                            employeeLeaveInfoFR.CertificateData = certificateDetails.CertificateData;
                            employeeLeaveInfoFR.BlobId = certificateDetails.BlobId;
                            referene = UpdateDatabase(SessionKey.Trim(), employeeLeaveInfoFR);
                        }
                        else if (_associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_PT) ||
                            _associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_PO))
                        {
                            employeeLeaveInfoPT.CertificateData = certificateDetails.CertificateData;
                            employeeLeaveInfoPT.BlobId = certificateDetails.BlobId;
                            referene = UpdateDatabase(SessionKey.Trim(), employeeLeaveInfoPT);

                        }
                        else if (_associateDetails.SelectedCountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_ES))
                        {
                            employeeLeaveInfoSP.CertificateData = certificateDetails.CertificateData;
                            employeeLeaveInfoSP.BlobId = certificateDetails.BlobId;
                            referene = UpdateDatabase(SessionKey.Trim(), employeeLeaveInfoSP);

                        }
                        else
                        {
                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Country Code Not Matched...For : Employee Number :  " + _associateDetails.AssociateNumber);
                            responseDetails.IsSuccess = false;
                            responseDetails.Message = Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED;
                            responseDetails.ErrorMessage = Constants.SELECTED_COUNTRY_CODE_IS_REQUIRED;
                            return Ok(responseDetails);
                        }



                        if (!string.IsNullOrEmpty(referene))
                        {

                            Console.WriteLine(Constants.DB_SUCCESS_MESSAGE);

                            responseDetails.IsSuccess = Constants.TRUE;
                            responseDetails.Message = Constants.SUCCESS_MESSAGE;
                            responseDetails.ErrorMessage = "";
                            ResponseData responseData = new ResponseData();

                            responseData.ReferenceNumber = referene;

                            List<ResponseData> responseDataList = new List<ResponseData>();
                            responseDataList.Add(responseData);
                            responseDetails.ResponseDataDetails = responseDataList;

                            return Ok(responseDetails); //Success
                        }
                        else
                        {

                            responseDetails.IsSuccess = false;
                            responseDetails.Message = Constants.DB_ERROR_MESSAGE;
                            responseDetails.ErrorMessage = Constants.DB_ERROR_MESSAGE;

                        }
                    }
                    else
                    {
                        responseDetails.IsSuccess = false;
                        responseDetails.Message = Constants.FILE_TYPE_ERROR_MESSAGE;
                        responseDetails.ErrorMessage = Constants.FILE_TYPE_ERROR_MESSAGE;

                    }

                }
                else
                {
                    responseDetails.IsSuccess = false;
                    responseDetails.Message = Constants.SESSION_KEY_ERROR_MESSAGE;
                    responseDetails.ErrorMessage = Constants.SESSION_KEY_ERROR_MESSAGE;

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("SendFileEmailNotification API Exception : " + ex.Message);
                if (ex.InnerException != null)
                {

                    Console.WriteLine("SendFileEmailNotification innerException : " + ex.InnerException);
                }
                responseDetails.IsSuccess = false;
                responseDetails.Message = ex.Message;
                responseDetails.ErrorMessage = ex.Message;


            }

            return Ok(responseDetails); //Failure

        }

        private bool CheckFileExtension(IFormFileCollection files)
        {
            IFormFileCollection tempFile = files;
            var tempFileuploaded = tempFile[0];
            string fileName = tempFileuploaded.FileName;
            string fileExtension = Path.GetExtension(fileName);

            if (fileExtension.ToUpper().Equals(".JPG") || fileExtension.ToUpper().Equals(".PNG") || fileExtension.ToUpper().Equals(".JPEG"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private EmployeeLeaveInformation_PT ReadPTRequestDetails(Associate associateDetails)
        {
            EmployeeLeaveInformation_PT employeeLeaveInfo = new EmployeeLeaveInformation_PT();

            //Associate details
            employeeLeaveInfo.EmailId = associateDetails.AssociateEmail;
            Console.WriteLine("EmailId : " + associateDetails.AssociateEmail);
            employeeLeaveInfo.EmployeeId = (associateDetails.AssociateNumber);
            employeeLeaveInfo.FirstName = associateDetails.AssociateFirstName;
            employeeLeaveInfo.LastName = associateDetails.AssociateLastName;
            employeeLeaveInfo.PhoneNumber = associateDetails.AssociateMobileNumber;
            employeeLeaveInfo.DataPrivacyTimeStamp = associateDetails.DataPrivacyTimeStamp;
            employeeLeaveInfo.SelectedCountryCode = associateDetails.SelectedCountryCode;
            employeeLeaveInfo.IsOcrCheck = associateDetails.IsOcrCheck;



            //Sick Leave Details
            employeeLeaveInfo.StartDate = associateDetails.workDisabilityCertificateDetailsInfo.StartDate;
            employeeLeaveInfo.EndDate = associateDetails.workDisabilityCertificateDetailsInfo.EndDate;
            employeeLeaveInfo.IllnessType = associateDetails.workDisabilityCertificateDetailsInfo.IllnessType;
            employeeLeaveInfo.IsFirstTimeIllness = associateDetails.workDisabilityCertificateDetailsInfo.IsFirstTimeSickness;
            employeeLeaveInfo.IsFollowupSickness = associateDetails.workDisabilityCertificateDetailsInfo.IsFollowupSickness;
            employeeLeaveInfo.CreatedDate = DateTime.UtcNow;
            employeeLeaveInfo.ModifiedDate = DateTime.UtcNow;
            employeeLeaveInfo.IsSynced = false;

            return employeeLeaveInfo;
        }

        private EmployeeLeaveInformation_FR ReadFRRequestDetails(Associate associateDetails)
        {
            EmployeeLeaveInformation_FR employeeLeaveInfo = new EmployeeLeaveInformation_FR();

            //Associate details
            employeeLeaveInfo.EmailId = associateDetails.AssociateEmail;
            Console.WriteLine(Utils.GetInstance().getTimeStamp() + " ReadFRRequestDetails() : EmailId : " + associateDetails.AssociateEmail);
            employeeLeaveInfo.EmployeeId = (associateDetails.AssociateNumber);
            employeeLeaveInfo.FirstName = associateDetails.AssociateFirstName;
            employeeLeaveInfo.LastName = associateDetails.AssociateLastName;
            employeeLeaveInfo.PhoneNumber = associateDetails.AssociateMobileNumber;
            employeeLeaveInfo.DataPrivacyTimeStamp = associateDetails.DataPrivacyTimeStamp;
            employeeLeaveInfo.SelectedCountryCode = associateDetails.SelectedCountryCode;
            employeeLeaveInfo.IsOcrCheck = associateDetails.IsOcrCheck;


            //Sick Leave Details
            employeeLeaveInfo.StartDate = associateDetails.workDisabilityCertificateDetailsInfo.StartDate;
            employeeLeaveInfo.EndDate = associateDetails.workDisabilityCertificateDetailsInfo.EndDate;
            employeeLeaveInfo.IsHospitalization = associateDetails.workDisabilityCertificateDetailsInfo.IsHospitalization;
            employeeLeaveInfo.IsCovid = associateDetails.workDisabilityCertificateDetailsInfo.IsCovid;
            employeeLeaveInfo.IsPartTime = associateDetails.workDisabilityCertificateDetailsInfo.IsPartTime;
            employeeLeaveInfo.IsAccident = associateDetails.workDisabilityCertificateDetailsInfo.IsAccident;
            employeeLeaveInfo.IsFirstTimeIllness = associateDetails.workDisabilityCertificateDetailsInfo.IsFirstTimeSickness;
            employeeLeaveInfo.IsFollowupSickness = associateDetails.workDisabilityCertificateDetailsInfo.IsFollowupSickness;
            employeeLeaveInfo.CreatedDate = DateTime.UtcNow;
            employeeLeaveInfo.ModifiedDate = DateTime.UtcNow;
            employeeLeaveInfo.IsSynced = false;

            return employeeLeaveInfo;
        }

        private EmployeeLeaveInformation ReadRequestDetails(Associate associateDetails)
        {
            EmployeeLeaveInformation employeeLeaveInfo = new EmployeeLeaveInformation();

            //Associate details
            employeeLeaveInfo.EmailId = associateDetails.AssociateEmail;
            Console.WriteLine("EmailId : " + associateDetails.AssociateEmail);
            employeeLeaveInfo.EmployeeId = (associateDetails.AssociateNumber);
            employeeLeaveInfo.FirstName = associateDetails.AssociateFirstName;
            employeeLeaveInfo.LastName = associateDetails.AssociateLastName;
            employeeLeaveInfo.PhoneNumber = associateDetails.AssociateMobileNumber;
            employeeLeaveInfo.DataPrivacyTimeStamp = associateDetails.DataPrivacyTimeStamp;
            employeeLeaveInfo.SelectedCountryCode = associateDetails.SelectedCountryCode;

            Console.WriteLine(DateTime.UtcNow.ToString() + " : DE Data :  " + associateDetails.IsOcrCheck);
            employeeLeaveInfo.IsOcrCheck = associateDetails.IsOcrCheck;



            //Sick Leave Details
            employeeLeaveInfo.StartDate = associateDetails.workDisabilityCertificateDetailsInfo.StartDate;
            employeeLeaveInfo.EndDate = associateDetails.workDisabilityCertificateDetailsInfo.EndDate;
            employeeLeaveInfo.IsAccident = associateDetails.workDisabilityCertificateDetailsInfo.IsAccident;
            employeeLeaveInfo.IsFirstTimeIllness = associateDetails.workDisabilityCertificateDetailsInfo.IsFirstTimeSickness;
            employeeLeaveInfo.CreatedDate = DateTime.UtcNow;
            employeeLeaveInfo.ModifiedDate = DateTime.UtcNow;
            employeeLeaveInfo.IsSynced = false;

            return employeeLeaveInfo;
        }

        private EmployeeLeaveInformation_ES ReadSPRequestDetails(Associate associateDetails)
        {
            EmployeeLeaveInformation_ES employeeLeaveInfo = new EmployeeLeaveInformation_ES();

            //Associate details
            employeeLeaveInfo.EmailId = associateDetails.AssociateEmail;
            Console.WriteLine("EmailId : " + associateDetails.AssociateEmail);
            employeeLeaveInfo.EmployeeId = (associateDetails.AssociateNumber);
            employeeLeaveInfo.FirstName = associateDetails.AssociateFirstName;
            employeeLeaveInfo.LastName = associateDetails.AssociateLastName;
            employeeLeaveInfo.PhoneNumber = associateDetails.AssociateMobileNumber;
            employeeLeaveInfo.DataPrivacyTimeStamp = associateDetails.DataPrivacyTimeStamp;
            employeeLeaveInfo.SelectedCountryCode = associateDetails.SelectedCountryCode;
            employeeLeaveInfo.IsOcrCheck = associateDetails.IsOcrCheck;



            //Sick Leave Details
            employeeLeaveInfo.StartDate = associateDetails.workDisabilityCertificateDetailsInfo.StartDate;
            employeeLeaveInfo.EndDate = associateDetails.workDisabilityCertificateDetailsInfo.EndDate;
            employeeLeaveInfo.KindofIllness = associateDetails.workDisabilityCertificateDetailsInfo.KindofIllness;
            employeeLeaveInfo.RequestType = associateDetails.workDisabilityCertificateDetailsInfo.RequestType;
            employeeLeaveInfo.FollowUpDate = associateDetails.workDisabilityCertificateDetailsInfo.FollowupDate;
            employeeLeaveInfo.CreatedDate = DateTime.UtcNow;
            employeeLeaveInfo.ModifiedDate = DateTime.UtcNow;
            if (associateDetails.workDisabilityCertificateDetailsInfo.AgreeContactDataTimeStamp != "")
            {
                employeeLeaveInfo.AgreeContactDataTimeStamp = DateTime.UtcNow;
            }
            else
            {
                employeeLeaveInfo.AgreeContactDataTimeStamp = null;
            }
            employeeLeaveInfo.IsSynced = false;

            return employeeLeaveInfo;
        }
        private string UpdateDatabase(string sesKey, EmployeeLeaveInformation_PT empInfo)
        {
            if (!string.IsNullOrEmpty(sesKey))
            {
                // var entity = _context.EmployeeLeaveInformations.Where(c => c.SessionKey == sesKey.Trim()).SingleOrDefault();
                var entityList = _context.EmployeeLeaveInformation_PT.Where(c => c.SessionKey == sesKey.Trim() &&
                (c.ReferenceNumber == null || c.ReferenceNumber == "")).ToList<EmployeeLeaveInformation_PT>();

                if (entityList != null && entityList.Count > 0)
                {
                    var entity = entityList[0];
                    bool decryptTest = DecryptTest(empInfo.FirstName, entity.SecretKey);
                    DecryptTest(empInfo.StartDate, entity.SecretKey);
                    DecryptTest(empInfo.EndDate, entity.SecretKey);

                    if (decryptTest)
                    {

                        Console.WriteLine("Decryption Test Pass....");

                        empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_PO, empInfo.SessionKey, empInfo.EmployeeId);

                        Console.WriteLine("ReferenceNumber : " + empInfo.ReferenceNumber);


                        //checking the refrence number in the database before inserting into db

                        EmployeeLeaveInformation_PT referenceEntity = null;
                        do
                        {

                            referenceEntity = _context.EmployeeLeaveInformation_PT.Where(c => c.ReferenceNumber == empInfo.ReferenceNumber).SingleOrDefault();
                            if (referenceEntity != null)
                            {

                                empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_PO, empInfo.SessionKey, empInfo.EmployeeId);

                            }
                        } while (referenceEntity != null);



                        entity.BlobId = empInfo.BlobId;

                        entity.CertificateData = empInfo.CertificateData;
                        entity.CreatedDate = empInfo.CreatedDate;
                        entity.DataPrivacyTimeStamp = empInfo.DataPrivacyTimeStamp;
                        entity.EmailId = empInfo.EmailId;
                        entity.EmployeeId = empInfo.EmployeeId;
                        entity.EndDate = empInfo.EndDate;
                        entity.FirstName = empInfo.FirstName;
                        entity.LastName = empInfo.LastName;
                        entity.IllnessType = empInfo.IllnessType;
                        entity.IsFirstTimeIllness = empInfo.IsFirstTimeIllness;
                        entity.IsFollowupSickness = empInfo.IsFollowupSickness;
                        entity.IsSynced = empInfo.IsSynced;
                        entity.Locale = empInfo.Locale;
                        entity.ModifiedDate = empInfo.ModifiedDate;
                        entity.PhoneNumber = empInfo.PhoneNumber;
                        entity.ReferenceNumber = empInfo.ReferenceNumber;
                        entity.RequestSource = empInfo.RequestSource;
                        entity.StartDate = empInfo.StartDate;
                        entity.IsOcrCheck = empInfo.IsOcrCheck;
                        entity.SelectedCountryCode = empInfo.SelectedCountryCode;

                        entity.Consent = empInfo.Consent == null ? Constants.FALSE : empInfo.Consent;


                        int dbRslt = _context.SaveChanges();
                        Console.WriteLine("DBResult : " + dbRslt);

                        if (dbRslt > 0)
                        {
                            SendEmail(empInfo.EmailId, empInfo.FirstName, empInfo.LastName, empInfo.ReferenceNumber, entity.SecretKey,
                                empInfo.Locale, empInfo.SelectedCountryCode);
                            return empInfo.ReferenceNumber;  //Success


                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine(Constants.SESSION_KEY_ERROR_MESSAGE);
                    return null;
                }
            }
            else
            {
                Console.WriteLine(Constants.SESSION_KEY_ERROR_MESSAGE);
                return null;
            }

            return null;
        }

        private string UpdateDatabase(string sesKey, EmployeeLeaveInformation_FR empInfo)
        {
            if (!string.IsNullOrEmpty(sesKey))
            {

                var entityList = _context.EmployeeLeaveInformation_FR.Where(c => c.SessionKey == sesKey.Trim() &&
                (c.ReferenceNumber == null || c.ReferenceNumber == "")).ToList<EmployeeLeaveInformation_FR>();

                if (entityList != null && entityList.Count > 0)
                {
                    var entity = entityList[0];
                    bool decryptTest = DecryptTest(empInfo.FirstName, entity.SecretKey);
                    DecryptTest(empInfo.StartDate, entity.SecretKey);
                    DecryptTest(empInfo.EndDate, entity.SecretKey);

                    if (decryptTest)
                    {

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : From UpdateDatabase():  Decryption Test Pass...For : " + empInfo.EmployeeId);

                        empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_FR, empInfo.SessionKey, empInfo.EmployeeId);

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + "ReferenceNumber : " + empInfo.ReferenceNumber +
                            " of session key : " + entity.SessionKey);


                        //checking the refrence number in the database before inserting into db

                        EmployeeLeaveInformation_FR referenceEntity = null;
                        do
                        {

                            referenceEntity = _context.EmployeeLeaveInformation_FR.Where(c => c.ReferenceNumber == empInfo.ReferenceNumber).SingleOrDefault();
                            if (referenceEntity != null)
                            {
                                Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : " + empInfo.ReferenceNumber +
                                  " This Reference Number is already allotted....for this session" + referenceEntity.SessionKey);

                                empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_FR, empInfo.SessionKey, empInfo.EmployeeId);

                            }
                        } while (referenceEntity != null);


                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + "DB Insertion Process Started for Reference Number : " + empInfo.ReferenceNumber +
                           " of session key : " + entity.SessionKey);

                        entity.BlobId = empInfo.BlobId;

                        entity.CertificateData = empInfo.CertificateData;
                        entity.CreatedDate = empInfo.CreatedDate;
                        entity.DataPrivacyTimeStamp = empInfo.DataPrivacyTimeStamp;
                        entity.EmailId = empInfo.EmailId;
                        entity.EmployeeId = empInfo.EmployeeId;
                        entity.EndDate = empInfo.EndDate;
                        entity.FirstName = empInfo.FirstName;
                        entity.LastName = empInfo.LastName;
                        entity.IsAccident = empInfo.IsAccident;
                        entity.IsHospitalization = empInfo.IsHospitalization;
                        entity.IsCovid = empInfo.IsCovid;
                        entity.IsPartTime = empInfo.IsPartTime;
                        entity.IsFirstTimeIllness = empInfo.IsFirstTimeIllness;
                        entity.IsFollowupSickness = empInfo.IsFollowupSickness;
                        entity.IsSynced = empInfo.IsSynced;
                        entity.Locale = empInfo.Locale;
                        entity.ModifiedDate = empInfo.ModifiedDate;
                        entity.PhoneNumber = empInfo.PhoneNumber;
                        entity.ReferenceNumber = empInfo.ReferenceNumber;
                        entity.RequestSource = empInfo.RequestSource;
                        entity.StartDate = empInfo.StartDate;
                        entity.IsOcrCheck = empInfo.IsOcrCheck;
                        entity.SelectedCountryCode = empInfo.SelectedCountryCode;

                        entity.Consent = empInfo.Consent == null ? Constants.FALSE : empInfo.Consent;

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + "Before DB Insertion : The Reference Number for : " + empInfo.ReferenceNumber +
                            " of session key : " + entity.SessionKey);

                        int dbRslt = _context.SaveChanges();

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + ": Reference Number : " + empInfo.ReferenceNumber +
                              " DBResult :" + dbRslt + " for Employee Number : " + empInfo.EmployeeId);

                        if (dbRslt > 0)
                        {
                            SendEmail(empInfo.EmailId, empInfo.FirstName, empInfo.LastName, empInfo.ReferenceNumber,
                                entity.SecretKey, empInfo.Locale, empInfo.SelectedCountryCode);
                            return empInfo.ReferenceNumber;  //Success


                        }
                        else
                        {
                            Console.WriteLine(Utils.GetInstance().getTimeStamp() + "DB Insertion failed for : Reference Number : " + empInfo.ReferenceNumber +
                           " DBResult :" + dbRslt + " for Employee Number : " + empInfo.EmployeeId);


                        }
                    }
                    else
                    {
                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : " + Constants.SESSION_KEY_NOT_FOUND_IN_DB_ERROR_MESSAGE +
                        "For this sesssion Key " + sesKey + " of Employee Number : " + empInfo.EmployeeId);
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : " + Constants.SESSION_KEY_NOT_FOUND_IN_DB_ERROR_MESSAGE +
                       "For this sesssion Key " + sesKey + " of Employee Number : " + empInfo.EmployeeId);
                    return null;
                }
            }
            else
            {
                Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : " + Constants.SESSION_KEY_MISSING_FROM_APPLICATION_ERROR_MESSAGE
                    + "For this sesssion Key " + sesKey + " of Employee Number : " + empInfo.EmployeeId);
                return null;
            }

            return null;
        }
        //private string UpdateDatabase(string sesKey, EmployeeLeaveInformation empInfo)
        //{

        //    if (!string.IsNullOrEmpty(sesKey))
        //    {
        //        // var entity = _context.EmployeeLeaveInformations.Where(c => c.SessionKey == sesKey.Trim()).SingleOrDefault();
        //        var entityList = _context.EmployeeLeaveInformations.Where(c => c.SessionKey == sesKey.Trim() &&
        //        (c.ReferenceNumber == null || c.ReferenceNumber == "")).ToList<EmployeeLeaveInformation>();

        //        if (entityList != null && entityList.Count > 0)
        //        {
        //            var entity = entityList[0];
        //            bool decryptTest = DecryptTest(empInfo.FirstName, entity.SecretKey);
        //            DecryptTest(empInfo.StartDate, entity.SecretKey);
        //            DecryptTest(empInfo.EndDate, entity.SecretKey);

        //            if (decryptTest)
        //            {

        //                Console.WriteLine("Decryption Test Pass....");

        //                empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_DE);

        //                Console.WriteLine("ReferenceNumber : " + empInfo.ReferenceNumber);


        //                //checking the refrence number in the database before inserting into db

        //                EmployeeLeaveInformation referenceEntity = null;
        //                do
        //                {

        //                    referenceEntity = _context.EmployeeLeaveInformations.Where(c => c.ReferenceNumber == empInfo.ReferenceNumber).SingleOrDefault();
        //                    if (referenceEntity != null)
        //                    {

        //                        empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_DE);

        //                    }
        //                } while (referenceEntity != null);



        //                entity.BlobId = empInfo.BlobId;

        //                entity.CertificateData = empInfo.CertificateData;
        //                entity.CreatedDate = empInfo.CreatedDate;
        //                entity.DataPrivacyTimeStamp = empInfo.DataPrivacyTimeStamp;
        //                entity.EmailId = empInfo.EmailId;
        //                entity.EmployeeId = empInfo.EmployeeId;
        //                entity.EndDate = empInfo.EndDate;
        //                entity.FirstName = empInfo.FirstName;
        //                entity.LastName = empInfo.LastName;
        //                entity.IsAccident = empInfo.IsAccident;
        //                entity.IsFirstTimeIllness = empInfo.IsFirstTimeIllness;

        //                entity.IsSynced = empInfo.IsSynced;
        //                entity.Locale = empInfo.Locale;
        //                entity.ModifiedDate = empInfo.ModifiedDate;
        //                entity.PhoneNumber = empInfo.PhoneNumber;
        //                entity.ReferenceNumber = empInfo.ReferenceNumber;
        //                entity.RequestSource = empInfo.RequestSource;
        //                entity.StartDate = empInfo.StartDate;

        //                Console.WriteLine(DateTime.UtcNow.ToString()+ " UpdateDatabase :  " + empInfo.IsOcrCheck + " Employee Number : "
        //                    +empInfo.EmployeeId);
        //                entity.IsOcrCheck = empInfo.IsOcrCheck;

        //                entity.Consent = empInfo.Consent == null ? Constants.FALSE : empInfo.Consent;


        //                int dbRslt = _context.SaveChanges();
        //                Console.WriteLine("DBResult : " + dbRslt);

        //                if (dbRslt > 0)
        //                {
        //                    SendEmail(empInfo.EmailId,empInfo.FirstName,empInfo.LastName,empInfo.ReferenceNumber, entity.SecretKey, 
        //                        empInfo.Locale,empInfo.SelectedCountryCode);
        //                    return empInfo.ReferenceNumber;  //Success


        //                }

        //            }
        //            else
        //            {
        //                return null;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine(Constants.SESSION_KEY_ERROR_MESSAGE);
        //            return null;
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine(Constants.SESSION_KEY_ERROR_MESSAGE);
        //        return null;
        //    }

        //    return null;
        //}

        //Spain
        private string UpdateDatabase(string sesKey, EmployeeLeaveInformation_ES empInfo)
        {
            if (!string.IsNullOrEmpty(sesKey))
            {
                // var entity = _context.EmployeeLeaveInformations.Where(c => c.SessionKey == sesKey.Trim()).SingleOrDefault();

                var entityList = _context.EmployeeLeaveInformation_ES.Where(c => c.SessionKey == sesKey.Trim() &&
                (c.ReferenceNumber == null || c.ReferenceNumber == "")).ToList<EmployeeLeaveInformation_ES>();

                if (entityList != null && entityList.Count > 0)
                {
                    var entity = entityList[0];
                    bool decryptTest = DecryptTest(empInfo.FirstName, entity.SecretKey);
                    DecryptTest(empInfo.StartDate, entity.SecretKey);
                    DecryptTest(empInfo.EndDate, entity.SecretKey);

                    if (decryptTest)
                    {

                        Console.WriteLine("Decryption Test Pass....");

                        empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_ES, empInfo.SessionKey, empInfo.EmployeeId);

                        Console.WriteLine("ReferenceNumber : " + empInfo.ReferenceNumber);


                        //checking the refrence number in the database before inserting into db

                        EmployeeLeaveInformation_ES referenceEntity = null;
                        do
                        {

                            referenceEntity = _context.EmployeeLeaveInformation_ES.Where(c => c.ReferenceNumber == empInfo.ReferenceNumber).SingleOrDefault();
                            if (referenceEntity != null)
                            {

                                empInfo.ReferenceNumber = "" + GetReferenceNumber(Constants.COUNTRY_CODE_ES, empInfo.SessionKey, empInfo.EmployeeId);

                            }
                        } while (referenceEntity != null);



                        entity.BlobId = empInfo.BlobId;

                        entity.CertificateData = empInfo.CertificateData;
                        entity.CreatedDate = empInfo.CreatedDate;
                        entity.DataPrivacyTimeStamp = empInfo.DataPrivacyTimeStamp;
                        entity.EmailId = empInfo.EmailId;
                        entity.EmployeeId = empInfo.EmployeeId;
                        entity.EndDate = empInfo.EndDate;
                        entity.FirstName = empInfo.FirstName;
                        entity.LastName = empInfo.LastName;
                        entity.KindofIllness = empInfo.KindofIllness;
                        entity.RequestType = empInfo.RequestType;
                        entity.FollowUpDate = empInfo.FollowUpDate;
                        entity.IsSynced = empInfo.IsSynced;
                        entity.Locale = empInfo.Locale;
                        entity.ModifiedDate = empInfo.ModifiedDate;
                        entity.PhoneNumber = empInfo.PhoneNumber;
                        entity.ReferenceNumber = empInfo.ReferenceNumber;
                        entity.RequestSource = empInfo.RequestSource;
                        entity.StartDate = empInfo.StartDate;
                        entity.IsOcrCheck = empInfo.IsOcrCheck;
                        entity.SelectedCountryCode = empInfo.SelectedCountryCode;
                        entity.AgreeContactDataTimeStamp = empInfo.AgreeContactDataTimeStamp;
                        entity.Consent = empInfo.Consent == null ? Constants.FALSE : empInfo.Consent;


                        int dbRslt = _context.SaveChanges();
                        Console.WriteLine("DBResult : " + dbRslt);

                        if (dbRslt > 0)
                        {
                            if (empInfo.EmailId != null)
                            {
                                SendEmail(empInfo.EmailId, empInfo.FirstName, empInfo.LastName, empInfo.ReferenceNumber, entity.SecretKey,
                                    empInfo.Locale, empInfo.SelectedCountryCode);
                            }
                            return empInfo.ReferenceNumber;  //Success


                        }

                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine(Constants.SESSION_KEY_ERROR_MESSAGE);
                    return null;
                }
            }
            else
            {
                Console.WriteLine(Constants.SESSION_KEY_ERROR_MESSAGE);
                return null;
            }

            return null;
        }

        private CertificateDetails ProcessCertificate(IFormFileCollection files, string requestSource, string employeeId, string sesKey, string keyStr)
        {
            CertificateDetails certificateDetails = new CertificateDetails()
            {
                CertificateData = null,
                BlobId = null
            };
            if (files.Count > 0)
            {

                var _fileuploaded = files[0];

                string fileName = _fileuploaded.FileName;

                string fileExtension = Path.GetExtension(fileName);


                if (_fileuploaded.Length > 0 && fileExtension.ToUpper().Equals(".JPG") || fileExtension.ToUpper().Equals(".PNG") ||
                    fileExtension.ToUpper().Equals(".JPEG"))
                {


                    if (requestSource.Equals("Web"))
                    {

                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " :  Request Source WEB:  Sesseion Key : " + sesKey + " of Employee Number : " + employeeId);

                        AESEncryption aes = new AESEncryption();

                        byte[] key = aes.ConvertStringToByteArray(keyStr);

                        System.IO.Stream fs = _fileuploaded.OpenReadStream();
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        byte[] bytes = br.ReadBytes((Int32)fs.Length);

                        byte[] encryptedBytes = aes.EncryptBytes(bytes, key);

                        string base64String = Convert.ToBase64String(encryptedBytes, 0, encryptedBytes.Length);


                        certificateDetails.BlobId = employeeId.ToString() + _fileuploaded.FileName;

                        certificateDetails.CertificateData = base64String;


                    }
                    else
                    {


                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " :  Request Source : " + requestSource + " : Sessions Key : " + sesKey + " of Employee Number : " + employeeId);
                        System.IO.Stream fs = _fileuploaded.OpenReadStream();
                        System.IO.BinaryReader br = new System.IO.BinaryReader(fs);
                        Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                        string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                        certificateDetails.BlobId = employeeId.ToString() + _fileuploaded.FileName;

                        certificateDetails.CertificateData = base64String;
                    }


                }

            }
            return certificateDetails;
        }

        private bool DecryptTest(string firstName, string secretKey)
        {
            AESEncryption aESEncryption = new AESEncryption();
            try
            {
                byte[] key = aESEncryption.ConvertStringToByteArray(secretKey);
                string Name = aESEncryption.Decrypt(key, firstName);
                if (!string.IsNullOrEmpty(Name))
                {
                    return true;

                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(Utils.GetInstance().getTimeStamp() + " DecryptTest Exception : " + ex.Message);
                return false;
            }
        }

        private void SendEmail(string emailId, string firstName, string lastName, string referenceNumber, string secretKey,
            string locale, string coutryCode)
        {

            if (!string.IsNullOrEmpty(emailId) && !string.IsNullOrEmpty(secretKey))
            {
                AESEncryption aes = new AESEncryption();
                try
                {
                    byte[] key = aes.ConvertStringToByteArray(secretKey);
                    emailId = aes.Decrypt(key, emailId);

                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " After Decryption Email Id : " + emailId);
                    firstName = aes.Decrypt(key, firstName);
                    lastName = aes.Decrypt(key, lastName);

                    firstName = EmailNotificationController.FirstLetterToUpper(firstName);
                    lastName = EmailNotificationController.FirstLetterToUpper(lastName);

                }
                catch (Exception ex)
                {

                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " SendEmail() : Data Decrytion Exception  : " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine(Utils.GetInstance().getTimeStamp() + " SendEmail() : Data Decrytion Exception  : " + ex.InnerException);
                    }
                }

            }

            string Language = locale.ToUpper();
            Console.WriteLine("Language  : " + Language);


            string htmlData = null;
            string senderAddress = "no-reply@fr.bosch.com";

            if (Constants.COUNTRY_CODE_FR.ToUpper().Equals(coutryCode.ToUpper()))
            {
                senderAddress = "no-reply@fr.bosch.com";
                htmlData = new FranceEmailTemplate().getEmailTemplate(Language, firstName + " " + lastName, referenceNumber);
            }
            else if (Constants.COUNTRY_CODE_PO.ToUpper().Equals(coutryCode.ToUpper()))
            {
                senderAddress = "no-reply@pt.bosch.com";
                htmlData = new PortugalEmailTemplate().getEmailTemplate(Language, firstName + " " + lastName, referenceNumber);
            }
            else if (Constants.COUNTRY_CODE_ES.ToUpper().Equals(coutryCode.ToUpper()))
            {
                senderAddress = "no-reply@es.bosch.com";
                firstName = Utils.FirstLetterToUpper(firstName);
                lastName = Utils.FirstLetterToUpper(lastName);
                htmlData = new SpainEmailTemplate().getEmailTemplate(Language, firstName + " " + lastName, referenceNumber);
            }
            else
            {
                Console.WriteLine(Utils.GetInstance().getTimeStamp() + " SendEmail() : Country code not matched...." + coutryCode.ToUpper());
            }


            if ((htmlData != null) && (!string.IsNullOrEmpty(emailId)))
            {
          
                string subject = "Bosch SAM";

                SmtpClient smtp = new SmtpClient();
                try
                {
                    MailMessage message = new MailMessage();

                    //message.From = new MailAddress("no-reply@de.bosch.com");
                    message.From = new MailAddress(senderAddress);
                    message.To.Add(new MailAddress(emailId));
                    message.Subject = subject;
                    message.IsBodyHtml = true; //to make message body as html  
                    message.Body = htmlData;
                    smtp.Port = 25;
                    smtp.Host = "rb-smtp-bosch2any.rbdmz01.com"; //10.58.177.140  Red-Hat server
                    //smtp.Host = "rb-smtp-int.bosch.com"; //for Bosch host      
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;

                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(message);

                    Console.WriteLine("Mail has been sent Successfully...");
                }
                catch (SmtpException se)
                {

                    Console.WriteLine("Smtp Host : " + smtp.Host);
                    Console.WriteLine("Smtp : Status Code : " + se.StatusCode);
                    Console.WriteLine("Smtp : Exception : " + se.Message);
                    Console.WriteLine("Smtp stackTrace : " + se.StackTrace);
                    Console.WriteLine("Employee Info : " + emailId);

                    if (se.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception is : " + se.InnerException);

                    }


                }
                catch (Exception ex)
                {

                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " SendEmail() : SendEmail Exception : " + ex.Message);
                    Console.WriteLine("SendEmail Employee Info : " + emailId);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Inner Exception is : " + ex.InnerException);
                    }

                }
            }
            else
            {
                Console.WriteLine("html data is null");
            }

        }


        public static string FirstLetterToUpper(string inputName)
        {
            if (inputName == null)
                return null;

            if (inputName.Length > 1)
            {
                inputName.ToLower();
                TextInfo info = CultureInfo.CurrentCulture.TextInfo;
                inputName = info.ToTitleCase(inputName);
            }
            return inputName;
        }

        [Route("GetAppVersionDetails"), HttpPost]
        public async Task<IActionResult> GetAppVersionDetails(AppVersionDetails appVersion)
        {

            AppVersionDetails result = new AppVersionDetails()
            {
                IsSuccess = false,
                MobileOsName = "",
                VersionCode = -1,
                VersionName = "0.0",
                AppName = "Bosch SAM"

            };
            if (!ModelState.IsValid)
            {
                Console.WriteLine(Utils.GetInstance().getTimeStamp() + " AppVersion API  : ModelState is not valid ");
                return BadRequest(ModelState);
            }

            try
            {
                if (appVersion.MobileOsName.Equals(Constants.IOS) || appVersion.MobileOsName.Equals(Constants.ANDROID))
                {


                    GlobalApplicationVersionDetails versionDetails = _context.GlobalApplicationVersionDetails.Where(x => x.MobileOsName == appVersion.MobileOsName).SingleOrDefault();


                    result.IsSuccess = true;
                    result.Message = "SUCCESS";
                    result.AppName = versionDetails.AppName;
                    result.MobileOsName = versionDetails.MobileOsName;
                    result.VersionCode = versionDetails.VersionCode;
                    result.VersionName = versionDetails.VersionName;

                }
                else
                {
                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : AppVersion API  : App Name is not matched ");
                    return BadRequest(ModelState);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(Utils.GetInstance().getTimeStamp() + " : AppVersion API  : Exception : " + ex.Message);
                result.IsSuccess = Constants.FALSE;
                result.Message = ex.Message;
            }

            return Ok(result);
        }

        private long GetReferenceNumber(string countryCode, string sessionKey, string empNo)
        {
            long referenceNumber = 0;
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.Serializable,
                Timeout = TimeSpan.MaxValue
            };

            ReaderWriterLockSlim methodLock = new ReaderWriterLockSlim();
            methodLock.EnterWriteLock();
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions))
                {

                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " Before GetReferenceNumber() : session key : " + sessionKey + " of employee number : " + empNo);

                    GlobalReferenceNumberDetails row = _context.GlobalReferenceNumberDetails.Where(x => x.CountryCode == countryCode).SingleOrDefault();

                    if (row != null)
                    {
                        row.ReferenceNumber = row.ReferenceNumber + 1;
                        referenceNumber = row.ReferenceNumber;
                    }

                    Console.WriteLine(Utils.GetInstance().getTimeStamp() + " After GetReferenceNumber() : Reference Number : " + referenceNumber + " : session key : " + sessionKey + "" +
                        " of employee number : " + empNo);

                    _context.SaveChanges();

                    scope.Complete();
                }
                return referenceNumber;
            }
            finally
            {
                methodLock.ExitWriteLock();

            }


        }


        [Route("GetIllnessTypes"), HttpPost]
        public async Task<IActionResult> GetIllnessTypes(IllnessRequest request)
        {

            IllnessRespone illnessRespone = new IllnessRespone()
            {
                IsSuccess = false,
                Message = "",
                ErrorMessage = "",
                IllnessTypes = new List<IllnessTypeDetails>()
            };
            List<IllnessTypeDetails> illnessTypes = null;

            //string referer = Request.Headers["Referer"].ToString().ToLower();
            RequestHeaders header = Request.GetTypedHeaders();
            Uri uriReferer = header.Referer;

            Console.WriteLine("UriReferer  : " + uriReferer);
            //if (uriReferer!=null && uriReferer.ToString().Equals("http://testboschaubscan-ci-de11-testboschaubscan-app.emea.osh-bosch.com/home"))
            //{
            //    Console.WriteLine("GetIllnessTypes API  : ModelState is not valid ");
            //    //return BadRequest("The request is not originated from trusted source");
            //}
            if (!ModelState.IsValid)
            {
                Console.WriteLine("GetIllnessTypes API  : ModelState is not valid ");
                return BadRequest(ModelState);
            }

            try
            {
                if (request.CountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_PO))
                {


                    illnessTypes = _context.IllnessTypeDetails.Where(x => x.CountryCode == request.CountryCode).ToList();
                }
                else
                {
                    illnessTypes = _context.IllnessTypeDetails.Where(x => x.CountryCode == Constants.COUNTRY_CODE_EN).ToList();

                    Console.WriteLine("GetIllnessTypes Country Code : " + request.CountryCode);

                }

                illnessRespone.Message = Constants.SUCCESS;
                illnessRespone.IsSuccess = true;
                illnessRespone.IllnessTypes = illnessTypes;

            }
            catch (Exception ex)
            {
                Console.WriteLine("GetIllnessTypes API  : Exception : " + ex.Message);
                illnessRespone.Message = ex.Message;
                illnessRespone.IsSuccess = false;

            }

            return Ok(illnessRespone);
        }



    }
}