﻿namespace personal_vocab.DTOs.Requests;

public class CreateTermDto
{
    public string Text { get; set; }
    public string Transcription { get; set; }
    public string Meaning { get; set; }
    public string Example { get; set; }
    public string Image { get; set; }

    public int DeckId { get; set; }
}

