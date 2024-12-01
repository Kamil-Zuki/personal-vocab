namespace personal_vocab.DTOs.Requests;

public class CreateGroupDto
{
    public string Name { get; set; }
    public int NewWordAmount { get; set; }
    public int RepeatedWordAmount { get; set; }
}
