//using System;
//using SystemEx.Html;
//using System.Specialized.JsTestRunner;
//namespace Html
//{
//    public class WebSocketTests
//    {
//        static WebSocketTests() { TestCaseBuilder.TestCase("WebSocketTests", typeof(WebSocketTests).Prototype); }
//        private WebSocket _socket;

//        public void setUp()
//        {
//            if (WebSocket.CanWebSocket)
//                _socket = WebSocket.Create("http://www.google.com", "http");
//        }

//        public void TestCreate_passes_when_socket_not_null()
//        {
//            if (_socket == null)
//                return;
//            Asserts.AssertNotNull(_socket);
//        }

//        public void TestUrl_passes_when_url_is_valid()
//        {
//            if (_socket == null)
//                return;
//            Asserts.AssertEquals("http://www.google.com", _socket.Url);
//        }
//    }
//}