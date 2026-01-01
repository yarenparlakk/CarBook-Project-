using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace UdemyCarBook.Application.Features.Mediator.Commands.CommentCommands
{
    public class CreateCommentCommand : IRequest
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public int BlogID { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

    }
}
