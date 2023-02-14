using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoboschienWeb.Helpers
{
    public class FranceEmailTemplate
    {

        public string getEmailTemplate(string language, string name, string referenceNumber)
        {
            string HtmlContent = "";

            Console.WriteLine("Email template : Language  : " + language);

            HtmlContent = "<!DOCTYPE html> <html><head>" +
                "<style>@font-face { font-family: BoschSansFont; src: url('https://rbeidashboarddiagdev.blob.core.windows.net/associate-data/bosch-fonts/BoschSans-Black.woff');}" +

"</style></head><body><div style='font-family:BoschSansFont;font-size:10pt;'>" +

               "Bonjour " + name + "," +
"<br /><br />Merci pour l’envoi de votre arrêt de travail à l’aide de l’application ! " +
"<br /><br />Le HR Service recevra votre arrêt de travail avec " + "<b>le numéro de référence " + referenceNumber + "</b>"+

"<br /><br />Veuillez garder ce numéro pour toute question et échange relatifs à votre arrêt avec votre HR Service. " +
"<br /><br />Nous allons informer votre responsable de votre incapacité de travailler et nous vous souhaitons, avant tout, un bon rétablissement." +
"<br /><br /><p>Si vous êtes hospitalisé pour 4 nuits ou plus, vous pouvez prétendre à une prestation d'assistance. Renseignez-vous <span style=" + "color:red;" + "><b>le plus rapidement possible avant d'engager des frais</b></span> auprès d'Aésio assistance (voir carte tiers payant).</p>" +

"<br /><br /><br />Votre" +
"<br /><br />HR Service" +
"<br /><br />Bosch SAM" +


"<div><br /><br /><br />=======================================================================================================================================</div>" +

                "<br /><br /><br />Hello " + name + "," +
"<br /><br />Thank you for sending your sick leave using the application! " +

"<br /><br />HR Service will receive your sick leave with the " + "<b>Reference number " + referenceNumber + "</b>" +



"<br /><br />Please keep this number in case you have any questions or need to discuss your absence with your HR Service." +

"<br /><br />We will inform your manager of your inability to work and wish you a good recovery" +
"<br /><br /> <p>If you are hospitalised for 4 nights or more, you may be entitled to assistance. Please contact Aésio assistance <span style="+"color:red;"+ "><b>as soon as possible before incurring any costs </b></span>(see third party payment card).</p>" +

"<br /><br /><br />Your" +
"<br /><br />HR Service" +
"<br /><br />Bosch SAM" +
 "</div></body></html>";


            return HtmlContent;


        }
    }
}
