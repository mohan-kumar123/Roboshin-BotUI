using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Helpers
{
    public class PortugalEmailTemplate
    {

        public string getEmailTemplate(string language, string name, string referenceNumber)
        {
            string HtmlContent = "";

            Console.WriteLine("Email template : Language  : " + language);

            HtmlContent = "<!DOCTYPE html> <html><head>" +
                "<style>@font-face { font-family: BoschSansFont; src: url('https://rbeidashboarddiagdev.blob.core.windows.net/associate-data/bosch-fonts/BoschSans-Black.woff');}" +

"</style></head><body><div style='font-family:BoschSansFont;font-size:10pt;'>" +

               "Caro(a) " + name + "," +
"<br /><br />Obrigado por utilizar a nossa aplicação! Esperamos que tenha tido uma experiência agradável." +
"<br /><br /> O seu certificado de Incapacidade Temporária foi enviado para o Serviço de RH Bosch Portugal." +
"<br /><br />Por favor guarde o seu " + "<b> Número de Pedido " + referenceNumber + "</b> caso necessite de nos contactar." +

"<br /><br />Os melhores cumprimentos e votos de rápidas melhoras " +
"<br /><br />Serviço de Recursos Humanos Bosch Portugal" +



"<div><br /><br /><br />=======================================================================================================================================</div>" +

                "<br /><br /><br />Dear " + name + "," +
"<br /><br />Thank you for using our application! We hope that you had the most pleasant experience." +
"<br /><br />You Illness certificate will be sent to HR Services Portugal." +
"<br /><br />Please save the following " + "<b> reference number " + referenceNumber + "</b> if you need to contact us." +



"<br /><br />Best Regards and our wishes for a speedy recovery" +

"<br /><br />Human Resources Services Portugal" +


 "</div></body></html>";


            return HtmlContent;


        }
    }
}
