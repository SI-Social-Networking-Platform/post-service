using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using post_service.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase
{
    private readonly PostService _postService;

    public PostController(PostService postService)
    {
        _postService = postService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost([FromBody] PostDTO postDto)
    {

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var post = new Post
        {
            UserId = int.Parse(userIdClaim.Value),
            Title = postDto.Title,
            Content = postDto.Content
        };

        var createdPost = await _postService.CreatePostAsync(post);
        return Ok(createdPost);
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
    // {
    //     var posts = await _postService.GetAllPostsAsync();
    //     return Ok(posts);
    // }
    
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Post>>> GetAllUserPosts()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("User ID not found in token");
        }

        var userId = int.Parse(userIdClaim.Value);

        var userPosts = await _postService.GetPostsByUserIdAsync(userId);

        return Ok(userPosts);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Post>> GetPost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
            return NotFound();
        return Ok(post);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
    {
        if (id != post.Id)
            return BadRequest("ID mismatch");

        var existingPost = await _postService.GetPostByIdAsync(id);
        if (existingPost == null)
            return NotFound();

        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || int.Parse(userIdClaim.Value) != existingPost.UserId)
        {
            return Forbid("Unauthorized to update this post");
        }

        await _postService.UpdatePostAsync(post);
        return NoContent();
    }

    [Authorize] 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null)
            return NotFound();
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null || int.Parse(userIdClaim.Value) != post.UserId)
        {
            return Forbid("Unauthorized to delete this post");
        }

        await _postService.DeletePostAsync(id);
        return NoContent();
    }
}
