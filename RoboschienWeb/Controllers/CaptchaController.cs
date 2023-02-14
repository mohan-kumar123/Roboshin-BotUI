using Microsoft.AspNetCore.Mvc;
using RoboschienWeb.Models.Entities.UI;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RoboschienWeb.Controllers
{
    [ApiController]
    [Route("Roboschien")]
    public class CaptchaController : Controller
    {
        
        private string _mobileSecretKey = "6LctI0kbAAAAALxkH-h17AC1aJXTSY5Ah-qu_3YL"; //DE code  :  6LcdfMMUAAAAACf-L7wbdssxueN6l9GwMvSeEvPF
        private string _webSecretKey = "6LfdPM0bAAAAAEy_JHszwRyvh_Z7X3gWM4-83fnP"; // DE Code  :  6Lc_qsMUAAAAAB9QiZ0jFGyEWKKrr-HEdm9CNKHG
        private string _iOSSecretKey = "6Lc1JUkbAAAAAAKCmEjvsNtGKm9v8pqBhNXn3Ayk";// DE Code :   6LdaO3EUAAAAABiCITmk7ZKKHgAUuOUTICje34U4
        private string _mobileGoogleAPI = "https://www.google.com/recaptcha/";

        [Route("captcha/verify"), HttpPost]

        public async Task<bool> ValidateMobileCaptcha(Captcha userCaptcha)
        {
          //  HttpClientHandler clientHandler = new HttpClientHandler();
            //clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
           // var client = new HttpClient(clientHandler)

            GoogleCaptchaResponse responseData = null;
            HttpClient _client = new HttpClient();
            _client.BaseAddress = new Uri(_mobileGoogleAPI);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
          

            GoogleCaptcha data = new GoogleCaptcha();

            if(userCaptcha.RequestSource.ToUpper()=="APP" || userCaptcha.RequestSource.ToUpper() == "Android")
            {
                data.secret = _mobileSecretKey;

            }
            else if(userCaptcha.RequestSource.ToUpper() == "WEB")
            {
                data.secret = _webSecretKey;
            }
            else if (userCaptcha.RequestSource.ToUpper() == "IOS")
            {
                data.secret = _iOSSecretKey;
            }
            else
            {
                return false;
            }

            try
            {
                data.response = userCaptcha.UserResponse;
                HttpResponseMessage response = await _client.PostAsync("api/siteverify?secret=" + data.secret + "&response=" + data.response, null);
                response.EnsureSuccessStatusCode();

                 responseData = await response.Content.ReadAsAsync<GoogleCaptchaResponse>();
                Console.WriteLine("GoogleCaptcha Response : "+responseData.IsSuccess+" Total Response : " +  responseData.ToString());

                return responseData.IsSuccess;
               // return true;
            }catch(Exception ex)
            {
                Console.WriteLine("ValidateMobileCaptcha : Exception : " + ex.Message);
                if (ex.InnerException!=null)
                {

                    Console.WriteLine("ValidateMobileCaptcha: Exception : " + ex.InnerException);


                }
                if (responseData==null)
                {
                    responseData = new GoogleCaptchaResponse();

                }

                //  return responseData.IsSuccess;
                return false;
            }

        }




      
    }
}
