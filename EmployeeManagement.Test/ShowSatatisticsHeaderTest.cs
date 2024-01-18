
using EmployeeManagement.ActionFilters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.Eventing.Reader;

namespace EmployeeManagement.Test
{
    public class ShowSatatisticsHeaderTest
    {
        [Fact]
        public void OnActionExecuting_Excuting_isTheShowStatisticsHeaderApplied()
        { 
            // Arrange
            var showStatisticHeaderFilter =
                new CheckShowStatisticsHeader();

            var httpContext = new DefaultHttpContext();

            var actionContext = new ActionContext(httpContext, new(), new(), new());

            var actionExcutingContext = new ActionExecutingContext(actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object?>(),
                controller: null!);

            // Act 

            showStatisticHeaderFilter.OnActionExecuting(actionExcutingContext);

            //Assert

            Assert.IsType<BadRequestResult>(actionExcutingContext.Result);
        }
    }
}
