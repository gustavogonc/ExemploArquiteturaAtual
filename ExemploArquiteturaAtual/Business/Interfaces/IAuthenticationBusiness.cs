using ExemploArquiteturaAtual.Models;

namespace ExemploArquiteturaAtual.Business.Interfaces;
    public interface IAuthenticationBusiness
    {
        Task<ResponseUserDTO> LoginAsync(LoginRequestDTO loginRequest);
    }

