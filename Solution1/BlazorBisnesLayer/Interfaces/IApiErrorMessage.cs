﻿using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorBusinessLogic.Interfaces
{
    public interface IApiErrorMessage
    {
        Task<bool> FaildToConnect(HttpResponseMessage response, HttpStatusCode[] IgnoreCode = null);
    }
}