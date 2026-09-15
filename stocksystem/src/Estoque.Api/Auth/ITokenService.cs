namespace Estoque.Api.Auth;

// A interface descreve O QUE precisamos fazer sem acoplar quem usa o serviço
// aos detalhes de COMO um JWT é montado.
public interface ITokenService
{
    // Recebe os dados necessários para formar a identidade e devolve o JWT
    // serializado como string, pronto para ser enviado ao cliente.
    string GenerateToken(
        string username,
        string role,
        IEnumerable<string> permissions);
}