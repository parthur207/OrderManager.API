using OrderManager.Application.Interfaces.IUseCase;
using OrderManager.Application.RepositoryInterface.UseCase;
using OrderManager.Domain.Enuns;
using OrderManager.Domain.Models.ReponsePattern;

public class OrderNumberGeneratorService : IOrderNumberGeneratorInterface
{
    private readonly ICheckOrderNumberExistsRepository _checkOrderNumberExistsRepository;
    private static readonly Random random = new Random(); 

    public OrderNumberGeneratorService(ICheckOrderNumberExistsRepository checkOrderNumberExistsRepository)
    {
        _checkOrderNumberExistsRepository = checkOrderNumberExistsRepository;
    }

    public async Task<ResponseModel<int>> GeneratorNumber()
    {
        ResponseModel<int> Response = new ResponseModel<int>();
        int numero = random.Next(1000, 10000);//Gera um número entre 1000 e 9999

        var ResponseRepository = await _checkOrderNumberExistsRepository.CheckOrderNumberRepository(numero);

        if (ResponseRepository.Status == ResponseStatusEnum.CriticalError)
        {
            Response.Status = ResponseRepository.Status;
            Response.Message = ResponseRepository.Message;
            return Response;
        }

        if (ResponseRepository.Status == ResponseStatusEnum.Error)
        {
            // Retorna a chamada recursiva
            return await GeneratorNumber();
        }

        Response.Status = ResponseStatusEnum.Success;
        Response.Content = numero;

        return Response;
    }
}
