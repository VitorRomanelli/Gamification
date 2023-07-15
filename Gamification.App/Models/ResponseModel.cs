using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification.App.Models
{
    public class ResponseHelper : ControllerBase
    {
        public IActionResult CreateResponse(ResponseModel response) => response.Status switch
        {
            200 => Ok(String.IsNullOrEmpty(response.Message) ? response.Content : response.Message),
            500 => StatusCode(500, String.IsNullOrEmpty(response.Message) ? response.Content : response.Message),
            422 => UnprocessableEntity(String.IsNullOrEmpty(response.Message) ? response.Content : response.Message),
            409 => Conflict(String.IsNullOrEmpty(response.Message) ? response.Content : response.Message),
            401 => Unauthorized(String.IsNullOrEmpty(response.Message) ? response.Content : response.Message),
            403 => Forbid(response.Message),
            404 => NotFound(String.IsNullOrEmpty(response.Message) ? response.Content : response.Message),
            _ => StatusCode(400, response.Message),
        };
    }

    public class ResponseModel
    {
        public int Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public object? Content { get; set; }

        public ResponseModel(int status, string message)
        {
            Status = status;
            Message = message;
        }

        public ResponseModel(int status, object content)
        {
            Status = status;
            Content = content;
        }

        public ResponseModel(int status, string message, object content)
        {
            Status = status;
            Message = message;
            Content = content;
        }
        
        public static ResponseModel BuildResponse(int statusCode, string message)
        {
            return new ResponseModel(statusCode, message);
        }

        public static ResponseModel BuildResponse(int statusCode, object obj)
        {
            return new ResponseModel(statusCode, obj);
        }

        public static ResponseModel BuildOkResponse(string message)
        {
            return new ResponseModel(200, message);
        }
        public static ResponseModel BuildOkResponse(object content)
        {
            return new ResponseModel(200, content);
        }

        public static ResponseModel BuildOkResponse(string message, object content)
        {
            return new ResponseModel(200, message, content);
        }

        public static ResponseModel BuildUnauthorizedResponse(string message)
        {
            return new ResponseModel(401, message);
        }
        public static ResponseModel BuildNotFoundResponse(string message)
        {
            return new ResponseModel(404, message);
        }

        public static ResponseModel BuildNotFoundResponse(string message, object content)
        {
            return new ResponseModel(404, message, content);
        }

        public static ResponseModel BuildConflictResponse(string message)
        {
            return new ResponseModel(409, message);
        }

        public static ResponseModel BuildConflictResponse(string message, object content)
        {
            return new ResponseModel(409, message, content);
        }

        public static ResponseModel BuildErrorResponse(string message)
        {
            return new ResponseModel(500, message);
        }
        public static ResponseModel BuildErrorResponse(object content)
        {
            return new ResponseModel(500, content);
        }
        public static ResponseModel BuildErrorResponse(string message, object content)
        {
            return new ResponseModel(500, message, content);
        }
    }
}
