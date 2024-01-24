namespace DBClasses
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int StackId { get; set; }    
    }
    public class FlashcardDTO
    {
        public FlashcardDTO(Flashcard card)
        {
            this.Front = card.Front;
            this.Back = card.Back;  
        }

        public string Front { get; set; }
        public string Back { get; set; }    
    }
    public class Stack
    {
        public int Id {  get; set; }
        public string Topic {  get; set; }
    }

    public class StudySession 
    {
        public int id { get; set; }
        public int points { get; set; }
        public int maxPoints { get; set; }
        int stackId { get; set; }
    }
}