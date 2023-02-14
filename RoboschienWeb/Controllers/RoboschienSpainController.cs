
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
    [Route("RoboschienSpain")]
    public class RoboschienSpainController : Controller
    {

        private readonly SickLeaveContext _context;

        private const string APP_NAME = "Bosch SAM";



        public RoboschienSpainController(SickLeaveContext context, IOptions<ApiSettings> apiSettings)
        {
            _context = context;



        }


       
        [Route("GetRequestTypes"), HttpPost]
        public async Task<IActionResult> GetRequestTypes(RequestTypeRequest request)
        {

            RequestTypeRespone illnessRespone = new RequestTypeRespone()
            {
                IsSuccess = false,
                Message = "",
                ErrorMessage = "",
                RequestTypes = new List<RequestTypeDetails>(),
                IllnessTypesSP = new List<KindofIllnessDetails>()
            };
            List<RequestTypeDetails> requestTypes = null;
            List<KindofIllnessDetails> illnessTypesSP = null;

            //string referer = Request.Headers["Referer"].ToString().ToLower();
            RequestHeaders header = Request.GetTypedHeaders();
            Uri uriReferer = header.Referer;

            Console.WriteLine("UriReferer  : " + uriReferer);
            //if (uriReferer!=null && uriReferer.ToString().Equals("http://testboschaubscan-ci-de11-testboschaubscan-app.emea.osh-bosch.com/home"))
            //{
            //    Console.WriteLine("GetrequestTypes API  : ModelState is not valid ");
            //    //return BadRequest("The request is not originated from trusted source");
            //}
            if (!ModelState.IsValid)
            {
                Console.WriteLine("GetrequestTypes API  : ModelState is not valid ");
                return BadRequest(ModelState);
            }

            try
            {
                if (request.CountryCode.ToUpper().Equals(Constants.COUNTRY_CODE_ES))
                {

                    requestTypes = _context.RequestTypeDetails.Where(x => x.CountryCode == request.CountryCode).ToList();
                    illnessTypesSP = _context.KindofIllnessDetails.Where(x => x.CountryCode == request.CountryCode).ToList();

                }
                else
                {
                    requestTypes = _context.RequestTypeDetails.Where(x => x.CountryCode == Constants.COUNTRY_CODE_EN).ToList();
                    illnessTypesSP = _context.KindofIllnessDetails.Where(x => x.CountryCode == Constants.COUNTRY_CODE_EN).ToList();
                    Console.WriteLine("GetrequestTypes Country Code : " + request.CountryCode);

                }

                illnessRespone.Message = Constants.SUCCESS;
                illnessRespone.IsSuccess = true;
                illnessRespone.RequestTypes = requestTypes;
                illnessRespone.IllnessTypesSP = illnessTypesSP;
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetrequestTypes API  : Exception : " + ex.Message);
                illnessRespone.Message = ex.Message;
                illnessRespone.IsSuccess = false;

            }

            return Ok(illnessRespone);
        }



    }
}



