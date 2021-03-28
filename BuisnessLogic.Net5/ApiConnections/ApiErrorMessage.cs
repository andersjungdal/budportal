using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorBuisnessLogic.Net5.Interfaces;
using BlazorBuisnessLogic.Net5.Models.UI;
using Microsoft.AspNetCore.Components;

namespace BlazorBuisnessLogic.Net5.ApiConnections
{
    public class ApiErrorMessage : IApiErrorMessage
    {
        public StateHolder Holder { get; set; }
        public NavigationManager Nav { get; set; }

        public ApiErrorMessage(StateHolder holder, NavigationManager nav)
        {
            Holder = holder;
            Nav = nav;
        }
        public async Task<bool> FaildToConnect(HttpResponseMessage response, HttpStatusCode[] ignoreCode = null)
        {
            ignoreCode = ignoreCode ?? new HttpStatusCode[0];
            if (response.IsSuccessStatusCode)
            {
                return Holder.Online = true;
            }
            if (ignoreCode.Contains(response.StatusCode))
            {
                return false;
            }
            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    if (Holder.User == null)
                    {
                        Holder.LogInState = true;
                    }
                    Nav.NavigateTo("/");
                    Holder.PubUp = new PopUp
                    {
                        Message = "You do not have authority to go to this site.\nPlease contact you supervisor for permision"
                    };
                    break;
                case HttpStatusCode.InternalServerError:
                    Holder.Online = false;
                    Holder.PubUp = new PopUp { Message = $"You do not have any connection\nPlease try again\nError:{response.StatusCode}" };
                    break;
                case HttpStatusCode.BadRequest:
                    Holder.PubUp = new PopUp { Message = $"You do not have all the necessary Data\nPlease check the Data and try again\nError:{response.StatusCode}" };
                    break;
                case HttpStatusCode.ServiceUnavailable:
                    Holder.PubUp = new PopUp { Message = $"The server is currently down, if you get this message again, please contact Energinet\nError:{response.StatusCode}" };
                    break;
            }
            return false;
        }
    }
}