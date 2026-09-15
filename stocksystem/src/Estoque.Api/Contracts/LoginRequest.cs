namespace Estoque.Api.Contracts;

// Request = aquilo que o cliente envia para a API.
// record é adequado para um objeto simples de transporte de dados.
public sealed record LoginRequest(
    string Username,
    string Password);