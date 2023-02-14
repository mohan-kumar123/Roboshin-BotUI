// {
//     "IsSuccess": true,
//     "Message": "Work disability certificate was successfully submitted to HRS-DE ",
//     "ErrorMessage": "",
//     "ResponseData": [
// 		{
// 			"ReferenceNumber" : "SK50697851HRSDE180707"
// 		}	
// 	]
// }
export class ResponseDetails{
    IsSuccess:boolean;
    Message:string;
    ErrorMessage:string;
    ResponseDataDetails:any[];
    Isocr:boolean=false;
}