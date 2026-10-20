namespace IPO.HelloWorld.ComponentTests.Helpers
{
    public class ApiTestContext
    {
        public HelloWorldApiFactory? Factory { get; set; }
        public HttpClient? Client { get; set; }
        public HttpResponseMessage? Response { get; set; }
    }
}
