﻿namespace personal_vocab.DTOs
{
    public class NoIdTermDTO
    {
        //public int Id { get; set; }
        public string Text { get; set; }
        public string Transcription { get; set; }
        public string Meaning { get; set; }
        public string Example { get; set; }
        public string Image { get; set; }

        public int DeckId { get; set; }
    }
}
