using DocumentFormat.OpenXml.Bibliography;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CardValidationIntTest.Support
{
    public class SupportData
    {
        private readonly RestRequest request;
        private readonly RestClient client;
        private RestResponse response;
        public string error { get; set; }
        public string errorContent { get; set; }
        public string wrongFieldName { get; set; }

        Data source = new Data();
        public SupportData()
        {
            using (StreamReader r = new StreamReader(@"../../../Data/cardData.json"))
            {
                string json = r.ReadToEnd();
                source = JsonSerializer.Deserialize<Data>(json);
                client = new RestClient(source.url);
                request = new RestRequest();
            }

        }

        public bool SendCardData(string cardType)
        {
            string cardNumber = "";
            string cOwner = source.owner;
            string cDate = source.date;
            string cCvv = source.cvv;
            switch (cardType.ToLower())
            {
                case "mastercard":
                    cardNumber = source.Master;
                    break;
                case "visa":
                    cardNumber = source.Visa;
                    break;
                case "american express":
                    cardNumber = source.AmericanExpress;
                    break;
                default:
                    error = "Card code \"" + cardType + "\" does not correspond any existent card type. \n " +
                        "card types are Mastercard, Visa and American Express";
                    return false;
            }
            

            Post body = new() { owner = cOwner, number = cardNumber, date = cDate, cvv = cCvv };
            request.AddJsonBody(body);

            response = client.ExecutePost(request);

            if(response.StatusCode.ToString().ToUpper() == "OK")
            {
                return true;
            }
            error = response.StatusCode.ToString();
            return false;
        }

        public void SendCardData(string emptyField, string wrongField)
        {
            string cardNumber = source.Master; ;
            string cOwner = source.owner;
            string cDate = source.date;
            string cCvv = source.cvv;


            if (emptyField != null)
            {
                switch (emptyField.ToLower())
                {
                    case "owner":
                        cOwner = "";
                        break;
                    case "date":
                        cDate = "";
                        break;
                    case "cvv":
                        cCvv = "";
                        break;
                    case "number":
                        cardNumber = "";
                        break;
                    default:
                        break;
                }


            }

            if(wrongField != null)
            {
                switch (wrongField.ToLower())
                {
                    case "ownerwithfournames":
                        cOwner = "Filipe Ferreira de Jesus";
                        wrongFieldName = "owner";
                        break;
                    case "datesmallerthancurrent":
                        cDate = (DateTime.Now.Month-1).ToString() + "/" + DateTime.Now.Year.ToString();
                        wrongFieldName = "date";
                        break;
                    case "datewrongformat":
                        cDate = "2022/12";
                        wrongFieldName = "date";
                        break;
                    case "cvvwithtwodigit":
                        cCvv = "12";
                        wrongFieldName = "cvv";
                        break;
                    case "cvvwithfivedigit":
                        cCvv = "12345";
                        wrongFieldName = "cvv";
                        break;
                    case "numberwrong":
                        cardNumber = "00000000000000";
                        wrongFieldName = "number";
                        break;
                    default:
                        break;
                }
            }




            Post body = new() { owner = cOwner, number = cardNumber, date = cDate, cvv = cCvv };
            request.AddJsonBody(body);

            response = client.ExecutePost(request);

            errorContent = response.Content.ToString();
            error = response.StatusCode.ToString();
        }

        public string GetCardTypeReturn()
        {
            return response.Content.ToString();
        }

    }
}
