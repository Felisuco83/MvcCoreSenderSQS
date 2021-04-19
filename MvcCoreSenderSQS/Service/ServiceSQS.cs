using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using MvcCoreSenderSQS.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MvcCoreSenderSQS.Service
{
    public class ServiceSQS
    {
        private IAmazonSQS clientSQS;
        private string queueUrl;

        public ServiceSQS (IAmazonSQS clientsqs, IConfiguration configuration)
        {
            this.queueUrl = configuration["AWSSQS:QueueUrl"];
            this.clientSQS = clientsqs;
        }

        public async Task<bool> SendMessageAsync(MensajeUsuario mensaje)
        {
            string data = JsonConvert.SerializeObject(mensaje);
            SendMessageRequest request = new SendMessageRequest(this.queueUrl, data);

            SendMessageResponse response = await this.clientSQS.SendMessageAsync(request);
            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
