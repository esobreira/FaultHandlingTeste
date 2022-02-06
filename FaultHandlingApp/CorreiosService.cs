using Microsoft.Practices.TransientFaultHandling;
using CorreiosAPI;
using System;
using System.Net;
using System.Threading.Tasks;
using System.ServiceModel;

namespace FaultHandling
{
    public static class CorreiosService
    {
        public static enderecoERP ConsultarCep(string cep)
        {
            Task<enderecoERP> retorno = null;
            AtendeClienteClient webClient = null;

            ITransientErrorDetectionStrategy transientExceptionDetection = new MyDetection();

            RetryStrategy retryStrategy = new FixedInterval(retryCount: 5, retryInterval: TimeSpan.FromSeconds(2));

            RetryPolicy retryPolicy = new RetryPolicy(transientExceptionDetection, retryStrategy);
            retryPolicy.Retrying += RetryPolicy_Retrying;

            try
            {
                webClient = new AtendeClienteClient();

                retorno = retryPolicy.ExecuteAsync(() => Task.Run(() => webClient.consultaCEP(cep)));
            }
            catch (System.Exception)
            {
                throw;
            }
            
            return retorno.Result;
        }

        private static void RetryPolicy_Retrying(object sender, RetryingEventArgs e)
        {
            //Gravar em NLog
            //throw new NotImplementedException();
            Console.WriteLine("Teste");
        }

        internal class MyDetection : ITransientErrorDetectionStrategy
        {
            public bool IsTransient(System.Exception exception) => exception is WebException || exception is EndpointNotFoundException;
        }

    }
}