﻿namespace Application.Authors.Queries.GetAuthorById
{
    public record GetAuthorByIdQuery(int Id): IRequest<AuthorDto>;
}
