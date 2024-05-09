public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}

public class PostDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    // Add any other relevant fields that the client will provide
}