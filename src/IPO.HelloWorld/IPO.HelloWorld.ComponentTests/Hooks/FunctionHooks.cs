using IPO.HelloWorld.ComponentTests.Helpers;
using Reqnroll;

namespace IPO.HelloWorld.ComponentTests.Hooks
{
    [Binding]
    public class FunctionHooks
    {
        private readonly FunctionTestContext _context;

        public FunctionHooks(FunctionTestContext context)
        {
            _context = context;
        }

        [AfterScenario("function")]
        public void DisposeHost()
        {
            _context.Host?.Dispose();
        }
    }
}
