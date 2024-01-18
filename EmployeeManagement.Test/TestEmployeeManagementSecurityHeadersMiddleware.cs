
using EmployeeManagement.Middleware;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Test
{
    public class TestEmployeeManagementSecurityHeadersMiddleware
    {
        [Fact]
        public async Task InvokeAsync_Invoke_TheHeadersMustBeAddedToTheResponseHeader()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            RequestDelegate next = (HttpContext httpContext) => Task.CompletedTask;

            var middleware = new EmployeeManagementSecurityHeadersMiddleware(next);


            // Act
            await middleware.InvokeAsync(httpContext);

            var firstHeader = httpContext.Response.Headers["Content-Security-Policy"];
            var secondHeader = httpContext.Response.Headers["X-Content-Type-Options"];
            // Assert

            Assert.Equal("default-src 'self';frame-ancestors 'none';", firstHeader);
            Assert.Equal("nosniff", secondHeader);
        }
    }
}
