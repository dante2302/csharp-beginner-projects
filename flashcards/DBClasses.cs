namespace DBClasses
{
    public class Flashcard
    {
        private int Id;
        public string Front
        { get; set; }
        public string Back { get; set; }
    }
    public class FlashcardDTO
    {
        public string Front { get; set; }
        public string Back { get; set; }
    }

    public class Stack
    {
        private int Id {  get; set; }
        public string Topic {  get; set; }
    }
    public class StackDTO
    {
        public string Topic { get; set; }
    }
    public class StudySession 
    {
        int points { get; set; }
        int maxPoints { get; set; }
        int StackId { get; set; }
    }
}