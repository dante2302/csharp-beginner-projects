namespace DBClasses
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string Front { get; set; }
        public string Back { get; set; }
        public int StackId { get; set; }    
    }
    public class Stack
    {
        public int Id {  get; set; }
        public string Topic {  get; set; }
    }

    public class StudySession 
    {
        int points { get; set; }
        int maxPoints { get; set; }
        int StackId { get; set; }
    }
}