namespace personal_vocab.DTOs.Responses;

public record GroupDto
(
    Guid Id,
    string Name,
    int NewWordAmount,
    int RepeatedWordAmount
);

