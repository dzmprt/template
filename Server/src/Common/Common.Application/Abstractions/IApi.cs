using Microsoft.AspNetCore.Builder;

namespace Common.Application.Abstractions;

public interface IApi
{
    void Register(WebApplication app, string baseApiUrl = null);
}