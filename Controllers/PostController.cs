using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using post_service.Services;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] Post post)
    {
        var createdPost = await _postService.CreatePostAsync(post);
        return Ok(createdPost);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
            return NotFound();
        return Ok(post);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
    {
        if (id != post.Id)
            return BadRequest("ID mismatch");
    
        var existingPost = await _postService.GetPostByIdAsync(id);
        if (existingPost == null)
            return NotFound();
    
        await _postService.UpdatePostAsync(post);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
            return NotFound();
    
        await _postService.DeletePostAsync(id);
        return NoContent();
    }

}