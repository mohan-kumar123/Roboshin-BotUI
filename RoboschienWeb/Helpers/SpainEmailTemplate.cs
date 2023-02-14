using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Helpers
{
    public class SpainEmailTemplate
    {
        public string getEmailTemplate(string language, string name, string referenceNumber)
        {
            string HtmlContent = "";

            Console.WriteLine("Email template : Language  : " + language);

            HtmlContent = "<!DOCTYPE html> <html><head>" +
                 "<style>@font-face { font-family: BoschSansFont; src: url('https://rbeidashboarddiagdev.blob.core.windows.net/associate-data/bosch-fonts/BoschSans-Black.woff');}" +

 "</style></head><body><div style='font-family:BoschSansFont;font-size:10pt;'>" +
 "Hola " + name + "," +
    "<br /><br />Gracias por enviar su parte médico de incapacidad temporal a través de la aplicación.  " +
    "<br /><br />HR Service recibirá su parte médico de incapacidad temporal con el " + "<b>número de referencia " + referenceNumber + "</b>" +
    "<br /><br />Por favor, guarde este número por si tiene alguna duda o necesita hablar de su ausencia con HR Service." +
    "<br /><br />Informaremos a su mando de su baja/confirmación/alta de incapacidad temporal y le deseamos una pronta recuperación" +
    "<br /><br />" + 
        

    
    "<br /><br />HR Service" +
    "<br /><br />Bosch SAM" +
    "<div><br /><br /><br />=======================================================================================================================================</div>" +
    "<br /><br /><br /> Hello " + name + "," +
        "<br /><br />Thank you for sending your sick leave using the application!  " +
        "<br /><br />HR Service will receive your sick leave with the" + "<b> Reference number " + referenceNumber + "</b>" +

        "<br /><br />Please keep this number in case you have any questions or need to discuss your absence with your HR Service. " +
        "<br /><br />We will inform your manager of your inability to work and wish you a good recovery" +
        "<br /><br />   " +



                  "<br /><br />HR Service" +
                  "<br /><br />Bosch SAM" +
                  "</div></body></html>";



            return HtmlContent;


        }
    }
}
