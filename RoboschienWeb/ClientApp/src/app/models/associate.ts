// {
// 	"AssociateDetails": {
// 			"AssociateNumber" : "50697851",
// 			"AssociateFirstName": "Suresh",
// 			"AssociateLastName": "Kona",
// 			"AssociateEmail": "suresh.kona@in.bosch.com",
// 			"AssociateMobileNumber": "+49 89-636-48018"
// 		}
// 	"WorkDisabilityCertificateDetails" : {
// 			"FirstTimeSickness" : true,
// 			"FollowUpSickness" : false,
// 			"Accident" : false,
// 			"StartDate" : "2018-07-07",
// 			"EndDate" : "2018-07-20",
// 		}
// }
import{WorkDisabilityCertificate} from './WorkDisabilityCertificate';
export class AssociateDetails{
    
   AssociateNumber:string;
   AssociateFirstName:string;
   AssociateLastName:string;
   AssociateEmail:string;
  AssociateMobileNumber: string;
  DataPrivacyTimeStamp: string;
  SelectedCountryCode: string;
  IsOcrCheck: boolean;

   WorkDisabilityCertificateDetails:WorkDisabilityCertificate;
  
}
