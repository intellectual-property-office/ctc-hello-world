using IPO.HelloWorld.ComponentTests.Helpers;
using Reqnroll;

namespace IPO.HelloWorld.ComponentTests.Hooks
{
    [Binding]
    public class ApiHooks
    {
        private readonly ApiTestContext _context;

        public ApiHooks(ApiTestContext context)
        {
            _context = context;
        }

        [BeforeScenario("api")]
        public void CreateFactory()
        {
            _context.Factory = new HelloWorldApiFactory();
        }

        [AfterScenario("api")]
        public void DisposeFactory()
        {
            _context.Response?.Dispose();
            _context.Client?.Dispose();
            _context.Factory?.Dispose();
        }
    }
}
