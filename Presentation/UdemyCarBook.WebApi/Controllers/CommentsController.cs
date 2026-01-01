using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UdemyCarBook.Application.Features.Mediator.Commands.CommentCommands;
using UdemyCarBook.Application.Features.RepositoryPattern;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly IGenericRepository<Comment> _commentsRepository;
        private readonly IMediator _mediator;

        public CommentsController(IGenericRepository<Comment> commentsRepository, IMediator mediator)
        {
            _commentsRepository = commentsRepository;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _commentsRepository.GetAll();
            return Ok(values);
        }

        [Authorize(Roles = "Admin,Manager,Member")]
        [HttpPost]
        public IActionResult CreateComment(Comment comment)
        {
            _commentsRepository.Create(comment);
            return Ok("Yorum başarıyla eklendi.");
        }

        [Authorize(Roles = "Admin,Manager,Member")]
        [HttpDelete]
        public IActionResult RemoveComment(int id)
        {
            var values = _commentsRepository.GetById(id);
            _commentsRepository.Remove(values);
            return Ok("Yorum başarıyla silindi.");
        }

        [Authorize(Roles = "Admin,Manager,Member")]
        [HttpPut]
        public IActionResult UpdateComment(Comment comment)
        {
            _commentsRepository.Update(comment);
            return Ok("Yorum başarıyla güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var values = _commentsRepository.GetById(id);
            return Ok(values);
        }

        [HttpGet("CommentListByBlog")]
        public IActionResult CommentListByBlog(int id)
        {
            var values = _commentsRepository.GetCommentsByBlogId(id);
            return Ok(values);
        }

        [HttpGet("CommentCountByBlog")]
        public IActionResult CommentCountByBlog(int id)
        {
            var value = _commentsRepository.GetCountCommentByBlog(id);
            return Ok(value);
        }

        [Authorize(Roles = "Admin,Manager,Member")]
        [HttpPost("CreateCommentWithMediator")]
        public async Task<IActionResult> CreateCommentWithMediator(CreateCommentCommand command)
        {
            await _mediator.Send(command);
            return Ok("Yorum başarıyla eklendi.");
        }
    }
}
