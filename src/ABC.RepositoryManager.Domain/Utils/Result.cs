using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ABC.RepositoryManager.Domain.Utils
{
    public class Result
    {
        public HttpStatusCode StatusCode { get; set; }
        public List<string> Erros { get; set; }

        public Result()
        {
            Erros = new List<string>();
        }

        public IActionResult ToActionResult()
        {
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    return Erros.Any() ? new ObjectResult(Erros)
                    {
                        StatusCode = (int)StatusCode
                    } : new OkResult();
                case HttpStatusCode.Created:
                    return new StatusCodeResult(201);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(Erros);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(Erros);
                case HttpStatusCode.Conflict:
                    return new ConflictObjectResult(Erros);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();
                case HttpStatusCode.PreconditionFailed:
                    return new StatusCodeResult(412);
                case HttpStatusCode.InternalServerError:
                default:
                    return new ObjectResult(Erros)
                    {
                        StatusCode = (int)StatusCode
                    };
            }
        }

        public static Result Sucess(List<string> erros = null)
        {
            return (erros == null || !erros.Any()) ? new Result
            {
                StatusCode = HttpStatusCode.OK
            } :
            new Result
            {
                StatusCode = HttpStatusCode.OK,
                Erros = erros
            };
        }

        public static Result Created()
        {
            return new Result
            {
                StatusCode = HttpStatusCode.Created
            };
        }

        public static Result NotFound()
        {
            return new Result
            {
                StatusCode = HttpStatusCode.NotFound
            };
        }

        public static Result NotFound(string erroMsg = "")
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.NotFound
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result BadRequest()
        {
            return new Result
            {
                StatusCode = HttpStatusCode.BadRequest
            };
        }

        public static Result BadRequest(string erroMsg = "")
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result BadRequest(params string[] erros)
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.BadRequest
            };
            resultado.Erros.AddRange(erros);
            return resultado;
        }

        public static Result Conflict()
        {
            return new Result
            {
                StatusCode = HttpStatusCode.Conflict
            };
        }

        public static Result Conflict(string erroMsg = "")
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.Conflict
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result PreconditionFailed()
        {
            return new Result
            {
                StatusCode = HttpStatusCode.PreconditionFailed
            };
        }

        public static Result PreconditionFailed(string erroMsg = "")
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.PreconditionFailed
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result Unauthorized()
        {
            return new Result
            {
                StatusCode = HttpStatusCode.Unauthorized
            };
        }

        public static Result Unauthorized(string erroMsg = "")
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.Unauthorized
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result Error(params string[] erros)
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
            resultado.Erros.AddRange(erros);
            return resultado;
        }

        public static Result Error(string erroMsg = "")
        {
            var resultado = new Result
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }
    }

    public class Result<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Resultado { get; set; }
        public List<string> Erros { get; set; }

        public Result()
        {
            Erros = new List<string>();
        }

        public IActionResult ToActionResult()
        {
            switch (StatusCode)
            {
                case HttpStatusCode.OK:
                    return Erros.Any() ? new OkObjectResult(new { Resultado = Resultado, Erros = Erros })
                    {
                        StatusCode = (int)StatusCode
                    } : new OkObjectResult(Resultado);
                case HttpStatusCode.Created:
                    return new CreatedResult("", Resultado);
                case HttpStatusCode.BadRequest:
                    return new BadRequestObjectResult(Erros);
                case HttpStatusCode.NotFound:
                    return new NotFoundObjectResult(Erros);
                case HttpStatusCode.Conflict:
                    return new ConflictObjectResult(Erros);
                case HttpStatusCode.Unauthorized:
                    return new UnauthorizedResult();
                case HttpStatusCode.PreconditionFailed:
                    return new StatusCodeResult(412);
                case HttpStatusCode.InternalServerError:
                default:
                    return new ObjectResult(Erros)
                    {
                        StatusCode = (int)StatusCode
                    };
            }
        }

        public static Result<T> Sucess(T? resultado = default, List<string> erros = null)
        {
            return (erros == null || !erros.Any()) ? new Result<T>
            {
                StatusCode = HttpStatusCode.OK,
                Resultado = resultado
            } :
            new Result<T>
            {
                StatusCode = HttpStatusCode.OK,
                Resultado = resultado,
                Erros = erros
            };
        }

        public static Result<T> Created(T resultado)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.Created,
                Resultado = resultado
            };
        }

        public static Result<T> NotFound(T resultado)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.NotFound,
                Resultado = resultado
            };
        }

        public static Result<T> NotFound(string erroMsg = "")
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.NotFound
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result<T> Conflict(T resultado)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.Conflict,
                Resultado = resultado
            };
        }

        public static Result<T> Conflict(string erroMsg = "")
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.Conflict
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result<T> PreconditionFailed(T resultado)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.PreconditionFailed,
                Resultado = resultado
            };
        }

        public static Result<T> PreconditionFailed(string erroMsg = "")
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.PreconditionFailed
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result<T> Unauthorized(T resultado)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Resultado = resultado
            };
        }

        public static Result<T> Unauthorized(string erroMsg = "")
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.Unauthorized
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result<T> BadRequest(T resultado)
        {
            return new Result<T>
            {
                StatusCode = HttpStatusCode.BadRequest,
                Resultado = resultado
            };
        }

        public static Result<T> BadRequest(string erroMsg = "")
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.BadRequest
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }

        public static Result<T> BadRequest(params string[] erros)
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.BadRequest
            };
            resultado.Erros.AddRange(erros);
            return resultado;
        }

        public static Result<T> Error(params string[] erros)
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.InternalServerError
            };
            resultado.Erros.AddRange(erros);
            return resultado;
        }

        public static Result<T> Error(string erroMsg = "")
        {
            var resultado = new Result<T>
            {
                StatusCode = HttpStatusCode.InternalServerError
            };

            if (!string.IsNullOrEmpty(erroMsg))
                resultado.Erros.Add(erroMsg);

            return resultado;
        }
    }
}
