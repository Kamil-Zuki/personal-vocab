namespace personal_vocab.DTOs.Responses;

public record ResponseDto<T>(
    bool Succeeded,
    T? Data = null,
    List<string>? Errors = null
) where T : class;


