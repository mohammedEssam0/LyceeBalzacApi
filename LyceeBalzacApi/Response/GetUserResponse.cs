using LyceeBalzacApi.data_models;

namespace LyceeBalzacApi.Response;

public record GetUserResponse(string firstName, string lastName, string email, Role role, string? phoneNumber,
    string? address);