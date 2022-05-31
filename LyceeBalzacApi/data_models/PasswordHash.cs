namespace LyceeBalzacApi.data_models;

public record PasswordHash(string password, byte[] salt);