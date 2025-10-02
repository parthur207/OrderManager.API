using OrderManager.Application.RepositoryInterface.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManager.Application.UseCases
{
    public class OrderNumberGeneratorService : IOrderNumberGeneratorInterface
    { 
        public int GeneratorNumber()
        {
            Random random = new Random();
            int numero = random.Next(1000, 10000); // Gera um número entre 1000 e 9999
            return numero;
        }
    }
}
